
using System.Collections;
using HP.Model;
namespace HP.Facade
{
	
	public class PlanOrderFacade : BaseFacade
	{
		protected static PlanOrderFacade instance = new PlanOrderFacade(new PlanOrderModel());
		protected PlanOrderFacade(PlanOrderModel model) : base(model)
		{
		}
		public static PlanOrderFacade Instance
		{
			get { return instance; }
		}
		protected PlanOrderFacade():base() 
		{ 
		} 
	
	}
}
	