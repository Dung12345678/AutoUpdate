
using System;
using System.Collections;
using HP.Facade;
using HP.Model;
namespace HP.Business
{
	
	public class KnifeSharpeningDetailsBO : BaseBO
	{
		private KnifeSharpeningDetailsFacade facade = KnifeSharpeningDetailsFacade.Instance;
		protected static KnifeSharpeningDetailsBO instance = new KnifeSharpeningDetailsBO();

		protected KnifeSharpeningDetailsBO()
		{
			this.baseFacade = facade;
		}

		public static KnifeSharpeningDetailsBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	