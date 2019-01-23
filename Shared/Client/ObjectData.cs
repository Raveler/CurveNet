using Bombardel.CurveNet.Shared.Curves;
using Bombardel.CurveNet.Shared.Serialization;
using Bombardel.CurveNet.Shared.Server;
using Bombardel.CurveNet.Shared.ServerMessages;
using System.Collections.Generic;
using System.Linq;

namespace Bombardel.CurveNet.Shared.Client
{

	public class ObjectData : ISerializable
	{
		public Id id;
		public Id owner;

		public List<CurveConfig> curves = new List<CurveConfig>();


		public void Deserialize(IDataReader reader)
		{
			id = Serializer.Deserialize<Id>(reader);
			owner = Serializer.Deserialize<Id>(reader);
			int n = reader.ReadInt();
			for (int i = 0; i < n; i++)
			{
				curves.Add(CurveConfig.DeserializeCurveConfig(reader));
			}
		}

		public void Serialize(IDataWriter writer)
		{
			writer.Write(id);
			writer.Write(owner);
			writer.Write(curves);
		}
	}
}
