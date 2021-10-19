
using System;
namespace HP.Model
{
	public class STDModel : BaseModel
	{
		private int iD;
		private string stepCode;
		private string articleID;
		private int sTT;
		private string smallGroup;
		private string workingName;
		private string valueTypeName;
		private int valueType;
		private string unit;
		private decimal originalValue;
		private decimal maxAllowance;
		private decimal minAllowance;
		private decimal toleranceValueMax;
		private decimal toleranceValueMin;
		private DateTime? createDate;
		private DateTime? updateDate;
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
	
		public int STT
		{
			get { return sTT; }
			set { sTT = value; }
		}
	
		public string SmallGroup
		{
			get { return smallGroup; }
			set { smallGroup = value; }
		}
	
		public string WorkingName
		{
			get { return workingName; }
			set { workingName = value; }
		}
	
		public string ValueTypeName
		{
			get { return valueTypeName; }
			set { valueTypeName = value; }
		}
	
		public int ValueType
		{
			get { return valueType; }
			set { valueType = value; }
		}
	
		public string Unit
		{
			get { return unit; }
			set { unit = value; }
		}
	
		public decimal OriginalValue
		{
			get { return originalValue; }
			set { originalValue = value; }
		}
	
		public decimal MaxAllowance
		{
			get { return maxAllowance; }
			set { maxAllowance = value; }
		}
	
		public decimal MinAllowance
		{
			get { return minAllowance; }
			set { minAllowance = value; }
		}
	
		public decimal ToleranceValueMax
		{
			get { return toleranceValueMax; }
			set { toleranceValueMax = value; }
		}
	
		public decimal ToleranceValueMin
		{
			get { return toleranceValueMin; }
			set { toleranceValueMin = value; }
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
	