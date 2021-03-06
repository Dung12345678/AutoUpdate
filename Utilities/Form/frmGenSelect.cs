using BMS.Business;
using BMS.Model;
using BMS.Utils;
using System;
using System.Data;
using System.Windows.Forms;

namespace BMS
{
	public partial class frmGenSelect : _Forms
	{
		#region Khai bao cac bien dung chung

		public BaseModel getModel;
		DataTable Source;
		//khai bao thong tin ten bang va cac cot can hien thi
		public string TableName = "";
		public string FieldName1 = "";
		public string FieldName2 = "";

		public string ParamInput = "";
		public bool IsMultipleSelect = false;
		public BaseModel[] getArrModel;
		public int ColumnsFirstWith = 0;

		public Expression exp;

		public string ColumnExpression = "";

		public string ColumnFindExpression = "";
		public string ColumnWhereExpression = "";
		public string ColumnWhereExpressionValue = "";
		public string TableExpression = "";

		#endregion

		#region Load Data

		public frmGenSelect(string captionForm, string tableName, string fieldName1, string fieldName2)
		{
			InitializeComponent();
			this.Text = captionForm;
			//gan gia tri khi load form de su dung nhu ten bang va truong
			TableName = tableName;
			FieldName1 = fieldName1;
			FieldName2 = fieldName2;
		}

		private void frmGenSelect_Load(object sender, EventArgs e)
		{
			txtKeyWord.Text = ParamInput;
			if (IsMultipleSelect == false)
				fgrLoadSearch.MultiSelect = false;
			else
				fgrLoadSearch.MultiSelect = true;
			LoadDataSearch();
			if (fgrLoadSearch.Rows.Count > 0)
			{
				fgrLoadSearch.Rows[0].Selected = true;
				fgrLoadSearch.Select();
			}
			else
			{
				txtKeyWord.Select();
				txtKeyWord.Select(0, 0);
			}

		}

		#endregion

		#region Cac su kien co ban

		private void btnClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		#endregion

		#region cac su kien tren Form

		private void btnSearch_Click(object sender, EventArgs e)
		{
			LoadDataSearch();
		}

		private void btnSelect_Click(object sender, EventArgs e)
		{
			GetData();
			if (IsMultipleSelect == false)
				if (getModel != null)
					this.Close();
				else
					return;
			else
				if ((getArrModel.Length != 0) || (getArrModel != null))
				this.Close();
			else
				return;
		}

		private void fgrLoadSearch_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			GetData();
			if (IsMultipleSelect == false)
				if (getModel != null)
					this.Close();
				else
					return;
			else
				if ((getArrModel.Length != 0) || (getArrModel != null))
				this.Close();
			else
				return;
		}

