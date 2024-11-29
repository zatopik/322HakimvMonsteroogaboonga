using Kazek.DB;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kazek
{
    internal class Us
    {
        public static ESHKEREEntities db = new ESHKEREEntities();
        public static Pofile LoggedUser;
        public DbSet<Pofile> Users { get; set; }

    }
}
