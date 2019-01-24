using Bombardel.CurveNet.Shared.Serialization;
using Bombardel.CurveNet.Shared.ServerMessages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bombardel.CurveNet.Shared.Curves
{
	public class IntKeyframeValue : IKeyframeValue<IntKeyframeValue>
	{
		public int Value => _value;


		private int _value;


		public IntKeyframeValue()
		{
		}
		public IntKeyframeValue(int value)
		{
			_value = value;
		}

		public void Deserialize(IDataReader reader)
		{
			_value = reader.ReadInt();
		}

		public IntKeyframeValue InterpolateTo(IntKeyframeValue next, float t)
		{
			int newValue = (int)Math.Round(_value + (next._value - _value) * t);
			return new IntKeyframeValue(newValue);
		}

		public void Serialize(IDataWriter writer)
		{
			writer.Write(_value);
		}
	}
}
