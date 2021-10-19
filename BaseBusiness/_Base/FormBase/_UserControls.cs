using System;
using System.Windows.Forms;

namespace BMS
{
	public partial class _UserControls : UserControl
	{
		public _UserControls()
		{
			InitializeComponent();
		}

		private void _ucBMSRpt_Load(object sender, EventArgs e)
		{
			Permissions.LoadUserControlPermission(this);
		}
	}
}
