using System;
using System.Collections;
using HP.BO;
using HP.Exceptions;
using HP.Utils;
using HP.Facade;
using HP.Model;
namespace HP.Business
{

	
	public class EventsLogBO : BaseBO
	{
		private EventsLogFacade facade = EventsLogFacade.Instance;
		protected static EventsLogBO instance = new EventsLogBO();

		protected EventsLogBO()
		{
			this.baseFacade = facade;
		}

		public static EventsLogBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	