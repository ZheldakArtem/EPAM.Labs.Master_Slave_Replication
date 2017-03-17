using Microsoft.Practices.Unity;
using ReplicationAPI.Interfaces;
using ReplicationAPI.Services;

namespace ReplicationAPI
{
	public static class UnityConfig
	{
		public static IUnityContainer BuildUnityContainer()
		{
			var container = new UnityContainer();
			container.RegisterType<IMasterService, MasterService>();

			return container;
		}
	}
}