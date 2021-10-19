
using System;
using System.Collections;
using HP.Facade;
using HP.Model;
namespace HP.Business
{

	
	public class GearBO : BaseBO
	{
		private GearFacade facade = GearFacade.Instance;
		protected static GearBO instance = new GearBO();

		protected GearBO()
		{
			this.baseFacade = facade;
		}

		public static GearBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	