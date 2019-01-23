namespace Bombardel.CurveNet.Server.Sessions
{

	public interface IGenericSerializer
	{

		byte[] Serialize(object obj);

		void Deserialize(byte[] bytes, int offset, int count);
	}
}
