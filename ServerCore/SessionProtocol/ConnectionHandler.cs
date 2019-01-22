using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Bombardel.CurveNet.Server.Sessions.Incoming;
using Bombardel.CurveNet.Server.Sessions.Outgoing;
using Bombardel.CurveNet.Server.WebSockets;
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

	public class ConnectionHandler : IConnectionHandler
	{
		private GameManager _gameManager;
		private Connection _connection;

		private string _id;

		private ObjectBasedSerializer _serializer;


		public ConnectionHandler(GameManager gameManager, Connection conn, string id)
		{
			_connection = conn;
			_gameManager = gameManager;
			_id = id;

			_serializer = new ObjectBasedSerializer();

			// incoming messages
			AddType<JoinRoomEvent>(_gameManager.JoinRoom);
			AddType<LeaveRoomEvent>(_gameManager.LeaveRoom);

			// outgoing messages
			AddType<ProtocolErrorMessage>(null);

		}

		private void AddType<T>(Action<string, T> messageCallback) {
			_serializer.AddType<T>(new JsonSerializer<T>((T obj) =>
			{
				messageCallback(_id, obj);
			}));
		}

		public void OnClose()
		{
			throw new NotImplementedException();
		}

		public void OnMessage(byte[] message)
		{
			try
			{
				_serializer.Deserialize(message, 0, message.Length);
			}
			catch (TypeNotRegisteredException e) {
				Send(new ProtocolErrorMessage()
				{
					error = ProtocolError.InvalidMessageType,
				});
			}
			catch (ProtocolErrorException e)
			{
				Send(new ProtocolErrorMessage()
				{
					error = e.error,
				});
			}
		}

		public void Send<T>(T obj) {
			try
			{
				byte[] bytes = _serializer.Serialize(obj);
			}
			catch (TypeNotRegisteredException e)
			{
				Send(new ProtocolErrorMessage()
				{
					error = ProtocolError.InvalidMessageType,
				});
			}
		}
	}
}
