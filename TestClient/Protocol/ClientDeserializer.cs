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
					_client.OnNewClient(Serializer.Deserialize<RoomEventData>(reader));
					break;

				case 1:
					_client.OnClientRemoved(Serializer.Deserialize<RoomEventData>(reader));
					break;

				case 2:
					_client.OnRoomData(Serializer.Deserialize<RoomData>(reader));
					break;

				case 3:
					_client.OnProtocolError((ProtocolError)reader.ReadInt());
					break;

				case 4:
					_client.OnObjectCreated(Serializer.Deserialize<ObjectData>(reader));
					break;

				default:
					throw new MessageTypeNotSupportedException();
			}
		}
	}
}
