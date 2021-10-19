
using System;
namespace HP.Model
{
	public class LotModel : BaseModel
	{
		private int iD;
		private string stepCode;
		private string articleID;
		private string orderMachining;
		private DateTime? createDate;
		private DateTime? updateDate;
		private DateTime? jGDate;
		private string worker;
		private string hM;
		public int ID
		{
			get { return iD; }
			set { iD = value; }
		}
	
		public string StepCode
		{
			get { return stepCode; }
			set { stepCode = value; }
		}
	
		public string ArticleID
		{
			get { return articleID; }
			set { articleID = value; }
		}
	
		public string OrderMachining
		{
			get { return orderMachining; }
			set { orderMachining = value; }
		}
	
		public DateTime? CreateDate
		{
			get { return createDate; }
			set { createDate = value; }
		}
	
		public DateTime? UpdateDate
		{
			get { return updateDate; }
			set { updateDate = value; }
		}
	
		public DateTime? JGDate
		{
			get { return jGDate; }
			set { jGDate = value; }
		}
	
		public string Worker
		{
			get { return worker; }
			set { worker = value; }
		}
	
		public string HM
		{
			get { return hM; }
			set { hM = value; }
		}
	
	}
}
	