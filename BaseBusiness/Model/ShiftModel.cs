
using System;
namespace BMS.Model
{
	public class ShiftModel : BaseModel
	{
		private int iD;
		private string code;
		private string name;
		private DateTime? startTime;
		private DateTime? endTime;
		private DateTime? totalTime;
		private DateTime? startTimeBreak;
		private DateTime? endTimeBreak;
		private DateTime? startTimeBreak1;
		private DateTime? endTimeBreak1;
		private DateTime? startTimeBreak2;
		private DateTime? endTimeBreak2;
		private DateTime? startTimeBreak3;
		private DateTime? endTimeBreak3;
		private DateTime? startTimeBreak4;
		private DateTime? endTimeBreak4;
		public int ID
		{
			get { return iD; }
			set { iD = value; }
		}

		public string Code
		{
			get { return code; }
			set { code = value; }
		}

		public string Name
		{
			get { return name; }
			set { name = value; }
		}

		public DateTime? StartTime
		{
			get { return startTime; }
			set { startTime = value; }
		}

		public DateTime? EndTime
		{
			get { return endTime; }
			set { endTime = value; }
		}

		public DateTime? TotalTime
		{
			get { return totalTime; }
			set { totalTime = value; }
		}

		public DateTime? StartTimeBreak
		{
			get { return startTimeBreak; }
			set { startTimeBreak = value; }
		}

		public DateTime? EndTimeBreak
		{
			get { return endTimeBreak; }
			set { endTimeBreak = value; }
		}

		public DateTime? StartTimeBreak1
		{
			get { return startTimeBreak1; }
			set { startTimeBreak1 = value; }
		}

		public DateTime? EndTimeBreak1
		{
			get { return endTimeBreak1; }
			set { endTimeBreak1 = value; }
		}

		public DateTime? StartTimeBreak2
		{
			get { return startTimeBreak2; }
			set { startTimeBreak2 = value; }
		}

		public DateTime? EndTimeBreak2
		{
			get { return endTimeBreak2; }
			set { endTimeBreak2 = value; }
		}

		public DateTime? StartTimeBreak3
		{
			get { return startTimeBreak3; }
			set { startTimeBreak3 = value; }
		}

		public DateTime? EndTimeBreak3
		{
			get { return endTimeBreak3; }
			set { endTimeBreak3 = value; }
		}

		public DateTime? StartTimeBreak4
		{
			get { return startTimeBreak4; }
			set { startTimeBreak4 = value; }
		}

		public DateTime? EndTimeBreak4
		{
			get { return endTimeBreak4; }
			set { endTimeBreak4 = value; }
		}

	}
}
