
using System;
namespace HP.Model
{
	public class JigDetailModel : BaseModel
	{
		private int iD;
		private int parentID;
		private string jigCode;
		private string jigName;
		private string description;
		private DateTime? createDate;
		private DateTime? updateDate;
		private string workingName;
		private string part;
		private int qty;
		public int ID
		{
			get { return iD; }
			set { iD = value; }
		}
	
		public int ParentID
		{
			get { return parentID; }
			set { parentID = value; }
		}
	
		public string JigCode
		{
			get { return jigCode; }
			set { jigCode = value; }
		}
	
		public string JigName
		{
			get { return jigName; }
			set { jigName = value; }
		}
	
		public string Description
		{
			get { return description; }
			set { description = value; }
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
	
		public string WorkingName
		{
			get { return workingName; }
			set { workingName = value; }
		}
	
		public string Part
		{
			get { return part; }
			set { part = value; }
		}
	
		public int Qty
		{
			get { return qty; }
			set { qty = value; }
		}
	
	}
}
	