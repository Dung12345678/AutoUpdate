using BMS.Facade;
namespace BMS.Business
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
