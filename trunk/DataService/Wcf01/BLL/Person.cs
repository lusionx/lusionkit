using System.Data.Services.Common;
using System.Linq;

namespace Wcf01
{
    public partial class KjptDB
    {
        public IQueryable<Person> Persons
        {
            get
            {
                var q = from a in this.cme_persons
                        join b in this.com_units on a.unit equals b.unit_id
                        join c in this.com_persons on a.com_person_id equals c.com_person_id                         
                        select new Person { Name = c.person_name, Hospital = b.unit_name };
                return q;
            }
        }

         
    }

    [DataServiceKey("Name")]
    public class Person
    {
        public string Name { get; set; }

        public string Hospital { get; set; }
    }
}
