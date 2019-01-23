using Bombardel.CurveNet.Server;
using Bombardel.CurveNet.Server.Sessions;
using Bombardel.CurveNet.Shared.Client;
using Bombardel.CurveNet.Shared.Serialization;
using Bombardel.CurveNet.Shared.ServerMessages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Bombardel.CurveNet.Shared.Server
{

	public class ClientSerializer : IClient
	{
		BinaryDataWriter _writer;
		private IConnection _connection;

		public ClientSerializer(IConnection connection)
		{
			_connection = connection;
			_writer = new BinaryDataWriter();
		}

		public void SendNewClient(RoomEventData newClient)
		{
			_writer.Reset();
			_writer.Write((byte)0);
			_writer.Write(newClient.roomName);
			_writer.Write(newClient.client.id);
			_connection.Send(_writer.ToArray());
		}

		public void SendRemoveClient(RoomEventData deadClient)
		{
			_writer.Reset();
			_writer.Write((byte)1);
			_writer.Write(deadClient.roomName);
			_writer.Write(deadClient.client.id);
			_connection.Send(_writer.ToArray());
		}

		public void SendRoomData(RoomData roomData)
		{
			_writer.Reset();
			_writer.Write((byte)2);
			string json = JsonConvert.SerializeObject(roomData);
			_writer.Write(json);
			_connection.Send(_writer.ToArray());
		}

		public void SendProtocolError(ProtocolError error)
		{
			_writer.Reset();
			_writer.Write((byte)3);
			_writer.Write((int)error);
			_connection.Send(_writer.ToArray());
		}
	}
}
