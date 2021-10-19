
using System;
using System.Collections;
using HP.Facade;
using HP.Model;
namespace HP.Business
{

	
	public class MotorPartListBO : BaseBO
	{
		private MotorPartListFacade facade = MotorPartListFacade.Instance;
		protected static MotorPartListBO instance = new MotorPartListBO();

		protected MotorPartListBO()
		{
			this.baseFacade = facade;
		}

		public static MotorPartListBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	