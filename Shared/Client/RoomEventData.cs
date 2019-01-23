using Bombardel.CurveNet.Shared.Serialization;
using Bombardel.CurveNet.Shared.ServerMessages;
using System.Collections.Generic;
using System.Linq;

namespace Bombardel.CurveNet.Shared.Client
{

	public class RoomEventData : ISerializable
	{
		public string roomName;
		public ClientData client;

		public void Deserialize(IDataReader reader)
		{
			roomName = reader.ReadString();
			client = Serializer.Deserialize<ClientData>(reader);
		}

		public void Serialize(IDataWriter writer)
		{
			writer.Write(roomName);
			client.Serialize(writer);
		}
	}
}
