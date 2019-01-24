using System;
using System.Collections.Generic;
using System.IO;

namespace Bombardel.CurveNet.Shared.Serialization
{

	public struct NetworkedId : IEquatable<NetworkedId>, IComparable<NetworkedId>
	{
		private RemoteStatus _remoteStatus;
		private Id _id;


		public NetworkedId(RemoteStatus status, Id id)
		{
			_remoteStatus = status;
			_id = id;
		}
		
		public int CompareTo(NetworkedId other)
		{
			if (_remoteStatus == other._remoteStatus) return _id.CompareTo(other._id);
			return _remoteStatus.CompareTo(other._remoteStatus);
		}

		public bool Equals(NetworkedId other)
		{
			return _remoteStatus == other._remoteStatus && _id == other._id;
		}

		public override bool Equals(object obj)
		{
			if (!(obj is NetworkedId)) return false;
			return ((NetworkedId)obj).Equals(this);
		}

		public override int GetHashCode()
		{
			if (_remoteStatus == RemoteStatus.Local) return _id.GetHashCode();
			else return -_id.GetHashCode()-1;
		}

		public override string ToString()
		{
			return _remoteStatus.ToString() + " " + _id.ToString();
		}
	}
}
