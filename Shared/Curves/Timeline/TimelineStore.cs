using Bombardel.CurveNet.Shared.Curves;
using Bombardel.CurveNet.Shared.Serialization;
using Bombardel.CurveNet.Shared.Server;
using Bombardel.CurveNet.Shared.ServerMessages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bombardel.CurveNet.Shared.Curves
{
	public class TimelineStore
	{
		public float Time
		{
			get
			{
				return _time;
			}
		}

		private float _time = 0.0f;

		private Dictionary<Id, ITimeline> _timelines = new Dictionary<Id, ITimeline>();


		public void CreateTimeline<T>(Id id, IKeyframeValueListener<T> listener, T initialValue, IInterpolator<T> interpolator) where T : IKeyframeValue<T>, new()
		{
			ITimeline timeline = new Timeline<T>(listener, initialValue, interpolator);
			_timelines.Add(id, timeline);
		}

		public void AddKeyframe(Id id, KeyframeData keyframe)
		{
			ITimeline timeline = GetTimeline(id);
			timeline.AddKeyframe(keyframe);
		}

		private ITimeline GetTimeline(Id id)
		{
			if (!_timelines.ContainsKey(id)) throw new ArgumentException("Timeline with id " + id + " does not exist.");
			ITimeline timeline = _timelines[id];
			return timeline;
		}
		
		public void SetTime(float time)
		{
			foreach (ITimeline timeline in _timelines.Values)
			{
				timeline.SetTime(time);
			}
		}

		public void AdvanceTime(float dt)
		{
			foreach (ITimeline timeline in _timelines.Values)
			{
				timeline.Advance(dt);
			}
		}
	}
}
