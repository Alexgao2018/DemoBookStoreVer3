using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyBookShopDao.Model;

namespace MyBookShopDao.DAL
{
    public class DBHelper<TEntity> where TEntity : class
    {
        internal DbContext context;
        internal DbSet<TEntity> dbSet;

        public DBHelper()
        {
            context = new MyBookShopEntities();
            dbSet = context.Set<TEntity>();
            context.Configuration.ValidateOnSaveEnabled = false;
        }
        public DBHelper(DbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
            context.Configuration.ValidateOnSaveEnabled = false;
        }

        //按主键查找对象，Find先去查缓存，如果不存在则查数据库（Db）
        public virtual TEntity GetByID(object id)
        {
            return dbSet.Find(id);
        }
        //插入
        public virtual void Insert(TEntity entity)
        {
            dbSet.Add(entity);

        }
        //删除
        public virtual void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }
        //使用替身删除
        public virtual void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);

        }
        //修改
        public virtual void Update(TEntity entityToUpdate)
        {
            if (context.Entry(entityToUpdate).State == EntityState.Detached)
            {
                dbSet.Attach(entityToUpdate);//附加到上下文
            }
            
            //修改状态
            context.Entry(entityToUpdate).State = EntityState.Modified;

        }
    }
}
