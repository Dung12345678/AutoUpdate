
using System.Collections;
using HP.Model;
namespace HP.Facade
{
	
	public class ProductCheckForceFacade : BaseFacade
	{
		protected static ProductCheckForceFacade instance = new ProductCheckForceFacade(new ProductCheckForceModel());
		protected ProductCheckForceFacade(ProductCheckForceModel model) : base(model)
		{
		}
		public static ProductCheckForceFacade Instance
		{
			get { return instance; }
		}
		protected ProductCheckForceFacade():base() 
		{ 
		} 
	
	}
}
	