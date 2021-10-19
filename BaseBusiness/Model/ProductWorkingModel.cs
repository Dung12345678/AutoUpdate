
using System;
namespace BMS.Model
{
	public class ProductWorkingModel : BaseModel
	{
		private int iD;
		private int productID;
		private int productStepID;
		private int workingID;
		private string workingName;
		private int valueType;
		private string valueTypeName;
		private string periodValue;
		private decimal minValue;
		private decimal maxValue;
		private string unit;
		private int sortOrder;
		private bool isGetAutoValueComport;
		private int comport;
		private bool isDeleted;
		private string createdBy;
		private DateTime? createdDate;
		private string updatedBy;
		private DateTime? updatedDate;
		private bool isGetAutoValueIP;
		private string port;
		private string ipAddress;
		private int checkValueType;
		private string productStepCode;
		private int actionType;
		private string reasonChange;
		private int isApproved;
		private bool isHidden;
		public int ID
		{
			get { return iD; }
			set { iD = value; }
		}

		public int ProductID
		{
			get { return productID; }
			set { productID = value; }
		}

		public int ProductStepID
		{
			get { return productStepID; }
			set { productStepID = value; }
		}

		public int WorkingID
		{
			get { return workingID; }
			set { workingID = value; }
		}

		public string WorkingName
		{
			get { return workingName; }
			set { workingName = value; }
		}

		public int ValueType
		{
			get { return valueType; }
			set { valueType = value; }
		}

		public string ValueTypeName
		{
			get { return valueTypeName; }
			set { valueTypeName = value; }
		}

		public string PeriodValue
		{
			get { return periodValue; }
			set { periodValue = value; }
		}

		public decimal MinValue
		{
			get { return minValue; }
			set { minValue = value; }
		}

		public decimal MaxValue
		{
			get { return maxValue; }
			set { maxValue = value; }
		}

		public string Unit
		{
			get { return unit; }
			set { unit = value; }
		}

		public int SortOrder
		{
			get { return sortOrder; }
			set { sortOrder = value; }
		}

		public bool IsGetAutoValueComport
		{
			get { return isGetAutoValueComport; }
			set { isGetAutoValueComport = value; }
		}

		public int Comport
		{
			get { return comport; }
			set { comport = value; }
		}

		public bool IsDeleted
		{
			get { return isDeleted; }
			set { isDeleted = value; }
		}

		public string CreatedBy
		{
			get { return createdBy; }
			set { createdBy = value; }
		}

		public DateTime? CreatedDate
		{
			get { return createdDate; }
			set { createdDate = value; }
		}

		public string UpdatedBy
		{
			get { return updatedBy; }
			set { updatedBy = value; }
		}

		public DateTime? UpdatedDate
		{
			get { return updatedDate; }
			set { updatedDate = value; }
		}

		public bool IsGetAutoValueIP
		{
			get { return isGetAutoValueIP; }
			set { isGetAutoValueIP = value; }
		}

		public string Port
		{
			get { return port; }
			set { port = value; }
		}

		public string IpAddress
		{
			get { return ipAddress; }
			set { ipAddress = value; }
		}

		public int CheckValueType
		{
			get { return checkValueType; }
			set { checkValueType = value; }
		}

		public string ProductStepCode
		{
			get { return productStepCode; }
			set { productStepCode = value; }
		}

		public int ActionType
		{
			get { return actionType; }
			set { actionType = value; }
		}

		public string ReasonChange
		{
			get { return reasonChange; }
			set { reasonChange = value; }
		}

		public int IsApproved
		{
			get { return isApproved; }
			set { isApproved = value; }
		}

		public bool IsHidden
		{
			get { return isHidden; }
			set { isHidden = value; }
		}

	}
}
