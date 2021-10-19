
using System.Collections;
using IE.Model;
namespace IE.Facade
{
	
	public class OrderPartFacade : BaseFacade
	{
		protected static OrderPartFacade instance = new OrderPartFacade(new OrderPartModel());
		protected OrderPartFacade(OrderPartModel model) : base(model)
		{
		}
		public static OrderPartFacade Instance
		{
			get { return instance; }
		}
		protected OrderPartFacade():base() 
		{ 
		} 
	
	}
}
	