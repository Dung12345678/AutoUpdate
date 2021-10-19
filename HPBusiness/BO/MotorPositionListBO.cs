
using System;
using System.Collections;
using HP.Facade;
using HP.Model;
namespace HP.Business
{

	
	public class MotorPositionListBO : BaseBO
	{
		private MotorPositionListFacade facade = MotorPositionListFacade.Instance;
		protected static MotorPositionListBO instance = new MotorPositionListBO();

		protected MotorPositionListBO()
		{
			this.baseFacade = facade;
		}

		public static MotorPositionListBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	