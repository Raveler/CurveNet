using Newtonsoft.Json;
using System;
using System.Text;

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
