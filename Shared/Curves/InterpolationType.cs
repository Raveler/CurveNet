using Bombardel.CurveNet.Shared.Serialization;
using Bombardel.CurveNet.Shared.ServerMessages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bombardel.CurveNet.Shared.Curves
{
	public enum InterpolationType
	{
		Linear,
		Step
	}

	public static class InterpolationTypeExtensions {

		public static IInterpolator<T> GetInterpolator<T>(this InterpolationType interpolationType) where T : IKeyframeValue<T>, new() {
			switch (interpolationType) {
				case InterpolationType.Linear:
					return new LinearInterpolator<T>();

				case InterpolationType.Step:
					return new StepInterpolator<T>();

				default:
					throw new ArgumentException("Interpolation type " + interpolationType + " does not have an interpolator defined for it.");
			}
		}
	}

	public interface IInterpolator<T> where T : IKeyframeValue<T>, new()
	{
		T Interpolate(T prev, T next, float t);
	}

	public class LinearInterpolator<T> : IInterpolator<T> where T : IKeyframeValue<T>, new()
	{
		public T Interpolate(T prev, T next, float t)
		{
			return prev.InterpolateTo(next, t);
		}
	}

	public class StepInterpolator<T> : IInterpolator<T> where T : IKeyframeValue<T>, new()
	{
		public T Interpolate(T prev, T next, float t)
		{
			return prev;
		}
	}
}
