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

		private Dictionary<Id, Timeline> _timelines = new Dictionary<Id, Timeline>();


		public void CreateTimeline(Id id, IKeyframeValueListener listener, IKeyframeValue initialValue)
		{
			Timeline timeline = new Timeline(listener, initialValue);
			_timelines.Add(id, timeline);
		}

		public void AddKeyframe(Id id, Keyframe keyframe)
		{
			Timeline timeline = GetTimeline(id);
			timeline.AddKeyframe(keyframe);
		}

		private Timeline GetTimeline(Id id)
		{
			if (!_timelines.ContainsKey(id)) throw new ArgumentException("Timeline with id " + id + " does not exist.");
			Timeline timeline = _timelines[id];
			return timeline;
		}

		public IKeyframeValue GetValue(Id id)
		{
			return GetTimeline(id).CalculateValue();
		}

		public void SetTime(float time)
		{
			foreach (Timeline timeline in _timelines.Values)
			{
				timeline.SetTime(time);
			}
		}

		public void AdvanceTime(float dt)
		{
			foreach (Timeline timeline in _timelines.Values)
			{
				timeline.Advance(dt);
			}
		}
	}
}
