using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alxf.Frame.DataBase;

namespace Alxf.ProduceCard.DAL
{
    public partial class Card_Apply
    {
        partial void OnCreated()
        {
            ModelTool.SetDefault(this);
            //this.ID = Guid.NewGuid();
        }

        partial void OnValidate(System.Data.Linq.ChangeAction action)
        {
            ModelTool.ValidateStringLength(this);
        }
    }
}
