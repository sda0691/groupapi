using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq.Expressions;
using System.Data.Entity;
using System.Data.Entity.Validation;

namespace API.Models
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        internal DbContext context;
        internal DbSet<T> DbSet= null;

        public GenericRepository(DbContext context)
        {
            DbSet = context.Set<T>();
            this.context = context;
        }


        public void Insert(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");

                this.DbSet.Add(entity);
            }
            catch (DbEntityValidationException dbEx)
            {
                throw new Exception("insert error");
            }
        }

        public void Update(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");

                this.context.Entry(entity).State = EntityState.Modified;
                //this.context.Entry(entity).State = EntityState.Added;
            }
            catch (DbEntityValidationException dbEx)
            {
                throw new Exception("Update Error");
            }
        }
        public void Delete(T entity)
        {
            DbSet.Attach(entity);
            this.DbSet.Remove(entity);
        }

        //public IQueryable<T> SearchFor(Expression<Func<T, bool>> predicate)
        //{
        //    return DataTable.Where(predicate);
        //}

        public IEnumerable <T> GetAll()
        {
            return DbSet.AsNoTracking().ToList();
        }

        // use either tableNoTracking or Get
        //public T GetById(int id)
        //{
        //    return DbSet.Find(id);
        //}

        public virtual IQueryable<T> TableNoTracking
        {
            get
            {
                return this.Entities.AsNoTracking();
            }
        }
        public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> where)
        {
            return DbSet.Where(where).AsNoTracking().ToList();
        }

        public T Get(Expression<Func<T, bool>> where)
        {
            return DbSet.Where(where).AsNoTracking().FirstOrDefault<T>();
        }
        public virtual IEnumerable<T> GetManyEager1(Expression<Func<T, bool>> where, string child)
        {
            return DbSet.Include(child).Where(where).AsNoTracking().ToList();
        }
        public virtual IList<T> GetManyEager(Expression<Func<T, bool>> where, params string[] navigationProperties)
        {
            List<T> list;
            IQueryable<T> dbQuery = context.Set<T>();
            //foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
            //    dbQuery = dbQuery.Include<T, object>(navigationProperty);

            foreach (var include in navigationProperties)
            {
                dbQuery = dbQuery.Include(include);
            }
            list = dbQuery
                .AsNoTracking()
                .Where(where)

                .ToList<T>();
            return list;
        }
        public virtual IQueryable<T> Table
        {
            get
            {
                return this.DbSet;
            }
        }
        // not being used
        protected virtual IDbSet<T> Entities
        {
            get
            {
                if (DbSet == null)
                    DbSet = context.Set<T>();
                return DbSet;
            }
        }
        public virtual IEnumerable<T> ExecWithStoreProcedure(string query, params object[] parameters)
        {
            return context.Database.SqlQuery<T>(query, parameters).ToList();
        }
    }
}