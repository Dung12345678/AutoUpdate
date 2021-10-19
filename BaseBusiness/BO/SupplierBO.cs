using BMS.Facade;
namespace BMS.Business
{


	public class SupplierBO : BaseBO
	{
		private SupplierFacade facade = SupplierFacade.Instance;
		protected static SupplierBO instance = new SupplierBO();

		protected SupplierBO()
		{
			this.baseFacade = facade;
		}

		public static SupplierBO Instance
		{
			get { return instance; }
		}


	}
}
