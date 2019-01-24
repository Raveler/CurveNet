using Bombardel.CurveNet.Shared.Serialization;
using Bombardel.CurveNet.Shared.Server;
using Bombardel.CurveNet.Shared.ServerMessages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bombardel.CurveNet.Shared.Curves
{

	public class CurveStore : ICurveNetworkClient
	{
		public float Time => _timelineStore.Time;


		private TimelineStore _timelineStore = new TimelineStore();

		private Dictionary<Id, PulseCurveBinder> _idToCurve = new Dictionary<Id, PulseCurveBinder>();
		private List<PulseCurveBinder> _curves = new List<PulseCurveBinder>();

		private ICurveNetworkServer _networkInterface;


		public CurveStore(ICurveNetworkServer networkInterface)
		{
			_networkInterface = networkInterface;
		}

		public void RegisterCurve<T>(Id curveId, IKeyframeValueListener<T> listener, T initialValue, IInterpolator<T> interpolator) where T : IKeyframeValue<T>, new()
		{
			_timelineStore.CreateTimeline(curveId, listener, initialValue, interpolator);
		}

		public void AddKeyframe(Id curveId, KeyframeData keyframe)
		{
			// add the keyframe to the timeline
			_timelineStore.AddKeyframe(curveId, keyframe);
		}

		public void SubmitKeyframe(Id curveId, KeyframeData keyframe)
		{
			// add the keyframe to the timeline
			AddKeyframe(curveId, keyframe);

			// send over the network to the server so that all the others will receive it as well
			_networkInterface.SubmitKeyframe(curveId, keyframe);
		}

		public void SetTime(float time)
		{
			_timelineStore.SetTime(time);
		}

		public void AdvanceTime(float dt)
		{
			_timelineStore.AdvanceTime(dt);
		}
	}
}
