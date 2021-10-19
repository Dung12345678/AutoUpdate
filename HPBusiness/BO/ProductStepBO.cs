
using System;
using System.Collections;
using HP.Facade;
using HP.Model;
namespace HP.Business
{

	
	public class ProductStepBO : BaseBO
	{
		private ProductStepFacade facade = ProductStepFacade.Instance;
		protected static ProductStepBO instance = new ProductStepBO();

		protected ProductStepBO()
		{
			this.baseFacade = facade;
		}

		public static ProductStepBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	