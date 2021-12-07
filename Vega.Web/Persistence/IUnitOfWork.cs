using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;
using Vega.Web.Models;
using System.Collections.Generic;

namespace Vega.Web.Persistence
{
    public interface IUnitOfWork
    {
        Task<int> CompleteAsync();
    }
}