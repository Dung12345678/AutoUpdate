using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Data.Filtering.Helpers;
using System.Diagnostics;
using System.IO;
using System.Data.OleDb;
using System.Globalization;
using BMS.Model;
using BMS.Utils;
using BMS.Business;
using ExcelDataReader;
using IE.Model;
using IE.Utils;
using IE.Business;
using Expressions = IE.Utils.Expression;
using Expression = BMS.Utils.Expression;
using ExpressionHP = HP.Utils.Expression;
using HP.Model;
using HP.Business;
using System.Text.RegularExpressions;
using System.Reflection;

namespace BMS
{

	public partial class frmUpdateDate : _Forms
	{
		DateTime dateTimeOld;
		DateTime dateTimeOldpathOrder;
		DateTime dateTimeOldpathSTD;
		DateTime dateTimeOldpathDao;
		DateTime dateTimeOldpathLOT;
		int _startDao = 1;
		int _startXuatEx = 1;
		int _startMotor = 1;
		int _startOrderPart = 1;
		int _startOrderPart1 = 1;
		int _startSonPlan = 2;
		int _startUpdateDateSTD = 1;
		int _startUpdateDateLOT = 1;
		int _Copy = 0;
		int _Copy1 = 0;

		private Thread _threadUpdateDateMotor;
		private Thread _threadUpdateDateOrderPart1;
		private Thread _threadUpdateDateOrderPart;
		private Thread _threadUpdateSonPlan;
		private Thread _threadUpdateDateDao;
		private Thread _threadUpdateDateSTD;
		private Thread _threadUpdateDateLOT;
		private Thread _threadXuatFileExcel;
		private Thread _threadXoaTrung;

		string pathOrderPart = Application.StartupPath + "/UpdateDate.txt";
		string pathOrderPart1 = Application.StartupPath + "/UpdateDateOrderPart.txt";
		string pathSubSonPlan = Application.StartupPath + "/UpdateDateSonPlan.txt";
		string pathMotor = Application.StartupPath + "/UpdateDateMotor.txt";
		string pathDao = Application.StartupPath + "/UpdateDateDao.txt";
		string pathSTD = Application.StartupPath + "/UpdateDateSTD.txt";
		string pathLOT = Application.StartupPath + "/UpdateDateLOT.txt";
		string pathSaves = System.Windows.Forms.Application.StartupPath + "/SaveOrder.txt";
		string pathPlanHypAndAltax = System.Windows.Forms.Application.StartupPath + "/SavePlanHypAndAltax.txt";
		string targetPath = @"D:\FileUpdate";
		string targetPathError = @"U:\Public\0. Kaizen Altax\DUNG\RTC\UpdateError";
		string pathBrowseMotor = "";
		//string pathOrder = "";
		//string path = "";
		string pathSonPlan = "";
		string pathSave = "";
		string pathPlanHypandAltaxs = "";
		string pathBrowseDao = "";
		string pathBrowseSTD = "";
		string pathBrowseLOT = "";

		DataTable dttOrderPart1 = new DataTable();
		DataTable dttOrderPart = new DataTable();
		DataTable dttSonPlan = new DataTable();
		DataTable dtMotor = new DataTable();
		DataTable dttDao = new DataTable();
		DataTable dttSTD = new DataTable();
		DataTable dttLOT = new DataTable();
		private DataSet dsSonPlan = new DataSet();
		public frmUpdateDate()
		{
			InitializeComponent();
			CheckFile();
		}
		void CheckFile()
		{
			if (!File.Exists(pathSubSonPlan))
			{
				File.WriteAllText(pathSubSonPlan, "");
			}
			if (!File.Exists(pathPlanHypAndAltax))
			{
				File.WriteAllText(pathPlanHypAndAltax, "");
			}
			if (!File.Exists(pathOrderPart))
			{
				File.WriteAllText(pathOrderPart, "");
			}
			if (!File.Exists(pathOrderPart1))
			{
				File.WriteAllText(pathOrderPart1, "");
			}
			if (!File.Exists(pathMotor))
			{
				File.WriteAllText(pathMotor, "");
			}
			if (!File.Exists(pathDao))
			{
				File.WriteAllText(pathDao, "");
			}
			if (!File.Exists(pathSTD))
			{
				File.WriteAllText(pathSTD, "");
			}
			if (!File.Exists(pathLOT))
			{
				File.WriteAllText(pathLOT, "");
			}
			if (!Directory.Exists(targetPath))
			{
				Directory.CreateDirectory(targetPath);
			}
			try
			{
				if (!Directory.Exists(targetPathError))
				{
					Directory.CreateDirectory(targetPathError);
				}
			}
			catch
			{

			}
		}
		private void frmUpdateDate_Load(object sender, EventArgs e)
		{
			//Update SonPlan
			pathSonPlan = File.ReadAllText(pathSubSonPlan);
			btnBrowseSonPlan.Text = pathSonPlan;
			//thread save UpdateDate
			_threadUpdateSonPlan = new Thread(UpdateDateSonPlan);
			_threadUpdateSonPlan.IsBackground = true;
			_threadUpdateSonPlan.Start();

			btnBrowseOrderPart1.Text = File.ReadAllText(pathOrderPart1);

			//thread save UpdateDateOrderPart1
			_threadUpdateDateOrderPart1 = new Thread(UpdateDateOrderPart1);
			_threadUpdateDateOrderPart1.IsBackground = true;
			_threadUpdateDateOrderPart1.Start();


			btnBrowseOrderPart.Text = File.ReadAllText(pathOrderPart);
			//thread save UpdateDateOrderPart
			_threadUpdateDateOrderPart = new Thread(UpdateDateOrderPart);
			_threadUpdateDateOrderPart.IsBackground = true;
			_threadUpdateDateOrderPart.Start();

			//thread save UpdateMotor
			pathBrowseMotor = File.ReadAllText(pathMotor);
			btnBrowseMotor.Text = pathBrowseMotor;
			_threadUpdateDateMotor = new Thread(UpdateDateMotor);
			_threadUpdateDateMotor.IsBackground = true;
			_threadUpdateDateMotor.Start();

			////thread xuất ra file excel lúc 4h chiều
			//_threadXuatFileExcel = new Thread(ExportExcel);
			//_threadXuatFileExcel.IsBackground = true;
			//_threadXuatFileExcel.Start();

			//thread xóa trùng nhau order
			_threadXoaTrung = new Thread(DeleteOrderPart);
			_threadXoaTrung.IsBackground = true;
			_threadXoaTrung.Start();

			pathSave = File.ReadAllText(pathSaves);
			btnCopyOrder.Text = pathSave;
			pathPlanHypandAltaxs = File.ReadAllText(pathPlanHypAndAltax);
			btnXuatExcel.Text = pathPlanHypandAltaxs;

			pathBrowseDao = File.ReadAllText(pathDao);
			btnBrowseDao.Text = pathBrowseDao;
			//thread save UpdateDateDao 2 tiếng update 1 lần 
			_threadUpdateDateDao = new Thread(UpdateDateDao);
			_threadUpdateDateDao.IsBackground = true;
			_threadUpdateDateDao.Start();

			pathBrowseSTD = File.ReadAllText(pathSTD);
			btnBrowseSTD.Text = pathBrowseSTD;
			//thread save UpdateDateDao 2 tiếng update 1 lần 
			_threadUpdateDateSTD = new Thread(UpdateDateSTD);
			_threadUpdateDateSTD.IsBackground = true;
			_threadUpdateDateSTD.Start();

			pathBrowseLOT = File.ReadAllText(pathLOT);
			btnBrowseLOT.Text = pathBrowseLOT;
			//thread save UpdateDateDao 2 tiếng update 1 lần 
			_threadUpdateDateLOT = new Thread(UpdateDateLOT);
			_threadUpdateDateLOT.IsBackground = true;
			_threadUpdateDateLOT.Start();

		}

