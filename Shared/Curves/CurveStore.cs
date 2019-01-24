using Bombardel.CurveNet.Shared.Serialization;
using Bombardel.CurveNet.Shared.Server;
using Bombardel.CurveNet.Shared.ServerMessages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bombardel.CurveNet.Shared.Curves
{

	public class CurveStore : ICurveNetworkClient
	{
		public float Time => _timelineStore.Time;


		private TimelineStore _timelineStore = new TimelineStore();

		private Dictionary<NetworkedId, Id> _networkedIdToInternalId = new Dictionary<NetworkedId, Id>();
		private IdStore _internalCurveIdStore = new IdStore();

		private ICurveNetworkServer _networkInterface;


		public CurveStore(ICurveNetworkServer networkInterface)
		{
			_networkInterface = networkInterface;
		}

		public Id RegisterLocalCurve<T>(IKeyframeValueListener<T> listener, T initialValue, IInterpolator<T> interpolator) where T : IKeyframeValue<T>, new()
		{
			// generate a unique id for the curve internally, that will NOT be used for any outside
			// communication.
			Id curveId = _internalCurveIdStore.GenerateId();
			_timelineStore.CreateTimeline(curveId, listener, initialValue, interpolator);

			// Since this is a new curve, we don't have a matching remote id yet to map to the internal id.
			// We therefore generate a local placeholder id that will be sent to the server to ask for
			// the final remote id.
			NetworkedId localId = new NetworkedId(RemoteStatus.Local, curveId);
			_networkedIdToInternalId.Add(localId, curveId);
			return curveId;
		}

		public void RegisterRemoteCurve<T>(Id remoteId, IKeyframeValueListener<T> listener, T initialValue, IInterpolator<T> interpolator) where T : IKeyframeValue<T>, new()
		{
			// generate a unique id for the curve internally, that will NOT be used for any outside
			// communication.
			Id curveId = _internalCurveIdStore.GenerateId();
			_timelineStore.CreateTimeline(curveId, listener, initialValue, interpolator);

			// Since this is a remote curve, we create a remote id and map it onto our internal id.
			NetworkedId remoteNetworkedId = new NetworkedId(RemoteStatus.Remote, remoteId);
			_networkedIdToInternalId.Add(remoteNetworkedId, curveId);
		}
		
		public void AddKeyframeToRemoteCurve(Id remoteId, KeyframeData keyframe)
		{
			// Find the curve id by mapping the remote id.
			NetworkedId networkedId = new NetworkedId(RemoteStatus.Remote, remoteId);

			// add the keyframe to the right curve.
			AddKeyframe(networkedId, keyframe);
		}

		public void SubmitKeyframeToServer(Id localId, KeyframeData keyframe)
		{
			// generate the networked id to find the curve internally.
			NetworkedId networkedId = new NetworkedId(RemoteStatus.Local, localId);
			AddKeyframe(networkedId, keyframe);

			// send over the network to the server so that all the others will receive it as well
			_networkInterface.SubmitLocalKeyframeToServer(localId, keyframe);
		}

		private void AddKeyframe(NetworkedId networkedId, KeyframeData keyframe)
		{
			if (!_networkedIdToInternalId.ContainsKey(networkedId)) throw new ArgumentException("No curve found with id " + networkedId);
			Id curveId = _networkedIdToInternalId[networkedId];

			// add the keyframe to the timeline
			_timelineStore.AddKeyframe(curveId, keyframe);
		}

		public void SetTime(float time)
		{
			_timelineStore.SetTime(time);
		}

		public void AdvanceTime(float dt)
		{
			_timelineStore.AdvanceTime(dt);
		}
	}
}
