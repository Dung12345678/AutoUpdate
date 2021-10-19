
using System.Collections;
using HP.Model;
namespace HP.Facade
{
	
	public class FormAndFunctionGroupFacade : BaseFacade
	{
		protected static FormAndFunctionGroupFacade instance = new FormAndFunctionGroupFacade(new FormAndFunctionGroupModel());
		protected FormAndFunctionGroupFacade(FormAndFunctionGroupModel model) : base(model)
		{
		}
		public static FormAndFunctionGroupFacade Instance
		{
			get { return instance; }
		}
		protected FormAndFunctionGroupFacade():base() 
		{ 
		} 
	
	}
}
	