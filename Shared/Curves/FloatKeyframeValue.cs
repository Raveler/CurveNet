using Bombardel.CurveNet.Shared.Serialization;
using Bombardel.CurveNet.Shared.ServerMessages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bombardel.CurveNet.Shared.Curves
{
	public class FloatKeyframeValue : IKeyframeValue<FloatKeyframeValue>
	{
		public float Value => _value;


		private float _value;


		public FloatKeyframeValue()
		{
		}
		public FloatKeyframeValue(float value)
		{
			_value = value;
		}

		public void Deserialize(IDataReader reader)
		{
			_value = reader.ReadFloat();
		}

		public FloatKeyframeValue InterpolateTo(FloatKeyframeValue next, float t)
		{
			float newValue = _value + (next._value - _value) * t;
			return new FloatKeyframeValue(newValue);
		}

		public void Serialize(IDataWriter writer)
		{
			writer.Write(_value);
		}
	}
}
