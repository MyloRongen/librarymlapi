using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace librarymylo_BLL.Models
{
    [Keyless]
    public class PersonCategory
    {
        public string PersonId { get; set; }
        public Person Person { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