		private void frmGenSearch_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
				this.Close();
		}

		private void txtKeyWord_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				LoadDataSearch();
				if (fgrLoadSearch.Rows.Count > 0)
					fgrLoadSearch.Focus();
			}
			else if (e.KeyCode == Keys.Escape)
				this.Close();
			else if (e.KeyCode == Keys.Down)
			{
				if (fgrLoadSearch.Rows.Count > 0)
				{
					fgrLoadSearch.Rows[0].Selected = true;
					fgrLoadSearch.Select();
				}
			}
		}

		private void fgrLoadSearch_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
				this.Close();
			else if (e.KeyCode == Keys.Enter)
			{
				GetData();
				if (IsMultipleSelect == false)
					if (getModel != null)
						this.Close();
					else
						return;
				else
					if ((getArrModel.Length != 0) || (getArrModel != null))
					this.Close();
				else
					return;
			}
			else if (e.KeyCode == Keys.Up)
			{
				if (fgrLoadSearch.CurrentCell.RowIndex < 1)
				{
					txtKeyWord.Focus();
					txtKeyWord.Select();
				}
			}
			else if ((e.KeyCode >= Keys.NumPad0) && (e.KeyCode <= Keys.NumPad9))
			{
				txtKeyWord.Text = e.KeyCode.ToString().Substring(e.KeyCode.ToString().Length - 1, 1);
				txtKeyWord.Focus();
				txtKeyWord.Select(1, 0);
			}
			else if ((e.KeyCode >= Keys.D0) && (e.KeyCode <= Keys.D9))
			{
				txtKeyWord.Text = e.KeyCode.ToString().Substring(e.KeyCode.ToString().Length - 1, 1);
				txtKeyWord.Focus();
				txtKeyWord.Select(1, 0);
			}
			else if ((e.KeyCode >= Keys.A) && (e.KeyCode != Keys.Z))
			{
				txtKeyWord.Text = e.KeyCode.ToString();
				txtKeyWord.Focus();
				txtKeyWord.Select(1, 0);
			}
		}

		#endregion

		#region cac ham viet them

		/// <summary>
		/// phuong thuc tim kiem va lay giu lieu tra ve
		/// </summary>
		private void LoadDataSearch()
		{
			string[] paramName = new string[1];
			object[] paramValue = new object[1];
			paramName[0] = "@SqlCommand";
			if (ColumnExpression.Equals(""))
			{
				paramValue[0] = "Select * from " + TableName + " where ((" + FieldName1 + " like N'" + txtKeyWord.Text + "%') or (" + FieldName2 +
								" like N'" + txtKeyWord.Text + "%'))";
			}
			else
			{
				paramValue[0] = "Select * from " + TableName + " where ((" + FieldName1 + " like N'" + txtKeyWord.Text + "%') or (" + FieldName2 +
								" like N'" + txtKeyWord.Text + "%')) AND (" + ColumnExpression + " in (select " + ColumnFindExpression + " from " + TableExpression +
								" where " + ColumnWhereExpression + "='" + ColumnWhereExpressionValue + "'))";
			}
			if (exp != null)
			{
				paramValue[0] = paramValue[0] + " AND " + exp.ToString();
			}
			paramValue[0] = paramValue[0] + " Order by " + FieldName1;
			try
			{
				Source = UsersBO.Instance.LoadDataFromSP("spSearchAllForTrans", "Table", paramName, paramValue);
				fgrLoadSearch.DataSource = Source;
				//hide columns
				for (int i = 0; i < fgrLoadSearch.Columns.Count; i++)
				{
					if (fgrLoadSearch.Columns[i].Name == FieldName1)
					{
						fgrLoadSearch.Columns[i].HeaderText = getName(FieldName1);
						fgrLoadSearch.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
						if (ColumnsFirstWith == 0)
							fgrLoadSearch.Columns[i].Width = 75;
						else
							fgrLoadSearch.Columns[i].Width = ColumnsFirstWith;
						fgrLoadSearch.Columns[i].Visible = true;
					}
					else if (fgrLoadSearch.Columns[i].Name == FieldName2)
					{
						fgrLoadSearch.Columns[i].Visible = true;
						fgrLoadSearch.Columns[i].HeaderText = getName(FieldName2);
					}
					else
						fgrLoadSearch.Columns[i].Visible = false;
				}
				//Hide or show Select
				if (fgrLoadSearch.Rows.Count > 0)
				{
					fgrLoadSearch.Rows[0].Selected = true;
					fgrLoadSearch.Select();
					btnSelect.Enabled = true;
				}
				else
				{
					btnSelect.Enabled = false;
					txtKeyWord.Select();
				}
			}
			catch
			{
				return;
			}
		}

		private string getName(string Field)
		{
			switch (Field.ToLower())
			{
				case "id":
					return "Khóa";
					break;
				case "name":
					return "Tên";
					break;
				case "code":
					return "Mã";
					break;
				case "description":
					return "Mô tả";
					break;
				case "fullname":
					return "Tên đầy đủ";
					break;
				default:
					return Field;
					break;

			}
		}

		/// <summary>
		/// 
		/// </summary>
		private void GetData()
		{
			if (fgrLoadSearch.Rows.Count > 0)
			{
				#region Nếu ở chế độ Single Select
				if (IsMultipleSelect == false)
				{
					try
					{
						for (int i = 0; i < Source.Rows.Count; i++)
						{
							if (Source.Rows[i][0].ToString().Equals(fgrLoadSearch.CurrentRow.Cells[0].FormattedValue.ToString()))
							{
								DataRow dr = Source.Rows[i];
								getModel = ProcessTransaction.PopulateModel(dr, ProcessTransaction.getClassName(TableName));
							}
						}
					}
					catch (Exception ex)
					{
						return;
					}
				}
				#endregion

				#region Nếu ở chế độ Multi select

				int Dem = 0;
				#region Đếm số Item được chọn
				for (int i = 0; i < fgrLoadSearch.Rows.Count; i++)
				{
					if (fgrLoadSearch.Rows[i].Selected == true)
						Dem = Dem + 1;
				}
				#endregion

				#region Nếu không có item nào được chọn
				if (Dem == 0)
				{
					return;
				}
				#endregion

				#region Nếu có Item được chọn
				else
				{
					getArrModel = new BaseModel[Dem];
					Dem = 0;
					for (int i = 0; i < fgrLoadSearch.Rows.Count; i++)
					{
						if (fgrLoadSearch.Rows[i].Selected == true)
							for (int j = 0; j < Source.Rows.Count; j++)
								if (Source.Rows[j][0].ToString().Equals(fgrLoadSearch.Rows[i].Cells[0].FormattedValue.ToString()))
								{
									DataRow dr = Source.Rows[j];
									getArrModel[Dem] = ProcessTransaction.PopulateModel(dr, ProcessTransaction.getClassName(TableName));
									Dem = Dem + 1;
								}
					}
				}
				#endregion

				#endregion
			}
		}

		#endregion
	}
}