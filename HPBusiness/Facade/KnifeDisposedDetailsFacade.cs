
using System.Collections;
using HP.Model;
namespace HP.Facade
{
	
	public class KnifeDisposedDetailsFacade : BaseFacade
	{
		protected static KnifeDisposedDetailsFacade instance = new KnifeDisposedDetailsFacade(new KnifeDisposedDetailsModel());
		protected KnifeDisposedDetailsFacade(KnifeDisposedDetailsModel model) : base(model)
		{
		}
		public static KnifeDisposedDetailsFacade Instance
		{
			get { return instance; }
		}
		protected KnifeDisposedDetailsFacade():base() 
		{ 
		} 
	
	}
}
	