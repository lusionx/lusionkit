using System;
using Excel;
namespace Tool
{
    public class MsExcel : IExcel
    {
        private Application _app = new ApplicationClass();
        private Workbook _book;
        private Worksheet _sheet;

        public Worksheet CurrentSheet
        {
            get { return _sheet; }
            set { _sheet = value; }
        }
        private object miss = Type.Missing;

        /// <summary>
        /// 默认1,2,3
        /// </summary>
        /// <param name="i"></param>
        public void ChangeCurrentSheet(int i)
        {
            CurrentSheet = (Worksheet)_book.Worksheets[i];
        }

        public string SheetName
        {
            get
            {
                return CurrentSheet.Name;
            }
            set
            {
                CurrentSheet.Name = value;
            }
        }

        public MsExcel()
        {
            _book = _app.Workbooks.Add(miss);
            CurrentSheet = (Worksheet)_book.ActiveSheet;
        }
        public MsExcel(string TemplatePath)
        {
            _book = _app.Workbooks.Open(TemplatePath, miss, miss, miss, miss, miss, miss, miss, miss, miss, miss, miss, miss, miss, miss);
            CurrentSheet = (Worksheet)_book.ActiveSheet;
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="path">xxx.xls</param>
        public void SaveAs(string path)
        {
            _book.SaveAs(path, XlFileFormat.xlWorkbookNormal, miss, miss, miss, miss, XlSaveAsAccessMode.xlExclusive, miss, miss, miss, miss, miss);
        }

        public void InsertPicture(string RangeName, string PicturePath)
        {
            Range rg = GetRange(RangeName);
            rg.Select();
            Pictures pics = (Pictures)CurrentSheet.Pictures(miss);//m_objSheet.Pictures(m_objOpt);
            pics.Insert(PicturePath, miss);
        }

        /// <summary>
        /// 将图片插入到指定的单元格位置，并设置图片的宽度和高度。
        /// 注意：图片必须是绝对物理路径
        /// </summary>
        /// <param name="RangeName">单元格名称，例如：B4</param>
        /// <param name="PicturePath">要插入图片的绝对路径。</param>
        /// <param name="PictuteWidth">插入后，图片在Excel中显示的宽度。</param>
        /// <param name="PictureHeight">插入后，图片在Excel中显示的高度。</param>
        public void InsertPicture(string RangeName, string PicturePath, float PictuteWidth, float PictureHeight)
        {
            Range m_objRange = GetRange(RangeName);
            //m_objRange.Select();
            float PicLeft, PicTop;
            PicLeft = System.Convert.ToSingle(m_objRange.Left);
            PicTop = System.Convert.ToSingle(m_objRange.Top);
            //参数含义：
            //图片路径
            //是否链接到文件
            //图片插入时是否随文档一起保存
            //图片在文档中的坐标位置（单位：points）
            //图片显示的宽度和高度（单位：points）
            //参数详细信息参见：http://msdn2.microsoft.com/zh-cn/library/aa221765(office.11).aspx
            CurrentSheet.Shapes.AddPicture(PicturePath, Microsoft.Office.Core.MsoTriState.msoFalse,
              Microsoft.Office.Core.MsoTriState.msoTrue, PicLeft, PicTop, PictuteWidth, PictureHeight);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="row">从1开始计数</param>
        /// <param name="col">从1开始计数</param>
        /// <param name="val"></param>
        public void SetValue(int row, int col, object val)
        {
            string a1 = Convert(col) + row.ToString();
            SetValue(a1, val);
        }

        public void SetValue(string A1, object val)
        {
            SetValue(A1, A1, val);
        }

        public void SetValue(string A1, string C3, object val)
        {
            Range rg = GetRange(A1, C3);

            rg.Value2 = val;
        }

        public object GetValue(string A1, string C3)
        {
            Range rg = GetRange(A1, C3);

            return rg.Value2 ?? string.Empty;
        }

        public object GetValue(string A1)
        {
            return GetValue(A1, A1);
        }

        private Range GetRange(string A1)
        {
            return GetRange(A1, A1);
        }

        private Range GetRange(string A1, string C3)
        {
            return CurrentSheet.get_Range(A1, C3);
        }

        private string Convert(int index)
        {
            int _index = index <= 0 ? 0 : index > 255 ? 254 : index - 1;
            return table[_index];
        }

        private static readonly string[] table = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "AA", "AB", "AC", "AD", "AE", "AF", "AG", "AH", "AI", "AJ", "AK", "AL", "AM", "AN", "AO", "AP", "AQ", "AR", "AS", "AT", "AU", "AV", "AW", "AX", "AY", "AZ", "BA", "BB", "BC", "BD", "BE", "BF", "BG", "BH", "BI", "BJ", "BK", "BL", "BM", "BN", "BO", "BP", "BQ", "BR", "BS", "BT", "BU", "BV", "BW", "BX", "BY", "BZ", "CA", "CB", "CC", "CD", "CE", "CF", "CG", "CH", "CI", "CJ", "CK", "CL", "CM", "CN", "CO", "CP", "CQ", "CR", "CS", "CT", "CU", "CV", "CW", "CX", "CY", "CZ", "DA", "DB", "DC", "DD", "DE", "DF", "DG", "DH", "DI", "DJ", "DK", "DL", "DM", "DN", "DO", "DP", "DQ", "DR", "DS", "DT", "DU", "DV", "DW", "DX", "DY", "DZ", "EA", "EB", "EC", "ED", "EE", "EF", "EG", "EH", "EI", "EJ", "EK", "EL", "EM", "EN", "EO", "EP", "EQ", "ER", "ES", "ET", "EU", "EV", "EW", "EX", "EY", "EZ", "FA", "FB", "FC", "FD", "FE", "FF", "FG", "FH", "FI", "FJ", "FK", "FL", "FM", "FN", "FO", "FP", "FQ", "FR", "FS", "FT", "FU", "FV", "FW", "FX", "FY", "FZ", "GA", "GB", "GC", "GD", "GE", "GF", "GG", "GH", "GI", "GJ", "GK", "GL", "GM", "GN", "GO", "GP", "GQ", "GR", "GS", "GT", "GU", "GV", "GW", "GX", "GY", "GZ", "HA", "HB", "HC", "HD", "HE", "HF", "HG", "HH", "HI", "HJ", "HK", "HL", "HM", "HN", "HO", "HP", "HQ", "HR", "HS", "HT", "HU", "HV", "HW", "HX", "HY", "HZ", "IA", "IB", "IC", "ID", "IE", "IF", "IG", "IH", "II", "IJ", "IK", "IL", "IM", "IN", "IO", "IP", "IQ", "IR", "IS", "IT", "IU", "IV" };


        public void Dispose()
        {
            _book.Close(miss, miss, miss);
            _app.Quit();
            ReleaseComObject(_sheet);
            ReleaseComObject(_book);
            ReleaseComObject(_app);
        }

        private void ReleaseComObject(object obj)
        {
            if (obj != null)
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
        }
    }
}
