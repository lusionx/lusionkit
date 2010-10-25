using System;
namespace Tool
{
    public interface IExcel : IDisposable
    {
        void ChangeCurrentSheet(int i);
        object GetValue(string A1);
        object GetValue(string A1, string C3);
        void InsertPicture(string RangeName, string PicturePath, float PictuteWidth, float PictureHeight);
        void InsertPicture(string RangeName, string PicturePath);
        void SaveAs(string path);
        void SetValue(string A1, object val);
        void SetValue(int row, int col, object val);
        void SetValue(string A1, string C3, object val);
        string SheetName { get; set; }
    }
}
