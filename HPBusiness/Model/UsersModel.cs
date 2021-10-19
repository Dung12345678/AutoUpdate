
using System;
namespace HP.Model
{
	public class UsersModel : BaseModel
	{
		private int iD;
		private string userCode;
		private string fullName;
		private DateTime? birthOfDate;
		private int sex;
		private string qualifications;
		private string bankAccount;
		private string bHYT;
		private string bHXH;
		private string cMTND;
		private string jobDescription;
		private string telephone;
		private string handPhone;
		private string homeAddress;
		private string resident;
		private string postalCode;
		private string departmentCode;
		private int departmentID;
		private int status;
		private string email;
		private DateTime? startWorking;
		private string position;
		private string imagePath;
		private string createdBy;
		private DateTime? createdDate;
		private string updatedBy;
		private DateTime? updatedDate;
		private bool isAdmin;
		public int ID
		{
			get { return iD; }
			set { iD = value; }
		}
	
		public string UserCode
		{
			get { return userCode; }
			set { userCode = value; }
		}
	
		public string FullName
		{
			get { return fullName; }
			set { fullName = value; }
		}
	
		public DateTime? BirthOfDate
		{
			get { return birthOfDate; }
			set { birthOfDate = value; }
		}
	
		public int Sex
		{
			get { return sex; }
			set { sex = value; }
		}
	
		public string Qualifications
		{
			get { return qualifications; }
			set { qualifications = value; }
		}
	
		public string BankAccount
		{
			get { return bankAccount; }
			set { bankAccount = value; }
		}
	
		public string BHYT
		{
			get { return bHYT; }
			set { bHYT = value; }
		}
	
		public string BHXH
		{
			get { return bHXH; }
			set { bHXH = value; }
		}
	
		public string CMTND
		{
			get { return cMTND; }
			set { cMTND = value; }
		}
	
		public string JobDescription
		{
			get { return jobDescription; }
			set { jobDescription = value; }
		}
	
		public string Telephone
		{
			get { return telephone; }
			set { telephone = value; }
		}
	
		public string HandPhone
		{
			get { return handPhone; }
			set { handPhone = value; }
		}
	
		public string HomeAddress
		{
			get { return homeAddress; }
			set { homeAddress = value; }
		}
	
		public string Resident
		{
			get { return resident; }
			set { resident = value; }
		}
	
		public string PostalCode
		{
			get { return postalCode; }
			set { postalCode = value; }
		}
	
		public string DepartmentCode
		{
			get { return departmentCode; }
			set { departmentCode = value; }
		}
	
		public int DepartmentID
		{
			get { return departmentID; }
			set { departmentID = value; }
		}
	
		public int Status
		{
			get { return status; }
			set { status = value; }
		}
	
		public string Email
		{
			get { return email; }
			set { email = value; }
		}
	
		public DateTime? StartWorking
		{
			get { return startWorking; }
			set { startWorking = value; }
		}
	
		public string Position
		{
			get { return position; }
			set { position = value; }
		}
	
		public string ImagePath
		{
			get { return imagePath; }
			set { imagePath = value; }
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
	
		public bool IsAdmin
		{
			get { return isAdmin; }
			set { isAdmin = value; }
		}
	
	}
}
	