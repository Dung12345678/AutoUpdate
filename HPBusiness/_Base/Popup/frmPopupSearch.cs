using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Data.SqlClient;
using System.Collections;
using System.Runtime.InteropServices;
using HP.Business;
using HP.Model;
using HP.Facade;
using HP.Utils;

namespace HP
{
    public partial class frmPopupSearch : frmPopupBase
    {   
        #region Khai bao cac bien dung chung 

        public BaseModel getModel;
        DataTable Source;
        //khai bao thong tin ten bang va cac cot can hien thi
        public string TableName = "";
        public string FieldName1 = "";
        public string FieldName2 = "";
        public string FieldOrder = "";

        public string ParamInput = "";
        public bool IsMultipleSelect = false;
        public BaseModel[] getArrModel;

        public Expression exp;

        public string ColumnExpression = "";
        public bool NotIn = false;
        public string ColumnFindExpression = "";
        public string TableExpression = "";
        public string ColumnWhereExpression = "";
        public bool Equal = true;
        public string ColumnWhereExpressionValue = "";
        public string ConditionExpression = "";

        public int ColumnWith = 85;

        #endregion

        #region Load Form

        public frmPopupSearch(string captionForm, string tableName, string fieldName1, string fieldName2)
        {
            InitializeComponent();
            this.Text = captionForm;
            //gan gia tri khi load form de su dung nhu ten bang va truong
            TableName = tableName;
            FieldName1 = fieldName1;
            FieldName2 = fieldName2;
        }

        public frmPopupSearch(string captionForm, string tableName, string fieldName1, string fieldName2, string fieldOrder)
            : this(captionForm, tableName, fieldName1, fieldName2)
        {
            FieldOrder = fieldOrder;
        }

        private void frmCshTransactionSearch_Load(object sender, EventArgs e)
        {
            txtKeyWord.Text = ParamInput;
            fgrLoadSearch.MultiSelect = IsMultipleSelect;

            LoadDataSearch();
            if (ParamInput.Length != 0)
            {
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
            else
            {
                txtKeyWord.Select();
                txtKeyWord.Select(0, 0);
            }
        }

        #endregion

        #region Cac chuc nang co ban tren form
        #endregion

        #region Cac su kien phat sinh 
        
        private void txtKeyWord_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LoadDataSearch();
            }
            else if (e.KeyCode == Keys.Down)
            {
                if (fgrLoadSearch.Rows.Count > 0)
                {
                    fgrLoadSearch.Rows[0].Selected = true;
                    fgrLoadSearch.Select();
                }
            }
            else if (e.KeyCode == Keys.Escape)
                this.Hide();
        }

