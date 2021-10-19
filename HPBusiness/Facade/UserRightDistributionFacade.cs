
using System.Collections;
using HP.Model;
namespace HP.Facade
{
	
	public class UserRightDistributionFacade : BaseFacade
	{
		protected static UserRightDistributionFacade instance = new UserRightDistributionFacade(new UserRightDistributionModel());
		protected UserRightDistributionFacade(UserRightDistributionModel model) : base(model)
		{
		}
		public static UserRightDistributionFacade Instance
		{
			get { return instance; }
		}
		protected UserRightDistributionFacade():base() 
		{ 
		} 
	
	}
}
	