using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alxf.DAL.Demo.Video
{
    public partial class VideoComment
    {
        partial void OnCreated()
        {
            this.ID = Guid.NewGuid();
            Alxf.Frame.DataBase.ModelTool.SetDefault(this);
        }

        partial void OnValidate(System.Data.Linq.ChangeAction action)
        {
            Alxf.Frame.DataBase.ModelTool.ValidateStringLength(this);
        }
    }
}
