


using BMS.Facade;
namespace BMS.Business
{


	public class ShorcutKeyBO : BaseBO
	{
		private ShorcutKeyFacade facade = ShorcutKeyFacade.Instance;
		protected static ShorcutKeyBO instance = new ShorcutKeyBO();

		protected ShorcutKeyBO()
		{
			this.baseFacade = facade;
		}

		public static ShorcutKeyBO Instance
		{
			get { return instance; }
		}


	}
}
