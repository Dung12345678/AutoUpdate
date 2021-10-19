
using System;
using System.Collections;
using IE.Facade;
using IE.Model;
namespace IE.Business
{
	
	public class CheckMotorBO : BaseBO
	{
		private CheckMotorFacade facade = CheckMotorFacade.Instance;
		protected static CheckMotorBO instance = new CheckMotorBO();

		protected CheckMotorBO()
		{
			this.baseFacade = facade;
		}

		public static CheckMotorBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	