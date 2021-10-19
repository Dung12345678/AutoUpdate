
using System;
namespace HP.Model
{
	public class StageWorkerModel : BaseModel
	{
		private int iD;
		private int workerID;
		private string workerCode;
		private string jobDescription;
		private int stageID;
		private string stageCode;
		public int ID
		{
			get { return iD; }
			set { iD = value; }
		}
	
		public int WorkerID
		{
			get { return workerID; }
			set { workerID = value; }
		}
	
		public string WorkerCode
		{
			get { return workerCode; }
			set { workerCode = value; }
		}
	
		public string JobDescription
		{
			get { return jobDescription; }
			set { jobDescription = value; }
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
	
	}
}
	