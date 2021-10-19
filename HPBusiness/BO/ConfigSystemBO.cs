
using System;
using System.Collections;
using HP.Facade;
using HP.Model;
namespace HP.Business
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
	