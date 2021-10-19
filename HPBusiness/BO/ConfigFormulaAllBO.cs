
using System;
using System.Collections;
using HP.Facade;
using HP.Model;
namespace HP.Business
{
	
	public class ConfigFormulaAllBO : BaseBO
	{
		private ConfigFormulaAllFacade facade = ConfigFormulaAllFacade.Instance;
		protected static ConfigFormulaAllBO instance = new ConfigFormulaAllBO();

		protected ConfigFormulaAllBO()
		{
			this.baseFacade = facade;
		}

		public static ConfigFormulaAllBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	