using EntityFramework.DynamicFilters;
using NetCommunitySolution.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCommunitySolution.Migrations.SeedData
{
    public class InitialHostDbBuilder
    {
        private readonly NetCommunitySolutionDbContext _context;

        public InitialHostDbBuilder(NetCommunitySolutionDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            _context.DisableAllFilters();
            new DefaultCustomerData(_context).Create();
        }
    }
}
