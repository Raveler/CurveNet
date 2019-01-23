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

		byte[] ToArray();
	}
}
