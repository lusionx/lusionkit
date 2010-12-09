using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alxf.ProduceCard.BLL
{
    public interface IStateAction
    {
        void Next(Card_ApplyModel pk);
        void Previous(Card_ApplyModel pk);
    }
}
