
using System.Collections;
using HP.Model;
namespace HP.Facade
{
	
	public class KnifeDetailListFacade : BaseFacade
	{
		protected static KnifeDetailListFacade instance = new KnifeDetailListFacade(new KnifeDetailListModel());
		protected KnifeDetailListFacade(KnifeDetailListModel model) : base(model)
		{
		}
		public static KnifeDetailListFacade Instance
		{
			get { return instance; }
		}
		protected KnifeDetailListFacade():base() 
		{ 
		} 
	
	}
}
	