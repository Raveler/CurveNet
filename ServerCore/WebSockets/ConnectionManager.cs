using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Bombardel.CurveNet.Server.WebSockets
{

	public class ConnectionManager
	{
		private List<Connection> _connections = new List<Connection>();

		private int _receiveBufferSize;

		private IConnectionHandlerFactory _connectionHandlerFactory;


		public ConnectionManager(int receiveBufferSize, IConnectionHandlerFactory connectionHandlerFactory)
		{
			_receiveBufferSize = receiveBufferSize;
			_connectionHandlerFactory = connectionHandlerFactory;
		}


		public async Task AcceptConnections(HttpContext context, Func<Task> next)
		{
			if (context.Request.Path == "/ws")
			{
				if (context.WebSockets.IsWebSocketRequest)
				{
					WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
					await OpenConnection(webSocket);
				}
				else
				{
					context.Response.StatusCode = 400;
				}
			}
			else
			{
				await next();
			}
		}


		public async Task OpenConnection(WebSocket socket)
		{
			// initialize the connection
			Connection connection = new Connection(socket, _receiveBufferSize);
			IConnectionHandler handler = _connectionHandlerFactory.CreateHandler(connection);
			_connections.Add(connection);

			// keep going until the connection is closed
			await connection.Run(handler);

			// done
			handler.OnClose();
		}

	}
}
