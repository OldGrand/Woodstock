using System;
using System.Collections.Generic;
using System.Text;

namespace Woodstock.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Product> ProductRepository { get; }
        IRepository<Sale> SaleRepository { get; }
        IRepository<Supply> SupplyRepository { get; }

        void Save();
    }
}
