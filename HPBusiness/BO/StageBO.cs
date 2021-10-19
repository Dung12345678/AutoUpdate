
using System;
using System.Collections;
using HP.Facade;
using HP.Model;
namespace HP.Business
{
	
	public class StageBO : BaseBO
	{
		private StageFacade facade = StageFacade.Instance;
		protected static StageBO instance = new StageBO();

		protected StageBO()
		{
			this.baseFacade = facade;
		}

		public static StageBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	