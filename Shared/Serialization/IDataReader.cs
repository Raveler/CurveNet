using System;
using System.Collections.Generic;
using System.IO;

namespace Bombardel.CurveNet.Shared.Serialization
{

	public interface IDataReader
	{
		byte ReadByte();
		int ReadInt();
		float ReadFloat();
		string ReadString();
		byte[] ReadByteArray();
	}
}
