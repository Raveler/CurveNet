using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Bombardel.CurveNet.Server.WebSockets;
using Bombardel.CurveNet.Shared.Client;
using Bombardel.CurveNet.Shared.ClientMessages;
using Bombardel.CurveNet.Shared.Server;
using Bombardel.CurveNet.Shared.ServerMessages;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using static Bombardel.CurveNet.Server.Sessions.ObjectBasedSerializer;

namespace Bombardel.CurveNet.Server.Sessions
{

	public class ConnectionHandler : IConnectionHandler, IServer
	{
		public IClient Connection => _clientSerializer;
		public string Id => _id;


		private GameManager _gameManager;
		private Connection _connection;
		private ClientSerializer _clientSerializer;

		private string _id;

		private ServerDeserializer _deserializer;


		public ConnectionHandler(GameManager gameManager, Connection conn, string id)
		{
			_connection = conn;
			_gameManager = gameManager;
			_id = id;
			_deserializer = new ServerDeserializer(this);
			_clientSerializer = new ClientSerializer(_connection);
		}

		public void OnClose()
		{
			throw new NotImplementedException();
		}

		public void OnMessage(byte[] message)
		{
			try
			{
				_deserializer.Deserialize(message);
			}
			catch (MessageTypeNotSupportedException) {
				Connection.SendProtocolError(ProtocolError.InvalidMessageType);
			}
			catch (ProtocolErrorException e)
			{
				Connection.SendProtocolError(e.error);
			}
		}


		public void JoinRoom(string roomName)
		{
			_gameManager.JoinRoom(this, roomName);
		}

		public void LeaveRoom(string roomName)
		{
			_gameManager.LeaveRoom(this, roomName);
		}

		public void ListRooms()
		{
			_gameManager.ListRooms(this);
		}
		
		public void AddCurve(string objectId)
		{
			throw new NotImplementedException();
		}

		public void AddObjectToRoom(string objectId, string roomName)
		{
			throw new NotImplementedException();
		}

		public void CreateObject(string objectId)
		{
			throw new NotImplementedException();
		}

		public void UpdateCurve(string objectId)
		{
			throw new NotImplementedException();
		}

		public void UpdateCurves(string objectId)
		{
			throw new NotImplementedException();
		}

		public void RemoveObject(string objectId)
		{
			throw new NotImplementedException();
		}
	}
}
