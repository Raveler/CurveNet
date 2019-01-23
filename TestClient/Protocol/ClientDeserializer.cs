using Bombardel.CurveNet.Server.Sessions;
using Bombardel.CurveNet.Shared.Client;
using Bombardel.CurveNet.Shared.Serialization;
using Bombardel.CurveNet.Shared.ServerMessages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Bombardel.CurveNet.Shared.Client
{

	public class ClientDeserializer
	{
		private IClient _client;

		public ClientDeserializer(IClient client)
		{
			_client = client;
		}

		public void Deserialize(byte[] bytes)
		{
			BinaryDataReader reader = new BinaryDataReader(bytes);
			byte type = reader.ReadByte();
			switch (type)
			{
				case 0:
					ReadNewClient(reader);
					break;

				case 1:
					ReadRemoveClient(reader);
					break;

				case 2:
					ReadRoomData(reader);
					break;

				case 3:
					ReadProtocolError(reader);
					break;

				default:
					throw new MessageTypeNotSupportedException();
			}
		}

		private void ReadNewClient(BinaryDataReader reader)
		{
			_client.SendNewClient(new RoomEventData()
			{
				roomName = reader.ReadString(),
				client = new ClientData()
				{
					id = reader.ReadString(),
				}
			});
		}

		private void ReadRemoveClient(BinaryDataReader reader)
		{
			_client.SendRemoveClient(new RoomEventData()
			{
				roomName = reader.ReadString(),
				client = new ClientData()
				{
					id = reader.ReadString(),
				}
			});
		}

		private void ReadRoomData(BinaryDataReader reader)
		{
			string json = reader.ReadString();
			_client.SendRoomData(JsonConvert.DeserializeObject<RoomData>(json));
		}

		private void ReadProtocolError(BinaryDataReader reader)
		{
			_client.SendProtocolError((ProtocolError)reader.ReadInt());
		}
	}
}
