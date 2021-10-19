
using System.Collections;
using HP.Model;
namespace HP.Facade
{
	
	public class AndonConfigFacade : BaseFacade
	{
		protected static AndonConfigFacade instance = new AndonConfigFacade(new AndonConfigModel());
		protected AndonConfigFacade(AndonConfigModel model) : base(model)
		{
		}
		public static AndonConfigFacade Instance
		{
			get { return instance; }
		}
		protected AndonConfigFacade():base() 
		{ 
		} 
	
	}
}
	