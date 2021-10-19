
using System.Collections;
using HP.Model;
namespace HP.Facade
{
	
	public class KnifeSharpeningDetailsFacade : BaseFacade
	{
		protected static KnifeSharpeningDetailsFacade instance = new KnifeSharpeningDetailsFacade(new KnifeSharpeningDetailsModel());
		protected KnifeSharpeningDetailsFacade(KnifeSharpeningDetailsModel model) : base(model)
		{
		}
		public static KnifeSharpeningDetailsFacade Instance
		{
			get { return instance; }
		}
		protected KnifeSharpeningDetailsFacade():base() 
		{ 
		} 
	
	}
}
	