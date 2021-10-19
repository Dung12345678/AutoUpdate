
using System;
using System.Collections;
using HP.Facade;
using HP.Model;
namespace HP.Business
{

	
	public class PartSonBO : BaseBO
	{
		private PartSonFacade facade = PartSonFacade.Instance;
		protected static PartSonBO instance = new PartSonBO();

		protected PartSonBO()
		{
			this.baseFacade = facade;
		}

		public static PartSonBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	