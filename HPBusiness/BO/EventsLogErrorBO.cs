
using System;
using System.Collections;
using HP.Exceptions;
using HP.Utils;
using HP.Facade;
using HP.Model;
namespace HP.Business
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
	