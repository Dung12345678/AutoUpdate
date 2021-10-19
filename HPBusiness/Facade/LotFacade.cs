
using System.Collections;
using HP.Model;
namespace HP.Facade
{
	
	public class LotFacade : BaseFacade
	{
		protected static LotFacade instance = new LotFacade(new LotModel());
		protected LotFacade(LotModel model) : base(model)
		{
		}
		public static LotFacade Instance
		{
			get { return instance; }
		}
		protected LotFacade():base() 
		{ 
		} 
	
	}
}
	