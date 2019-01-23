using Bombardel.CurveNet.Shared.Curves;
using Bombardel.CurveNet.Shared.Serialization;
using Bombardel.CurveNet.Shared.Server;
using Bombardel.CurveNet.Shared.ServerMessages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bombardel.CurveNet.Shared.Curves
{
	public class InvalidCurveValueTypeException : Exception { }

	public abstract class CurveConfig : ISerializable, ICurveConfig
	{
		public CurveType type;
		public CurveValueType valueType;


		public static CurveConfig DeserializeCurveConfig(IDataReader reader)
		{
			CurveType type = (CurveType)reader.ReadByte();
			CurveValueType valueType = (CurveValueType)reader.ReadByte();

			CurveConfig config = null;
			switch (valueType) {
				case CurveValueType.Int:
					config = new IntCurveConfig(type);
					break;

				case CurveValueType.Float:
					config = new FloatCurveConfig(type);
					break;

				case CurveValueType.String:
					config = new StringCurveConfig(type);
					break;

				default:
					throw new InvalidCurveValueTypeException();
			}

			config.DeserializeDefaultValue(reader);

			return config;
		}

		public void Deserialize(IDataReader reader)
		{
			throw new NotSupportedException();
		}

		public void Serialize(IDataWriter writer)
		{
			writer.Write((byte)type);
			writer.Write((byte)valueType);
			SerializeDefaultValue(writer);
		}

		public abstract ICurve CreateCurve();

		public abstract void SerializeDefaultValue(IDataWriter writer);
		public abstract void DeserializeDefaultValue(IDataReader reader);


	}
}
