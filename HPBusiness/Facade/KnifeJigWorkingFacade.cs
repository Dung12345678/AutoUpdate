
using System.Collections;
using HP.Model;
namespace HP.Facade
{
	
	public class KnifeJigWorkingFacade : BaseFacade
	{
		protected static KnifeJigWorkingFacade instance = new KnifeJigWorkingFacade(new KnifeJigWorkingModel());
		protected KnifeJigWorkingFacade(KnifeJigWorkingModel model) : base(model)
		{
		}
		public static KnifeJigWorkingFacade Instance
		{
			get { return instance; }
		}
		protected KnifeJigWorkingFacade():base() 
		{ 
		} 
	
	}
}
	