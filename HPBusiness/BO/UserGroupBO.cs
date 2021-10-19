
using System;
using System.Collections;
using HP.Facade;
using HP.Model;
namespace HP.Business
{

	
	public class UserGroupBO : BaseBO
	{
		private UserGroupFacade facade = UserGroupFacade.Instance;
		protected static UserGroupBO instance = new UserGroupBO();

		protected UserGroupBO()
		{
			this.baseFacade = facade;
		}

		public static UserGroupBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	