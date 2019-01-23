namespace Bombardel.CurveNet.Shared.ServerMessages
{
	public class ProtocolErrorMessage
	{
		public ProtocolError error;

		public string errorDescription
		{
			get
			{
				return error.GetDescription();
			}
		}
	}
}
