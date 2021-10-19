
using System;
using System.Collections;
using HP.Facade;
using HP.Model;
namespace HP.Business
{
	
	public class StageWorkerBO : BaseBO
	{
		private StageWorkerFacade facade = StageWorkerFacade.Instance;
		protected static StageWorkerBO instance = new StageWorkerBO();

		protected StageWorkerBO()
		{
			this.baseFacade = facade;
		}

		public static StageWorkerBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	