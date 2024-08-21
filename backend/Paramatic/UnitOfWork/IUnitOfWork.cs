using System;
using Paramatic.Repositories;

namespace Paramatic.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> Repository<T>() where T : class;
        int Complete();
    }
}