
using System;
using System.Collections;
using HP.Facade;
using HP.Model;
namespace HP.Business
{
	
	public class JigDetailBO : BaseBO
	{
		private JigDetailFacade facade = JigDetailFacade.Instance;
		protected static JigDetailBO instance = new JigDetailBO();

		protected JigDetailBO()
		{
			this.baseFacade = facade;
		}

		public static JigDetailBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	