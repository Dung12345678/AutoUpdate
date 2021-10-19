using BMS.Facade;
namespace BMS.Business
{


	public class ProductBO : BaseBO
	{
		private ProductFacade facade = ProductFacade.Instance;
		protected static ProductBO instance = new ProductBO();

		protected ProductBO()
		{
			this.baseFacade = facade;
		}

		public static ProductBO Instance
		{
			get { return instance; }
		}


	}
}
