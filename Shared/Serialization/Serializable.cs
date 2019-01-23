using System;
using System.Collections.Generic;
using System.IO;

namespace Bombardel.CurveNet.Shared.Serialization
{
	public abstract class Serializer
	{
		public static T Deserialize<T>(IDataReader reader) where T : ISerializable, new()
		{
			T obj = new T();
			obj.Deserialize(reader);
			return obj;
		}
	}
}
