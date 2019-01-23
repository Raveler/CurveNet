using System;
using System.Collections.Generic;
using System.IO;

namespace Bombardel.CurveNet.Shared.Serialization
{

	public interface IDataWriter
	{
		void Reset();

		void Write(byte value);
		void Write(int value);
		void Write(float value);
		void Write(string value);
		void Write(byte[] data);

		void Write(List<byte> list);
		void Write(List<int> list);
		void Write(List<float> list);
		void Write(List<string> list);

		void Write(ISerializable value);

		void Write<T>(List<T> list) where T : ISerializable;

		byte[] ToArray();
	}
}
