using BMS.Exceptions;
using BMS.Model;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;


namespace BMS.Utils
{
	/// <summary>
	/// Class dùng để xử lí Transaction
	/// -- Nguyễn Văn Thao - 29/9/2009 --
	/// </summary>
	public class ProcessTransaction
	{
		#region Khai bao cac bien dung chung

		public SqlConnection cnn;
		public SqlTransaction tran;
		public static SqlCommand cmd;
		public static SqlDataAdapter da;


		#endregion

		#region Constructor

		public ProcessTransaction()
		{
			cnn = new SqlConnection(DBUtils.GetDBConnectionString());
		}
		/// <summary>
		/// Kết nối pt với 1 db khác
		/// </summary>
		/// <param name="dbType">0: db default, 1: Interface, 2: SC Interface</param>
		public ProcessTransaction(int dbType)
		{
			if (dbType > 0)
				cnn = new SqlConnection(DBUtils.GetOtherDBConnectionString(dbType));
			else
				cnn = new SqlConnection(DBUtils.GetDBConnectionString());
		}
		#endregion

		#region Phuong thuc su dung them

		/// <summary>
		/// Phương thức lấy ra tên của Model của table
		/// </summary>
		/// <param name="tableName">Tên của table</param>
		/// <returns>Tên của Model</returns>
		/// Author :Nguyễn Văn Thao
		/// Date:29/9/2009
		public static string getClassName(string tableName)
		{
			string result = "";
			result = tableName + "Model";

			return result;
		}

		/// <summary>
		/// Phương thức đổ dũ liệu từ DataRow vào Model
		/// -- Nguyễn Văn Thao - 29/9/2009 --
		/// </summary>
		/// <param name="dr">DataRow</param>
		/// <param name="model">Tên Model</param>
		/// <returns>Object</returns>
		private static object PopulateObject(DataRow dr, object model)
		{
			PropertyInfo[] propertiesName = model.GetType().GetProperties();

			for (int i = 0; i < propertiesName.Length; i++)
			{
				Object value = dr[propertiesName[i].Name];
				if (value != DBNull.Value)
				{
					propertiesName[i].SetValue(model, value, null);
				}
			}

			return model;
		}

		/// <summary>
		/// Phương thức đổ dũ liệu từ DataRow vào Model
		/// -- Nguyễn Văn Thao - 29/9/2009 --
		/// </summary>
		/// <param name="dr">DataRow</param>
		/// <param name="model">Tên Model</param>
		/// <returns>Object</returns>
		private static object PopulateObject(DataRow dr, string fullname)
		{
			Object model = Activator.CreateInstance(Type.GetType(fullname));
			return PopulateObject(dr, model);
		}

		/// <summary>
		/// Phương thức đổ dũ liệu từ DataRow vào Model
		/// -- Nguyễn Văn Thao - 29/9/2009 --
		/// </summary>
		/// <param name="dr">DataRow</param>
		/// <param name="model">Tên Model</param>
		/// <returns>Object</returns>
		public static BaseModel PopulateModel(DataRow dr, string name)
		{
			return (BaseModel)PopulateObject(dr, "BMS.Model." + name);
		}

		#endregion

		#region Cac method thuc hien Insert,delete,update,select

		/// <summary>
		/// Lấy về dữ liệu thông qua 1 câu command
		/// -- Nguyễn Văn Thao - 29/9/2009 --
		/// </summary>
		/// <param name="strComm">Chuỗi Command để Excute</param>
		/// <returns>DataTable</returns>
		public DataTable Select(string strComm)
		{
			try
			{
				cmd = new SqlCommand("spGenSearchWithCommand", cnn, tran);
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.Add(new SqlParameter("@sqlCommand", strComm));

				//cmd.ExecuteNonQuery();

				da = new SqlDataAdapter(cmd);
				DataSet ds = new DataSet();
				da.Fill(ds);
				return ds.Tables[0];
			}
			catch (SqlException se)
			{
				throw new Exception("Sellect error :" + se.Message);
			}
		}

