﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksAllocation.Utils.DataStructure;
using TasksAllocation.Utils.Validation;
using TasksAllocation.Utils.Constants;

namespace TasksAllocation.Components
{
    /// <summary>
    /// The class inherits the Commnucation superclass
    /// without redefining any behaviours.
    /// </summary>
    public class RemoteCommunication : Communication
    { 
        public RemoteCommunication()
        {
        }

        public RemoteCommunication(Map mapData) : base(mapData)
        {
            OpeningClosingSection = new PairSection(
                CffKeywords.OPENING_REMOTE_COMMUNICATION,
                CffKeywords.CLOSING_REMOTE_COMMUNICATION);
        }
    }
}
