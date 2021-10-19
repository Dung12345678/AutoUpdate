
using System;
namespace HP.Model
{
	public class MachineModel : BaseModel
	{
		private int iD;
		private string machineCode;
		private int stageID;
		private string stageCode;
		private string machineName;
		private string description;
		public int ID
		{
			get { return iD; }
			set { iD = value; }
		}
	
		public string MachineCode
		{
			get { return machineCode; }
			set { machineCode = value; }
		}
	
		public int StageID
		{
			get { return stageID; }
			set { stageID = value; }
		}
	
		public string StageCode
		{
			get { return stageCode; }
			set { stageCode = value; }
		}
	
		public string MachineName
		{
			get { return machineName; }
			set { machineName = value; }
		}
	
		public string Description
		{
			get { return description; }
			set { description = value; }
		}
	
	}
}
	