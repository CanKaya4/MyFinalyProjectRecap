using Core.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess.EntityFramework
{
    //bana bir TEntity ver yani çalışacağım tabloyu ver. Product,Category veya customer gibi
    //Bir de TContext ver. Northwind context gibi
    public class EfEntityRepositoryBase<TEntity,TContext>:IEntityRepository<TEntity> where TEntity : class, IEntity, new() where TContext : DbContext, new()
    {
        public void Add(TEntity entity)
        {
            using (TContext northwindContext = new TContext())
            {
                //Referansı yakala,o aslında eklenecek bir nesne, ve ekle. demek
                var addedEntity = northwindContext.Entry(entity);
                addedEntity.State = EntityState.Added;
                northwindContext.SaveChanges();
            }
        }

        public void Delete(TEntity entity)
        {
            using (TContext northwindContext = new TContext())
            {
                var deletedEntity = northwindContext.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                northwindContext.SaveChanges();
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (TContext northwindContext = new TContext())
            {
                return northwindContext.Set<TEntity>().SingleOrDefault(filter);
            }
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            //eğer bir filtre verilmemişse tüm ürünleri getir. if ile yazılacak  
            using (TContext northwindContext = new TContext())
            {
                //ternary operatörü ile yazım
                // Eğer filtre null ise burayı çalıştır                            //Eğer filtre verilmişse burayı çalıştır.
                // return filter == null ? northwindContext.Set<Product>().ToList() : northwindContext.Set<Product>().Where(filter).ToList();


                //Normal yazım
                if (filter == null)
                {
                    return northwindContext.Set<TEntity>().ToList();
                }
                else
                {
                    return northwindContext.Set<TEntity>().Where(filter).ToList();
                }
            }
        }

        public void Update(TEntity entity)
        {
            using (TContext northwindContext = new TContext())
            {
                var updateEntity = northwindContext.Entry(entity);
                updateEntity.State = EntityState.Modified;
                northwindContext.SaveChanges();
            }
        }
    }
}
