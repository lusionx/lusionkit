using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alxf.ProduceCard.BLL
{
    /// <summary>
    /// 订单状态,对应Enmu表
    /// </summary>
    public enum ApplyState
    {
        未提交 = 0,
        已提交 = 10,
        已确认 = 20,
        数据处理中 = 30,
        制卡中 = 40,
        验卡中 = 50,
        已入库 = 60,
        已发货 = 70,
        已收货 = 80
    }    
}
