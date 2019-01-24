using Bombardel.CurveNet.Shared.Curves;
using Bombardel.CurveNet.Shared.Serialization;
using Bombardel.CurveNet.Shared.Server;
using Bombardel.CurveNet.Shared.ServerMessages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bombardel.CurveNet.Shared.Curves
{
	public class Timeline<T> where T : IKeyframeValue<T>
	{
		private float _time = 0.0f;

		private List<Keyframe> _keyframes = new List<Keyframe>();

		private int _prevKeyframe;
		private IKeyframeValue<T> _value;

		private IKeyframeValueListener _listener;


		public Timeline(IKeyframeValueListener listener, IKeyframeValue initialValue)
		{
			_listener = listener;

			_keyframes.Add(new Keyframe(0.0f, initialValue));
			ResetPrevKeyframe(); // should always set it to 0
			UpdateValue();
		}

		public void AddKeyframe(Keyframe keyframe)
		{
			// Go backwards from the last keyframe, so that if the keyframe is later than the last one,
			// we don't have to loop through all of them (this is the most common case).
			// Because there is always a keyframe at time 0, this will always insert it.
			int insertIndex = -1;
			for (int i = _keyframes.Count - 1; i >= 0; --i) {
				if (_keyframes[i].Time <= keyframe.Time)
				{
					insertIndex = i + 1;
					_keyframes.Insert(i + 1, keyframe);
					break;
				}
			}

			// if the keyframe was inserted at or before our prevIndex, we re-calculate it
			if (insertIndex <= _prevKeyframe) {
				ResetPrevKeyframe();
			}
		}

		private void ResetPrevKeyframe()
		{
			// find the first keyframe that is later than the current time
			// the keyframe before that is our prevKeyframe
			_prevKeyframe = -1;
			for (int i = 0; i < _keyframes.Count; ++i)
			{
				if (_keyframes[i].Time > _time)
				{
					_prevKeyframe = i - 1;
					break;
				}
			}

			// no keyframe found later than our time - the last keyframe is the prev one
			if (_prevKeyframe == -1)
			{
				_prevKeyframe = _keyframes.Count - 1;
			}
		}

		public void SetTime(float time) {
			_time = time;
			ResetPrevKeyframe();
			UpdateValue();
		}

		public void Advance(float dt)
		{

			// calculate the new time
			_time += dt;

			// advance through all keyframes until we reach the keyframe beyond this time
			while (_prevKeyframe < _keyframes.Count - 1 && _time >= _keyframes[_prevKeyframe + 1].Time)
			{
				++_prevKeyframe;

				// we passed this keyframe, so we trigger it
				_listener.SetKeyframePassed(_keyframes[_prevKeyframe].Value);
			}

			// we have now found our prevKeyframe that can be used for interpolation below
			UpdateValue();
		}

		private void UpdateValue()
		{
			// calculate the current value, possibly interpolated if linear
			IKeyframeValue value = CalculateValue();

			// set it
			_listener.SetValue(value);
		}

		private IKeyframeValue CalculateValue()
		{
			// we are beyond the last keyframe - just return the value of that keyframe
			if (_prevKeyframe == _keyframes.Count - 1)
			{
				return _keyframes[_prevKeyframe].Value;
			}

			// Interpolate between the two current keyframes to acquire the current value.
			// There MUST be two keyframes at least to get here, otherwise the above condition
			// would trigger.
			Keyframe prevKeyframe = _keyframes[_prevKeyframe];
			Keyframe nextKeyframe = _keyframes[_prevKeyframe + 1];
			float t = (_time - prevKeyframe.Time) / (nextKeyframe.Time - prevKeyframe.Time);
			return prevKeyframe.Value.InterpolateTo(nextKeyframe.Value, t);
		}

	}


	public interface IBla<T>
	{
		void X(T other);
	}

	public class Bla : IBla<Bla>
	{
		public void X(Bla other)
		{
			throw new NotImplementedException();
		}
	}

	public class GenBla<T> where T : IBla<T>
	{
		void Test(T v)
		{
			v.X(v);
		}
	}
}
