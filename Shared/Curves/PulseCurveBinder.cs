using Bombardel.CurveNet.Shared.Serialization;
using Bombardel.CurveNet.Shared.ServerMessages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bombardel.CurveNet.Shared.Curves
{
	public class PulseCurveBinder : IKeyframeValueListener<PulseKeyframeValue>
	{
		private Action _onPulseReceived;

		private CurveStore _curveStore;

		private Id _id;


		public PulseCurveBinder(CurveStore curveStore, Id curveId, Action onPulseReceived)
		{
			_id = curveId;
			_curveStore = curveStore;
			curveStore.RegisterCurve(curveId, this, PulseKeyframeValue.Empty, InterpolationType.Linear.GetInterpolator<PulseKeyframeValue>());
			_onPulseReceived = onPulseReceived;
		}

		public void SendPulseToServer()
		{
			_curveStore.SubmitKeyframe(_id, new KeyframeData(new Keyframe<PulseKeyframeValue>(_curveStore.Time, PulseKeyframeValue.Empty)));
		}

		public void SetValue(PulseKeyframeValue value)
		{
			// pulse curves don't care about values
		}

		public void SetKeyframePassed(PulseKeyframeValue value)
		{
			_onPulseReceived();
		}
	}
}
