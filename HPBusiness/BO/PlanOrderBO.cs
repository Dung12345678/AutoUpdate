
using System;
using System.Collections;
using HP.Facade;
using HP.Model;
namespace HP.Business
{

	
	public class PlanOrderBO : BaseBO
	{
		private PlanOrderFacade facade = PlanOrderFacade.Instance;
		protected static PlanOrderBO instance = new PlanOrderBO();

		protected PlanOrderBO()
		{
			this.baseFacade = facade;
		}

		public static PlanOrderBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	