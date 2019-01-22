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
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Bombardel.CurveNet.Server.Sessions
{

	public class JsonSerializer<T> : ISerializer<T>
	{
		private Action<T> _messageCallback;


		public JsonSerializer(Action<T> messageCallback) {
			_messageCallback = messageCallback;
		}

		public void Deserialize(byte[] bytes, int offset, int length)
		{
			string s = Encoding.ASCII.GetString(bytes, offset, length);
			T obj = JsonConvert.DeserializeObject<T>(s);
			_messageCallback(obj);
		}

		public byte[] Serialize(T obj)
		{
			return Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(obj));
		}
	}
}
