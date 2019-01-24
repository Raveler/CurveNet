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


		public PulseCurveBinder(CurveStore curveStore, Action onPulseReceived)
		{
			_curveStore = curveStore;
			_onPulseReceived = onPulseReceived;
		}

		public void RegisterLocal()
		{
			_id = _curveStore.RegisterLocalCurve(this, PulseKeyframeValue.Empty, InterpolationType.Linear.GetInterpolator<PulseKeyframeValue>());
		}

		public void RegisterRemote(Id id)
		{
			_id = id;
			_curveStore.RegisterRemoteCurve(id, this, PulseKeyframeValue.Empty, InterpolationType.Linear.GetInterpolator<PulseKeyframeValue>());
		}

		public void SendPulseToServer()
		{
			_curveStore.SubmitKeyframeToServer(_id, new KeyframeData(new Keyframe<PulseKeyframeValue>(_curveStore.Time, PulseKeyframeValue.Empty)));
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
