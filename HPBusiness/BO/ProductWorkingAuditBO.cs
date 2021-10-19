
using System;
using System.Collections;
using HP.Facade;
using HP.Model;
namespace HP.Business
{
	
	public class ProductWorkingAuditBO : BaseBO
	{
		private ProductWorkingAuditFacade facade = ProductWorkingAuditFacade.Instance;
		protected static ProductWorkingAuditBO instance = new ProductWorkingAuditBO();

		protected ProductWorkingAuditBO()
		{
			this.baseFacade = facade;
		}

		public static ProductWorkingAuditBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	