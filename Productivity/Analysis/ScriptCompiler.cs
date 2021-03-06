﻿using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.CSharp;

namespace Productivity.Analysis
{
    public sealed class ScriptCompiler
    {
        private static readonly CSharpCodeProvider compiler;
        private static readonly CompilerParameters options;

        static ScriptCompiler()
        {
            compiler = new CSharpCodeProvider();
            options = new CompilerParameters
            {
                GenerateExecutable = false,
                GenerateInMemory = true,
            };
            options.ReferencedAssemblies.Add("System.dll");
            options.ReferencedAssemblies.Add("System.Core.dll");
            options.ReferencedAssemblies.Add("EventsLibrary.dll");
            options.ReferencedAssemblies.Add("Microsoft.CSharp.dll");
        }

        private static CodeCompileUnit FormatSource(string source, IList<Tuple<Type, string>> parameters, Type returnType)
        {
            var compileUnit = new CodeCompileUnit();
            var surrogateNamespace = new CodeNamespace("code");
            compileUnit.Namespaces.Add(surrogateNamespace);
            surrogateNamespace.Imports.Add(new CodeNamespaceImport("System"));
            surrogateNamespace.Imports.Add(new CodeNamespaceImport("System.Collections.Generic"));
            surrogateNamespace.Imports.Add(new CodeNamespaceImport("System.Linq"));
            surrogateNamespace.Imports.Add(new CodeNamespaceImport("System.Text"));
            surrogateNamespace.Imports.Add(new CodeNamespaceImport("System.Text.RegularExpressions"));
            var surrogateType = new CodeTypeDeclaration("script");
            surrogateType.Attributes = MemberAttributes.Public | MemberAttributes.Static | MemberAttributes.Final;
            surrogateNamespace.Types.Add(surrogateType);
            var targetMethod = new CodeMemberMethod { Name = "main" };
            surrogateType.Members.Add(targetMethod);

            targetMethod.ReturnType = new CodeTypeReference(returnType);
            targetMethod.Attributes = MemberAttributes.Public | MemberAttributes.Static;
            foreach (var param in parameters)
            {
                targetMethod.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(param.Item1), param.Item2));
            }
            var statements = new CodeSnippetStatement(source) { LinePragma = new CodeLinePragma("script", 1) };
            targetMethod.Statements.Add(statements);
            targetMethod.Statements.Add(new CodeMethodReturnStatement(new CodePrimitiveExpression(null)));

            return compileUnit;
        }

        public MethodInfo CompileToMetod(string source, IList<Tuple<Type, string>> parameters, Type returnType)
        {
            var compilationUnit = FormatSource(source, parameters, returnType);

            var results = compiler.CompileAssemblyFromDom(options, compilationUnit);
            if (results.Errors.HasErrors)
            {
                throw new ScriptCompileFailedException(results.Errors.Cast<CompilerError>().ToArray(), results.Output.Cast<string>().ToArray());
            }

            var assembly = results.CompiledAssembly;
            var type = assembly.GetType("code.script");
            var method = type.GetMethod("main");

            return method;
        }
    }
}
