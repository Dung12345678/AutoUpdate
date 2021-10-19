
using System;
using System.Collections;
using HP.Facade;
using HP.Model;
namespace HP.Business
{
	
	public class STDBO : BaseBO
	{
		private STDFacade facade = STDFacade.Instance;
		protected static STDBO instance = new STDBO();

		protected STDBO()
		{
			this.baseFacade = facade;
		}

		public static STDBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	