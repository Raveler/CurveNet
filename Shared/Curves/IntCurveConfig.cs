using Bombardel.CurveNet.Shared.Curves;
using Bombardel.CurveNet.Shared.Serialization;
using Bombardel.CurveNet.Shared.ServerMessages;
using System.Collections.Generic;
using System.Linq;

namespace Bombardel.CurveNet.Shared.Curves
{

	public class IntCurveConfig : CurveConfig
	{
		public int defaultValue;


		public IntCurveConfig(CurveType type)
		{
			this.type = type;
			this.valueType = CurveValueType.Int;
		}

		public IntCurveConfig(CurveType type, int defaultValue)
		{
			this.type = type;
			this.valueType = CurveValueType.Int;
			this.defaultValue = defaultValue;
		}

		public override ICurve CreateCurve()
		{
			throw new System.NotImplementedException();
		}

		public override void DeserializeDefaultValue(IDataReader reader)
		{
			defaultValue = reader.ReadInt();
		}

		public override void SerializeDefaultValue(IDataWriter writer)
		{
			writer.Write(defaultValue);
		}
	}
}
