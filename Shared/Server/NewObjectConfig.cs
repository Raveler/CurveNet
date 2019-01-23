using Bombardel.CurveNet.Shared.Curves;
using Bombardel.CurveNet.Shared.Serialization;
using Bombardel.CurveNet.Shared.ServerMessages;
using System.Collections.Generic;
using System.Linq;

namespace Bombardel.CurveNet.Shared.Server
{

	public class NewObjectConfig : ISerializable
	{
		public List<CurveConfig> curves = new List<CurveConfig>();

		public List<Id> rooms = new List<Id>();


		public void Serialize(IDataWriter writer)
		{
			writer.Write(curves);
			writer.Write(rooms);
		}

		public void Deserialize(IDataReader reader)
		{
			int n = reader.ReadInt();
			NewObjectConfig config = new NewObjectConfig();
			for (int i = 0; i < n; i++)
			{
				curves.Add(CurveConfig.DeserializeCurveConfig(reader));
			}
			rooms = reader.ReadList<Id>();
		}
	}
}
