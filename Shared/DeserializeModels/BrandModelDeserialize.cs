﻿using Shared.DeserializeModels;
using System.ComponentModel.DataAnnotations;

namespace Shared.DeserializeModels
{
	public class BrandModelDeserialize : IDeserializeModel
    {
		public int Id { get; set; }
        public string Name { get; set; }
    }
}
