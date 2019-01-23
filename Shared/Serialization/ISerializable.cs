using System;
using System.Collections.Generic;
using System.IO;

namespace Bombardel.CurveNet.Shared.Serialization
{
	public delegate T CreateInstanceDelegate<T>() where T : ISerializable;


	public interface ISerializable
	{

		void Serialize(IDataWriter writer);

		void Deserialize(IDataReader reader);

	}
}
