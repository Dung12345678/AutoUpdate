
using System;
using System.Collections;
using HP.Facade;
using HP.Model;
namespace HP.Business
{

	
	public class ProductGroupBO : BaseBO
	{
		private ProductGroupFacade facade = ProductGroupFacade.Instance;
		protected static ProductGroupBO instance = new ProductGroupBO();

		protected ProductGroupBO()
		{
			this.baseFacade = facade;
		}

		public static ProductGroupBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	