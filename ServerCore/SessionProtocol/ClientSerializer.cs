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

		public void OnNewClient(RoomEventData newClient)
		{
			_writer.Reset();
			_writer.Write((byte)0);
			newClient.Serialize(_writer);
			_connection.Send(_writer.ToArray());
		}

		public void OnClientRemoved(RoomEventData deadClient)
		{
			_writer.Reset();
			_writer.Write((byte)1);
			deadClient.Serialize(_writer);
			_connection.Send(_writer.ToArray());
		}

		public void OnRoomData(RoomData roomData)
		{
			_writer.Reset();
			_writer.Write((byte)2);
			roomData.Serialize(_writer);
			_connection.Send(_writer.ToArray());
		}

		public void OnProtocolError(ProtocolError error)
		{
			_writer.Reset();
			_writer.Write((byte)3);
			_writer.Write((int)error);
			_connection.Send(_writer.ToArray());
		}

		public void OnObjectCreated(ObjectData data)
		{
			_writer.Reset();
			_writer.Write((byte)4);
			data.Serialize(_writer);
			_connection.Send(_writer.ToArray());
		}
	}
}
