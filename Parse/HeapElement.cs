﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Parse
{
    public class HeapElement
    {
        public string Name { get; set; }
        public AllowedType Type { get; set; }
        public object Value { get; set; }
    }
}
