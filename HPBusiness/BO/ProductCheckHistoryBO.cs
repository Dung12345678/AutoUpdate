
using System;
using System.Collections;
using HP.Facade;
using HP.Model;
namespace HP.Business
{

	
	public class ProductCheckHistoryBO : BaseBO
	{
		private ProductCheckHistoryFacade facade = ProductCheckHistoryFacade.Instance;
		protected static ProductCheckHistoryBO instance = new ProductCheckHistoryBO();

		protected ProductCheckHistoryBO()
		{
			this.baseFacade = facade;
		}

		public static ProductCheckHistoryBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	