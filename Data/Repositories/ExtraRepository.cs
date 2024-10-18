using Application.Interfaces;
using Infrastructure.Interfaces;
using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ExtraRepository : BaseRepository<Extra>, IExtraRepository
    {
        public ExtraRepository(Order2ServeDbContext dbContext, ILog logger) : base(dbContext, logger)
        {
        }
    }
}
