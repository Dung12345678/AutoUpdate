
using System.Collections;
using HP.Model;
namespace HP.Facade
{
	
	public class JigDetailFacade : BaseFacade
	{
		protected static JigDetailFacade instance = new JigDetailFacade(new JigDetailModel());
		protected JigDetailFacade(JigDetailModel model) : base(model)
		{
		}
		public static JigDetailFacade Instance
		{
			get { return instance; }
		}
		protected JigDetailFacade():base() 
		{ 
		} 
	
	}
}
	