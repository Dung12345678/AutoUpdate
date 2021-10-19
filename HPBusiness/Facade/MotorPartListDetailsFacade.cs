
using System.Collections;
using HP.Model;
namespace HP.Facade
{
	
	public class MotorPartListDetailsFacade : BaseFacade
	{
		protected static MotorPartListDetailsFacade instance = new MotorPartListDetailsFacade(new MotorPartListDetailsModel());
		protected MotorPartListDetailsFacade(MotorPartListDetailsModel model) : base(model)
		{
		}
		public static MotorPartListDetailsFacade Instance
		{
			get { return instance; }
		}
		protected MotorPartListDetailsFacade():base() 
		{ 
		} 
	
	}
}
	