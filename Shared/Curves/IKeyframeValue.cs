using Bombardel.CurveNet.Shared.Serialization;
using Bombardel.CurveNet.Shared.ServerMessages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bombardel.CurveNet.Shared.Curves
{
	public interface IKeyframeValue<T> where T : IKeyframeValue<T>
	{
		T InterpolateTo(T next, float t);

		void Serialize(IDataWriter writer);

		void Deserialize(IDataReader reader);
	}
}
