using Bombardel.CurveNet.Shared.Serialization;
using Bombardel.CurveNet.Shared.ServerMessages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bombardel.CurveNet.Shared.Curves
{
	public class PulseKeyframeValue : IKeyframeValue<PulseKeyframeValue>
	{

		public PulseKeyframeValue InterpolateTo(PulseKeyframeValue next, float t)
		{
			return this; // doesn't matter, isn't used anyway in the case of pulses
		}

		public void Serialize(IDataWriter writer)
		{
		}

		public void Deserialize(IDataReader reader)
		{
		}

		public static PulseKeyframeValue Empty
		{
			get
			{
				return _empty;
			}
		}

		private static PulseKeyframeValue _empty = new PulseKeyframeValue();
	}
}
