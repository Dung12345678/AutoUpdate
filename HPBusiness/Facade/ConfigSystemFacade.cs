
using System.Collections;
using HP.Model;
namespace HP.Facade
{
	
	public class ConfigSystemFacade : BaseFacade
	{
		protected static ConfigSystemFacade instance = new ConfigSystemFacade(new ConfigSystemModel());
		protected ConfigSystemFacade(ConfigSystemModel model) : base(model)
		{
		}
		public static ConfigSystemFacade Instance
		{
			get { return instance; }
		}
		protected ConfigSystemFacade():base() 
		{ 
		} 
	
	}
}
	