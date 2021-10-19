
using System;
namespace HP.Model
{
	public class KnifeProcessedListModel : BaseModel
	{
		private int iD;
		private int knifeID;
		private string knifeCode;
		private int workerID;
		private DateTime? dateProcess;
		private string productCode;
		private int machineID;
		private string machineCode;
		private int quantity;
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
	
		public int WorkerID
		{
			get { return workerID; }
			set { workerID = value; }
		}
	
		public DateTime? DateProcess
		{
			get { return dateProcess; }
			set { dateProcess = value; }
		}
	
		public string ProductCode
		{
			get { return productCode; }
			set { productCode = value; }
		}
	
		public int MachineID
		{
			get { return machineID; }
			set { machineID = value; }
		}
	
		public string MachineCode
		{
			get { return machineCode; }
			set { machineCode = value; }
		}
	
		public int Quantity
		{
			get { return quantity; }
			set { quantity = value; }
		}
	
	}
}
	