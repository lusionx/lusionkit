using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alxf.Frame.ModelBase;
using Alxf.ProduceCard.DAL;

namespace Alxf.ProduceCard.BLL
{
    public class Card_ApplyStateModelResponsibility : ResponsibilityBase<Card_ApplyStateModel, ManufactureTestDataContext>
    {
        public override void Insert(Card_ApplyStateModel model)
        {
            throw new NotImplementedException();
        }

        public override void Update(Card_ApplyStateModel model)
        {
            throw new NotImplementedException();
        }

        public override void Delete(Card_ApplyStateModel model)
        {
            throw new NotImplementedException();
        }

        public override IQueryable<Card_ApplyStateModel> Query()
        {
            throw new NotImplementedException();
        }

        public void ChangeState(Guid applyid, string user, ApplyState st)
        {
            Card_ApplyStateModel newstate = new Card_ApplyStateModel();
            newstate.ChangeTime = DateTime.Now;
            newstate.ID = Guid.NewGuid();
            newstate.IsCurrent = true;
            newstate.State = (int)st;
            newstate.HandlerUser = user;
            newstate.ApplyID = applyid;
            this.DB.Card_ApplyStates.Single(a => a.ApplyID == applyid && a.IsCurrent).IsCurrent = false;
            this.DB.Card_ApplyStates.InsertOnSubmit(newstate.DataModel);
            SaveChanges();
        }
    }
}
