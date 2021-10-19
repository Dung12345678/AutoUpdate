using BMS.Facade;
namespace BMS.Business
{


	public class DepartmentBO : BaseBO
	{
		private DepartmentFacade facade = DepartmentFacade.Instance;
		protected static DepartmentBO instance = new DepartmentBO();

		protected DepartmentBO()
		{
			this.baseFacade = facade;
		}

		public static DepartmentBO Instance
		{
			get { return instance; }
		}


	}
}
