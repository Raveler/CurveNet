using System;
using System.Collections.Generic;
using System.IO;

namespace Bombardel.CurveNet.Shared.Serialization
{

	public class Id : IEquatable<Id>, IComparable<Id>, ISerializable
	{
		private readonly byte flagMask = 0b1000_0000;
		private readonly byte dataMask = 0b0111_1111;


		private int _id = 0;

		public Id()
		{
		}
		public Id(int id)
		{
			_id = id;
		}

		public void Serialize(IDataWriter writer)
		{
			// perform a special kind of serialization so that objects don't ALWAYS occupy 4 bytes
			bool done = false;
			int id = _id;
			while (!done)
			{
				int nextByte = id & dataMask;
				id = id >> 7;
				if (id == 0) done = true;
				if (!done) nextByte = nextByte | flagMask; // append the flag mask to the byte, if we have more data
				writer.Write((byte)nextByte);
			}
		}


		public void Deserialize(IDataReader reader) {
			bool done = false;
			int multiplier = 1;
			_id = 0;
			while (!done)
			{
				byte b = reader.ReadByte();
				int flag = b & flagMask;
				int data = b & dataMask;
				_id += data * multiplier;
				if (flag == 0) done = true;
				else multiplier *= 128;
			}
		}
		
		public override bool Equals(object obj)
		{
			if (!(obj is Id)) return false;
			return ((Id)obj)._id == _id;
		}

		public override int GetHashCode()
		{
			return _id;
		}

		public bool Equals(Id other)
		{
			return other._id == _id;
		}

		public int CompareTo(Id other)
		{
			return _id.CompareTo(other._id);
		}

		public override string ToString()
		{
			return "[" + _id + "]";
		}
	}
}
