using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alxf.ProduceCard.BLL
{
    /// <summary>
    /// 前方代表
    /// </summary>
    public class UserDaibiao : IStateAction
    {
        /// <summary>
        /// 提交订单
        /// </summary>
        /// <param name="pk"></param>
        public void Next(Card_ApplyModel pk)
        {
            Card_ApplyStateModelResponsibility res = new Card_ApplyStateModelResponsibility();
            res.ChangeState(pk.ID, pk.ZhuLiName, ApplyState.已提交);
        }

        /// <summary>
        /// 删除订单
        /// </summary>
        /// <param name="pk"></param>
        public void Previous(Card_ApplyModel pk)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 前方代表新建申请
        /// </summary>
        /// <param name="zhika">制卡数量</param>
        /// <param name="buka">补卡数量</param>
        /// <param name="zhuli">提交给的助理</param>
        /// <param name="upfileid">上传文件的ID</param>
        public void NewApply(int zhika,int buka,string zhuli,Guid upfileid)
        {
            Card_ApplyModelResponsibility rep = new Card_ApplyModelResponsibility();
            Card_ApplyModel obj = new Card_ApplyModel();
            obj.ProduceQuantity=zhika;
            obj.AdditionalQuantity = buka;
            obj.ZhuLiName = zhuli;
            obj.OriginalData = upfileid;
            obj.OrderSerial = Serial.CreateAppleSerial(rep.Query());
            rep.Insert(obj);
        }
    }
}
