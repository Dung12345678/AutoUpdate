
using System;
using System.Collections;
using HP.Facade;
using HP.Model;
namespace HP.Business
{
	
	public class KnifeJigWorkingBO : BaseBO
	{
		private KnifeJigWorkingFacade facade = KnifeJigWorkingFacade.Instance;
		protected static KnifeJigWorkingBO instance = new KnifeJigWorkingBO();

		protected KnifeJigWorkingBO()
		{
			this.baseFacade = facade;
		}

		public static KnifeJigWorkingBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	