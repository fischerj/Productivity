﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EventsLibrary
{
    public sealed class RemoveEventAction : EventAction
    {
        public RemoveEventAction(Event removeEvent) : base(removeEvent)
        {
        }
    }
}
