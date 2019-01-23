using Bombardel.CurveNet.Shared.Curves;
using Bombardel.CurveNet.Shared.Serialization;
using Bombardel.CurveNet.Shared.ServerMessages;
using System.Collections.Generic;
using System.Linq;

namespace Bombardel.CurveNet.Shared.Curves
{

	public class FloatCurveConfig : CurveConfig
	{
		public float defaultValue;


		public FloatCurveConfig(CurveType type)
		{
			this.type = type;
			this.valueType = CurveValueType.Float;
		}

		public FloatCurveConfig(CurveType type, float defaultValue)
		{
			this.type = type;
			this.valueType = CurveValueType.Float;
			this.defaultValue = defaultValue;
		}

		public override ICurve CreateCurve()
		{
			throw new System.NotImplementedException();
		}

		public override void DeserializeDefaultValue(IDataReader reader)
		{
			defaultValue = reader.ReadFloat();
		}

		public override void SerializeDefaultValue(IDataWriter writer)
		{
			writer.Write(defaultValue);
		}
	}
}