		public DataTable SelectWithoutSP(string strComm)
		{
			try
			{
				cmd = new SqlCommand(strComm, cnn, tran);
				cmd.CommandType = CommandType.Text;
				da = new SqlDataAdapter(cmd);
				DataSet ds = new DataSet();
				da.Fill(ds);
				return ds.Tables[0];
			}
			catch (SqlException se)
			{
				throw new Exception("Sellect error :" + se.Message);
			}
		}

		/// <summary>
		/// Lấy dữ liệu đổ về thông qua Store Procedure với only parameter
		/// -- Nguyễn Văn Thao - 29/9/2009 --
		/// </summary>
		/// <param name="procedureName">Tên Store Procedure</param>
		/// <param name="mySqlParameter">Parameter</param>
		/// <param name="nameSetToTable">Tên của table lấy ra</param>
		/// <returns>DataTable</returns>
		public DataTable getTable(string procedureName, SqlParameter mySqlParameter, string nameSetToTable)
		{
			DataTable table = new DataTable();
			try
			{

				cmd = new SqlCommand(procedureName, cnn, tran);
				cmd.CommandType = CommandType.StoredProcedure;
				da = new SqlDataAdapter(cmd);
				DataSet myDataSet = new DataSet();
				if (mySqlParameter != null)
					cmd.Parameters.Add(mySqlParameter);
				//cmd.ExecuteNonQuery();
				da.Fill(myDataSet, nameSetToTable);
				table = myDataSet.Tables[nameSetToTable];
			}
			catch (SqlException ex)
			{
				tran.Rollback();
				throw new Exception(ex.Message);
			}
			return table;
		}

		/// <summary>
		/// Lấy dữ liệu đổ về thông qua Store Procedure với only parameter
		/// -- Nguyễn Văn Thao - 29/9/2009 --
		/// </summary>
		/// <param name="procedureName">Tên Store Procedure</param>
		/// <param name="nameSetToTable">Tên của table lấy ra</param>
		/// <param name="mySqlParameter">Mảng các parameter</param>
		/// <returns></returns>
		public DataTable getTable(string procedureName, string nameSetToTable, params SqlParameter[] mySqlParameter)
		{
			DataTable table = new DataTable();
			try
			{
				cmd = new SqlCommand(procedureName, cnn, tran);
				cmd.CommandType = CommandType.StoredProcedure;
				da = new SqlDataAdapter(cmd);
				DataSet myDataSet = new DataSet();
				for (int i = 0; i < mySqlParameter.Length; i++)
					cmd.Parameters.Add(mySqlParameter[i]);
				//cmd.ExecuteNonQuery();
				da.Fill(myDataSet, nameSetToTable);
				table = myDataSet.Tables[nameSetToTable];
			}
			catch (SqlException ex)
			{
				tran.Rollback();
				throw new Exception(ex.Message);
			}
			return table;
		}

		/// <summary>
		/// Lấy dữ liệu đổ về thông qua Store Procedure với only parameter
		/// -- Nguyễn Văn Thao - 29/9/2009 --
		/// </summary>
		/// <param name="procedureName">Tên Store Procedure</param>
		/// <param name="nameSetToTable">Tên của table lấy ra</param>
		/// <param name="paramName">Danh sách tên param</param>
		/// <param name="paramValue">Danh sách giá trị các param</param>
		/// <returns>DataTable</returns>
		public DataTable getTable(string procedureName, string nameSetToTable, string[] paramName, object[] paramValue)
		{
			DataTable table = new DataTable();
			try
			{
				cmd = new SqlCommand(procedureName, cnn, tran);
				cmd.CommandType = CommandType.StoredProcedure;
				da = new SqlDataAdapter(cmd);
				DataSet myDataSet = new DataSet();
				SqlParameter sqlParam;
				for (int i = 0; i < paramName.Length; i++)
				{
					sqlParam = new SqlParameter(paramName[i], paramValue[i]);
					cmd.Parameters.Add(sqlParam);
				}
				//cmd.ExecuteNonQuery();
				da.Fill(myDataSet, nameSetToTable);
				table = myDataSet.Tables[nameSetToTable];
			}
			catch (SqlException ex)
			{
				tran.Rollback();
				throw new Exception(ex.Message);
			}
			return table;
		}

