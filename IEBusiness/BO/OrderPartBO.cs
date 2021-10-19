
using System;
using System.Collections;
using IE.Facade;
using IE.Model;
namespace IE.Business
{
	
	public class OrderPartBO : BaseBO
	{
		private OrderPartFacade facade = OrderPartFacade.Instance;
		protected static OrderPartBO instance = new OrderPartBO();

		protected OrderPartBO()
		{
			this.baseFacade = facade;
		}

		public static OrderPartBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	