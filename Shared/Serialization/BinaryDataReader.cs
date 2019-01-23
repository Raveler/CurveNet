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

		public List<byte> ReadByteList()
		{
			return ReadList(ReadByte);
		}

		public List<int> ReadIntList()
		{
			return ReadList(ReadInt);
		}

		public List<float> ReadFloatList()
		{
			return ReadList(ReadFloat);
		}

		public List<string> ReadStringList()
		{
			return ReadList(ReadString);
		}

		private List<T> ReadList<T>(Func<T> itemReader)
		{
			int n = ReadInt();
			List<T> list = new List<T>();
			for (int i = 0; i < n; i++)
			{
				list.Add(itemReader());
			}
			return list;
		}

		public List<T> ReadList<T>() where T : ISerializable, new()
		{
			int n = ReadInt();
			List<T> list = new List<T>();
			for (int i = 0; i < n; i++)
			{
				T instance = new T();
				instance.Deserialize(this);
				list.Add(instance);
			}
			return list;
		}
	}
}
