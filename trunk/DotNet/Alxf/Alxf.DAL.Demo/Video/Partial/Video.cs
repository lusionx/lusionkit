using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alxf.DAL.Demo.Video
{
    public partial class VideoObj
    {
        partial void OnCreated()
        {
            this.DownLink = this.Name_cn = this.Name_en = this.Remark;
            this.TypeID = Guid.Empty;
        }

        partial void OnValidate(System.Data.Linq.ChangeAction action)
        {
            
        }
    }
}
