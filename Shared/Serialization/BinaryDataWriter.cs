using System;
using System.Collections.Generic;
using System.IO;

namespace Bombardel.CurveNet.Shared.Serialization
{

	public class BinaryDataWriter : IDataWriter
	{
		private BinaryWriter _writer;
		private MemoryStream _stream;


		public BinaryDataWriter() {
			_stream = new MemoryStream();
			_writer = new BinaryWriter(_stream);
		}

		public void Reset()
		{
			_stream.Seek(0, SeekOrigin.Begin);
			_stream.SetLength(0);
		}

		public byte[] ToArray()
		{
			return _stream.ToArray();
		}

		public void Write(byte value)
		{
			_writer.Write(value);
		}

		public void Write(float value)
		{
			_writer.Write(value);
		}

		public void Write(int value)
		{
			_writer.Write(value);
		}

		public void Write(string value)
		{
			_writer.Write(value);
		}

		public void Write(ISerializable value)
		{
			value.Serialize(this);
		}

		public void Write(byte[] data)
		{
			_writer.Write(data.Length);
			_writer.Write(data);
		}

		public void Write(List<int> list)
		{
			Write(list, Write);
		}

		public void Write(List<float> list)
		{
			Write(list, Write);
		}

		public void Write(List<string> list)
		{
			Write(list, Write);
		}

		public void Write(List<byte> list)
		{
			Write(list, Write);
		}

		public void Write<T>(List<T> list) where T : ISerializable
		{
			Write(list.Count);
			for (int i = 0; i < list.Count; ++i)
			{
				list[i].Serialize(this);
			}
		}

		private void Write<T>(List<T> list, Action<T> itemWriter)
		{
			Write(list.Count);
			for (int i = 0; i < list.Count; ++i)
			{
				itemWriter(list[i]);
			}
		}
	}
}
