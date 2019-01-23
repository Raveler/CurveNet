using Bombardel.CurveNet.Shared.Serialization;
using Bombardel.CurveNet.Shared.ServerMessages;
using System.Collections.Generic;
using System.Linq;

namespace Bombardel.CurveNet.Shared.Curves
{

	public class StringCurveConfig : CurveConfig
	{
		public string defaultValue;


		public StringCurveConfig(CurveType type)
		{
			this.type = type;
			this.valueType = CurveValueType.String;
		}

		public StringCurveConfig(CurveType type, string defaultValue)
		{
			this.type = type;
			this.valueType = CurveValueType.String;
			this.defaultValue = defaultValue;
		}

		public override ICurve CreateCurve()
		{
			throw new System.NotImplementedException();
		}

		public override void DeserializeDefaultValue(IDataReader reader)
		{
			defaultValue = reader.ReadString();
		}

		public override void SerializeDefaultValue(IDataWriter writer)
		{
			writer.Write(defaultValue);
		}
	}
}
