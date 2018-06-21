using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq.Expressions;
namespace API.Models

{
    public interface IGenericRepository<T>
    {
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        IEnumerable<T> GetAll();
        //T GetById(int id);
        IQueryable<T> Table { get; }
        IQueryable<T> TableNoTracking { get; }
        IEnumerable<T> GetMany(Expression<Func<T, bool>> where);
        IEnumerable<T> GetManyEager1(Expression<Func<T, bool>> where, string child);
        IList<T> GetManyEager(Expression<Func<T, bool>> where, params string[] navigationProperties);  
        T Get(Expression<Func<T, bool>> where);
        IEnumerable<T> ExecWithStoreProcedure(string query, params object[] parameters);
    }
}