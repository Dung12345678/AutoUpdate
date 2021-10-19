
using System;
namespace BMS.Model
{
	public class OrderPartModel : BaseModel
	{
		private int iD;
		private string orderCode;
		private string articleID;
		private int cnt;
		private string description;
		private int qty;
		private string shelf;
		private string location;
		private string lot;
		private DateTime? createAt;
		private string userr;
		private string orderCodeAndCnt;
		private DateTime? createDate;
		private DateTime? updateDate;
		public int ID
		{
			get { return iD; }
			set { iD = value; }
		}
	
		public string OrderCode
		{
			get { return orderCode; }
			set { orderCode = value; }
		}
	
		public string ArticleID
		{
			get { return articleID; }
			set { articleID = value; }
		}
	
		public int Cnt
		{
			get { return cnt; }
			set { cnt = value; }
		}
	
		public string Description
		{
			get { return description; }
			set { description = value; }
		}
	
		public int Qty
		{
			get { return qty; }
			set { qty = value; }
		}
	
		public string Shelf
		{
			get { return shelf; }
			set { shelf = value; }
		}
	
		public string Location
		{
			get { return location; }
			set { location = value; }
		}
	
		public string Lot
		{
			get { return lot; }
			set { lot = value; }
		}
	
		public DateTime? CreateAt
		{
			get { return createAt; }
			set { createAt = value; }
		}
	
		public string Userr
		{
			get { return userr; }
			set { userr = value; }
		}
	
		public string OrderCodeAndCnt
		{
			get { return orderCodeAndCnt; }
			set { orderCodeAndCnt = value; }
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
	
	}
}
	