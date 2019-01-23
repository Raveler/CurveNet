using System;
using System.Collections.Generic;
using System.IO;

namespace Bombardel.CurveNet.Shared.Serialization
{

	public class BinaryDataReader : IDataReader
	{
		private BinaryReader _reader;

		public BinaryDataReader(byte[] bytes)
		{
			_reader = new BinaryReader(new MemoryStream(bytes));
		}

		public byte ReadByte()
		{
			return _reader.ReadByte();
		}

		public byte[] ReadByteArray()
		{
			int count = _reader.ReadInt32();
			return _reader.ReadBytes(count);
		}

		public float ReadFloat()
		{
			return _reader.ReadSingle();
		}

		public int ReadInt()
		{
			return _reader.ReadInt32();
		}

		public string ReadString()
		{
			return _reader.ReadString();
		}
	}
}
