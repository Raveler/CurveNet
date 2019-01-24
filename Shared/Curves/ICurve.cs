using Bombardel.CurveNet.Shared.Serialization;
using Bombardel.CurveNet.Shared.ServerMessages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bombardel.CurveNet.Shared.Curves
{
	public interface ICurve
	{
		Id Id { get; }

		void ApplyConfig(CurveConfig config);

		CurveConfig GetConfig();
	}
}
