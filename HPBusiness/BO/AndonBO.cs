
using System;
using System.Collections;
using HP.Facade;
using HP.Model;
namespace HP.Business
{

	
	public class AndonBO : BaseBO
	{
		private AndonFacade facade = AndonFacade.Instance;
		protected static AndonBO instance = new AndonBO();

		protected AndonBO()
		{
			this.baseFacade = facade;
		}

		public static AndonBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	