using Bombardel.CurveNet.Shared.Client;
using Bombardel.CurveNet.Shared.Curves;
using Bombardel.CurveNet.Shared.Serialization;
using Bombardel.CurveNet.Shared.Server;
using Bombardel.CurveNet.Shared.ServerMessages;
using System.Collections.Generic;
using System.Linq;

namespace Bombardel.CurveNet.Shared.Objects
{

	public class Object
	{
		public Id Id => _id;
		public Id Owner => _owner;


		private Id _id;
		private Id _owner;
		private NewObjectConfig _config;

		private List<ICurve> _curves = new List<ICurve>();



		public Object(NewObjectConfig config, Id id, Id owner)
		{
			_id = id;
			_owner = owner;
			_config = config;
		}

		public ObjectData GetData()
		{
			return new ObjectData()
			{
				id = _id,
				owner = _owner,
				curves = _config.curves,
			};
		}
	}
}
