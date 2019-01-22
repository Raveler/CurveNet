using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
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

	public class Connection
	{
		private class WebSocketClosedException : Exception {
			public WebSocketCloseStatus CloseStatus { get; set; }
			public string CloseStatusDescription { get; set; }
		}

		private WebSocket _socket;

		private ConcurrentQueue<ArraySegment<byte>> _outgoingMessages = new ConcurrentQueue<ArraySegment<byte>>();

		private int _bufferSize;
		private byte[] _inBuffer;

		private ILogger<Connection> _logger;


		public Connection(WebSocket socket, int bufferSize)
		{
			_socket = socket;
			_bufferSize = bufferSize;
			_inBuffer = new byte[_bufferSize];
			_logger = LoggingFactory.CreateLogger<Connection>();
		}

		public async Task Run(IConnectionHandler handler)
		{
			// start a separate thread for sending messages
			CancellationTokenSource sendMessageToken = new CancellationTokenSource();
			Task sendMessageTask = Task.Run(() => SendMessages(sendMessageToken.Token));

			// create a byte segment from the buffer
			ArraySegment<byte> inSegment = new ArraySegment<byte>(_inBuffer, 0, _bufferSize);

			// start receiving messages until we're done
			WebSocketCloseStatus closeStatus = WebSocketCloseStatus.Empty;
			string closeStatusDescription = null;
			try
			{
				while (true)
				{
					byte[] msg = await ReceiveFullMessage(CancellationToken.None);
					handler.OnMessage(msg);
				}
			}
			catch (WebSocketException ex)
			{
				switch (ex.WebSocketErrorCode)
				{
					case WebSocketError.ConnectionClosedPrematurely:
						_logger.LogError("WebSocket connection closed prematurely");
						// handle error
						break;

					default:
						// handle error
						_logger.LogError($"WebSocket closed: {ex.Message}");
						break;
				}
			}
			catch (WebSocketClosedException ex) {
				closeStatus = ex.CloseStatus;
				closeStatusDescription = ex.CloseStatusDescription;
			}

			// wait for the outgoing message pipe to stop
			sendMessageToken.Cancel();
			await sendMessageTask;


			// finally, close the socket
			await _socket.CloseAsync(closeStatus, closeStatusDescription, CancellationToken.None);
		}

		async Task<byte[]> ReceiveFullMessage(CancellationToken cancelToken)
		{
			WebSocketReceiveResult response;

			MemoryStream stream = new MemoryStream();

			do
			{
				response = await _socket.ReceiveAsync(new ArraySegment<byte>(_inBuffer), cancelToken);
				stream.Write(_inBuffer, 0, response.Count);

				// the socket was closed!
				if (response.CloseStatus.HasValue)
				{
					throw new WebSocketClosedException()
					{
						CloseStatus = response.CloseStatus.Value,
						CloseStatusDescription = response.CloseStatusDescription,
					};
				}

			} while (!response.EndOfMessage);

			byte[] msg = stream.ToArray();
			stream.Close(); // not required, but we don't want to keep it in memory any longer than necessary
			return msg;
		}


		private async void SendMessages(CancellationToken cancelToken)
		{
			byte[] outBuffer = new byte[_bufferSize];

			// just keep sending messages forevah
			while (!cancelToken.IsCancellationRequested)
			{

				// process all messages
				while (_outgoingMessages.TryDequeue(out ArraySegment<byte> msg)) {
					await _socket.SendAsync(msg, WebSocketMessageType.Binary, true, cancelToken);
				}

				// short sleep
				await Task.Delay(TimeSpan.FromMilliseconds(100));

			}

		}

		public void Send(ArraySegment<byte> message)
		{
			_outgoingMessages.Enqueue(message);
		}
	}
}
