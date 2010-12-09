using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alxf.Frame.ModelBase;
using Alxf.DAL.Demo.Video;
using Alxf.Frame.Exception;


namespace Alxf.BLL.Demo.Video
{
    public class VideoModelResponsibility : ResponsibilityBase<VideoModel, ADBDataContext>
    {
        public override void Insert(VideoModel model)
        {
            var dm = model.DataModel;

            if (dm.TypeID == Guid.Empty)
            {
                var obj = DB.VideoTypes.SingleOrDefault(a => a.Name == model.TypeName);
                if (obj == null)
                {
                    throw new ForeignKeyException();
                }
                dm.TypeID = obj.ID;
            }

            DB.VideoObjs.InsertOnSubmit(dm);
            this.SaveChanges();
        }

        public override void Update(VideoModel model)
        {
            var dm = model.DataModel;
            VideoType vt = null;
            if (dm.TypeID == Guid.Empty)
            {
                vt = DB.VideoTypes.SingleOrDefault(a => a.Name == model.TypeName);
                dm.TypeID = vt.ID;
            }
            else
            {
                vt = DB.VideoTypes.Single(a => a.ID == dm.TypeID);
            }
            VideoObj obj = DB.VideoObjs.Single(a => a.ID == dm.ID);
            CopyObjectProperty(dm, obj);
            SaveChanges();
        }

        public override void Delete(VideoModel model)
        {
            var obj = DB.VideoObjs.Single(a => a.ID == model.ID);
            DB.VideoObjs.DeleteOnSubmit(obj);
            this.SaveChanges();
        }

        public override IQueryable<VideoModel> Query()
        {
            var q = from a in DB.VideoObjs
                    join b in DB.VideoTypes on a.TypeID equals b.ID
                    select new VideoModel
                    {
                        ID = a.ID,
                        Name_cn = a.Name_cn,
                        Name_en = a.Name_en,
                        TypeID = a.TypeID,
                        Episodes = a.Episodes,
                        UpdateTime = a.UpdateTime,
                        DownLink = a.DownLink,
                        Remark = a.Remark,
                        TypeName = b.Name
                    };
            return q;
        }
    }
}
