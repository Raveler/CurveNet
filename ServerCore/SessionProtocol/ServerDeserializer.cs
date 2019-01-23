using Bombardel.CurveNet.Server.Sessions;
using Bombardel.CurveNet.Shared.Client;
using Bombardel.CurveNet.Shared.Serialization;
using Bombardel.CurveNet.Shared.ServerMessages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Bombardel.CurveNet.Shared.Server
{

	public class ServerDeserializer
	{
		public class MessageTypeNotSupportedException : Exception {};

		private IServer _server;

		public ServerDeserializer(IServer server)
		{
			_server = server;
		}

		public void Deserialize(byte[] bytes)
		{
			BinaryDataReader reader = new BinaryDataReader(bytes);
			byte type = reader.ReadByte();
			switch (type)
			{
				case 0:
					ReadJoinRoom(reader);
					break;

				case 1:
					ReadLeaveRoom(reader);
					break;

				case 2:
					ReadListRooms(reader);
					break;

				case 3:
					ReadCreateObject(reader);
					break;

				default:
					throw new MessageTypeNotSupportedException();
			}
		}

		private void ReadJoinRoom(BinaryDataReader reader)
		{
			_server.JoinRoom(reader.ReadString());
		}

		private void ReadLeaveRoom(BinaryDataReader reader)
		{
			_server.LeaveRoom(reader.ReadString());
		}

		private void ReadListRooms(BinaryDataReader reader)
		{
			_server.ListRooms();
		}

		private void ReadCreateObject(BinaryDataReader reader)
		{
			NewObjectConfig objectConfig = Serializer.Deserialize<NewObjectConfig>(reader);
			_server.CreateObject(objectConfig);
		}
	}
}
