using Photon.Realtime;
using VRC;

namespace CitraClient.Modules.Base
{
	public class ModuleBase
	{
		public virtual void Start()
		{
		}

		public virtual void Update()
		{
		}

		public virtual void Quit()
		{
		}

		public virtual void QMLoaded()
		{
		}

		public virtual void VRCJoin(VRC.Player player)
		{
		}

		public virtual void VRCLeave(VRC.Player player)
		{
		}

		public virtual void PhotonJoin(Photon.Realtime.Player player)
		{
		}

		public virtual void PhotonLeave(Photon.Realtime.Player player)
		{
		}

		public virtual void OnSceneLoad(int buildIndex, string sceneName)
		{
		}

		public virtual void GUI()
		{
		}
	}
}
