
using System.Collections;
using HP.Model;
namespace HP.Facade
{
	
	public class StageFacade : BaseFacade
	{
		protected static StageFacade instance = new StageFacade(new StageModel());
		protected StageFacade(StageModel model) : base(model)
		{
		}
		public static StageFacade Instance
		{
			get { return instance; }
		}
		protected StageFacade():base() 
		{ 
		} 
	
	}
}
	