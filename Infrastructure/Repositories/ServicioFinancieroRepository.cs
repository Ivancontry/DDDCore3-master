using Domain.Contracts;
using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories
{
    public class ServicioFinancieroRepository : GenericRepository<ServicioFinanciero>, IServicioFinancieroRepository
    {
        public ServicioFinancieroRepository(IDbContext context)
              : base(context)
        {
            
        }

    }
}
