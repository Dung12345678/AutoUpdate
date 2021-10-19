using BMS.Facade;
namespace BMS.Business
{


	public class UserGroupRightDistributionBO : BaseBO
	{
		private UserGroupRightDistributionFacade facade = UserGroupRightDistributionFacade.Instance;
		protected static UserGroupRightDistributionBO instance = new UserGroupRightDistributionBO();

		protected UserGroupRightDistributionBO()
		{
			this.baseFacade = facade;
		}

		public static UserGroupRightDistributionBO Instance
		{
			get { return instance; }
		}


	}
}