		void UpdateDateLOT()
		{
			while (true)
			{
				Thread.Sleep(5000);
				if (_startUpdateDateLOT == 1)
				{
					try
					{
						string Path1 = "";
						if (btnBrowseLOT.Text.Trim() == "") continue;
						DateTime dateTime = File.GetLastWriteTime(btnBrowseLOT.Text.Trim());
						if (dateTime != dateTimeOldpathLOT)
						{
							//Copy vào file @"D:\FileUpdate" trên server
							dateTimeOldpathLOT = dateTime;
							try
							{
								//Copy file Save vào thư mục 
								string sourcePath = btnBrowseLOT.Text;
								string[] PathSplit = btnBrowseLOT.Text.Trim().Split('\\');
								Path1 = PathSplit[PathSplit.Length - 1];
								//Đường dẫn file Update
								string sourceFile = System.IO.Path.Combine(sourcePath);
								string destFile = System.IO.Path.Combine(targetPath, Path1);
								//Copy file từ file nguồn đến file đích
								System.IO.File.Copy(sourceFile, destFile, true);
							}
							catch
							{

							}

						}
						else
						{
							continue;
						}
						_startUpdateDateLOT = 0;
						if (btnBrowseLOT.Text.Trim() == "") continue;
						if (Path.GetExtension(@"D:\FileUpdate\" + Path1).ToUpper() == ".TXT")
						{
							string filename = @"D:\FileUpdate\" + Path1;
							//Tạo bảng
							dttLOT = new DataTable();
							//THêm cột vào bảng
							dttLOT.Columns.Add("F1");
							dttLOT.Columns.Add("F2");
							dttLOT.Columns.Add("F3");
							dttLOT.Columns.Add("F4");
							dttLOT.Columns.Add("F5");
							dttLOT.Columns.Add("F6");
							dttLOT.Columns.Add("F7");
							dttLOT.Columns.Add("F8");
							dttLOT.Columns.Add("F9");
							dttLOT.Columns.Add("F10");
							dttLOT.Columns.Add("F11");
							dttLOT.Columns.Add("F12");
							dttLOT.Columns.Add("F13");
							dttLOT.Columns.Add("F14");
							dttLOT.Columns.Add("F15");
							dttLOT.Columns.Add("F16");
							dttLOT.Columns.Add("F17");
							dttLOT.Columns.Add("F18");
							dttLOT.Columns.Add("F19");
							dttLOT.Columns.Add("F20");
							dttLOT.Columns.Add("F21");
							dttLOT.Columns.Add("F22");
							dttLOT.Columns.Add("F23");
							dttLOT.Columns.Add("F24");
							dttLOT.Columns.Add("F25");
							dttLOT.Columns.Add("F26");

							//gọi hàm đọc file txt
							string noidung = Lib.GetFileContentTXT(filename);
							//Cắt xuống dòng -"\n"
							string[] strContent = noidung.Split('\n');
							foreach (string dong in strContent)
							{

								if (String.IsNullOrEmpty(dong))
									break;

								//Cắt dấu "|"
								string[] _dong = dong.Split('\t');
								if (_dong.Count() < 25) continue;
								DataRow dr1 = dttLOT.NewRow();
								dr1["F1"] = _dong[0];//Br
								dr1["F2"] = _dong[1];//GoodsCode
								dr1["F3"] = _dong[2];
								dr1["F4"] = _dong[3];//OrderMachining
								dr1["F5"] = _dong[4];
								dr1["F6"] = _dong[5];
								dr1["F7"] = _dong[6];
								dr1["F8"] = _dong[7];//CreateDate
								dr1["F9"] = _dong[8];
								dr1["F10"] = _dong[9];
								dr1["F11"] = _dong[10];//Quantity
								dr1["F12"] = _dong[11];
								dr1["F13"] = _dong[12];
								dr1["F14"] = _dong[13];
								dr1["F15"] = _dong[14];
								dr1["F16"] = _dong[15];
								dr1["F17"] = _dong[16];
								dr1["F18"] = _dong[17];
								dr1["F19"] = _dong[18];
								dr1["F20"] = _dong[19];
								dr1["F21"] = _dong[20];
								dr1["F22"] = _dong[21];
								dr1["F23"] = _dong[22];
								dr1["F24"] = _dong[23];
								dr1["F25"] = _dong[24];
								dr1["F26"] = _dong[25];
								dttLOT.Rows.Add(dr1);
							}
							this.Invoke((MethodInvoker)delegate
							{
								SaveLOT();
							});
						}
					}
					catch (Exception ex)
					{
						dateTimeOldpathLOT = DateTime.Now;
						_startUpdateDateLOT = 1;
					}
				}
			}
		}
		async void SaveLOT()
		{
			Task task = Task.Factory.StartNew(() =>
			{
				int rowCount = dttLOT.Rows.Count;
				for (int i = 0; i < rowCount; i++)
				{
					try
					{
						string StepCode = TextUtils.ToString(dttLOT.Rows[i]["F1"]);//StepCode
						string ArticleID = TextUtils.ToString(dttLOT.Rows[i]["F2"]);//ArticleID
						string OrderMachining = TextUtils.ToString(dttLOT.Rows[i]["F4"]);//OrderMachining
						string HM = TextUtils.ToString(dttLOT.Rows[i]["F5"]);
						//Kiểm tra nếu mã nhóm hoặc mã sản phẩm trống thì next
						if (string.IsNullOrEmpty(StepCode) || string.IsNullOrEmpty(ArticleID))
						{
							continue;
						}
						ExpressionHP exp2 = new ExpressionHP("StepCode", StepCode);
						ExpressionHP exp1 = new ExpressionHP("ArticleID", ArticleID);
						ExpressionHP exp3 = new ExpressionHP("OrderMachining", OrderMachining);
						ExpressionHP exp4 = new ExpressionHP("HM", HM);
						ArrayList arr = LotBO.Instance.FindByExpression(exp1.And(exp2).And(exp3).And(exp4));
						if (arr.Count > 0) continue;
						LotModel lotModel = new LotModel();

						#region SetValue
						lotModel.StepCode = StepCode;
						lotModel.ArticleID = ArticleID;
						lotModel.OrderMachining = OrderMachining;
						lotModel.HM = HM;
						lotModel.JGDate = TextUtils.ToDate2(dttLOT.Rows[i]["F7"]);
						lotModel.Worker = TextUtils.ToString(dttLOT.Rows[i]["F9"]);
						#endregion
						lotModel.CreateDate = DateTime.Now;
						lotModel.UpdateDate = DateTime.Now;
						lotModel.ID = (int)LotBO.Instance.Insert(lotModel);

					}
					catch
					{
						//ErrorLog.errorLog("Chạy save orderPart", $"{ex}", Environment.NewLine);
					}
				}
			});

			await task;
			_startUpdateDateLOT = 1;
		}
		void UpdateDateSTD()
		{
			while (true)
			{
				Thread.Sleep(5000);
				if (_startUpdateDateSTD == 1)
				{
					try
					{
						string Path1 = "";
						if (btnBrowseSTD.Text.Trim() == "") continue;
						DateTime dateTime = File.GetLastWriteTime(btnBrowseSTD.Text.Trim());
						if (dateTime != dateTimeOldpathSTD)
						{
							dateTimeOldpathSTD = dateTime;
							try
							{
								//Copy file Save vào thư mục 
								string sourcePath = btnBrowseSTD.Text;
								string[] PathSplit = btnBrowseSTD.Text.Trim().Split('\\');
								Path1 = PathSplit[PathSplit.Length - 1];
								//Đường dẫn file Update

								string sourceFile = System.IO.Path.Combine(sourcePath);
								string destFile = System.IO.Path.Combine(targetPath, Path1);
								//Copy file từ file nguồn đến file đích
								System.IO.File.Copy(sourceFile, destFile, true);
							}
							catch
							{

							}
						}
						else
						{
							continue;
						}
						_startUpdateDateSTD = 0;
						if (btnBrowseSTD.Text.Trim() == "") continue;
						if (Path.GetExtension(@"D:\FileUpdate\" + Path1).ToUpper() == ".TXT")
						{
							string filename = @"D:\FileUpdate\" + Path1;
							//Tạo bảng
							dttSTD = new DataTable();
							#region
							//THêm cột vào bảng
							dttSTD.Columns.Add("F1");
							dttSTD.Columns.Add("F2");
							dttSTD.Columns.Add("F3");
							dttSTD.Columns.Add("F4");
							dttSTD.Columns.Add("F5");
							dttSTD.Columns.Add("F6");
							dttSTD.Columns.Add("F7");
							dttSTD.Columns.Add("F8");
							dttSTD.Columns.Add("F9");
							dttSTD.Columns.Add("F10");
							dttSTD.Columns.Add("F11");
							dttSTD.Columns.Add("F12");
							dttSTD.Columns.Add("F13");
							dttSTD.Columns.Add("F14");
							dttSTD.Columns.Add("F15");
							dttSTD.Columns.Add("F16");
							dttSTD.Columns.Add("F17");
							dttSTD.Columns.Add("F18");
							dttSTD.Columns.Add("F19");
							dttSTD.Columns.Add("F20");
							dttSTD.Columns.Add("F21");
							dttSTD.Columns.Add("F22");
							dttSTD.Columns.Add("F23");
							dttSTD.Columns.Add("F24");
							dttSTD.Columns.Add("F25");
							dttSTD.Columns.Add("F26");
							dttSTD.Columns.Add("F27");
							dttSTD.Columns.Add("F28");
							dttSTD.Columns.Add("F29");
							dttSTD.Columns.Add("F30");
							dttSTD.Columns.Add("F31");
							dttSTD.Columns.Add("F32");
							dttSTD.Columns.Add("F33");
							dttSTD.Columns.Add("F34");
							dttSTD.Columns.Add("F35");
							dttSTD.Columns.Add("F36");
							dttSTD.Columns.Add("F37");
							dttSTD.Columns.Add("F38");
							dttSTD.Columns.Add("F39");
							dttSTD.Columns.Add("F40");
							dttSTD.Columns.Add("F41");
							dttSTD.Columns.Add("F42");
							dttSTD.Columns.Add("F43");
							dttSTD.Columns.Add("F44");
							dttSTD.Columns.Add("F45");
							dttSTD.Columns.Add("F46");
							dttSTD.Columns.Add("F47");
							dttSTD.Columns.Add("F48");
							#endregion
							//gọi hàm đọc file txt
							string noidung = Lib.GetFileContentTXT(filename);
							//Cắt xuống dòng -"\n"
							string[] strContent = noidung.Split('\n');
							foreach (string dong in strContent)
							{

								if (String.IsNullOrEmpty(dong))
									break;

								//Cắt dấu "|"
								string[] _dong = dong.Split('\t');
								if (_dong.Count() < 47) continue;
								DataRow dr1 = dttSTD.NewRow();
								#region
								dr1["F1"] = _dong[0];//Br
								dr1["F2"] = _dong[1];//GoodsCode
								dr1["F3"] = _dong[2];
								dr1["F4"] = _dong[3];//OrderMachining
								dr1["F5"] = _dong[4];
								dr1["F6"] = _dong[5];
								dr1["F7"] = _dong[6];
								dr1["F8"] = _dong[7];//CreateDate
								dr1["F9"] = _dong[8];
								dr1["F10"] = _dong[9];
								dr1["F11"] = _dong[10];//Quantity
								dr1["F12"] = _dong[11];
								dr1["F13"] = _dong[12];
								dr1["F14"] = _dong[13];
								dr1["F15"] = _dong[14];
								dr1["F16"] = _dong[15];
								dr1["F17"] = _dong[16];
								dr1["F18"] = _dong[17];
								dr1["F19"] = _dong[18];
								dr1["F20"] = _dong[19];
								dr1["F21"] = _dong[20];
								dr1["F22"] = _dong[21];
								dr1["F23"] = _dong[22];
								dr1["F24"] = _dong[23];
								dr1["F25"] = _dong[24];
								dr1["F26"] = _dong[25];
								dr1["F27"] = _dong[26];
								dr1["F28"] = _dong[27];
								dr1["F29"] = _dong[28];
								dr1["F30"] = _dong[29];
								dr1["F31"] = _dong[30];
								dr1["F32"] = _dong[31];
								dr1["F33"] = _dong[32];
								dr1["F34"] = _dong[33];
								dr1["F35"] = _dong[34];
								dr1["F36"] = _dong[35];
								dr1["F37"] = _dong[36];
								dr1["F38"] = _dong[37];
								dr1["F39"] = _dong[38];
								dr1["F40"] = _dong[39];
								dr1["F41"] = _dong[40];
								dr1["F42"] = _dong[41];
								dr1["F43"] = _dong[42];
								dr1["F44"] = _dong[43];
								dr1["F45"] = _dong[44];
								dr1["F46"] = _dong[45];
								dr1["F47"] = _dong[46];
								dr1["F48"] = _dong[47];
								#endregion
								dttSTD.Rows.Add(dr1);
							}



							this.Invoke((MethodInvoker)delegate
							{
								SaveSTD();
							});

						}


					}
					catch (Exception ex)
					{
						dateTimeOldpathSTD = DateTime.Now;
						_startUpdateDateSTD = 1;
					}
				}
			}
		}
		async void SaveSTD()
		{
			Task task = Task.Factory.StartNew(() =>
			{
				int rowCount = dttSTD.Rows.Count;
				for (int i = 0; i < rowCount; i++)
				{
					try
					{
						string StepCode = TextUtils.ToString(dttSTD.Rows[i]["F1"]);//StepCode
						string ArticleID = TextUtils.ToString(dttSTD.Rows[i]["F2"]);//ArticleID
						int STT = TextUtils.ToInt(TextUtils.ToDouble(dttSTD.Rows[i]["F4"]));//STT

						//Kiểm tra nếu mã nhóm hoặc mã sản phẩm trống thì next
						if (string.IsNullOrEmpty(StepCode) || string.IsNullOrEmpty(ArticleID))
						{
							continue;
						}
						ExpressionHP exp2 = new ExpressionHP("StepCode", StepCode);
						ExpressionHP exp1 = new ExpressionHP("ArticleID", ArticleID);
						ExpressionHP exp3 = new ExpressionHP("STT", STT);
						ArrayList arr = STDBO.Instance.FindByExpression(exp1.And(exp2).And(exp3));
						if (arr.Count > 0) continue;
						STDModel sTDModel = new STDModel();

						#region SetValue
						//string a = "";
						sTDModel.StepCode = StepCode;
						sTDModel.ArticleID = ArticleID;
						sTDModel.STT = STT;
						sTDModel.SmallGroup = TextUtils.ToString(dttSTD.Rows[i]["F7"]);
						sTDModel.WorkingName = TextUtils.ToString(dttSTD.Rows[i]["F13"]);
						//if (STT == 1 || STT == 2|| STT=)
						//	sTDModel.ValueTypeName = TextUtils.ToInt(TextUtils.ToDouble(dttSTD.Rows[i]["F7"]));
						//sTDModel.ValueType = Shelf;
						sTDModel.Unit = TextUtils.ToString(dttSTD.Rows[i]["F15"]);
						sTDModel.OriginalValue = TextUtils.ToDecimal(dttSTD.Rows[i]["F26"]);// giá trị ban đầu
						sTDModel.MaxAllowance = TextUtils.ToDecimal(dttSTD.Rows[i]["F27"]);//Dung sai max 
						sTDModel.MinAllowance = TextUtils.ToDecimal(dttSTD.Rows[i]["F28"]);//Dung sai min
						sTDModel.ToleranceValueMax = TextUtils.ToDecimal(dttSTD.Rows[i]["F30"]);//giá trị sau khi tính dung sai Max
						sTDModel.ToleranceValueMin = TextUtils.ToDecimal(dttSTD.Rows[i]["F31"]);//giá trị sau khi tính dung sai Min
						#endregion
						sTDModel.CreateDate = DateTime.Now;
						sTDModel.UpdateDate = DateTime.Now;
						sTDModel.ID = (int)STDBO.Instance.Insert(sTDModel);

					}
					catch
					{
						//ErrorLog.errorLog("Chạy save orderPart", $"{ex}", Environment.NewLine);
					}
				}
			});

			await task;
			_startUpdateDateSTD = 1;
		}
		/// <summary>
		/// Xóa các mã trùng nhau khi 4 h 
		/// </summary>
		void DeleteOrderPart()
		{
			while (true)
			{
				Thread.Sleep(5000);
				DateTime dateTimeStart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 16, 01, 01);
				DateTime dateTimeEnd = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 16, 01, 08);
				if ((DateTime.Now >= dateTimeStart && DateTime.Now <= dateTimeEnd))
				{
					try
					{//Xóa các Order có AritceID trùng nhau
						TextUtils.ExcuteProcedure("spDeleteDuplicate", new string[] { }, new object[] { });
						//MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
					}
					catch (Exception ex)
					{
						//MessageBox.Show($"{ex}", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					}
				}
			}
		}
		void ExportExcel()
		{
			while (true)
			{
				Thread.Sleep(5000);
				try
				{
					if (btnXuatExcel.Text.Trim() == "") continue;
					DateTime dateTimeStart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 16, 01, 01);
					DateTime dateTimeEnd = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 16, 01, 06);
					if ((DateTime.Now >= dateTimeStart && DateTime.Now <= dateTimeEnd) && _startXuatEx == 2)
					{
						_startXuatEx = 1;
					}
					if (_startXuatEx == 1)
					{
						_startXuatEx = 0;
						//Hiển thị bảng datatable line Altax
						DataTable dtLineAltax = TextUtils.Select("SELECT p.AssemblyProduct as OrderCode,p.ProductCode,p.Qty AS OrderQty, o.ArticleID, o.Description,o.Qty, o.Shelf, pg.ProductGroupCode FROM [ShiStock].[dbo].[OrderPart] o JOIN [SumitomoTest].[dbo].[ProductionPlan] p ON o.OrderCodeAndCnt = p.OrderCodeFull JOIN	[SumitomoTest].[dbo].Product d ON p.ProductCode=d.ProductCode JOIN [SumitomoTest].[dbo].ProductGroup pg ON d.ProductGroupID= pg.ID WHERE p.Status = 0");
						//Hiển thị bảng Datatable Line Hyp
						DataTable dtLineHyp = TextUtils.Select("SELECT p.AssemblyProduct as OrderCode,p.ProductCode,p.Qty AS OrderQty, o.ArticleID, o.Description,o.Qty, o.Shelf, pg.ProductGroupCode FROM [ShiStock].[dbo].[OrderPart] o JOIN [SumitomoHyp].[dbo].[ProductionPlan] p ON o.OrderCodeAndCnt = p.OrderCodeFull JOIN	[SumitomoHyp].[dbo].Product d ON p.ProductCode=d.ProductCode JOIN [SumitomoHyp].[dbo].ProductGroup pg ON d.ProductGroupID= pg.ID WHERE p.Status = 0");

						//Xuaasst ra file
						Lib.ExportToExcel(dtLineAltax, btnXuatExcel.Text.Trim() + "\\LineAltax" + $"{ DateTime.Now.ToString("dd-MM-yyyy hh-mm")}");
						Lib.ExportToExcel(dtLineHyp, btnXuatExcel.Text.Trim() + "\\LineHyp" + $"{ DateTime.Now.ToString("dd-MM-yyyy hh-mm")}");
						_startXuatEx = 2;
					}
				}
				catch (Exception ex)
				{
					//MessageBox.Show($"{ex}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
					////_start1 = 1;
				}
			}
		}
		void UpdateDateDao()
		{
			while (true)
			{
				Thread.Sleep(5000);
				try
				{
					if (_startDao == 1)
					{
						string Path1 = "";
						if (btnBrowseDao.Text.Trim() == "") continue;
						DateTime dateTime = File.GetLastWriteTime(btnBrowseDao.Text.Trim());
						if (dateTime != dateTimeOldpathDao)
						{
							dateTimeOldpathDao = dateTime;
							try
							{
								//Copy file Save vào thư mục 
								string sourcePath = btnBrowseDao.Text;
								string[] PathSplit = btnBrowseDao.Text.Trim().Split('\\');
								Path1 = PathSplit[PathSplit.Length - 1];
								//Đường dẫn file Update
								string sourceFile = System.IO.Path.Combine(sourcePath);
								string destFile = System.IO.Path.Combine(targetPath, Path1);
								//Copy file từ file nguồn đến file đích
								System.IO.File.Copy(sourceFile, destFile, true);
							}
							catch
							{

							}
						}
						else
						{
							continue;
						}
						if (btnBrowseDao.Text.Trim() == "") continue;

						_startDao = 0;
						List<string> lstCode = new List<string>();
						List<string> lstCount = new List<string>();
						List<string> lstInt = new List<string>();
						List<CInspectionData> lstcInspectionDatas = new List<CInspectionData>();

						string Order1 = "";
						string Order2 = "";
						string Order3 = "";
						if (Path.GetExtension(@"D:\FileUpdate\" + Path1).ToUpper() == ".TXT")
						{
							string filename = @"D:\FileUpdate\" + Path1;
							//Tạo bảng
							dttDao = new DataTable();
							#region
							//THêm cột vào bảng
							dttDao.Columns.Add("F1");
							dttDao.Columns.Add("F2");
							dttDao.Columns.Add("F3");
							dttDao.Columns.Add("F4");
							dttDao.Columns.Add("F5");
							dttDao.Columns.Add("F6");
							dttDao.Columns.Add("F7");
							dttDao.Columns.Add("F8");
							dttDao.Columns.Add("F9");
							dttDao.Columns.Add("F10");
							dttDao.Columns.Add("F11");
							dttDao.Columns.Add("F12");
							dttDao.Columns.Add("F13");
							dttDao.Columns.Add("F14");
							dttDao.Columns.Add("F15");
							dttDao.Columns.Add("F16");
							dttDao.Columns.Add("F17");
							dttDao.Columns.Add("F18");
							dttDao.Columns.Add("F19");
							dttDao.Columns.Add("F20");
							dttDao.Columns.Add("F21");
							dttDao.Columns.Add("F22");
							dttDao.Columns.Add("F23");
							dttDao.Columns.Add("F24");
							dttDao.Columns.Add("F25");
							dttDao.Columns.Add("F26");
							dttDao.Columns.Add("F27");
							dttDao.Columns.Add("F28");
							dttDao.Columns.Add("F29");
							dttDao.Columns.Add("F30");
							dttDao.Columns.Add("F31");
							dttDao.Columns.Add("F32");
							dttDao.Columns.Add("F33");
							dttDao.Columns.Add("F34");
							dttDao.Columns.Add("F35");
							dttDao.Columns.Add("F36");
							dttDao.Columns.Add("F37");
							dttDao.Columns.Add("F38");


							dttDao.Columns.Add("F39");
							dttDao.Columns.Add("F40");
							dttDao.Columns.Add("F41");
							dttDao.Columns.Add("F42");
							dttDao.Columns.Add("F43");
							dttDao.Columns.Add("F44");
							dttDao.Columns.Add("F45");
							dttDao.Columns.Add("F46");
							dttDao.Columns.Add("F47");
							dttDao.Columns.Add("F48");
							dttDao.Columns.Add("F49");
							dttDao.Columns.Add("F50");
							dttDao.Columns.Add("F51");
							dttDao.Columns.Add("F52");
							dttDao.Columns.Add("F53");
							dttDao.Columns.Add("F54");
							dttDao.Columns.Add("F55");
							dttDao.Columns.Add("F56");
							dttDao.Columns.Add("F57");
							dttDao.Columns.Add("F58");
							dttDao.Columns.Add("F59");
							dttDao.Columns.Add("F60");
							dttDao.Columns.Add("F61");
							dttDao.Columns.Add("F62");
							dttDao.Columns.Add("F63");
							dttDao.Columns.Add("F64");
							dttDao.Columns.Add("F65");
							dttDao.Columns.Add("F66");
							dttDao.Columns.Add("F67");
							dttDao.Columns.Add("F68");
							dttDao.Columns.Add("F69");
							dttDao.Columns.Add("F70");
							dttDao.Columns.Add("F71");
							dttDao.Columns.Add("F72");
							dttDao.Columns.Add("F73");
							dttDao.Columns.Add("F74");
							dttDao.Columns.Add("F75");
							dttDao.Columns.Add("F76");
							dttDao.Columns.Add("F77");
							dttDao.Columns.Add("F78");
							dttDao.Columns.Add("F79");
							dttDao.Columns.Add("F80");
							dttDao.Columns.Add("F81");
							dttDao.Columns.Add("F82");
							dttDao.Columns.Add("F83");
							dttDao.Columns.Add("F84");
							dttDao.Columns.Add("F85");
							dttDao.Columns.Add("F86");
							dttDao.Columns.Add("F87");
							dttDao.Columns.Add("F88");
							dttDao.Columns.Add("F89");
							dttDao.Columns.Add("F90");
							dttDao.Columns.Add("F91");
							dttDao.Columns.Add("F92");
							dttDao.Columns.Add("F93");
							dttDao.Columns.Add("F94");
							dttDao.Columns.Add("F95");
							dttDao.Columns.Add("F96");
							dttDao.Columns.Add("F97");
							dttDao.Columns.Add("F98");
							dttDao.Columns.Add("F99");
							dttDao.Columns.Add("F100");
							dttDao.Columns.Add("F101");
							dttDao.Columns.Add("F102");
							dttDao.Columns.Add("F103");
							dttDao.Columns.Add("F104");
							dttDao.Columns.Add("F105");
							dttDao.Columns.Add("F106");
							dttDao.Columns.Add("F107");
							dttDao.Columns.Add("F108");
							dttDao.Columns.Add("F109");
							dttDao.Columns.Add("F110");
							dttDao.Columns.Add("F111");
							dttDao.Columns.Add("F112");
							dttDao.Columns.Add("F113");
							dttDao.Columns.Add("F114");
							dttDao.Columns.Add("F115");
							dttDao.Columns.Add("F116");
							dttDao.Columns.Add("F117");
							dttDao.Columns.Add("F118");
							dttDao.Columns.Add("F119");
							dttDao.Columns.Add("F120");
							dttDao.Columns.Add("F121");
							dttDao.Columns.Add("F122");
							dttDao.Columns.Add("F123");
							dttDao.Columns.Add("F124");
							dttDao.Columns.Add("F125");
							dttDao.Columns.Add("F126");
							dttDao.Columns.Add("F127");
							dttDao.Columns.Add("F128");
							dttDao.Columns.Add("F129");
							dttDao.Columns.Add("F130");
							dttDao.Columns.Add("F131");
							dttDao.Columns.Add("F132");
							dttDao.Columns.Add("F133");
							dttDao.Columns.Add("F134");
							dttDao.Columns.Add("F135");
							dttDao.Columns.Add("F136");
							dttDao.Columns.Add("F137");
							dttDao.Columns.Add("F138");
							dttDao.Columns.Add("F139");
							dttDao.Columns.Add("F140");
							dttDao.Columns.Add("F141");
							dttDao.Columns.Add("F142");
							dttDao.Columns.Add("F143");
							dttDao.Columns.Add("F144");
							dttDao.Columns.Add("F145");
							dttDao.Columns.Add("F146");
							dttDao.Columns.Add("F147");
							dttDao.Columns.Add("F148");
							dttDao.Columns.Add("F149");
							dttDao.Columns.Add("F150");
							dttDao.Columns.Add("F151");
							dttDao.Columns.Add("F152");
							dttDao.Columns.Add("F153");
							dttDao.Columns.Add("F154");
							dttDao.Columns.Add("F155");
							dttDao.Columns.Add("F156");
							dttDao.Columns.Add("F157");
							dttDao.Columns.Add("F158");
							dttDao.Columns.Add("F159");
							dttDao.Columns.Add("F160");
							dttDao.Columns.Add("F161");
							dttDao.Columns.Add("F162");
							dttDao.Columns.Add("F163");
							#endregion
							DataTable dtOld = dttDao.Clone();

							//gọi hàm đọc file txt
							string noidung = Lib.GetFileContentTXT(filename);
							//Cắt xuống dòng -"\n"
							string OrderNew = "";
							string OrderOld = "";
							string[] _dongOld = new string[16];
							string[] _dongCount = new string[16];
							string[] strContent = noidung.Split('\n');
							int CheckIs1Or2 = 0;
							foreach (string dong in strContent)
							{
								if (String.IsNullOrEmpty(dong))
									continue;
								//Cắt dấu "|"
								//string[] _dong = dong.Split('\t');
								string[] _dong = dong.Split('\t');
								OrderNew = _dong[1] + _dong[3] + _dong[7];
								//Khi Order khác thì thêm vào bảng
								if (OrderOld != OrderNew)
								{
									// add vào bảng
									if (Order1.Trim() == Order2.Trim())
									{
										for (int i = 0; i < dtOld.Rows.Count; i++)
										{
											#region
											DataRow dr1 = dttDao.NewRow();
											dr1["F1"] = _dongOld[0];//Br
											dr1["F2"] = _dongOld[1];//GoodsCode
											dr1["F3"] = _dongOld[2];
											dr1["F4"] = _dongOld[3];//OrderMachining
											dr1["F5"] = _dongOld[4];
											dr1["F6"] = _dongOld[5];
											dr1["F7"] = _dongOld[6];
											dr1["F8"] = _dongOld[7];//CreateDate
											dr1["F9"] = _dongOld[8];
											dr1["F10"] = _dongOld[9];
											dr1["F11"] = _dongOld[10];//Quantity
											dr1["F12"] = _dongOld[11];
											dr1["F13"] = _dongOld[12];
											dr1["F14"] = _dongOld[13];
											dr1["F15"] = _dongOld[14];

											string so = Regex.Replace(TextUtils.ToString(dtOld.Rows[i]["F6"]), ",", string.Empty).Trim(); //tách mã số trong code 
											dr1["F16"] = TextUtils.ToString(dtOld.Rows[i]["F5"]) + so;//Code	
											if (dr1["F16"].ToString().Trim() == "") continue;

											dr1["F17"] = TextUtils.ToDate2(dtOld.Rows[i]["F17"]);//Date
											dr1["F18"] = TextUtils.ToString(dtOld.Rows[i]["F18"]);
											dr1["F19"] = TextUtils.ToString(dtOld.Rows[i]["F19"]);

											//dr1["F17"] = lstcInspectionDatas[i].Row4;//Date
											//dr1["F18"] = lstcInspectionDatas[i].Row5;//Worker
											//dr1["F19"] = lstcInspectionDatas[i].Row6;//NameLocal
											for (int j = 20; j < dtOld.Columns.Count; j++)
											{
												dr1[$"F{j}"] = TextUtils.ToString(dtOld.Rows[i][$"F{j}"]);
											}
											#endregion
											dttDao.Rows.Add(dr1);
										}
										dtOld.Clear();
									}
									OrderOld = OrderNew;
									_dongOld = _dong;
									Order1 = "";
									Order2 = "";
								}
								int CheckDong0 = TextUtils.ToInt(_dong[6]);
								_dongCount = _dong;
								string[] AddDong = _dong[15].Split('|');
								//add các dòng đã cắt dấu "|" vào các dòng của bảng
								if (TextUtils.ToInt(_dong[6]) == 1 || TextUtils.ToInt(_dong[6]) == 2)
								{
									CheckIs1Or2 = 0;
									//string[] AddDong = _dong[15].Split('|');

									if (TextUtils.ToInt(_dong[6]) == 1)
									{
										try
										{
											Order1 = _dong[1];
											for (int i = 0; i < AddDong.Count(); i++)
											{
												DataRow dataRow = dtOld.NewRow();
												for (int j = 0; j < dttDao.Columns.Count; j++)
												{
													dataRow[dttDao.Columns[j].Caption] = "";
												}
												dataRow["F5"] = AddDong[i]; //mã code
												dtOld.Rows.Add(dataRow);
												//lstCode.Add(AddDong[i]);
											}
										}
										catch (Exception ex)
										{
											try
											{
												//Copy file Save vào thư mục 
												string sourcePath = btnBrowseDao.Text;
												string[] PathSplit = btnBrowseDao.Text.Trim().Split('\\');
												string PathError = PathSplit[PathSplit.Length - 1];
												//Đường dẫn file Update
												string sourceFile = System.IO.Path.Combine(sourcePath);
												string destFile = System.IO.Path.Combine(targetPathError, "InsertDao" + DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss") + ".txt");
												//Copy file từ file nguồn đến file đích
												System.IO.File.Copy(sourceFile, destFile, true);

												//ErrorLog.errorLog("Lỗi file Update Dao", $"{ex}", Environment.NewLine);
											}
											catch
											{

											}
										}

									}
									else if (TextUtils.ToInt(_dong[6]) == 2)
									{
										try
										{
											Order2 = _dong[1];
											for (int i = 0; i < AddDong.Count(); i++)
											{
												dtOld.Rows[i]["F6"] = AddDong[i];//giá trị số trong code
																				 //lstInt.Add(AddDong[i]);
											}
										}
										catch (Exception ex)
										{
											try
											{
												//Copy file Save vào thư mục 
												string sourcePath = btnBrowseDao.Text;
												string[] PathSplit = btnBrowseDao.Text.Trim().Split('\\');
												string PathError = PathSplit[PathSplit.Length - 1];
												//Đường dẫn file Update
												string sourceFile = System.IO.Path.Combine(sourcePath);
												string destFile = System.IO.Path.Combine(targetPathError, "InsertDao" + DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss") + ".txt");
												//Copy file từ file nguồn đến file đích
												System.IO.File.Copy(sourceFile, destFile, true);

												//ErrorLog.errorLog("Lỗi file Update Dao", $"{ex}", Environment.NewLine);
											}
											catch
											{

											}
										}
									}
								}

								//18 giá trị được lưu Max;Min;Giá trị
								for (int i = 0; i < AddDong.Count(); i++)
								{
									if (dtOld.Rows.Count <= i) continue;
									//Lấy số thứ tự để Add vào từng list
									switch (TextUtils.ToInt(_dong[6]))
									{
										#region
										case 0:
											//dtOld.Rows[i][$"F"]
											//lstcInspectionDatas[i].Row1 = AddDong[i];
											break;
										case 1:
											//dtOld.Rows[i][$"F"]
											//lstcInspectionDatas[i].Row1 = AddDong[i];
											break;
										case 2:
											//dtOld.Rows[i][$"F{j}"]
											//lstcInspectionDatas[i].Row2 = AddDong[i];
											break;
										case 3:
											//dtOld.Rows[i][$"F{j}"]
											//lstcInspectionDatas[i].Row2 = AddDong[i];
											break;
										case 4:
											//dtOld.Rows[i][$"F{j}"]
											//lstcInspectionDatas[i].Row4 = AddDong[i];
											break;
										case 5:
											dtOld.Rows[i][$"F18"] = AddDong[i];
											//lstcInspectionDatas[i].Row5 = AddDong[i];
											break;
										case 6:
											dtOld.Rows[i][$"F19"] = AddDong[i];
											//lstcInspectionDatas[i].Row6 = AddDong[i];
											break;
										case 7:
											//dtOld.Rows[i][$"F"]
											//lstcInspectionDatas[i].Row1 = AddDong[i];
											break;
										case 8:
											//dtOld.Rows[i][$"F"]
											//lstcInspectionDatas[i].Row1 = AddDong[i];
											break;
										case 9:
											//dtOld.Rows[i][$"F"]
											//lstcInspectionDatas[i].Row1 = AddDong[i];
											break;
										//case 7:
										//	lstcInspectionDatas[i].Row7 = AddDong[i];
										//	break;

										default:
											for (int j = 20; j < dtOld.Columns.Count; j++)
											{
												if (TextUtils.ToString(dtOld.Rows[i][$"F{j}"]).Trim() == "")
												{
													dtOld.Rows[i][$"F{j}"] = AddDong[i];
													dtOld.Rows[i][$"F{j + 1}"] = $"{_dong[11]}";
													dtOld.Rows[i][$"F{j + 2}"] = $"{_dong[12]}";
													break;
												}
											}
											break;

											#endregion
									}
								}
								//dtOld.Rows.Add();
							}
							//Add vào giá trị cuối cùng vào bảng 
							if (dtOld.Rows.Count > 0 && Order1.Trim() == Order2.Trim())
							{
								for (int i = 0; i < dtOld.Rows.Count; i++)
								{
									DataRow dr1 = dttDao.NewRow();
									dr1["F1"] = _dongOld[0];//Br
									dr1["F2"] = _dongOld[1];//GoodsCode
									dr1["F3"] = _dongOld[2];
									dr1["F4"] = _dongOld[3];//OrderMachining
									dr1["F5"] = _dongOld[4];
									dr1["F6"] = _dongOld[5];
									dr1["F7"] = _dongOld[6];
									dr1["F8"] = _dongOld[7];//CreateDate
									dr1["F9"] = _dongOld[8];
									dr1["F10"] = _dongOld[9];
									dr1["F11"] = _dongOld[10];//Quantity
									dr1["F12"] = _dongOld[11];
									dr1["F13"] = _dongOld[12];
									dr1["F14"] = _dongOld[13];
									dr1["F15"] = _dongOld[14];

									string so = Regex.Replace(TextUtils.ToString(dtOld.Rows[i]["F6"]), ",", string.Empty).Trim(); //tách mã số trong code 
									dr1["F16"] = TextUtils.ToString(dtOld.Rows[i]["F5"]) + so;//Code	
									if (dr1["F16"].ToString().Trim() == "") continue;
									dr1["F17"] = TextUtils.ToString(dtOld.Rows[i]["F17"]);//Date
									dr1["F18"] = TextUtils.ToString(dtOld.Rows[i]["F18"]);
									dr1["F19"] = TextUtils.ToString(dtOld.Rows[i]["F19"]);
									for (int j = 20; j < dtOld.Columns.Count; j++)
									{
										dr1[$"F{j}"] = TextUtils.ToString(dtOld.Rows[i][$"F{j}"]);
									}
									dttDao.Rows.Add(dr1);

								}
								dtOld.Clear();
								OrderOld = OrderNew;
								Order1 = "";
								Order2 = "";
							}
							this.Invoke((MethodInvoker)delegate
							{
								SaveDao();
							});
						}
					}
				}
				catch (Exception ex)
				{
					try
					{
						//Copy file Save vào thư mục 
						string sourcePath = btnBrowseDao.Text;
						string[] PathSplit = btnBrowseDao.Text.Trim().Split('\\');
						string PathError = PathSplit[PathSplit.Length - 1];
						//Đường dẫn file Update
						string sourceFile = System.IO.Path.Combine(sourcePath);
						string destFile = System.IO.Path.Combine(targetPathError, "InsertDao" + DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss") + ".txt");
						//Copy file từ file nguồn đến file đích
						System.IO.File.Copy(sourceFile, destFile, true);
					//	ErrorLog.errorLog("Lỗi file Update Dao", $"{ex}", Environment.NewLine);
					}
					catch
					{

					}
					dateTimeOldpathDao = DateTime.Now;
					_startDao = 1;
				}
			}
		}
		async void SaveDao()
		{
			Task task = Task.Factory.StartNew(() =>
			{
				int rowCount = dttDao.Rows.Count;
				for (int i = 0; i < rowCount; i++)
				{
					try
					{
						string _KnifeCode = Lib.ToString(dttDao.Rows[i]["F16"]);
						if (string.IsNullOrEmpty(_KnifeCode))
						{
							continue;
						}
						ProductKnifeModel productKnifeModel = new ProductKnifeModel();

						#region SetValue
						//string a = "";
						productKnifeModel.Code = _KnifeCode;//Code
						productKnifeModel.StepCode = Lib.ToString(dttDao.Rows[i]["F1"]);// Công đoạn
						productKnifeModel.OrderMachining = Lib.ToString(dttDao.Rows[i]["F4"]);//order
						productKnifeModel.ArticleID = Lib.ToString(dttDao.Rows[i]["F2"]);// mã hàng 
						productKnifeModel.Date = Lib.ToDate3(dttDao.Rows[i]["F8"]);//date
						productKnifeModel.Quantity = Lib.ToInt(dttDao.Rows[i]["F11"]);//SỐ lượng
						productKnifeModel.Worker = Lib.ToString(dttDao.Rows[i]["F18"]);// Tên người làm
						productKnifeModel.NameLocal = Lib.ToString(dttDao.Rows[i]["F19"]);// Tên máy

						productKnifeModel.RealValue = Lib.ToString(dttDao.Rows[i]["F20"]);// Giá trị thực tế 1
						productKnifeModel.RealValueMin = Lib.ToString(dttDao.Rows[i]["F21"]);// Giá trị thực tế 1
						productKnifeModel.RealValueMax = Lib.ToString(dttDao.Rows[i]["F22"]);// Giá trị thực tế 1

						productKnifeModel.RealValue1 = Lib.ToString(dttDao.Rows[i]["F23"]);// Giá trị thực tế 1
						productKnifeModel.RealValue1Min = Lib.ToString(dttDao.Rows[i]["F24"]);// Giá trị thực tế 1
						productKnifeModel.RealValue1Max = Lib.ToString(dttDao.Rows[i]["F25"]);// Giá trị thực tế 1

						productKnifeModel.RealValue2 = Lib.ToString(dttDao.Rows[i]["F26"]);// Giá trị thực tế 1
						productKnifeModel.RealValue2Min = Lib.ToString(dttDao.Rows[i]["F27"]);// Giá trị thực tế 1
						productKnifeModel.RealValue2Max = Lib.ToString(dttDao.Rows[i]["F28"]);// Giá trị thực tế 1

						productKnifeModel.RealValue3 = Lib.ToString(dttDao.Rows[i]["F29"]);// Giá trị thực tế 1
						productKnifeModel.RealValue3Min = Lib.ToString(dttDao.Rows[i]["F30"]);// Giá trị thực tế 1
						productKnifeModel.RealValue3Max = Lib.ToString(dttDao.Rows[i]["F31"]);// Giá trị thực tế 1

						productKnifeModel.RealValue4 = Lib.ToString(dttDao.Rows[i]["F32"]);// Giá trị thực tế 1
						productKnifeModel.RealValue4Min = Lib.ToString(dttDao.Rows[i]["F33"]);// Giá trị thực tế 1
						productKnifeModel.RealValue4Max = Lib.ToString(dttDao.Rows[i]["F34"]);// Giá trị thực tế 1

						productKnifeModel.RealValue5 = Lib.ToString(dttDao.Rows[i]["F35"]);// Giá trị thực tế 1
						productKnifeModel.RealValue5Min = Lib.ToString(dttDao.Rows[i]["F36"]);// Giá trị thực tế 1
						productKnifeModel.RealValue5Max = Lib.ToString(dttDao.Rows[i]["F37"]);// Giá trị thực tế 1

						productKnifeModel.RealValue6 = Lib.ToString(dttDao.Rows[i]["F38"]);// Giá trị thực tế 1
						productKnifeModel.RealValue6Min = Lib.ToString(dttDao.Rows[i]["F39"]);// Giá trị thực tế 1
						productKnifeModel.RealValue6Max = Lib.ToString(dttDao.Rows[i]["F40"]);

						productKnifeModel.RealValue7 = Lib.ToString(dttDao.Rows[i]["F41"]);
						productKnifeModel.RealValue7Min = Lib.ToString(dttDao.Rows[i]["F42"]);
						productKnifeModel.RealValue7Max = Lib.ToString(dttDao.Rows[i]["F43"]);

						productKnifeModel.RealValue8 = Lib.ToString(dttDao.Rows[i]["F44"]);
						productKnifeModel.RealValue8Min = Lib.ToString(dttDao.Rows[i]["F45"]);
						productKnifeModel.RealValue8Max = Lib.ToString(dttDao.Rows[i]["F46"]);

						productKnifeModel.RealValue9 = Lib.ToString(dttDao.Rows[i]["F47"]);
						productKnifeModel.RealValue9Min = Lib.ToString(dttDao.Rows[i]["F48"]);
						productKnifeModel.RealValue9Max = Lib.ToString(dttDao.Rows[i]["F49"]);

						productKnifeModel.RealValue10 = Lib.ToString(dttDao.Rows[i]["F50"]);
						productKnifeModel.RealValue10Min = Lib.ToString(dttDao.Rows[i]["F51"]);
						productKnifeModel.RealValue10Max = Lib.ToString(dttDao.Rows[i]["F52"]);

						productKnifeModel.RealValue11 = Lib.ToString(dttDao.Rows[i]["F53"]);
						productKnifeModel.RealValue11Min = Lib.ToString(dttDao.Rows[i]["F54"]);
						productKnifeModel.RealValue11Max = Lib.ToString(dttDao.Rows[i]["F55"]);

						productKnifeModel.RealValue12 = Lib.ToString(dttDao.Rows[i]["F56"]);
						productKnifeModel.RealValue12Min = Lib.ToString(dttDao.Rows[i]["F57"]);
						productKnifeModel.RealValue12Max = Lib.ToString(dttDao.Rows[i]["F58"]);

						productKnifeModel.RealValue13 = Lib.ToString(dttDao.Rows[i]["F59"]);
						productKnifeModel.RealValue13Min = Lib.ToString(dttDao.Rows[i]["F60"]);
						productKnifeModel.RealValue13Max = Lib.ToString(dttDao.Rows[i]["F61"]);

						productKnifeModel.RealValue14 = Lib.ToString(dttDao.Rows[i]["F62"]);
						productKnifeModel.RealValue14Min = Lib.ToString(dttDao.Rows[i]["F63"]);
						productKnifeModel.RealValue14Max = Lib.ToString(dttDao.Rows[i]["F64"]);

						productKnifeModel.RealValue15 = Lib.ToString(dttDao.Rows[i]["F65"]);
						productKnifeModel.RealValue15Min = Lib.ToString(dttDao.Rows[i]["F66"]);
						productKnifeModel.RealValue15Max = Lib.ToString(dttDao.Rows[i]["F67"]);

						productKnifeModel.RealValue16 = Lib.ToString(dttDao.Rows[i]["F68"]);
						productKnifeModel.RealValue16Min = Lib.ToString(dttDao.Rows[i]["F69"]);
						productKnifeModel.RealValue16Max = Lib.ToString(dttDao.Rows[i]["F70"]);

						productKnifeModel.RealValue17 = Lib.ToString(dttDao.Rows[i]["F71"]);
						productKnifeModel.RealValue17Min = Lib.ToString(dttDao.Rows[i]["F72"]);
						productKnifeModel.RealValue17Max = Lib.ToString(dttDao.Rows[i]["F73"]);

						productKnifeModel.RealValue18 = Lib.ToString(dttDao.Rows[i]["F74"]);
						productKnifeModel.RealValue18Min = Lib.ToString(dttDao.Rows[i]["F75"]);
						productKnifeModel.RealValue18Max = Lib.ToString(dttDao.Rows[i]["F76"]);

						productKnifeModel.RealValue19 = Lib.ToString(dttDao.Rows[i]["F77"]);
						productKnifeModel.RealValue19Min = Lib.ToString(dttDao.Rows[i]["F78"]);
						productKnifeModel.RealValue19Max = Lib.ToString(dttDao.Rows[i]["F79"]);

						productKnifeModel.RealValue20 = Lib.ToString(dttDao.Rows[i]["F80"]);
						productKnifeModel.RealValue20Min = Lib.ToString(dttDao.Rows[i]["F81"]);
						productKnifeModel.RealValue20Max = Lib.ToString(dttDao.Rows[i]["F82"]);

						productKnifeModel.RealValue21 = Lib.ToString(dttDao.Rows[i]["F83"]);
						productKnifeModel.RealValue21Min = Lib.ToString(dttDao.Rows[i]["F84"]);
						productKnifeModel.RealValue21Max = Lib.ToString(dttDao.Rows[i]["F85"]);

						productKnifeModel.RealValue22 = Lib.ToString(dttDao.Rows[i]["F86"]);
						productKnifeModel.RealValue22Min = Lib.ToString(dttDao.Rows[i]["F87"]);
						productKnifeModel.RealValue22Max = Lib.ToString(dttDao.Rows[i]["F88"]);

						productKnifeModel.RealValue23 = Lib.ToString(dttDao.Rows[i]["F89"]);
						productKnifeModel.RealValue23Min = Lib.ToString(dttDao.Rows[i]["F90"]);
						productKnifeModel.RealValue23Max = Lib.ToString(dttDao.Rows[i]["F91"]);

						productKnifeModel.RealValue24 = Lib.ToString(dttDao.Rows[i]["F92"]);
						productKnifeModel.RealValue24Min = Lib.ToString(dttDao.Rows[i]["F93"]);
						productKnifeModel.RealValue24Max = Lib.ToString(dttDao.Rows[i]["F94"]);

						productKnifeModel.RealValue25 = Lib.ToString(dttDao.Rows[i]["F95"]);
						productKnifeModel.RealValue25Min = Lib.ToString(dttDao.Rows[i]["F96"]);
						productKnifeModel.RealValue25Max = Lib.ToString(dttDao.Rows[i]["F97"]);

						productKnifeModel.RealValue26 = Lib.ToString(dttDao.Rows[i]["F98"]);
						productKnifeModel.RealValue26Min = Lib.ToString(dttDao.Rows[i]["F99"]);
						productKnifeModel.RealValue26Max = Lib.ToString(dttDao.Rows[i]["F100"]);

						productKnifeModel.RealValue27 = Lib.ToString(dttDao.Rows[i]["F101"]);
						productKnifeModel.RealValue27Min = Lib.ToString(dttDao.Rows[i]["F102"]);
						productKnifeModel.RealValue27Max = Lib.ToString(dttDao.Rows[i]["F103"]);

						productKnifeModel.RealValue28 = Lib.ToString(dttDao.Rows[i]["F104"]);
						productKnifeModel.RealValue28Min = Lib.ToString(dttDao.Rows[i]["F105"]);
						productKnifeModel.RealValue28Max = Lib.ToString(dttDao.Rows[i]["F106"]);

						productKnifeModel.RealValue29 = Lib.ToString(dttDao.Rows[i]["F107"]);
						productKnifeModel.RealValue29Min = Lib.ToString(dttDao.Rows[i]["F108"]);
						productKnifeModel.RealValue29Max = Lib.ToString(dttDao.Rows[i]["F109"]);

						productKnifeModel.RealValue30 = Lib.ToString(dttDao.Rows[i]["F110"]);
						productKnifeModel.RealValue30Min = Lib.ToString(dttDao.Rows[i]["F111"]);
						productKnifeModel.RealValue30Max = Lib.ToString(dttDao.Rows[i]["F112"]);

						productKnifeModel.RealValue31 = Lib.ToString(dttDao.Rows[i]["F113"]);
						productKnifeModel.RealValue31Min = Lib.ToString(dttDao.Rows[i]["F114"]);
						productKnifeModel.RealValue31Max = Lib.ToString(dttDao.Rows[i]["F115"]);

						productKnifeModel.RealValue32 = Lib.ToString(dttDao.Rows[i]["F116"]);
						productKnifeModel.RealValue32Min = Lib.ToString(dttDao.Rows[i]["F117"]);
						productKnifeModel.RealValue32Max = Lib.ToString(dttDao.Rows[i]["F118"]);

						productKnifeModel.RealValue33 = Lib.ToString(dttDao.Rows[i]["F119"]);
						productKnifeModel.RealValue33Min = Lib.ToString(dttDao.Rows[i]["F120"]);
						productKnifeModel.RealValue33Max = Lib.ToString(dttDao.Rows[i]["F121"]);

						productKnifeModel.RealValue34 = Lib.ToString(dttDao.Rows[i]["F122"]);
						productKnifeModel.RealValue34Min = Lib.ToString(dttDao.Rows[i]["F123"]);
						productKnifeModel.RealValue34Max = Lib.ToString(dttDao.Rows[i]["F124"]);

						productKnifeModel.RealValue35 = Lib.ToString(dttDao.Rows[i]["F125"]);
						productKnifeModel.RealValue35Min = Lib.ToString(dttDao.Rows[i]["F126"]);
						productKnifeModel.RealValue35Max = Lib.ToString(dttDao.Rows[i]["F127"]);

						productKnifeModel.RealValue36 = Lib.ToString(dttDao.Rows[i]["F128"]);
						productKnifeModel.RealValue36Min = Lib.ToString(dttDao.Rows[i]["F129"]);
						productKnifeModel.RealValue36Max = Lib.ToString(dttDao.Rows[i]["F130"]);

						productKnifeModel.RealValue37 = Lib.ToString(dttDao.Rows[i]["F131"]);
						productKnifeModel.RealValue37Min = Lib.ToString(dttDao.Rows[i]["F132"]);
						productKnifeModel.RealValue37Max = Lib.ToString(dttDao.Rows[i]["F133"]);

						productKnifeModel.RealValue38 = Lib.ToString(dttDao.Rows[i]["F134"]);
						productKnifeModel.RealValue38Min = Lib.ToString(dttDao.Rows[i]["F135"]);
						productKnifeModel.RealValue38Max = Lib.ToString(dttDao.Rows[i]["F136"]);

						productKnifeModel.RealValue39 = Lib.ToString(dttDao.Rows[i]["F137"]);
						productKnifeModel.RealValue39Min = Lib.ToString(dttDao.Rows[i]["F138"]);
						productKnifeModel.RealValue39Max = Lib.ToString(dttDao.Rows[i]["F139"]);

						productKnifeModel.RealValue40 = Lib.ToString(dttDao.Rows[i]["F140"]);
						productKnifeModel.RealValue40Min = Lib.ToString(dttDao.Rows[i]["F141"]);
						productKnifeModel.RealValue40Max = Lib.ToString(dttDao.Rows[i]["F142"]);

						productKnifeModel.RealValue41 = Lib.ToString(dttDao.Rows[i]["F143"]);
						productKnifeModel.RealValue41Min = Lib.ToString(dttDao.Rows[i]["F144"]);
						productKnifeModel.RealValue41Max = Lib.ToString(dttDao.Rows[i]["F145"]);

						productKnifeModel.RealValue42 = Lib.ToString(dttDao.Rows[i]["F146"]);
						productKnifeModel.RealValue42Min = Lib.ToString(dttDao.Rows[i]["F147"]);
						productKnifeModel.RealValue42Max = Lib.ToString(dttDao.Rows[i]["F148"]);

						productKnifeModel.RealValue43 = Lib.ToString(dttDao.Rows[i]["F149"]);
						productKnifeModel.RealValue43Min = Lib.ToString(dttDao.Rows[i]["F150"]);
						productKnifeModel.RealValue43Max = Lib.ToString(dttDao.Rows[i]["F151"]);

						productKnifeModel.RealValue44 = Lib.ToString(dttDao.Rows[i]["F152"]);
						productKnifeModel.RealValue44Min = Lib.ToString(dttDao.Rows[i]["F153"]);
						productKnifeModel.RealValue44Max = Lib.ToString(dttDao.Rows[i]["F154"]);

						productKnifeModel.RealValue45 = Lib.ToString(dttDao.Rows[i]["F155"]);
						productKnifeModel.RealValue45Min = Lib.ToString(dttDao.Rows[i]["F156"]);
						productKnifeModel.RealValue45Max = Lib.ToString(dttDao.Rows[i]["F157"]);

						productKnifeModel.RealValue46 = Lib.ToString(dttDao.Rows[i]["F158"]);
						productKnifeModel.RealValue46Min = Lib.ToString(dttDao.Rows[i]["F159"]);
						productKnifeModel.RealValue46Max = Lib.ToString(dttDao.Rows[i]["F160"]);

						productKnifeModel.RealValue47 = Lib.ToString(dttDao.Rows[i]["F161"]);
						productKnifeModel.RealValue47Min = Lib.ToString(dttDao.Rows[i]["F162"]);
						productKnifeModel.RealValue47Max = Lib.ToString(dttDao.Rows[i]["F163"]);

						//productKnifeModel.RealValue48 = Lib.ToString(dttDao.Rows[i]["F164"]);
						//productKnifeModel.RealValue48Min = Lib.ToString(dttDao.Rows[i]["F165"]);
						//productKnifeModel.RealValue48Max = Lib.ToString(dttDao.Rows[i]["F166"]);

						//productKnifeModel.RealValue49 = Lib.ToString(dttDao.Rows[i]["F167"]);
						//productKnifeModel.RealValue49Min = Lib.ToString(dttDao.Rows[i]["F168"]);
						//productKnifeModel.RealValue49Max = Lib.ToString(dttDao.Rows[i]["F169"]);



						#endregion

						ExpressionHP exp1 = new ExpressionHP("Code", _KnifeCode);
						ExpressionHP exp2 = new ExpressionHP("StepCode", productKnifeModel.StepCode);

						ArrayList arr = ProductKnifeBO.Instance.FindByExpression(exp1.And(exp2));
						if (arr.Count == 0)
						{
							ProductKnifeBO.Instance.Insert(productKnifeModel);
						}
					}
					catch (Exception ex)
					{
						//MessageBox.Show("Lỗi lưu dữ liệu tại dòng " + i + Environment.NewLine + ex.ToString());
					}
				}
			});

			await task;
			_startDao = 1;
		}


		void UpdateDateMotor()
		{
			while (true)
			{
				Thread.Sleep(5000);
				try
				{
					this.Invoke((MethodInvoker)delegate
					{
						string Path1 = "";
						if (btnBrowseMotor.Text.Trim() == "") return;
						DateTime dateTimeStart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 08, 00, 01);
						DateTime dateTimeEnd = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 08, 59, 59);
						DateTime dateTimeStart1 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 01, 00, 01);
						DateTime dateTimeEnd1 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 01, 59, 59);
						if (((DateTime.Now >= dateTimeStart && DateTime.Now <= dateTimeEnd) || (DateTime.Now >= dateTimeStart1 && DateTime.Now <= dateTimeEnd1)) && _startMotor == 2)
						{
							_startMotor = 1;
						}
						if (_startMotor == 1)
						{
							DateTime dateTime = File.GetLastWriteTime(btnBrowseMotor.Text.Trim());
							if (dateTime != dateTimeOld)
							{
								dateTimeOld = dateTime;
								try
								{
									//Copy file Save vào thư mục 
									string sourcePath = btnBrowseMotor.Text;
									string[] PathSplit = btnBrowseMotor.Text.Trim().Split('\\');
									Path1 = PathSplit[PathSplit.Length - 1];
									//Đường dẫn file Update
									string sourceFile = System.IO.Path.Combine(sourcePath);
									string destFile = System.IO.Path.Combine(targetPath, Path1);
									//Copy file từ file nguồn đến file đích
									System.IO.File.Copy(sourceFile, destFile, true);
								}
								catch
								{

								}
							}
							else
							{
								return;
							}
							_startMotor = 0;
							if (Path.GetExtension(@"D:\FileUpdate\" + Path1).ToUpper() == ".TXT")
							{
								string filename = @"D:\FileUpdate\" + Path1;
								//Tạo bảng
								dtMotor = new DataTable();
								//THêm cột vào bảng
								dtMotor.Columns.Add("F1");
								dtMotor.Columns.Add("F2");
								dtMotor.Columns.Add("F3");
								dtMotor.Columns.Add("F4");
								dtMotor.Columns.Add("F5");
								dtMotor.Columns.Add("F6");
								dtMotor.Columns.Add("F7");
								dtMotor.Columns.Add("F8");
								dtMotor.Columns.Add("F9");
								dtMotor.Columns.Add("F10");
								dtMotor.Columns.Add("F11");
								dtMotor.Columns.Add("F12");
								//gọi hàm đọc file txt
								string noidung = Lib.GetFileContentTXT(filename);
								//Cắt xuống dòng -"\n"
								string[] strContent = noidung.Split('\n');
								foreach (string dong in strContent)
								{
									if (String.IsNullOrEmpty(dong))
										break;
									//Cắt dấu "|"
									string[] _dong = dong.Split('|');

									//add các dồng đã cắt dấu "|" vào các dòng của bảng
									DataRow dr = dtMotor.NewRow();

									dr["F1"] = _dong[0];
									dr["F2"] = _dong[1];
									dr["F3"] = _dong[2];
									dr["F4"] = _dong[3];
									dr["F5"] = _dong[4];
									dr["F6"] = _dong[5];
									dr["F7"] = _dong[6];
									dr["F8"] = _dong[7];
									dr["F9"] = _dong[8];
									dr["F10"] = _dong[9];
									dr["F11"] = _dong[10];
									dr["F12"] = _dong[11];
									dtMotor.Rows.Add(dr);
								}
								this.Invoke((MethodInvoker)delegate
								{
									if (dtMotor.Rows.Count <= 0) return;
									dtMotor.Columns.Add("DATEF1", typeof(DateTime));
									for (int i = 1; i < dtMotor.Rows.Count; i++)
									{
										if (string.IsNullOrEmpty(TextUtils.ToString(dtMotor.Rows[i]["F5"])))
										{
											continue;
										}
										if (TextUtils.ToString(dtMotor.Rows[i]["F1"]).Trim() != "")
											dtMotor.Rows[i]["DATEF1"] = DateTime.FromOADate(TextUtils.ToDouble(dtMotor.Rows[i]["F1"]));
									}
									this.Invoke((MethodInvoker)delegate
									{
										SaveMotor();
									});

								});
							}

						}
					});
				}
				catch
				{
					_startMotor = 1;
				}
			}
		}
		async void SaveMotor()
		{
			Task task = Task.Factory.StartNew(() =>
			{
				int rowCount = dtMotor.Rows.Count;
				for (int i = 0; i < rowCount; i++)
				{
					try
					{
						string _CardNo = Lib.ToString(dtMotor.Rows[i]["F5"]);
						string ArticleID = Lib.ToString(dtMotor.Rows[i]["F8"]);
						//Kiểm tra nếu mã nhóm hoặc mã sản phẩm trống thì next
						if (string.IsNullOrEmpty(_CardNo) || string.IsNullOrEmpty(ArticleID))
						{
							continue;
						}
						CheckMotorModel _CheckMotorModel = new CheckMotorModel();

						#region SetValue
						//string a = "";
						_CheckMotorModel.CardNo = _CardNo;
						_CheckMotorModel.ArticleID = ArticleID;
						_CheckMotorModel.SalesOrder = Lib.ToString(dtMotor.Rows[i]["F2"]);
						_CheckMotorModel.OrderedQty = Lib.ToInt(dtMotor.Rows[i]["F3"]);
						_CheckMotorModel.QtyOfShipOrder = Lib.ToInt(dtMotor.Rows[i]["F4"]);
						_CheckMotorModel.MotorInspSealNo = Lib.ToString(dtMotor.Rows[i]["F6"]);
						_CheckMotorModel.SerialNo = Lib.ToString(dtMotor.Rows[i]["F7"]);
						_CheckMotorModel.AssemblyOrderNo = Lib.ToString(dtMotor.Rows[i]["F9"]);
						_CheckMotorModel.NoOfJG = Lib.ToInt(dtMotor.Rows[i]["F10"]);
						_CheckMotorModel.Descriptions = Lib.ToString(dtMotor.Rows[i]["F11"]);
						_CheckMotorModel.UPR = Lib.ToString(dtMotor.Rows[i]["F12"]);
						_CheckMotorModel.JGDate = Lib.ToDate2(dtMotor.Rows[i]["F1"]);//date
						if (Lib.ToDate3(dtMotor.Rows[i]["F1"]) == new DateTime(1950, 1, 1))
						{
							_CheckMotorModel.JGDate = Lib.ToDate3(dtMotor.Rows[i]["DATEF1"].ToString());
						}
						#endregion
						Expressions exp1 = new Expressions("CardNo", _CardNo);
						Expressions exp2 = new Expressions("MotorInspSealNo", _CheckMotorModel.MotorInspSealNo);
						Expressions exp3 = new Expressions("SerialNo", _CheckMotorModel.SerialNo);
						ArrayList arr = CheckMotorBO.Instance.FindByExpression(exp1.And(exp2).And(exp3));
						if (arr.Count > 0)
						{

						}
						else
						{
							CheckMotorBO.Instance.Insert(_CheckMotorModel);
						}

					}
					catch (Exception ex)
					{
						//MessageBox.Show("Lỗi lưu dữ liệu tại dòng " + i + Environment.NewLine + ex.ToString());
					}
				}
			});
			await task;
			_startMotor = 2;
		}
		void UpdateDateSonPlan()
		{
			while (true)
			{
				Thread.Sleep(5000);
				try
				{
					DateTime dateTimeStart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 08, 00, 01);
					DateTime dateTimeEnd = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 08, 59, 59);
					DateTime dateTimeStart1 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 00, 01);
					DateTime dateTimeEnd1 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);
					if (((DateTime.Now >= dateTimeStart && DateTime.Now <= dateTimeEnd) || (DateTime.Now >= dateTimeStart1 && DateTime.Now <= dateTimeEnd1)) && _startSonPlan == 2)
					{
						_startSonPlan = 1;
					}
					if (_startSonPlan == 1)
					{
						_startSonPlan = 0;
						if (pathSonPlan == "") continue;
						try
						{
							var stream = new FileStream(btnBrowseSonPlan.Text, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

							var sw = new Stopwatch();
							sw.Start();

							IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream);

							var openTiming = sw.ElapsedMilliseconds;

							dsSonPlan = reader.AsDataSet(new ExcelDataSetConfiguration()
							{
								UseColumnDataType = false,
								ConfigureDataTable = (tableReader) => new ExcelDataTableConfiguration()
								{
									UseHeaderRow = false
								}
							});
							//var tablenames = GetTablenames(ds.Tables);
							//cboSheet.DataSource = tablenames;
						}
						catch (Exception ex)
						{
							_startSonPlan = 2;
							//ErrorLog.errorLog("Chạy DataReader lỗi khi xuất ra DataSet", " ", Environment.NewLine);
						}
						if (dsSonPlan == null) continue;
						if (dsSonPlan.Tables.Count <= 0) continue;
						this.Invoke((MethodInvoker)delegate
						{
							SaveSonPlan();

						});
					}
				}
				catch
				{
					_startSonPlan = 2;
					//	ErrorLog.errorLog("Chạy DataReader lỗi khi xuất ra DataTable", " ", Environment.NewLine);
				}
			}
		}
		async void SaveSonPlan()
		{
			Task task = Task.Factory.StartNew(() =>
			{
				this.Invoke((MethodInvoker)delegate
				{

					dttSonPlan = dsSonPlan.Tables[2];
					if (dttSonPlan.Rows.Count <= 0) return;
					dttSonPlan.Columns.Add("DATEF1", typeof(DateTime));
					dttSonPlan.Columns.Add("DATEF6", typeof(DateTime));
					dttSonPlan.Columns.Add("DATEF17", typeof(DateTime));
					for (int i = 1; i < dttSonPlan.Rows.Count; i++)
					{
						if (string.IsNullOrEmpty(TextUtils.ToString(dttSonPlan.Rows[i]["F3"])) || string.IsNullOrEmpty(TextUtils.ToString(dttSonPlan.Rows[i]["F9"])))
						{
							continue;
						}
						if (TextUtils.ToString(dttSonPlan.Rows[i]["F1"]).Trim() != "")
							dttSonPlan.Rows[i]["DATEF1"] = DateTime.FromOADate(TextUtils.ToDouble(dttSonPlan.Rows[i]["F1"]));
						if (TextUtils.ToString(dttSonPlan.Rows[i]["F6"]).Trim() != "")
							dttSonPlan.Rows[i]["DATEF6"] = DateTime.FromOADate(TextUtils.ToDouble(dttSonPlan.Rows[i]["F6"]));
						if (TextUtils.ToString(dttSonPlan.Rows[i]["F17"]).Trim() != "")
							dttSonPlan.Rows[i]["DATEF17"] = DateTime.FromOADate(TextUtils.ToDouble(dttSonPlan.Rows[i]["F17"]));
					}
				});
				int rowCount = dttSonPlan.Rows.Count;
				this.Invoke((MethodInvoker)delegate
				{
					for (int i = 0; i < rowCount; i++)
					{
						try
						{
							if (i < 6) continue;
							string _partCode = TextUtils.ToString(dttSonPlan.Rows[i]["F3"]);
							string _orderCode = TextUtils.ToString(dttSonPlan.Rows[i]["F9"]);

							// Kiem tra xem dong do' co du? thong tin hop le hay khong
							if (string.IsNullOrEmpty(_partCode) || string.IsNullOrEmpty(_orderCode))
							{
								continue;
							}

							SonPlanModel sonPlanModel = new SonPlanModel();

							#region Set value
							sonPlanModel.DateExported = TextUtils.ToDate3(dttSonPlan.Rows[i]["F1"]);
							if (TextUtils.ToDate3(dttSonPlan.Rows[i]["F1"]) == new DateTime(1950, 1, 1))
							{
								sonPlanModel.DateExported = TextUtils.ToDate2(dttSonPlan.Rows[i]["DATEF1"].ToString());
							}
							sonPlanModel.PartCode = _partCode;
							sonPlanModel.LotSize = TextUtils.ToInt(dttSonPlan.Rows[i]["F4"]);
							sonPlanModel.QtyPlan = TextUtils.ToInt(dttSonPlan.Rows[i]["F5"]);
							sonPlanModel.ProdDate = TextUtils.ToDate3(dttSonPlan.Rows[i]["F6"]);
							if (TextUtils.ToDate3(dttSonPlan.Rows[i]["F6"]) == new DateTime(1950, 1, 1))
							{
								sonPlanModel.ProdDate = TextUtils.ToDate2(dttSonPlan.Rows[i]["DATEF6"].ToString());
							}
							sonPlanModel.RealProdQty = TextUtils.ToInt(dttSonPlan.Rows[i]["F7"]);
							sonPlanModel.NG = TextUtils.ToInt(dttSonPlan.Rows[i]["F8"]);
							sonPlanModel.OrderCode = _orderCode;
							sonPlanModel.SaleCode = TextUtils.ToString(dttSonPlan.Rows[i]["F10"]);
							sonPlanModel.OP = TextUtils.ToInt(dttSonPlan.Rows[i]["F11"]);
							sonPlanModel.ShipTo = TextUtils.ToString(dttSonPlan.Rows[i]["F12"]);
							sonPlanModel.ShipVia = TextUtils.ToString(dttSonPlan.Rows[i]["F13"]);
							sonPlanModel.ConfirmCode = TextUtils.ToString(dttSonPlan.Rows[i]["F14"]);
							sonPlanModel.Note = TextUtils.ToString(dttSonPlan.Rows[i]["F15"]);
							sonPlanModel.WorkerCode = TextUtils.ToString(dttSonPlan.Rows[i]["F16"]);
							sonPlanModel.PrintedDate = TextUtils.ToDate3(dttSonPlan.Rows[i]["F17"]);
							if (TextUtils.ToDate3(dttSonPlan.Rows[i]["F17"]) == new DateTime(1950, 1, 1))
							{
								sonPlanModel.PrintedDate = TextUtils.ToDate2(dttSonPlan.Rows[i]["DATEF17"].ToString());
							}
							//sonPlanModel.Cnt = TextUtils.ToInt(dttSonPlan.Rows[i]["F23"]);

							#endregion

							// Kiem tra xem ma san pham/ma order da ton tai chua
							Expression exp1 = new Expression("PartCode", _partCode);
							Expression exp2 = new Expression("OrderCode", _orderCode);
							ArrayList arr = SonPlanBO.Instance.FindByExpression(exp1.And(exp2));
							if (arr.Count > 0)
							{
								for (int j = 0; j < arr.Count; j++)
								{
									sonPlanModel.ID = (arr[j] as SonPlanModel).ID;
									SonPlanBO.Instance.Update(sonPlanModel);
								}
							}
							else
							{
								sonPlanModel.ID = (int)SonPlanBO.Instance.Insert(sonPlanModel);
							}

						}
						catch (Exception er)
						{
							_startSonPlan = 2;
							//ErrorLog.errorLog("Lỗi dữ liệu tại dòng" + i + Environment.NewLine + er.ToString(), " ", "");
						}
					}
				});

			});
			await task;
			_startSonPlan = 2;

		}
		void UpdateDateOrderPart()
		{
			while (true)
			{
				Thread.Sleep(5000);
				try
				{
					if (_startOrderPart == 1)
					{
						this.Invoke((MethodInvoker)delegate
						{
							try
							{
								if (btnBrowseOrderPart.Text.Trim() == "") return;
								DateTime dateTime = File.GetLastWriteTime(btnBrowseOrderPart.Text.Trim());
								if (dateTime != dateTimeOldpathOrder)
								{
									dateTimeOldpathOrder = dateTime;
									try
									{
										//Copy file Save vào thư mục 
										if (btnCopyOrder.Text == "") return;
										string sourcePath = btnBrowseOrderPart.Text;
										string targetPath1 = btnCopyOrder.Text;
										string sourceFile = System.IO.Path.Combine(sourcePath);
										string destFile = System.IO.Path.Combine(targetPath1, "OrderPart_" + DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss") + ".txt");
										//Copy file từ file nguồn đến file đích
										System.IO.File.Copy(sourceFile, destFile, true);
									}
									catch
									{

									}
								}
								else
								{
									return;
								}

								dttOrderPart = ConvertCsvToDataTable(btnBrowseOrderPart.Text.Trim());
								//dttOrderPart = GetDataTableFromCsv(btnBrowseOrderPart.Text.Trim(), checkisFirstRowHeader.Checked);
								if (dttOrderPart == null || dttOrderPart.Rows.Count <= 0) return;
								//DataRow dr = dttOrderPart.NewRow();
								//for (int i = 0; i < dttOrderPart.Columns.Count; i++)
								//{
								//	dr[$"{dttOrderPart.Columns[i].ColumnName}"] = dttOrderPart.Columns[i].ColumnName;
								//	dttOrderPart.Columns[i].ColumnName = "F" + (i + 1);
								//}
								//dttOrderPart.Rows.InsertAt(dr, 0);
								_startOrderPart = 0;
								for (int i = 0; i < dttOrderPart.Columns.Count; i++)
								{
									dttOrderPart.Columns[i].ColumnName = "F" + (i + 1);
								}
								this.Invoke((MethodInvoker)delegate
								{
									SaveOrderPart();
								});
							}
							catch
							{
								_startOrderPart = 1;
								//ErrorLog.errorLog("Chạy DataReader CSV lỗi khi xuất ra DataTable", " Hiển thị table OrderPart ", Environment.NewLine);
							}
						});
					}
				}
				catch
				{
					//ErrorLog.errorLog("Chạy DataReader CSV lỗi khi xuất ra DataTable", " UpdateDateOrderPart ", Environment.NewLine);
				}
			}
		}
		async void SaveOrderPart()
		{
			Task task = Task.Factory.StartNew(() =>
			{
				int rowCount = dttOrderPart.Rows.Count;
				string OrderOld = "";
				int cntOld = 999;
				for (int i = 0; i < rowCount; i++)
				{
					try
					{
						string _ordercode = TextUtils.ToString(dttOrderPart.Rows[i]["F1"]).Trim('"');
						string ArticleID = TextUtils.ToString(dttOrderPart.Rows[i]["F3"]).Trim('"');
						string Shelf = TextUtils.ToString(dttOrderPart.Rows[i]["F6"]).Trim('"');
						int _Cnt = TextUtils.ToInt(TextUtils.ToDouble(TextUtils.ToString(dttOrderPart.Rows[i]["F2"]).Trim('"')));
						//Kiểm tra nếu mã nhóm hoặc mã sản phẩm trống thì next
						if (string.IsNullOrEmpty(_ordercode) || string.IsNullOrEmpty(ArticleID))
						{
							continue;
						}
						//Nếu _CNt và OrderCode tồn tại trong database xóa cũ đi và insert mới vào
						if (_ordercode != OrderOld || _Cnt != cntOld)
						{
							OrderOld = _ordercode;
							cntOld = _Cnt;
							TextUtils.ExcuteSQL($"DELETE [ShiStock].[dbo].[OrderPart] WHERE OrderCode=N'{_ordercode}' AND Cnt ='{_Cnt}'");
						}

						string Location = TextUtils.ToString(dttOrderPart.Rows[i]["F7"]).Trim('"'); ;
						if (Location.Length < 7)
						{
							int LengthLocation = Location.Length;
							for (int j = 0; j < 7 - LengthLocation; j++)
							{
								Location = "0" + Location;
							}
						}
						Expression exp2 = new Expression("OrderCode", _ordercode);
						Expression exp1 = new Expression("ArticleID", ArticleID);
						Expression exp3 = new Expression("Cnt", _Cnt);
						Expression exp4 = new Expression("Shelf", Shelf);
						Expression exp5 = new Expression("Location", Location);
						ArrayList arr = OrderPartBO.Instance.FindByExpression(exp1.And(exp2).And(exp3).And(exp4).And(exp5));
						if (arr == null || arr.Count > 0) continue;
						OrderPartModel orderPart = new OrderPartModel();
						#region SetValue
						//string a = "";
						orderPart.OrderCode = _ordercode;
						orderPart.ArticleID = ArticleID;
						orderPart.Cnt = _Cnt;
						orderPart.Description = TextUtils.ToString(dttOrderPart.Rows[i]["F4"]).Trim('"');
						if (TextUtils.ToString(dttOrderPart.Rows[i]["F5"]).Contains("#"))
						{
							orderPart.Qty = TextUtils.ToInt(TextUtils.ToString(dttOrderPart.Rows[i]["F5"]).Trim('"').Split('#')[0]);
						}
						else
						{
							orderPart.Qty = TextUtils.ToInt(TextUtils.ToDouble(TextUtils.ToString(dttOrderPart.Rows[i]["F5"]).Trim('"')));
						}
						orderPart.Shelf = Shelf;
						orderPart.Location = Location;
						orderPart.Lot = TextUtils.ToString(dttOrderPart.Rows[i]["F8"]).Trim('"'); ;
						orderPart.CreateAt = TextUtils.ToDate2(TextUtils.ToString(dttOrderPart.Rows[i]["F9"]).Trim('"'));//date
																														 //if (TextUtils.ToDate3(dttOrderPart.Rows[i]["F9"]) == new DateTime(1950, 1, 1))
																														 //{
																														 //	orderPart1.CreateAt = TextUtils.ToDate2(dttOrderPart.Rows[i]["DATEF9"].ToString());
																														 //}
						orderPart.Userr = TextUtils.ToString(dttOrderPart.Rows[i]["F10"]).Trim('"'); // đích
						orderPart.OrderCodeAndCnt = _ordercode + orderPart.Cnt;
						#endregion
						//product.UpdatedAt = DateTime.Now;
						//product.CreatedAt = DateTime.Now;
						orderPart.CreateDate = DateTime.Now;
						orderPart.ID = (int)OrderPartBO.Instance.Insert(orderPart);
						//lstProductionPlanBO.Add(product);
						//}

					}
					catch (Exception ex)
					{
						//ErrorLog.errorLog("Chạy save orderPart", $"{ex}", Environment.NewLine);
					}
				}
			});

			await task;

			_startOrderPart = 1;
		}
		void UpdateDateOrderPart1()
		{
			while (true)
			{
				Thread.Sleep(5000);
				try
				{
					if (_startOrderPart1 == 1)
					{
						this.Invoke((MethodInvoker)delegate
						{
							try
							{

								if (btnBrowseOrderPart1.Text.Trim() == "") return;
								DateTime dateTime = File.GetLastWriteTime(btnBrowseOrderPart1.Text.Trim());
								if (dateTime != dateTimeOld)
								{
									dateTimeOld = dateTime;
									try
									{
										//Copy file Save vào thư mục 
										if (btnCopyOrder.Text == "") return;
										string sourcePath = btnBrowseOrderPart1.Text;
										string targetPath1 = btnCopyOrder.Text;//Thu mục copy chuyển đến 
										string sourceFile = System.IO.Path.Combine(sourcePath);
										string destFile = System.IO.Path.Combine(targetPath1, "OrderPart1_" + DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss") + ".txt");
										//Copy file từ file nguồn đến file đích
										System.IO.File.Copy(sourceFile, destFile, true);
									}
									catch
									{

									}
								}
								else
								{
									return;
								}
								dttOrderPart1 = ConvertCsvToDataTable(btnBrowseOrderPart1.Text.Trim());
								//dttOrderPart1 = GetDataTableFromCsv1(btnBrowseOrderPart1.Text.Trim(), checkisFirstRowHeader.Checked);
								if (dttOrderPart1 == null || dttOrderPart1.Rows.Count <= 0) return;
								//DataRow dr = dttOrderPart1.NewRow();
								//for (int i = 0; i < dttOrderPart1.Columns.Count; i++)
								//{
								//	dr[$"{dttOrderPart1.Columns[i].ColumnName}"] = dttOrderPart1.Columns[i].ColumnName;
								//	dttOrderPart1.Columns[i].ColumnName = "F" + (i + 1);
								//}
								//dttOrderPart1.Rows.InsertAt(dr, 0);
								_startOrderPart1 = 0;
								for (int i = 0; i < dttOrderPart1.Columns.Count; i++)
								{
									dttOrderPart1.Columns[i].ColumnName = "F" + (i + 1);
								}
								this.Invoke((MethodInvoker)delegate
								{
									SaveOrderPart1();
								});
							}
							catch
							{
								_startOrderPart1 = 1;
								//ErrorLog.errorLog("Chạy DataReader CSV lỗi khi xuất ra DataTable", " Hiển thị table ", Environment.NewLine);
							}
						});
						//dtt = null;
					}
				}
				catch
				{
					//ErrorLog.errorLog("Chạy DataReader CSV lỗi khi xuất ra DataTable", " UpdateDate ", Environment.NewLine);
				}
			}
		}
		async void SaveOrderPart1()
		{
			Task task = Task.Factory.StartNew(() =>
			{
				int rowCount = dttOrderPart1.Rows.Count;
				string OrderOld = "";
				int cntOld = 999;
				for (int i = 0; i < rowCount; i++)
				{
					try
					{
						string _ordercode = TextUtils.ToString(dttOrderPart1.Rows[i]["F1"]).Trim('"');
						string ArticleID = TextUtils.ToString(dttOrderPart1.Rows[i]["F3"]).Trim('"');
						int _Cnt = TextUtils.ToInt(TextUtils.ToDouble(TextUtils.ToString(dttOrderPart1.Rows[i]["F2"]).Trim('"')));
						if (_ordercode != OrderOld || _Cnt != cntOld)
						{
							OrderOld = _ordercode;
							cntOld = _Cnt;
							TextUtils.ExcuteSQL($"DELETE [ShiStock].[dbo].[OrderPart] WHERE OrderCode=N'{_ordercode}' AND Cnt ='{_Cnt}'");
						}
						string Shelf = TextUtils.ToString(dttOrderPart1.Rows[i]["F6"]).Trim('"'); ;
						string Location = TextUtils.ToString(dttOrderPart1.Rows[i]["F7"]).Trim('"'); ;
						if (Location.Length < 7)
						{
							int LengthLocation = Location.Length;
							for (int j = 0; j < 7 - LengthLocation; j++)
							{
								Location = "0" + Location;
							}
						}
						//Kiểm tra nếu mã nhóm hoặc mã sản phẩm trống thì next
						if (string.IsNullOrEmpty(_ordercode) || string.IsNullOrEmpty(ArticleID))
						{
							continue;
						}
						Expression exp2 = new Expression("OrderCode", _ordercode);
						Expression exp1 = new Expression("ArticleID", ArticleID);
						Expression exp3 = new Expression("Cnt", _Cnt);
						Expression exp4 = new Expression("Shelf", Shelf);
						Expression exp5 = new Expression("Location", Location);
						ArrayList arr = OrderPartBO.Instance.FindByExpression(exp1.And(exp2).And(exp3).And(exp4).And(exp5));
						if (arr == null || arr.Count > 0) continue;
						OrderPartModel orderPart1 = new OrderPartModel();

						#region SetValue
						//string a = "";

						orderPart1.OrderCode = _ordercode;
						orderPart1.ArticleID = ArticleID;
						orderPart1.Cnt = _Cnt;
						orderPart1.Description = TextUtils.ToString(dttOrderPart1.Rows[i]["F4"]).Trim('"'); ;
						if (TextUtils.ToString(dttOrderPart1.Rows[i]["F5"]).Contains("#"))
						{
							orderPart1.Qty = TextUtils.ToInt(TextUtils.ToString(dttOrderPart1.Rows[i]["F5"]).Trim('"').Split('#')[0]);
						}
						else
						{
							orderPart1.Qty = TextUtils.ToInt(TextUtils.ToDouble(TextUtils.ToString(dttOrderPart1.Rows[i]["F5"]).Trim('"')));
						}
						orderPart1.Shelf = Shelf;
						orderPart1.Location = Location;
						orderPart1.Lot = TextUtils.ToString(dttOrderPart1.Rows[i]["F8"]).Trim('"'); ;
						orderPart1.CreateAt = TextUtils.ToDate2(TextUtils.ToString(dttOrderPart1.Rows[i]["F9"]).Trim('"'));//date
																														   //if (TextUtils.ToDate3(dttOrderPart1.Rows[i]["F9"]) == new DateTime(1950, 1, 1))
																														   //{
																														   //	orderPart.CreateAt = TextUtils.ToDate2(dttOrderPart1.Rows[i]["DATEF9"].ToString());
																														   //}
						orderPart1.Userr = TextUtils.ToString(dttOrderPart1.Rows[i]["F10"]).Trim('"');  // đích
						orderPart1.OrderCodeAndCnt = _ordercode + orderPart1.Cnt;
						#endregion
						orderPart1.CreateDate = DateTime.Now;
						orderPart1.ID = (int)OrderPartBO.Instance.Insert(orderPart1);
						//lstProductionPlanBO.Add(product);
						//}
					}
					catch (Exception ex)
					{

					}
				}
			});

			await task;
			_startOrderPart1 = 1;
		}
		private static IList<string> GetTablenames(DataTableCollection tables)
		{
			var tableList = new List<string>();
			foreach (var table in tables)
			{
				tableList.Add(table.ToString());
			}

			return tableList;
		}
		public DataTable ConvertCsvToDataTable(string filePath)
		{
			try
			{
				//reading all the lines(rows) from the file.
				string[] rows = File.ReadAllLines(filePath);
				DataTable dtData = new DataTable();
				string[] rowValues = null;
				DataRow dr = dtData.NewRow();

				//Creating columns
				if (rows.Length > 0)
				{
					foreach (string columnName in rows[0].Split(','))
						dtData.Columns.Add(columnName);
				}

				//Creating row for each line.(except the first line, which contain column names)
				string[] stringSeparators = new string[] { @""",""" };
				for (int row = 1; row < rows.Length; row++)
				{
					rowValues = rows[row].Split(stringSeparators, StringSplitOptions.None);
					dr = dtData.NewRow();
					dr.ItemArray = rowValues;
					dtData.Rows.Add(dr);
				}

				return dtData;
			}
			catch (Exception ex)
			{
				return null;
			}

		}

		/// <summary>
		/// Đọc file CSV
		/// </summary>
		/// <param name="path"></param>
		/// <param name="isFirstRowHeader"></param>
		/// <returns></returns>
		public static DataTable GetDataTableFromCsv(string path, bool isFirstRowHeader)
		{
			string header = isFirstRowHeader ? "Yes" : "No";

			string pathOnly = Path.GetDirectoryName(path);
			string fileName = Path.GetFileName(path);

			string sql = @"SELECT * FROM [" + fileName + "]";

			using (OleDbConnection connection = new OleDbConnection(
					  @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + pathOnly +
					  ";Extended Properties=\"Text;HDR=" + header + "\""))
			using (OleDbCommand command = new OleDbCommand(sql, connection))
			using (OleDbDataAdapter adapter = new OleDbDataAdapter(command))
			{
				DataTable dataTable = new DataTable();
				dataTable.Locale = CultureInfo.CurrentCulture;
				adapter.Fill(dataTable);
				return dataTable;
			}
		}
		public static DataTable GetDataTableFromCsv1(string path, bool isFirstRowHeader)
		{
			string header = isFirstRowHeader ? "Yes" : "No";

			string pathOnly = Path.GetDirectoryName(path);
			string fileName = Path.GetFileName(path);

			string sql = @"SELECT * FROM [" + fileName + "]";

			using (OleDbConnection connection = new OleDbConnection(
					  @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + pathOnly +
					  ";Extended Properties=\"Text;HDR=" + header + "\""))
			using (OleDbCommand command = new OleDbCommand(sql, connection))
			using (OleDbDataAdapter adapter = new OleDbDataAdapter(command))
			{
				DataTable dataTable = new DataTable();
				dataTable.Locale = CultureInfo.CurrentCulture;
				adapter.Fill(dataTable);
				return dataTable;
			}
		}

		private void chkRun_CheckedChanged(object sender, EventArgs e)
		{
			//duong dan

			string path_my_app = Application.StartupPath + "\\RTCLine.exe";
			if (chkRun.Checked)
			{
				if (File.Exists(path_my_app))
				{
					SetStartup(path_my_app, true);
				}
			}
			else
			{
				if (File.Exists(path_my_app))
				{
					SetStartup(path_my_app, false);
				}
			}

		}
		//auto chạy khi khởi động windowns
		private void SetStartup(string AppName, bool enable)
		{
			//try
			//{
			string runKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";
			Microsoft.Win32.RegistryKey startupKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(runKey);
			if (enable)
			{
				if (startupKey.GetValue(AppName) == null)
				{
					startupKey.Close();
					startupKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(runKey, true);
					//startupKey.SetValue(AppName, Assembly.GetExecutingAssembly().Location + " /StartMinimized");
					startupKey.SetValue(AppName, Application.StartupPath + "\\RTCLine.exe");
					startupKey.Close();
				}
			}
			else
			{
				startupKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(runKey, true);
				startupKey.DeleteValue(AppName, false);
				startupKey.Close();
			}
			//}
			//catch(Exception ex)
			//{ }
		}

		private void btnBrowseSonPlan_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			OpenFileDialog openFileDialog1 = new OpenFileDialog();
			var result = openFileDialog1.ShowDialog();
			if (result == DialogResult.OK)
			{
				btnBrowseSonPlan.Text = openFileDialog1.FileName;
				pathSonPlan = btnBrowseSonPlan.Text.Trim();
				File.WriteAllText(Application.StartupPath + "/UpdateDateSonPlan.txt", pathSonPlan);

			}
		}

		private void btnBrowseOrderPart1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			OpenFileDialog openFileDialog1 = new OpenFileDialog();
			var result = openFileDialog1.ShowDialog();
			if (result == DialogResult.OK)
			{
				btnBrowseOrderPart1.Text = openFileDialog1.FileName;
				pathOrderPart1 = btnBrowseOrderPart1.Text.Trim();
				File.WriteAllText(Application.StartupPath + "/UpdateDateOrderPart.txt", pathOrderPart1);

			}
		}
		private void btnBrowseMotor_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			OpenFileDialog openFileDialog1 = new OpenFileDialog();
			var result = openFileDialog1.ShowDialog();
			if (result == DialogResult.OK)
			{
				btnBrowseMotor.Text = openFileDialog1.FileName;
				pathBrowseMotor = btnBrowseMotor.Text.Trim();
				File.WriteAllText(Application.StartupPath + "/UpdateDateMotor.txt", pathBrowseMotor);
			}
		}

