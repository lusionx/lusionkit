using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;


namespace Perpor.DAL
{
    public partial class PerporDBDataContext
    {
        public override void SubmitChanges(ConflictMode failureMode)
        {
            try
            {
                base.SubmitChanges(ConflictMode.ContinueOnConflict);
            }
            catch (ChangeConflictException)
            {
                foreach (var x in this.ChangeConflicts)
                {
                    x.Resolve(RefreshMode.KeepCurrentValues);
                }
            }
        }
    }

    public partial class core_UserPage
    {

    }
}
