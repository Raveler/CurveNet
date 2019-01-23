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

	public class ServerSerializer : IServer
	{
		BinaryDataWriter _writer;
		private IServerConnection _connection;

		public ServerSerializer(IServerConnection connection)
		{
			_connection = connection;
			_writer = new BinaryDataWriter();
		}

		private void Write(Action<IDataWriter> serializeCallback)
		{

		}

		public void JoinRoom(string roomName)
		{
			_writer.Reset();
			_writer.Write((byte)0);
			_writer.Write(roomName);
			_connection.Send(_writer.ToArray());
		}

		public void LeaveRoom(string roomName)
		{
			_writer.Reset();
			_writer.Write((byte)1);
			_writer.Write(roomName);
			_connection.Send(_writer.ToArray());
		}

		public void ListRooms()
		{
			_writer.Reset();
			_writer.Write((byte)2);
			_connection.Send(_writer.ToArray());
		}

		public void CreateObject(NewObjectConfig objectConfig)
		{
			_writer.Reset();
			_writer.Write((byte)3);
			objectConfig.Serialize(_writer);
			_connection.Send(_writer.ToArray());
		}

		public void AddCurve(string objectId)
		{
			throw new System.NotImplementedException();
		}

		public void AddObjectToRoom(string objectId, string roomName)
		{
			throw new System.NotImplementedException();
		}


		public void RemoveObject(string objectId)
		{
			throw new System.NotImplementedException();
		}

		public void UpdateCurve(string objectId)
		{
			throw new System.NotImplementedException();
		}

		public void UpdateCurves(string objectId)
		{
			throw new System.NotImplementedException();
		}
	}
}
