﻿namespace Productivity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;
    using Productivity.Models;
    using Productivity.Analysis;

    public partial class ProductivityView : Form
    {
        private EventsConnection db;

        public ProductivityView()
        {
            this.db = new EventsConnection();
            InitializeComponent();

            var startTime = DateTime.Today.ToUniversalTime();
            this.productivityBar.StartTime = startTime;
        }

        private void ProductivityView_Shown(object sender, EventArgs e)
        {
            RefreshAnalysis();
        }

        private void ProductivityView_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Hide();
            }
        }

        private void manageRulesButton_Click(object sender, EventArgs e)
        {
            var ruleManager = new RuleManager(this.db);
            ruleManager.ShowDialog(this);
            RefreshAnalysis();
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            RefreshAnalysis();
        }

        private void RefreshAnalysis()
        {
            if (this.Enabled)
            {
                this.Enabled = false;
                this.analysisWorker.RunWorkerAsync();
            }
        }

        private void analysisWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var startTime = this.productivityBar.StartTime;
            var endTime = startTime + this.productivityBar.TimeSpan;

            var timelineAnalyzer = new TimelineAnalyzer(this.db);
            e.Result = timelineAnalyzer.Analyze(startTime, endTime);
        }

        private void analysisWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                var result = (List<TimelineSegment>)e.Result;
                this.productivityBar.Segments = result;

                var score = CalculateOverall(result);
                this.scoreLabel.Text = score.Item1.ToString("P");
                this.timeScoredLabel.Text = FormatTime(score.Item2);
            }

            this.Enabled = true;
        }

        private string FormatTime(TimeSpan timeSpan)
        {
            var sb = new StringBuilder();
            bool shown = false;

            if (timeSpan.Days > 0)
            {
                sb.Append(timeSpan.Days + " days");
                shown = true;
            }

            if (shown || timeSpan.Hours > 0)
            {
                sb.Append((shown ? ", " : "") + timeSpan.Hours + " hrs");
                shown = true;
            }

            sb.Append((shown ? ", " : "") + timeSpan.Minutes + " min");

            return sb.ToString();
        }

        private Tuple<double, TimeSpan> CalculateOverall(List<TimelineSegment> segments)
        {
            var productiveMs = 0.0;
            var unproductiveMs = 0.0;

            foreach (var segment in segments)
            {
                if (segment.Productivity.HasValue)
                {
                    var segmentMs = (segment.EndTime - segment.StartTime).TotalMilliseconds;
                    var productivePortion = segmentMs * (segment.Productivity.Value / 100.0);
                    var unproductivePortion = segmentMs - productivePortion;

                    productiveMs += productivePortion;
                    unproductiveMs += unproductivePortion;
                }
            }

            var totalMs = productiveMs + unproductiveMs;

            var productivityScore = totalMs == 0.0 ? 0.0 : productiveMs / totalMs;
            var timeScored = TimeSpan.FromMilliseconds(totalMs);

            return Tuple.Create(productivityScore, timeScored);
        }
    }
}
