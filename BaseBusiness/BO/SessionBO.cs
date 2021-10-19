using BMS.Facade;
namespace BMS.Business
{


	public class SessionBO : BaseBO
	{
		private SessionFacade facade = SessionFacade.Instance;
		protected static SessionBO instance = new SessionBO();

		protected SessionBO()
		{
			this.baseFacade = facade;
		}

		public static SessionBO Instance
		{
			get { return instance; }
		}


	}
}
