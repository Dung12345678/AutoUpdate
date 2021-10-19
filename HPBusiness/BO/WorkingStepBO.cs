
using System;
using System.Collections;
using HP.Facade;
using HP.Model;
namespace HP.Business
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
	