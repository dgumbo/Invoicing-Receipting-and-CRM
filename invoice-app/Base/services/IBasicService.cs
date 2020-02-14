using invoice_app;
using invoice_demo_app.Basic.models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Threading.Tasks;

/**
* @author denzil
* @param <T> 
*/
namespace invoice_demo_app.Basic.services
{
    public interface IBasicService<T> where T : BaseEntity
    {
        public abstract AppDbContext GetDbContext();
        public abstract DbSet<T> GetDbSet();

        public DbSet<T> FindAll()
        {
            return GetDbSet();
        }

        public List<T> FindAllEntities()
        {
            return GetDbSet().ToListAsync().Result;
        }

        public async ValueTask<T> FindAsync(int id)
        {
            return await GetDbSet().FindAsync(id); 
        } 

        public T Find(int id)
        {
            return GetDbSet().Find (id);
        }

        public async ValueTask<T> CreateAsync(T data)
        {
            data.ValidateBaseEntityProperties();
            await GetDbSet().AddAsync(data);
            GetDbContext().SaveChanges();

            return data;
        } 

        public T Create(T data)
        {
            data.ValidateBaseEntityProperties();
            GetDbSet().Add(data);
            GetDbContext().SaveChanges();
            return data;
        }

        public async ValueTask<T> UpdateAsync(T updateData)
        {
            updateData.ValidateBaseEntityProperties();
            EntityEntry<T> t = GetDbSet().Attach(updateData); 
            GetDbContext().Entry(t).State = EntityState.Modified;

            await GetDbContext().SaveChangesAsync();
            return t.Entity;
        }

        public T Update(T updateData)
        {
            updateData.ValidateBaseEntityProperties();
            EntityEntry<T> t = GetDbSet().Attach(updateData);
            t.State = EntityState.Modified;
            GetDbContext().SaveChanges();
            return t.Entity;
        }

        public async ValueTask<T> DeleteAsync(int id)
        {
            T entity = await GetDbSet().FindAsync(id); 

            if (entity != null)
            {
                GetDbSet().Remove(entity);
                await GetDbContext().SaveChangesAsync();
                return entity;
            }
            return null;
        }

        public  T Delete(int id)
        {
            T entity =  GetDbSet().Find (id);
            if (entity != null)
            {
                GetDbSet().Remove(entity);
                GetDbContext().SaveChanges();
                return entity;
            }
            return null;
        }
    }
}