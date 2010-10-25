using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alxf.Frame.ModelBase;
using Alxf.Frame.DataBase;

namespace Alxf.ProduceCard.BLL
{
    public partial class Card_ApplyModel : IB2D<Alxf.ProduceCard.DAL.Card_Apply>
    {
        #region Generate
        public Card_ApplyModel()
        {
            Alxf.Frame.DataBase.ModelTool.SetDefault(this);
        }
        public System.Guid ID { get; set; }
        public System.String OrderSerial { get; set; }
        public System.Int32 ProduceQuantity { get; set; }
        public System.Int32 AdditionalQuantity { get; set; }
        public System.String ZhuLiName { get; set; }
        public System.Guid OriginalData { get; set; }
        public System.Int32 ConfirmQuantity { get; set; }
        public System.Guid HandledData { get; set; }
        public System.Int32 HandledQuantity { get; set; }
        public System.Guid ProducerID { get; set; }
        public System.String StorageSerial { get; set; }
        public System.String Remark { get; set; }
        public Alxf.ProduceCard.DAL.Card_Apply DataModel
        {
            get
            {
                var obj = new Alxf.ProduceCard.DAL.Card_Apply();
                obj.ID = this.ID;
                obj.OrderSerial = this.OrderSerial;
                obj.ProduceQuantity = this.ProduceQuantity;
                obj.AdditionalQuantity = this.AdditionalQuantity;
                obj.ZhuLiName = this.ZhuLiName;
                obj.OriginalData = this.OriginalData;
                obj.ConfirmQuantity = this.ConfirmQuantity;
                obj.HandledData = this.HandledData;
                obj.HandledQuantity = this.HandledQuantity;
                obj.ProducerID = this.ProducerID;
                obj.StorageSerial = this.StorageSerial;
                obj.Remark = this.Remark;
                return obj;
            }
            set
            {
                this.ID = value.ID;
                this.OrderSerial = value.OrderSerial;
                this.ProduceQuantity = value.ProduceQuantity;
                this.AdditionalQuantity = value.AdditionalQuantity;
                this.ZhuLiName = value.ZhuLiName;
                this.OriginalData = value.OriginalData;
                this.ConfirmQuantity = value.ConfirmQuantity;
                this.HandledData = value.HandledData;
                this.HandledQuantity = value.HandledQuantity;
                this.ProducerID = value.ProducerID;
                this.StorageSerial = value.StorageSerial;
                this.Remark = value.Remark;
            }
        }
        partial void ValidateID();
        partial void ValidateOrderSerial();
        partial void ValidateProduceQuantity();
        partial void ValidateAdditionalQuantity();
        partial void ValidateZhuLiName();
        partial void ValidateOriginalData();
        partial void ValidateConfirmQuantity();
        partial void ValidateHandledData();
        partial void ValidateHandledQuantity();
        partial void ValidateProducerID();
        partial void ValidateStorageSerial();
        partial void ValidateRemark();
        #endregion
    }
}
