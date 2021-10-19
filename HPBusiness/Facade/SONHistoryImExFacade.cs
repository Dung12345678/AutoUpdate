
using System.Collections;
using HP.Model;
namespace HP.Facade
{
	
	public class SONHistoryImExFacade : BaseFacade
	{
		protected static SONHistoryImExFacade instance = new SONHistoryImExFacade(new SONHistoryImExModel());
		protected SONHistoryImExFacade(SONHistoryImExModel model) : base(model)
		{
		}
		public static SONHistoryImExFacade Instance
		{
			get { return instance; }
		}
		protected SONHistoryImExFacade():base() 
		{ 
		} 
	
	}
}
	