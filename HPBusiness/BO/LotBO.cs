
using System;
using System.Collections;
using HP.Facade;
using HP.Model;
namespace HP.Business
{
	
	public class LotBO : BaseBO
	{
		private LotFacade facade = LotFacade.Instance;
		protected static LotBO instance = new LotBO();

		protected LotBO()
		{
			this.baseFacade = facade;
		}

		public static LotBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	