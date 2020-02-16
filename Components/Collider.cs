﻿using System;
using System.Drawing;

namespace CMDR.Components
{
    public struct Collider : IComponent
    {
        #region IComponent
        public int Handle { get; set; }
        public Type ID { get; set; }
        public ParentList Parents { get; set; }
        #endregion

        public bool[,] Data;
    }
}
