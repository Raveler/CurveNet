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

	public class GenericToTypeSerializer<T> : IGenericSerializer
	{
		public class InvalidTypeException : Exception { }

		private ISerializer<T> _serializer;


		public GenericToTypeSerializer(ISerializer<T> serializer)
		{
			_serializer = serializer;
		}


		public void Deserialize(byte[] bytes, int offset, int count)
		{
			_serializer.Deserialize(bytes, offset, count);
		}

		public byte[] Serialize(object obj)
		{
			if (!(obj is T)) throw new InvalidTypeException();
			return _serializer.Serialize((T)obj);
		}
	}
}
