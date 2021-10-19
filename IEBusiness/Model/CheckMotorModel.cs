
using System;
namespace IE.Model
{
	public class CheckMotorModel : BaseModel
	{
		private int iD;
		private string cardNo;
		private string salesOrder;
		private int orderedQty;
		private int qtyOfShipOrder;
		private string motorInspSealNo;
		private string serialNo;
		private string articleID;
		private string assemblyOrderNo;
		private int noOfJG;
		private string descriptions;
		private string uPR;
		private DateTime? jGDate;
		public int ID
		{
			get { return iD; }
			set { iD = value; }
		}
	
		public string CardNo
		{
			get { return cardNo; }
			set { cardNo = value; }
		}
	
		public string SalesOrder
		{
			get { return salesOrder; }
			set { salesOrder = value; }
		}
	
		public int OrderedQty
		{
			get { return orderedQty; }
			set { orderedQty = value; }
		}
	
		public int QtyOfShipOrder
		{
			get { return qtyOfShipOrder; }
			set { qtyOfShipOrder = value; }
		}
	
		public string MotorInspSealNo
		{
			get { return motorInspSealNo; }
			set { motorInspSealNo = value; }
		}
	
		public string SerialNo
		{
			get { return serialNo; }
			set { serialNo = value; }
		}
	
		public string ArticleID
		{
			get { return articleID; }
			set { articleID = value; }
		}
	
		public string AssemblyOrderNo
		{
			get { return assemblyOrderNo; }
			set { assemblyOrderNo = value; }
		}
	
		public int NoOfJG
		{
			get { return noOfJG; }
			set { noOfJG = value; }
		}
	
		public string Descriptions
		{
			get { return descriptions; }
			set { descriptions = value; }
		}
	
		public string UPR
		{
			get { return uPR; }
			set { uPR = value; }
		}
	
		public DateTime? JGDate
		{
			get { return jGDate; }
			set { jGDate = value; }
		}
	
	}
}
	