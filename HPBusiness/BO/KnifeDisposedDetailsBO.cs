
using System;
using System.Collections;
using HP.Facade;
using HP.Model;
namespace HP.Business
{
	
	public class KnifeDisposedDetailsBO : BaseBO
	{
		private KnifeDisposedDetailsFacade facade = KnifeDisposedDetailsFacade.Instance;
		protected static KnifeDisposedDetailsBO instance = new KnifeDisposedDetailsBO();

		protected KnifeDisposedDetailsBO()
		{
			this.baseFacade = facade;
		}

		public static KnifeDisposedDetailsBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	