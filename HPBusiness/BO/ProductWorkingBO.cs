
using System;
using System.Collections;
using HP.Facade;
using HP.Model;
namespace HP.Business
{

	
	public class ProductWorkingBO : BaseBO
	{
		private ProductWorkingFacade facade = ProductWorkingFacade.Instance;
		protected static ProductWorkingBO instance = new ProductWorkingBO();

		protected ProductWorkingBO()
		{
			this.baseFacade = facade;
		}

		public static ProductWorkingBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	