
using System;
using System.Collections;
using HP.BO;
using HP.Exceptions;
using HP.Utils;
using HP.Facade;
using HP.Model;
namespace HP.Business
{

	
	public class ActivityLogBO : BaseBO
	{
		private ActivityLogFacade facade = ActivityLogFacade.Instance;
		protected static ActivityLogBO instance = new ActivityLogBO();

		protected ActivityLogBO()
		{
			this.baseFacade = facade;
		}

		public static ActivityLogBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	