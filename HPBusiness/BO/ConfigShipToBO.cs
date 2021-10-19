
using System;
using System.Collections;
using HP.Facade;
using HP.Model;
namespace HP.Business
{
	
	public class ConfigShipToBO : BaseBO
	{
		private ConfigShipToFacade facade = ConfigShipToFacade.Instance;
		protected static ConfigShipToBO instance = new ConfigShipToBO();

		protected ConfigShipToBO()
		{
			this.baseFacade = facade;
		}

		public static ConfigShipToBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	