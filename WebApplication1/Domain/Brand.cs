﻿namespace Server.Domain
{
	public class Brand
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public virtual ICollection<Car> Car { get; set; }
	}
}
