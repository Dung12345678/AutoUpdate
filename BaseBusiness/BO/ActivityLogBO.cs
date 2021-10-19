using BMS.Facade;
namespace BMS.Business
{


	public class ActivityLogBO : BaseBO
	{
		private ActivityLogFacade facade = ActivityLogFacade.Instance;
		protected static ActivityLogBO instance = new ActivityLogBO();

		protected ActivityLogBO()
		{
			this.baseFacade = facade;
		}

		public static ActivityLogBO Instance
		{
			get { return instance; }
		}


	}
}