		private void btnBrowseDao_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			OpenFileDialog openFileDialog1 = new OpenFileDialog();
			var result = openFileDialog1.ShowDialog();
			if (result == DialogResult.OK)
			{
				btnBrowseDao.Text = openFileDialog1.FileName;
				pathBrowseDao = btnBrowseDao.Text.Trim();
				File.WriteAllText(Application.StartupPath + "/UpdateDateDao.txt", pathBrowseDao);
			}
		}
		private void btnCopyOrder_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			FolderBrowserDialog od = new FolderBrowserDialog();
			if (od.ShowDialog() == DialogResult.OK)
			{
				try
				{
					btnCopyOrder.Text = od.SelectedPath;
					File.WriteAllText(Application.StartupPath + "/SaveOrder.txt", btnCopyOrder.Text.Trim());
				}
				catch
				{

				}
			}
		}
		private void btnXuatExcel_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			FolderBrowserDialog od = new FolderBrowserDialog();
			if (od.ShowDialog() == DialogResult.OK)
			{
				try
				{
					btnXuatExcel.Text = od.SelectedPath;
					File.WriteAllText(Application.StartupPath + "/SavePlanHypAndAltax.txt", btnXuatExcel.Text.Trim());
				}
				catch
				{

				}
			}
		}
		private void btnBrowseSTD_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			OpenFileDialog openFileDialog1 = new OpenFileDialog();
			var result = openFileDialog1.ShowDialog();
			if (result == DialogResult.OK)
			{
				btnBrowseSTD.Text = openFileDialog1.FileName;
				pathBrowseSTD = btnBrowseSTD.Text.Trim();
				File.WriteAllText(Application.StartupPath + "/UpdateDateSTD.txt", pathBrowseSTD);
			}
		}

		private void btnBrowseOrderPart_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			OpenFileDialog openFileDialog1 = new OpenFileDialog();
			var result = openFileDialog1.ShowDialog();
			if (result == DialogResult.OK)
			{
				btnBrowseOrderPart.Text = openFileDialog1.FileName;
				pathOrderPart = btnBrowseOrderPart.Text.Trim();
				File.WriteAllText(Application.StartupPath + "/UpdateDate.txt", pathOrderPart);

			}
		}

		private void btnBrowseLOT_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			OpenFileDialog openFileDialog1 = new OpenFileDialog();
			var result = openFileDialog1.ShowDialog();
			if (result == DialogResult.OK)
			{
				btnBrowseLOT.Text = openFileDialog1.FileName;
				pathBrowseLOT = btnBrowseLOT.Text.Trim();
				File.WriteAllText(Application.StartupPath + "/UpdateDateLOT.txt", pathBrowseLOT);
			}
		}
	}
}
