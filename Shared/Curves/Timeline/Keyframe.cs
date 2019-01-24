using Bombardel.CurveNet.Shared.Curves;
using Bombardel.CurveNet.Shared.Serialization;
using Bombardel.CurveNet.Shared.Server;
using Bombardel.CurveNet.Shared.ServerMessages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Bombardel.CurveNet.Shared.Curves
{
	public class Keyframe<T> : IKeyframe where T : IKeyframeValue<T>, new()
	{
		public T Value { get; set; }
		public float Time { get; set; }


		public Keyframe()
		{
		}

		public Keyframe(float time, T value)
		{
			if (time < 0.0f) throw new ArgumentOutOfRangeException("Time must be a positive value");
			Time = time;
			Value = value;
		}

		public void Deserialize(byte[] bytes)
		{
			BinaryDataReader reader = new BinaryDataReader(bytes);
			Time = reader.ReadFloat();
			Value = new T();
			Value.Deserialize(reader);
		}

		public byte[] Serialize()
		{
			BinaryDataWriter writer = new BinaryDataWriter();
			writer.Write(Time);
			Value.Serialize(writer);
			return writer.ToArray();
		}
	}
}
