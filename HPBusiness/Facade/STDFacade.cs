
using System.Collections;
using HP.Model;
namespace HP.Facade
{
	
	public class STDFacade : BaseFacade
	{
		protected static STDFacade instance = new STDFacade(new STDModel());
		protected STDFacade(STDModel model) : base(model)
		{
		}
		public static STDFacade Instance
		{
			get { return instance; }
		}
		protected STDFacade():base() 
		{ 
		} 
	
	}
}
	