		public void ExecSP(string procedureName, SqlParameter mySqlParameter)
		{
			try
			{

				cmd = new SqlCommand(procedureName, cnn, tran);
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.Add(mySqlParameter);
				cmd.ExecuteNonQuery();
			}
			catch (SqlException ex)
			{
				tran.Rollback();
				throw new Exception(ex.Message);
			}
		}

		public void ExecSP(string procedureName, string[] paramName, object[] paramValue)
		{
			try
			{

				cmd = new SqlCommand(procedureName, cnn, tran);
				cmd.CommandType = CommandType.StoredProcedure;
				SqlParameter sqlParam;
				for (int i = 0; i < paramName.Length; i++)
				{
					sqlParam = new SqlParameter(paramName[i], paramValue[i]);
					cmd.Parameters.Add(sqlParam);
				}

				cmd.ExecuteNonQuery();
			}
			catch (SqlException ex)
			{
				tran.Rollback();
				throw new Exception(ex.Message);
			}
		}
		/// <summary>
		/// Tìm kiếm theo thuộc tính trả về mảng Model
		/// -- Nguyễn Văn Thao - 29/9/2009 --
		/// </summary>
		/// <param name="tableName">Tên của Table</param>
		/// <param name="fieldName">Danh sách tên của parameter</param>
		/// <param name="fieldValue">Danh sách giá trị của parameter</param>
		/// <returns>ArrayList</returns>
		public ArrayList FindByAttribute(string tableName, string fieldName, string fieldValue)
		{
			ArrayList result = new ArrayList();
			try
			{
				string sql = "select * from " + tableName + " where " + fieldName + "=" + fieldValue;
				cmd = new SqlCommand(sql, cnn, tran);
				cmd.CommandTimeout = 6000;
				cmd.CommandType = CommandType.Text;
				cmd.CommandText = sql;

				SqlDataAdapter da = new SqlDataAdapter(cmd);
				DataSet ds = new DataSet();
				da.Fill(ds, "TABLE");
				//begin get data
				string classname = getClassName(tableName);
				for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
				{
					DataRow dr = ds.Tables[0].Rows[i];
					result.Add(PopulateModel(dr, getClassName(tableName)));
				}
				//end and return
				return result;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		/// <summary>
		/// Tìm kiếm theo Expression trả về mảng Model
		/// -- Nguyễn Văn Thao - 29/9/2009 --
		/// </summary>
		/// <param name="tableName">Tên của Table</param>
		/// <param name="exp">Expression</param>
		/// <returns>ArrayList</returns>
		public ArrayList FindByExpression(string tableName, Expression exp)
		{
			ArrayList result = new ArrayList();
			try
			{
				string sql = DBUtils.SQLSelect(tableName, exp);
				cmd = new SqlCommand(sql, cnn, tran);
				cmd.CommandTimeout = 6000;
				cmd.CommandType = CommandType.Text;
				cmd.CommandText = sql;

				SqlDataAdapter da = new SqlDataAdapter(cmd);
				DataSet ds = new DataSet();
				da.Fill(ds, "TABLE");
				//begin get data
				string classname = getClassName(tableName);
				for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
				{
					DataRow dr = ds.Tables[0].Rows[i];
					result.Add(PopulateModel(dr, getClassName(tableName)));
				}
				//end and return
				return result;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		/// <summary>
		/// Tìm kiếm theo PrimaryKey
		/// -- Nguyễn Văn Thao - 29/9/2009 --
		/// </summary>
		/// <param name="tableName">Tên của Table</param>
		/// <param name="ID">PK</param>
		/// <returns>Model</returns>
		public BaseModel FindByPK(string tableName, Int64 ID)
		{
			ArrayList result = new ArrayList();
			try
			{
				string sql = "select * from " + tableName + " where ID=" + ID;
				cmd = new SqlCommand(sql, cnn, tran);
				cmd.CommandTimeout = 6000;
				cmd.CommandType = CommandType.Text;
				cmd.CommandText = sql;

				SqlDataAdapter da = new SqlDataAdapter(cmd);
				DataSet ds = new DataSet();
				da.Fill(ds, "TABLE");
				//begin get data
				string classname = getClassName(tableName);
				for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
				{
					DataRow dr = ds.Tables[0].Rows[i];
					result.Add(PopulateModel(dr, getClassName(tableName)));
				}
				if (result.Count > 0)
					return (BaseModel)result[0];
				else
					return null;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		/// <summary>
		/// Hàm insert dữ liệu sử dụng Model
		/// -- Nguyễn Văn Thao - 29/9/2009 --
		/// </summary>
		/// <param name="baseModel">Model</param>
		/// <returns>Decimal</returns>
		public decimal Insert(BaseModel baseModel)
		{
			#region Get systemDate
			DateTime sysTime = GetSystemDate();
			#endregion

			#region Khai bao cac bien cac bien connection
			string TableName = baseModel.GetType().Name.Substring(0, baseModel.GetType().Name.Length - 5);
			string sql = DBUtils.SQLInsert(baseModel);
			cmd = new SqlCommand(sql, cnn, tran);
			cmd.CommandType = CommandType.Text;
			PropertyInfo[] propertiesName = baseModel.GetType().GetProperties();
			object value;
			#endregion

			#region Gan gia tri cac command goc
			for (int i = 0; i < propertiesName.Length; i++)
			{
				value = propertiesName[i].GetValue(baseModel, null);

				if (!propertiesName[i].Name.Equals("ID"))
				{
					if (propertiesName[i].Name.ToLower().Equals("createdby") || propertiesName[i].Name.ToLower().Equals("updatedby"))
					{
						cmd.Parameters.Add("@" + propertiesName[i].Name, SqlDbType.NVarChar).Value = !String.IsNullOrEmpty(Global.AppUserName) ? Global.AppUserName : (value ?? "");
					}
					else if (propertiesName[i].Name.ToLower().Equals("createddate") || propertiesName[i].Name.ToLower().Equals("updateddate") || propertiesName[i].Name.ToLower().Equals("createdate") || propertiesName[i].Name.ToLower().Equals("updatedate"))
					{
						cmd.Parameters.Add("@" + propertiesName[i].Name, SqlDbType.DateTime).Value = sysTime;
					}
					else if (propertiesName[i].Name.ToLower().Equals("userinsertid") || propertiesName[i].Name.ToLower().Equals("userupdateid"))
					{
						cmd.Parameters.Add("@" + propertiesName[i].Name, SqlDbType.Int).Value = Global.UserID != 0 ? Global.UserID : (value ?? 0);
					}
					else if (value != null)
					{
						if (propertiesName[i].PropertyType.Equals(typeof(DateTime)))
						{
							if ((DateTime)value == DateTime.MinValue)
								value = DefValues.Sql_MinDate;
						}
						cmd.Parameters.Add("@" + propertiesName[i].Name, DBUtils.ConvertToSQLType(propertiesName[i].PropertyType)).Value = value;
					}
					else
					{
						if (propertiesName[i].PropertyType.Equals(typeof(DateTime?)))
						{
							cmd.Parameters.Add("@" + propertiesName[i].Name, DBUtils.ConvertToSQLType(propertiesName[i].PropertyType)).Value = DBNull.Value;
						}
						else
						{
							cmd.Parameters.Add("@" + propertiesName[i].Name, DBUtils.ConvertToSQLType(propertiesName[i].PropertyType)).Value = "";
						}
					}
				}
			}
			#endregion
			try
			{
				return (decimal)cmd.ExecuteScalar();
			}
			catch (SqlException se)
			{
				tran.Rollback();
				throw new Exception("Insert " + baseModel.GetType().Name + " error :" + se.Message);
			}
		}

		/// <summary>
		/// Update dữ liệu thông qua Model
		/// -- Nguyễn Văn Thao - 29/9/2009 --
		/// </summary>
		/// <param name="baseModel">Model</param>
		public void Update(BaseModel baseModel)
		{
			#region Get systemDate
			DateTime sysTime = GetSystemDate();
			#endregion

			#region Khai bao cac bien connection
			string TableName = baseModel.GetType().Name.Substring(0, baseModel.GetType().Name.Length - 5);
			string sql = DBUtils.SQLUpdate(baseModel);
			cmd = new SqlCommand(sql, cnn, tran);
			cmd.CommandType = CommandType.Text;
			PropertyInfo[] propertiesName = baseModel.GetType().GetProperties();
			object value;
			#endregion

			#region Gan cac bien vao command goc
			for (int i = 0; i < propertiesName.Length; i++)
			{
				SqlDbType dbType = DBUtils.ConvertToSQLType(propertiesName[i].PropertyType);
				value = propertiesName[i].GetValue(baseModel, null);

				if (propertiesName[i].Name.ToLower().Equals("updatedby"))
				{
					cmd.Parameters.Add("@" + propertiesName[i].Name, SqlDbType.NVarChar).Value = !String.IsNullOrEmpty(Global.AppUserName) ? Global.AppUserName : (value ?? "");
				}
				else if (propertiesName[i].Name.ToLower().Equals("updateddate") || propertiesName[i].Name.ToLower().Equals("updatedate"))
				{
					cmd.Parameters.Add("@" + propertiesName[i].Name, SqlDbType.DateTime).Value = sysTime;
				}
				else if (propertiesName[i].Name.ToLower().Equals("userupdateid"))
				{
					cmd.Parameters.Add("@" + propertiesName[i].Name, SqlDbType.Int).Value = Global.UserID != 0 ? Global.UserID : (value ?? 0);
				}
				else if (value != null)
				{
					if (propertiesName[i].PropertyType.Equals(typeof(DateTime)))
					{
						if ((DateTime)value == DateTime.MinValue)
							value = DefValues.Sql_MinDate;
					}
					cmd.Parameters.Add("@" + propertiesName[i].Name, dbType).Value = value;
				}
				else
				{
					if (propertiesName[i].PropertyType.Equals(typeof(DateTime?)))
					{
						cmd.Parameters.Add("@" + propertiesName[i].Name, dbType).Value = DBNull.Value;
					}
					else
					{
						cmd.Parameters.Add("@" + propertiesName[i].Name, dbType).Value = "";
					}
				}
			}
			#endregion

			try
			{
				cmd.ExecuteNonQuery();
			}
			catch (SqlException se)
			{
				tran.Rollback();
				throw new Exception("Update " + baseModel.GetType().Name + " error :" + se.Message);
			}
		}

		/// <summary>
		/// Hàm Update dữ liệu 
		/// </summary>
		/// <param name="TableName">Tên bảng</param>
		/// <param name="FieldExpression">Danh sách trường được lấy làm điều kiện Update</param>
		/// <param name="ValueExpression">Danh sách giá trị trường được lấy làm điều kiện Update</param>
		/// <param name="FieldChange">Danh sách trường được Update</param>
		/// <param name="ValueChange">Danh sách giá trị được Update</param>
		public void UpdateAttribute(string TableName, string[] FieldExpression, object[] ValueExpression, string[] FieldChange, object[] ValueChange)
		{
			string[] str = new string[] { "", "", "", "", "" };
			//Khai bao chuoi command
			string command = "Update " + TableName + " Set ";
			//Nhat Value
			for (int i = 0; i < FieldChange.Length; i++)
			{
				if (i != FieldChange.Length - 1)
					command = command + FieldChange[i] + "=@" + FieldChange[i] + ",";
				else
					command = command + FieldChange[i] + "=@" + FieldChange[i];
			}
			//Nhat Dieu Kien
			command = command + " where ";
			if (FieldExpression.Length == 1)
			{
				command = command + FieldExpression[0] + "=@" + FieldExpression[0];
			}
			else
			{
				for (int j = 0; j < FieldExpression.Length; j++)
				{
					if (j != FieldExpression.Length - 1)
						command = command + FieldExpression[j] + "=@" + FieldExpression[j] + " And ";
					else
						command = command + FieldExpression[j] + "=@" + FieldExpression[j];
				}
			}
			//Khai bao doi tuong command
			try
			{
				cmd = new SqlCommand(command, cnn, tran);
				cmd.CommandType = CommandType.Text;
				SqlParameter param;
				for (int i = 0; i < FieldChange.Length; i++)
				{
					param = new SqlParameter(FieldChange[i], ValueChange[i]);
					cmd.Parameters.Add(param);
				}
				for (int j = 0; j < FieldExpression.Length; j++)
				{
					param = new SqlParameter(FieldExpression[j], ValueExpression[j]);
					cmd.Parameters.Add(param);
				}

				cmd.ExecuteNonQuery();
			}
			catch (SqlException se)
			{
				tran.Rollback();
				throw new Exception("Update error :" + se.Message);
			}
		}

		/// <summary>
		/// Update dữ liệu thông qua chuỗi Command
		/// -- Nguyễn Văn Thao - 29/9/2009 --
		/// </summary>
		/// <param name="command">Chuỗi Command</param>
		public void UpdateCommand(string command)
		{
			try
			{
				cmd = new SqlCommand("spSearchAllForTrans", cnn, tran);
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.Add(new SqlParameter("@sqlCommand", command));

				cmd.ExecuteNonQuery();
			}
			catch (SqlException se)
			{
				tran.Rollback();
				throw new Exception("Update error :" + se.Message);
			}
		}

		/// <summary>
		/// Xóa dữ liệu thông qua Primary Key
		/// -- Nguyễn Văn Thao - 29/9/2009 --
		/// </summary>
		/// <param name="tableName">Tên Table</param>
		/// <param name="PKID">Primary Key</param>
		public void Delete(string tableName, int PKID)
		{
			string sql = "delete from " + tableName + " where ID=" + PKID;
			cmd = new SqlCommand(sql, cnn, tran);

			try
			{
				cmd.ExecuteNonQuery();
			}
			catch (SqlException se)
			{
				tran.Rollback();
				throw new Exception("Delete " + tableName + " error :" + se.Message);
			}
		}

		public void Delete(string tableName, string FieldName, string Value)
		{
			string sql = "delete from " + tableName + " where " + FieldName + "= '" + Value + "'";
			cmd = new SqlCommand(sql, cnn, tran);

			try
			{
				cmd.ExecuteNonQuery();
			}
			catch (SqlException se)
			{
				tran.Rollback();
				throw new Exception("Delete " + tableName + " error :" + se.Message);
			}
		}

		public void Delete(string tableName, string FieldName, int Value)
		{
			string sql = "delete from " + tableName + " where " + FieldName + "=" + Value;
			cmd = new SqlCommand(sql, cnn, tran);

			try
			{
				cmd.ExecuteNonQuery();
			}
			catch (SqlException se)
			{
				tran.Rollback();
				throw new Exception("Delete " + tableName + " error :" + se.Message);
			}
		}

		public void TruncateTable(BaseModel model)
		{
			string sql = "truncate table " + model.GetType().Name.Replace("Model", "");
			cmd = new SqlCommand(sql, cnn, tran);
			try
			{
				cmd.ExecuteNonQuery();
			}
			catch (SqlException se)
			{
				tran.Rollback();
				throw new Exception("Truncate " + model.GetType().Name.Replace("Model", "") + " error :" + se.Message);
			}
		}
		public void DeleteTableInterface(BaseModel model, string ma_cty)
		{
			DeleteTableInterface(model, ma_cty, "", "");
		}
		public void DeleteTableInterface(BaseModel model, string ma_cty, string fieldName, string so_phieu)
		{
			string sql = "delete " + model.GetType().Name.Replace("Model", "") + " where Ma_cty = '" + ma_cty + "'";
			if (!String.IsNullOrEmpty(fieldName))
				sql += " and " + fieldName + " = '" + so_phieu + "'";
			cmd = new SqlCommand(sql, cnn, tran);
			try
			{
				cmd.ExecuteNonQuery();
			}
			catch (SqlException se)
			{
				tran.Rollback();
				throw new Exception("delete " + model.GetType().Name.Replace("Model", "") + " error :" + se.Message);
			}
		}
		/// <summary>
		/// Xóa dữ liệu theo Attribute
		/// -- Nguyễn Văn Thao - 29/9/2009 --
		/// </summary>
		/// <param name="tableName">Tên table</param>
		/// <param name="FieldName">Tên Fiels</param>
		/// <param name="FieldValue">Giá trị của field</param>
		public void DeleteByAttribute(string tableName, string FieldName, string FieldValue)
		{
			string sql = "delete from " + tableName + " where " + FieldName + "=" + FieldValue;
			cmd = new SqlCommand(sql, cnn, tran);

			try
			{
				cmd.ExecuteNonQuery();
			}
			catch (SqlException se)
			{
				tran.Rollback();
				throw new Exception("Delete " + tableName + " error :" + se.Message);
			}
		}

		#endregion

		#region Cac method thuc hien voi store procedure
		public static void ExcuteNonQuery(string SPName, string ParamName, object ParamValue)
		{ }

		/// <summary>
		/// Lay ve ngay thang cua he thong.
		/// </summary>
		/// <returns></returns>
		public DateTime GetSystemDate()
		{
			try
			{
				return Convert.ToDateTime(this.Select("SELECT GETDATE() AS SystemDate").Rows[0][0]);
			}
			catch
			{
				return DateTime.Now;
			}
		}
		#endregion

		#region Mo Dong ket noi va Transaction

		/// <summary>
		/// Mở kết nối để thực hiện transacion
		/// -- Nguyễn Văn Thao - 29/9/2009 --
		/// </summary>
		public void OpenConnection()
		{
			if (cnn.State == ConnectionState.Closed)
			{
				cnn.Open();
			}
		}

		/// <summary>
		/// Bắt đầu thực hiện Transaction
		/// -- Nguyễn Văn Thao - 29/9/2009 --
		/// </summary>
		public void BeginTransaction()
		{ tran = cnn.BeginTransaction(); return; }

		/// <summary>
		/// Xác nhận lại các giao dịch
		/// -- Nguyễn Văn Thao - 29/9/2009 --
		/// </summary>
		public void CommitTransaction()
		{
			tran.Commit();
			return;
		}

		/// <summary>
		/// Hủy bỏ các giao dịch
		/// -- NHT --
		/// </summary>
		public void RollBack()
		{
			tran.Rollback();
			return;
		}

		/// <summary>
		/// Đóng kết nối
		/// -- Nguyễn Văn Thao - 29/9/2009 --
		/// </summary>
		public void CloseConnection()
		{
			if (cnn.State == ConnectionState.Open)
				cnn.Close();
		}

		#endregion


		public virtual DataTable LoadDataFromSP(string procedureName, string nameSetToTable, string[] paramName, object[] paramValue)
		{
			DataTable table = new DataTable();
			SqlParameter sqlParam;

			try
			{
				cmd = new SqlCommand(procedureName, cnn, tran);
				cmd.CommandType = CommandType.StoredProcedure;
				SqlDataAdapter mySqlDataAdapter = new SqlDataAdapter(cmd);

				DataSet myDataSet = new DataSet();
				if (paramName != null)
				{
					for (int i = 0; i < paramName.Length; i++)
					{
						sqlParam = new SqlParameter(paramName[i], paramValue[i]);
						cmd.Parameters.Add(sqlParam);
					}
				}
				//cmd.ExecuteNonQuery();

				mySqlDataAdapter.Fill(myDataSet, nameSetToTable);

				table = myDataSet.Tables[nameSetToTable];
			}
			catch (SqlException e)
			{
				tran.Rollback();
				throw new FacadeException(e.Message);
			}
			return table;
		}

	}
}
