
using System.Collections;
using HP.Model;
namespace HP.Facade
{
	
	public class ProductKnifeFacade : BaseFacade
	{
		protected static ProductKnifeFacade instance = new ProductKnifeFacade(new ProductKnifeModel());
		protected ProductKnifeFacade(ProductKnifeModel model) : base(model)
		{
		}
		public static ProductKnifeFacade Instance
		{
			get { return instance; }
		}
		protected ProductKnifeFacade():base() 
		{ 
		} 
	
	}
}
	