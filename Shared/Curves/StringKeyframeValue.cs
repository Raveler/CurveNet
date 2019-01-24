using Bombardel.CurveNet.Shared.Serialization;
using Bombardel.CurveNet.Shared.ServerMessages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bombardel.CurveNet.Shared.Curves
{
	public class StringKeyframeValue : IKeyframeValue<StringKeyframeValue>
	{
		public string Value => _value;


		private string _value;


		public StringKeyframeValue()
		{
		}
		public StringKeyframeValue(string value)
		{
			_value = value;
		}

		public void Deserialize(IDataReader reader)
		{
			_value = reader.ReadString();
		}

		public StringKeyframeValue InterpolateTo(StringKeyframeValue next, float t)
		{
			return this; // we can't interpolate strings
		}

		public void Serialize(IDataWriter writer)
		{
			writer.Write(_value);
		}
	}
}
