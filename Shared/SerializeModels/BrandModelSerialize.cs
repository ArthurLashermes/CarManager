﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Shared.SerializeModels
{
    public class BrandModelSerialize : ISerializeModelSerialize
    {
        public string Name { get; set; }
    }
}