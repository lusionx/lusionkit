using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alxf.Frame.ModelBase;

namespace Alxf.BLL.Demo.Video
{
    public partial class VideoModel : IB2D<Alxf.DAL.Demo.Video.VideoObj>
    {
        #region Generate
        public System.Guid ID { get; set; }
        public System.String Name_cn { get; set; }
        public System.String Name_en { get; set; }
        public System.Guid TypeID { get; set; }
        public System.Int32 Episodes { get; set; }
        public System.DateTime UpdateTime { get; set; }
        public System.String DownLink { get; set; }
        public System.String Remark { get; set; }
        public Alxf.DAL.Demo.Video.VideoObj DataModel
        {
            get
            {
                var obj = new Alxf.DAL.Demo.Video.VideoObj();
                obj.ID = this.ID;
                obj.Name_cn = this.Name_cn;
                obj.Name_en = this.Name_en;
                obj.TypeID = this.TypeID;
                obj.Episodes = this.Episodes;
                obj.UpdateTime = this.UpdateTime;
                obj.DownLink = this.DownLink;
                obj.Remark = this.Remark;
                return obj;
            }
            set
            {
                this.ID = value.ID;
                this.Name_cn = value.Name_cn;
                this.Name_en = value.Name_en;
                this.TypeID = value.TypeID;
                this.Episodes = value.Episodes;
                this.UpdateTime = value.UpdateTime;
                this.DownLink = value.DownLink;
                this.Remark = value.Remark;
            }
        }
        partial void ValidateID();
        partial void ValidateName_cn();
        partial void ValidateName_en();
        partial void ValidateTypeID();
        partial void ValidateEpisodes();
        partial void ValidateUpdateTime();
        partial void ValidateDownLink();
        partial void ValidateRemark();
        #endregion

        
        partial void ValidateName_en()
        {
            throw new NotImplementedException();
        }

        public string TypeName { get; set; }
    }
}