        private void fgrLoadSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Hide();
            else if (e.KeyCode == Keys.Up)
            {
                if (fgrLoadSearch.CurrentCell.RowIndex < 1)
                {
                    txtKeyWord.Select();
                    txtKeyWord.Focus();
                }
            }
            else if (e.KeyCode == Keys.Enter)
            {
                GetData(fgrLoadSearch.CurrentRow.Index);
                if (IsMultipleSelect == false)
                    if (getModel != null)
                        this.Hide();
                    else
                        return;
                else
                    if ((getArrModel.Length != 0) || (getArrModel != null))
                        this.Hide();
                    else
                        return;
            }
            else if ((e.KeyCode >= Keys.NumPad0) && (e.KeyCode <= Keys.NumPad9))
            {
                txtKeyWord.Text = e.KeyCode.ToString().Substring(e.KeyCode.ToString().Length-1,1);
                txtKeyWord.Select(1, 0);
                txtKeyWord.Focus();
            }
            else if ((e.KeyCode >= Keys.D0) && (e.KeyCode <= Keys.D9))
            {
                txtKeyWord.Text = e.KeyCode.ToString().Substring(e.KeyCode.ToString().Length - 1, 1);
                txtKeyWord.Select(1, 0);
                txtKeyWord.Focus();
            }
            else if ((e.KeyCode>=Keys.A) && (e.KeyCode != Keys.Z)&&(e.Control==false))
            {
                txtKeyWord.Text = e.KeyCode.ToString();
                txtKeyWord.Select(1, 0);
                txtKeyWord.Focus();
            }
        }

        private void fgrLoadSearch_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            GetData(e.RowIndex);
            if (!IsMultipleSelect)
                if (getModel != null)
                    this.Hide();
            else
                if (getArrModel != null || getArrModel.Length != 0)
                    this.Hide();
        }

        private void frmPopupSearch_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        #endregion

        #region Cac ham viet them

        /// <summary>
        /// phuong thuc tim kiem va lay du lieu tra ve
        /// </summary>
        private void LoadDataSearch()
        {
            string[] paramName = new string[1];
            object[] paramValue = new object[1];
            paramName[0] = "@SqlCommand";
            if (ColumnExpression.Equals(""))
            {
                paramValue[0] = "Select * from " + TableName + " where ((" + FieldName1 + " like N'%" + txtKeyWord.Text + "%') or (" + FieldName2 +
                                " like N'%" + txtKeyWord.Text + "%'))";
            }
            else
            {
                if (NotIn == false)
                {
                    if (ConditionExpression == "")
                    {
                        if (Equal == true)
                            paramValue[0] = "Select * from " + TableName + " where ((" + FieldName1 + " like N'%" + txtKeyWord.Text + "%') or (" + FieldName2 +
                                            " like N'%" + txtKeyWord.Text + "%')) AND (" + ColumnExpression + " in (select " + ColumnFindExpression + " from " + TableExpression +
                                            " where " + ColumnWhereExpression + "='" + ColumnWhereExpressionValue + "'))";
                        else
                            paramValue[0] = "Select * from " + TableName + " where ((" + FieldName1 + " like N'%" + txtKeyWord.Text + "%') or (" + FieldName2 +
                                            " like N'%" + txtKeyWord.Text + "%')) AND (" + ColumnExpression + " in (select " + ColumnFindExpression + " from " + TableExpression +
                                            " where " + ColumnWhereExpression + "!='" + ColumnWhereExpressionValue + "'))";
                    }
                    else
                    {
                        paramValue[0] = "Select * from " + TableName + " where ((" + FieldName1 + " like N'%" + txtKeyWord.Text + "%') or (" + FieldName2 +
                                        " like N'%" + txtKeyWord.Text + "%')) AND (" + ColumnExpression + " in (select " + ColumnFindExpression + " from " + TableExpression +
                                        " where " + ConditionExpression + "))";
                    }
                }
                else
                {
                    if (ConditionExpression == "")
                    {
                        if(Equal==true)
                            paramValue[0] = "Select * from " + TableName + " where ((" + FieldName1 + " like N'%" + txtKeyWord.Text + "%') or (" + FieldName2 +
                                            " like N'%" + txtKeyWord.Text + "%')) AND (" + ColumnExpression + " not in (select " + ColumnFindExpression + " from " + TableExpression +
                                            " where " + ColumnWhereExpression + "='" + ColumnWhereExpressionValue + "'))";
                        else
                            paramValue[0] = "Select * from " + TableName + " where ((" + FieldName1 + " like N'%" + txtKeyWord.Text + "%') or (" + FieldName2 +
                                            " like N'%" + txtKeyWord.Text + "%')) AND (" + ColumnExpression + " not in (select " + ColumnFindExpression + " from " + TableExpression +
                                            " where " + ColumnWhereExpression + "!='" + ColumnWhereExpressionValue + "'))";
                    }
                    else
                    {
                        paramValue[0] = "Select * from " + TableName + " where ((" + FieldName1 + " like N'%" + txtKeyWord.Text + "%') or (" + FieldName2 +
                                        " like N'%" + txtKeyWord.Text + "%')) AND (" + ColumnExpression + " not in (select " + ColumnFindExpression + " from " + TableExpression +
                                        " where " + ConditionExpression + "))";
                    }
                }
            }
            if (exp != null)
            {
                paramValue[0] = paramValue[0] + " AND " + exp.ToString();
            }
            if (FieldOrder == "")
                paramValue[0] = paramValue[0] +  " order by " + FieldName1 + " asc";
            else
                paramValue[0] = paramValue[0] + " order by " + FieldOrder + " asc";
            try
            {
                Source = UsersBO.Instance.LoadDataFromSP("spSearchAllForTrans", "Table", paramName, paramValue);
                fgrLoadSearch.DataSource = Source;
                lblRowCount.Text = Source.Rows.Count.ToString();
                //hide columns
                for (int i = 0; i < fgrLoadSearch.Columns.Count; i++)
                {
                    if (fgrLoadSearch.Columns[i].Name == FieldName1)
                    {
                        fgrLoadSearch.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                        fgrLoadSearch.Columns[i].Width = ColumnWith;
                        fgrLoadSearch.Columns[i].Visible = true;
                    }
                    else if (fgrLoadSearch.Columns[i].Name == FieldName2)
                        fgrLoadSearch.Columns[i].Visible = true;
                    else
                        fgrLoadSearch.Columns[i].Visible = false;
                }
                //Hide or show Select
                if (fgrLoadSearch.Rows.Count > 0)
                {
                    fgrLoadSearch.Rows[0].Selected = true;
                    fgrLoadSearch.Select();
                    //btnSelect.Enabled = true;
                }
                else
                {
                    //btnSelect.Enabled = false;
                    txtKeyWord.Select();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void GetData(int rowIndex)
        {
            if (fgrLoadSearch.Rows.Count > 0)
            {
                #region Nếu ở chế độ Single Select
                if (!IsMultipleSelect)
                {
                    try
                    {
                        for (int i = 0; i < Source.Rows.Count; i++)
                        {
                            if (Source.Rows[i][0].ToString().Equals(fgrLoadSearch.Rows[rowIndex].Cells[0].FormattedValue.ToString()))
                            {
                                DataRow dr = Source.Rows[i];
                                getModel = ProcessTransaction.PopulateModel(dr, ProcessTransaction.getClassName(TableName));
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
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

        public BaseModel SelectFirstObject()
        {
            LoadDataSearch();
            fgrLoadSearch_CellDoubleClick(fgrLoadSearch, new DataGridViewCellEventArgs(0, 0));
            return getModel;
        }

        #endregion
    }
}