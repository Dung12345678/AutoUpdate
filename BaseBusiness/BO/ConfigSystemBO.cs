using BMS.Facade;
namespace BMS.Business
{


	public class ConfigSystemBO : BaseBO
	{
		private ConfigSystemFacade facade = ConfigSystemFacade.Instance;
		protected static ConfigSystemBO instance = new ConfigSystemBO();

		protected ConfigSystemBO()
		{
			this.baseFacade = facade;
		}

		public static ConfigSystemBO Instance
		{
			get { return instance; }
		}


	}
}
