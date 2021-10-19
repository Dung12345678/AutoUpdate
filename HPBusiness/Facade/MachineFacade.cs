
using System.Collections;
using HP.Model;
namespace HP.Facade
{
	
	public class MachineFacade : BaseFacade
	{
		protected static MachineFacade instance = new MachineFacade(new MachineModel());
		protected MachineFacade(MachineModel model) : base(model)
		{
		}
		public static MachineFacade Instance
		{
			get { return instance; }
		}
		protected MachineFacade():base() 
		{ 
		} 
	
	}
}
	