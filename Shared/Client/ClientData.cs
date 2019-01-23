using Bombardel.CurveNet.Shared.Serialization;
using Bombardel.CurveNet.Shared.ServerMessages;
using System.Collections.Generic;
using System.Linq;

namespace Bombardel.CurveNet.Shared.Client
{

	public class ClientData : ISerializable
	{
		public Id id;
		
		public void Deserialize(IDataReader reader)
		{
			id = Serializer.Deserialize<Id>(reader);
		}

		public void Serialize(IDataWriter writer)
		{
			id.Serialize(writer);
		}

		public override string ToString()
		{
			return id.ToString();
		}
	}
}
