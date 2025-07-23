using System;
using System.Collections.Generic;

namespace CadastroMetasVendedores.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        // Operações básicas CRUD
        int Insert(T entity);
        bool Update(T entity);
        bool Delete(int id);
        T GetById(int id);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetActive();

        // Operações específicas
        bool Exists(int id);
        bool ActivateDeactivate(int id, bool activate);
    }
}