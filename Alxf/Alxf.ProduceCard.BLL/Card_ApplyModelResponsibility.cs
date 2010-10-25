using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alxf.Frame.ModelBase;
using Alxf.ProduceCard.DAL;

namespace Alxf.ProduceCard.BLL
{
    public class Card_ApplyModelResponsibility : ResponsibilityBase<Card_ApplyModel, ManufactureTestDataContext>
    {
        public override void Insert(Card_ApplyModel model)
        {
            var dm = model.DataModel;

            var q = from a in DB.Card_Applies

                    where a.AdditionalQuantity == 1 && a.ConfirmQuantity == 1

                    select new { a.AdditionalQuantity, a.ConfirmQuantity };

            DB.Card_Applies.InsertOnSubmit(dm);

            SaveChanges();
        }

        public override void Update(Card_ApplyModel model)
        {
            throw new NotImplementedException();
        }

        public override void Delete(Card_ApplyModel model)
        {
            throw new NotImplementedException();
        }

        public override IQueryable<Card_ApplyModel> Query()
        {
            throw new NotImplementedException();
        }
    }
}
