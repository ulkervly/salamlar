using App.Core.Entities;
using App.Core.Repositories;
using App.Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Repositories
{
    public class MemeberRepository : GenericRepository<Member>,IMemeberRepository
    {
        public MemeberRepository(AppDbContext context) : base(context)
        {
        }
    }
}
