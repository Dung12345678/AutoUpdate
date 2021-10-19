
using System;
using System.Collections;
using HP.Facade;
using HP.Model;
namespace HP.Business
{
	
	public class AreaBO : BaseBO
	{
		private AreaFacade facade = AreaFacade.Instance;
		protected static AreaBO instance = new AreaBO();

		protected AreaBO()
		{
			this.baseFacade = facade;
		}

		public static AreaBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	