
using System;
namespace HP.Model
{
	public class WeightModel : BaseModel
	{
		private int iD;
		private string productGroup;
		private double plateWeight;
		private double jigWeight;
		public int ID
		{
			get { return iD; }
			set { iD = value; }
		}
	
		public string ProductGroup
		{
			get { return productGroup; }
			set { productGroup = value; }
		}
	
		public double PlateWeight
		{
			get { return plateWeight; }
			set { plateWeight = value; }
		}
	
		public double JigWeight
		{
			get { return jigWeight; }
			set { jigWeight = value; }
		}
	
	}
}
	