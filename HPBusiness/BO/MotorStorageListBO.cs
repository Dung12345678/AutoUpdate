
using System;
using System.Collections;
using HP.Facade;
using HP.Model;
namespace HP.Business
{

	
	public class MotorStorageListBO : BaseBO
	{
		private MotorStorageListFacade facade = MotorStorageListFacade.Instance;
		protected static MotorStorageListBO instance = new MotorStorageListBO();

		protected MotorStorageListBO()
		{
			this.baseFacade = facade;
		}

		public static MotorStorageListBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	