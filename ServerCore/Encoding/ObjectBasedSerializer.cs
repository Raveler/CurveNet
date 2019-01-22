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

	public class ObjectBasedSerializer : IGenericSerializer
	{
		public class TooManyTypesException : Exception {
			public int MaxAmount = 256;
		}
		public class TypeNotRegisteredException : Exception { }

		private struct TypeData {
			public int id;
			public IGenericSerializer serializer;
		}

		private Dictionary<Type, TypeData> _registeredTypes = new Dictionary<Type, TypeData>();
		private Dictionary<int, Type> _idToType = new Dictionary<int, Type>();


		public void AddType<T>(ISerializer<T> serializer) {
			int typeIdx = _registeredTypes.Count;
			if (typeIdx > 255) throw new TooManyTypesException();

			_registeredTypes.Add(typeof(T), new TypeData()
			{
				id = typeIdx,
				serializer = new GenericToTypeSerializer<T>(serializer),
			});
			_idToType.Add(typeIdx, typeof(T));
		}

		public byte[] Serialize(object obj)
		{
			TypeData data = GetTypeData(obj.GetType());

			MemoryStream stream = new MemoryStream();
			stream.WriteByte((byte)data.id);
			stream.Write(data.serializer.Serialize(obj));
			byte[] bytes = stream.ToArray();
			stream.Close();
			return bytes;
		}

		public void Deserialize(byte[] bytes, int offset, int count)
		{
			int typeId = (int)bytes[0];
			Type type = _idToType[typeId];
			TypeData data = GetTypeData(type);
			data.serializer.Deserialize(bytes, 1, bytes.Length - 1);
		}

		private TypeData GetTypeData(Type type)
		{
			if (!_registeredTypes.ContainsKey(type))
			{
				throw new TypeNotRegisteredException();
			}

			TypeData data = _registeredTypes[type];
			return data;
		}

	}
}
