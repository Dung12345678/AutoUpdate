
using System;
using System.Collections;
using HP.Facade;
using HP.Model;
namespace HP.Business
{

	
	public class ProductCheckHistoryDetailBO : BaseBO
	{
		private ProductCheckHistoryDetailFacade facade = ProductCheckHistoryDetailFacade.Instance;
		protected static ProductCheckHistoryDetailBO instance = new ProductCheckHistoryDetailBO();

		protected ProductCheckHistoryDetailBO()
		{
			this.baseFacade = facade;
		}

		public static ProductCheckHistoryDetailBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	