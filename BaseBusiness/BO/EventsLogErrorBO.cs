using BMS.Facade;
namespace BMS.Business
{


	public class EventsLogErrorBO : BaseBO
	{
		private EventsLogErrorFacade facade = EventsLogErrorFacade.Instance;
		protected static EventsLogErrorBO instance = new EventsLogErrorBO();

		protected EventsLogErrorBO()
		{
			this.baseFacade = facade;
		}

		public static EventsLogErrorBO Instance
		{
			get { return instance; }
		}


	}
}
