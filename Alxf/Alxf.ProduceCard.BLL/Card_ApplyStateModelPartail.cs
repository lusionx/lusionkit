using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alxf.Frame.ModelBase;

namespace Alxf.ProduceCard.BLL
{
    public partial class Card_ApplyStateModel : IB2D<Alxf.ProduceCard.DAL.Card_ApplyState>
    {
        #region Generate
        public Card_ApplyStateModel()
        {
            Alxf.Frame.DataBase.ModelTool.SetDefault(this);
        }
        public System.Guid ID { get; set; }
        public System.Guid ApplyID { get; set; }
        public System.String HandlerUser { get; set; }
        public System.Int32 State { get; set; }
        public System.DateTime ChangeTime { get; set; }
        public System.Boolean IsCurrent { get; set; }
        public System.String Remark { get; set; }
        public Alxf.ProduceCard.DAL.Card_ApplyState DataModel
        {
            get
            {
                var obj = new Alxf.ProduceCard.DAL.Card_ApplyState();
                obj.ID = this.ID;
                obj.ApplyID = this.ApplyID;
                obj.HandlerUser = this.HandlerUser;
                obj.State = this.State;
                obj.ChangeTime = this.ChangeTime;
                obj.IsCurrent = this.IsCurrent;
                obj.Remark = this.Remark;
                return obj;
            }
            set
            {
                this.ID = value.ID;
                this.ApplyID = value.ApplyID;
                this.HandlerUser = value.HandlerUser;
                this.State = value.State;
                this.ChangeTime = value.ChangeTime;
                this.IsCurrent = value.IsCurrent;
                this.Remark = value.Remark;
            }
        }
        partial void ValidateID();
        partial void ValidateApplyID();
        partial void ValidateHandlerUser();
        partial void ValidateState();
        partial void ValidateChangeTime();
        partial void ValidateIsCurrent();
        partial void ValidateRemark();
        #endregion
    }
}
