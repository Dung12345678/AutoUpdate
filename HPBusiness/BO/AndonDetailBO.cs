
using System;
using System.Collections;
using HP.Facade;
using HP.Model;
namespace HP.Business
{

	
	public class AndonDetailBO : BaseBO
	{
		private AndonDetailFacade facade = AndonDetailFacade.Instance;
		protected static AndonDetailBO instance = new AndonDetailBO();

		protected AndonDetailBO()
		{
			this.baseFacade = facade;
		}

		public static AndonDetailBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	