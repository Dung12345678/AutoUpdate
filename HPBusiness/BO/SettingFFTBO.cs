
using System;
using System.Collections;
using HP.Facade;
using HP.Model;
namespace HP.Business
{

	
	public class SettingFFTBO : BaseBO
	{
		private SettingFFTFacade facade = SettingFFTFacade.Instance;
		protected static SettingFFTBO instance = new SettingFFTBO();

		protected SettingFFTBO()
		{
			this.baseFacade = facade;
		}

		public static SettingFFTBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	