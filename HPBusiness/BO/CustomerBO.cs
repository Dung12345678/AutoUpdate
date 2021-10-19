
using System;
using System.Collections;
using HP.Facade;
using HP.Model;
namespace HP.Business
{

	
	public class CustomerBO : BaseBO
	{
		private CustomerFacade facade = CustomerFacade.Instance;
		protected static CustomerBO instance = new CustomerBO();

		protected CustomerBO()
		{
			this.baseFacade = facade;
		}

		public static CustomerBO Instance
		{
			get { return instance; }
		}
		
	    
	}
}
	