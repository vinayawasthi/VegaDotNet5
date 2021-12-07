

using Vega.Web.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;
using Vega.Web.Models;
using System.Collections.Generic;

namespace Vega.Web.Persistence
{
    public class UnitOfWOrk : IUnitOfWork
    {
        private readonly AppDbContext dbContext;

        public UnitOfWOrk(AppDbContext dbContext){
            this.dbContext = dbContext;
        }
        public async Task<int> CompleteAsync()
        {
            return await this.dbContext.SaveChangesAsync();
        }
    }
}