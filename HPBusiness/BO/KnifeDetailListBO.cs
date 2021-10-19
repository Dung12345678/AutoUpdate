
using System;
using System.Collections;
using HP.Facade;
using HP.Model;
namespace HP.Business
{
	
	public class KnifeDetailListBO : BaseBO
	{
		private KnifeDetailListFacade facade = KnifeDetailListFacade.Instance;
		protected static KnifeDetailListBO instance = new KnifeDetailListBO();

		protected KnifeDetailListBO()
		{
			this.baseFacade = facade;
		}

		public static KnifeDetailListBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	