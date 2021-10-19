
using System.Collections;
using HP.Model;
namespace HP.Facade
{
	
	public class KnifeProcessedListFacade : BaseFacade
	{
		protected static KnifeProcessedListFacade instance = new KnifeProcessedListFacade(new KnifeProcessedListModel());
		protected KnifeProcessedListFacade(KnifeProcessedListModel model) : base(model)
		{
		}
		public static KnifeProcessedListFacade Instance
		{
			get { return instance; }
		}
		protected KnifeProcessedListFacade():base() 
		{ 
		} 
	
	}
}
	