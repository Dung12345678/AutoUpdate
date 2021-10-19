
using System.Collections;
using HP.Model;
namespace HP.Facade
{
	
	public class CompanyFacade : BaseFacade
	{
		protected static CompanyFacade instance = new CompanyFacade(new CompanyModel());
		protected CompanyFacade(CompanyModel model) : base(model)
		{
		}
		public static CompanyFacade Instance
		{
			get { return instance; }
		}
		protected CompanyFacade():base() 
		{ 
		} 
	
	}
}
	