
using System;
using System.Collections;
using HP.Facade;
using HP.Model;
namespace HP.Business
{

	
	public class SONHistoryImExBO : BaseBO
	{
		private SONHistoryImExFacade facade = SONHistoryImExFacade.Instance;
		protected static SONHistoryImExBO instance = new SONHistoryImExBO();

		protected SONHistoryImExBO()
		{
			this.baseFacade = facade;
		}

		public static SONHistoryImExBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	