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

		public void Write(byte[] data)
		{
			_writer.Write(data.Length);
			_writer.Write(data);
		}
	}
}
