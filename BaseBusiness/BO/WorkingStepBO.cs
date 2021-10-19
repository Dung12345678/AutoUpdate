using BMS.Facade;
namespace BMS.Business
{


	public class WorkingStepBO : BaseBO
	{
		private WorkingStepFacade facade = WorkingStepFacade.Instance;
		protected static WorkingStepBO instance = new WorkingStepBO();

		protected WorkingStepBO()
		{
			this.baseFacade = facade;
		}

		public static WorkingStepBO Instance
		{
			get { return instance; }
		}


	}
}
