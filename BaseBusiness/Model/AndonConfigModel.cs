namespace BMS.Model
{
	public class AndonConfigModel : BaseModel
	{
		private int iD;
		private decimal fontSize1;
		private decimal fontSize2;
		private decimal fontSize3;
		private decimal fontSize4;
		private decimal fontSize5;
		private decimal fontSize6;
		private decimal fontSize7;
		private int takt;
		private string tcpIp;
		private int socketPort;
		private int isStopCD8;
		private string comPLC;
		private string areaDelayPLC;
		private string areaRiskPLC;
		public int ID
		{
			get { return iD; }
			set { iD = value; }
		}

		public decimal FontSize1
		{
			get { return fontSize1; }
			set { fontSize1 = value; }
		}

		public decimal FontSize2
		{
			get { return fontSize2; }
			set { fontSize2 = value; }
		}

		public decimal FontSize3
		{
			get { return fontSize3; }
			set { fontSize3 = value; }
		}

		public decimal FontSize4
		{
			get { return fontSize4; }
			set { fontSize4 = value; }
		}

		public decimal FontSize5
		{
			get { return fontSize5; }
			set { fontSize5 = value; }
		}

		public decimal FontSize6
		{
			get { return fontSize6; }
			set { fontSize6 = value; }
		}

		public decimal FontSize7
		{
			get { return fontSize7; }
			set { fontSize7 = value; }
		}

		public int Takt
		{
			get { return takt; }
			set { takt = value; }
		}

		public string TcpIp
		{
			get { return tcpIp; }
			set { tcpIp = value; }
		}

		public int SocketPort
		{
			get { return socketPort; }
			set { socketPort = value; }
		}

		public int IsStopCD8
		{
			get { return isStopCD8; }
			set { isStopCD8 = value; }
		}

		public string ComPLC
		{
			get { return comPLC; }
			set { comPLC = value; }
		}

		public string AreaDelayPLC
		{
			get { return areaDelayPLC; }
			set { areaDelayPLC = value; }
		}

		public string AreaRiskPLC
		{
			get { return areaRiskPLC; }
			set { areaRiskPLC = value; }
		}

	}
}
