
using System.Collections;
using HP.Model;
namespace HP.Facade
{
	
	public class ProductCheckHistoryDetailFacade : BaseFacade
	{
		protected static ProductCheckHistoryDetailFacade instance = new ProductCheckHistoryDetailFacade(new ProductCheckHistoryDetailModel());
		protected ProductCheckHistoryDetailFacade(ProductCheckHistoryDetailModel model) : base(model)
		{
		}
		public static ProductCheckHistoryDetailFacade Instance
		{
			get { return instance; }
		}
		protected ProductCheckHistoryDetailFacade():base() 
		{ 
		} 
	
	}
}
	