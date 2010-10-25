using System;
using System.Collections.Generic;
using System.Data.Services;
using System.Linq;
using System.ServiceModel.Web;
using System.Web;

namespace SLWeb
{
    public class NorthwindWebData : DataService<DataService.NorthwindEntities>
    {
        // 仅调用此方法一次以初始化涉及服务范围的策略。
        public static void InitializeService(IDataServiceConfiguration config)
        {
            // TODO: 设置规则以指明哪些实体集和服务操作是可见的、可更新的，等等。
            // 示例:
            config.SetEntitySetAccessRule("*", EntitySetRights.All);
            // config.SetServiceOperationAccessRule("MyServiceOperation", ServiceOperationRights.All);
        }
    }
}
