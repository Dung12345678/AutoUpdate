
using System.Collections;
using IE.Model;
namespace IE.Facade
{
	
	public class CheckMotorFacade : BaseFacade
	{
		protected static CheckMotorFacade instance = new CheckMotorFacade(new CheckMotorModel());
		protected CheckMotorFacade(CheckMotorModel model) : base(model)
		{
		}
		public static CheckMotorFacade Instance
		{
			get { return instance; }
		}
		protected CheckMotorFacade():base() 
		{ 
		} 
	
	}
}
	