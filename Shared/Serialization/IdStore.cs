using System;
using System.Collections.Generic;
using System.IO;

namespace Bombardel.CurveNet.Shared.Serialization
{

	public class IdStore
	{
		private int _nextId = 1;

		public Id GenerateId()
		{
			return new Id(_nextId++);
		}
	}
}
