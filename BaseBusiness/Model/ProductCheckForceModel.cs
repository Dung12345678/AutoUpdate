
using System;
namespace BMS.Model
{
	public class ProductCheckForceModel : BaseModel
	{
		private int iD;
		private string productCode;
		private decimal checkForceMin;
		private decimal checkForceMax;
		private int gunNumber;
		private int jobNumber;
		private int qtyBuLong;
		private int shootNumber;
		private int qtyOcBanGa;
		private int qtyOcBanThat;
		private decimal setForce;
		private string productStepCode;
		public int ID
		{
			get { return iD; }
			set { iD = value; }
		}
	
		public string ProductCode
		{
			get { return productCode; }
			set { productCode = value; }
		}
	
		public decimal CheckForceMin
		{
			get { return checkForceMin; }
			set { checkForceMin = value; }
		}
	
		public decimal CheckForceMax
		{
			get { return checkForceMax; }
			set { checkForceMax = value; }
		}
	
		public int GunNumber
		{
			get { return gunNumber; }
			set { gunNumber = value; }
		}
	
		public int JobNumber
		{
			get { return jobNumber; }
			set { jobNumber = value; }
		}
	
		public int QtyBuLong
		{
			get { return qtyBuLong; }
			set { qtyBuLong = value; }
		}
	
		public int ShootNumber
		{
			get { return shootNumber; }
			set { shootNumber = value; }
		}
	
		public int QtyOcBanGa
		{
			get { return qtyOcBanGa; }
			set { qtyOcBanGa = value; }
		}
	
		public int QtyOcBanThat
		{
			get { return qtyOcBanThat; }
			set { qtyOcBanThat = value; }
		}
	
		public decimal SetForce
		{
			get { return setForce; }
			set { setForce = value; }
		}
	
		public string ProductStepCode
		{
			get { return productStepCode; }
			set { productStepCode = value; }
		}
	
	}
}
	