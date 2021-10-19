
using System;
using System.Collections;
using HP.Facade;
using HP.Model;
namespace HP.Business
{
	
	public class ConfigFormulaBO : BaseBO
	{
		
		private ConfigFormulaFacade facade = ConfigFormulaFacade.Instance;
		protected static ConfigFormulaBO instance = new ConfigFormulaBO();

		protected ConfigFormulaBO()
		{
			this.baseFacade = facade;
		}

		public static ConfigFormulaBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	