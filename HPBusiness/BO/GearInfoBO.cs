
using System;
using System.Collections;
using HP.Facade;
using HP.Model;
namespace HP.Business
{

	
	public class GearInfoBO : BaseBO
	{
		private GearInfoFacade facade = GearInfoFacade.Instance;
		protected static GearInfoBO instance = new GearInfoBO();

		protected GearInfoBO()
		{
			this.baseFacade = facade;
		}

		public static GearInfoBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	