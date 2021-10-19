
using System;
using System.Collections;
using HP.Facade;
using HP.Model;
namespace HP.Business
{
	
	public class ProductKnifeBO : BaseBO
	{
		private ProductKnifeFacade facade = ProductKnifeFacade.Instance;
		protected static ProductKnifeBO instance = new ProductKnifeBO();

		protected ProductKnifeBO()
		{
			this.baseFacade = facade;
		}

		public static ProductKnifeBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	