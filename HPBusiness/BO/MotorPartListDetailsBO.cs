
using System;
using System.Collections;
using HP.Facade;
using HP.Model;
namespace HP.Business
{

	
	public class MotorPartListDetailsBO : BaseBO
	{
		private MotorPartListDetailsFacade facade = MotorPartListDetailsFacade.Instance;
		protected static MotorPartListDetailsBO instance = new MotorPartListDetailsBO();

		protected MotorPartListDetailsBO()
		{
			this.baseFacade = facade;
		}

		public static MotorPartListDetailsBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	