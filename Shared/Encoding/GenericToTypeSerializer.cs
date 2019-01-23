using System;

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
