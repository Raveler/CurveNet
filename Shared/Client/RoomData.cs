using Bombardel.CurveNet.Shared.Serialization;
using Bombardel.CurveNet.Shared.ServerMessages;
using System.Collections.Generic;
using System.Linq;

namespace Bombardel.CurveNet.Shared.Client
{

	public class RoomData : ISerializable
	{
		public string roomName;

		public List<ClientData> clients = new List<ClientData>();


		public void Deserialize(IDataReader reader)
		{
			roomName = reader.ReadString();
			int nClients = reader.ReadInt();
			for (int i = 0; i < nClients; ++i)
			{
				clients.Add(Serializer.Deserialize<ClientData>(reader));
			}
		}

		public void Serialize(IDataWriter writer)
		{
			writer.Write(roomName);
			writer.Write(clients.Count);
			for (int i = 0; i < clients.Count; ++i)
			{
				clients[i].Serialize(writer);
			}
		}
	}
}
