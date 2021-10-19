
using System;
namespace HP.Model
{
	public class KnifeDisposedDetailsModel : BaseModel
	{
		private int iD;
		private int knifeID;
		private string knifeCode;
		private DateTime? dateDisposed;
		private int workerID;
		public int ID
		{
			get { return iD; }
			set { iD = value; }
		}
	
		public int KnifeID
		{
			get { return knifeID; }
			set { knifeID = value; }
		}
	
		public string KnifeCode
		{
			get { return knifeCode; }
			set { knifeCode = value; }
		}
	
		public DateTime? DateDisposed
		{
			get { return dateDisposed; }
			set { dateDisposed = value; }
		}
	
		public int WorkerID
		{
			get { return workerID; }
			set { workerID = value; }
		}
	
	}
}
	