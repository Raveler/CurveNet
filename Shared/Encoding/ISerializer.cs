namespace Bombardel.CurveNet.Server.Sessions
{

	public interface ISerializer<T>
	{

		byte[] Serialize(T obj);

		void Deserialize(byte[] bytes, int offset, int length);
	}
}
