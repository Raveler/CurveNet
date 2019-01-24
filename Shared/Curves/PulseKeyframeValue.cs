using Bombardel.CurveNet.Shared.Serialization;
using Bombardel.CurveNet.Shared.ServerMessages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bombardel.CurveNet.Shared.Curves
{
	public class PulseKeyframeValue : IKeyframeValue<PulseKeyframeValue>
	{
		private OnPulseDelegate _callback;


		public PulseKeyframeValue(OnPulseDelegate callback)
		{
			_callback = callback;
		}

		public PulseKeyframeValue InterpolateTo(PulseKeyframeValue next, float t)
		{
			return this; // doesn't matter, isn't used anyway in the case of pulses
		}

		public void OnPassed()
		{
			if (_empty != null) _callback();
		}

		public static PulseKeyframeValue Empty
		{
			get
			{
				return _empty;
			}
		}

		private static PulseKeyframeValue _empty = new PulseKeyframeValue(null);
	}
}
