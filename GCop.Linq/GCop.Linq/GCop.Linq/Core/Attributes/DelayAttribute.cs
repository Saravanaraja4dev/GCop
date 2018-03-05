﻿namespace GCop.Linq.Core.Attributes
{
    using System;

    public class DelayAttribute:Attribute
    {
        public DelayAttribute(int minute)
        {
            Minute = minute;
        }

        public int Minute { get; set; }
    }
}
