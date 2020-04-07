using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mods;
using System.Linq.Expressions;
using System.Data.Entity;

namespace Repo
{
    public class BaseRepository<T> where T : class
    {
        DbContext dbContext = null;
        public DbContext MyDbContext
        {
            get
            {
                if (dbContext == null)
                {
                    dbContext = new DbContext("name=DwEntities");
                }
                return dbContext;
            }
        }

        /// <summary>
        /// 获取所有商品信息
        /// </summary>
        /// <returns></returns>
        public List<T> GetAll()
        {
            return MyDbContext.Set<T>().ToList();
        }

        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public List<T> GetByWhere(Expression<Func<T, bool>> where)
        {
            /*加了一个.AsNoTracking() 原本是 return MyDbContext.Set<T>().Where(where).ToList();
            解决了 Attaching an entity of type 
            'Mods.dongwu' failed because another entity of the same type
                already has the same primary key value. This can happen 
                when using the 'Attach' method or setting the state of an 
                entity to 'Unchanged' or 'Modified' if any entities in the 
                graph have conflicting key values. This may be because some
                entities are new and have not yet received database-generated
                key values. In this case use the 'Add' method or the 'Added' 
                entity state to track the graph and then set the state of 
                non-new entities to 'Unchanged' or 'Modified' as appropriate.  报错 
                网站 https://blog.csdn.net/liunianqingshi/article/details/49925645?utm_source=blogxgwz4 */
            return MyDbContext.Set<T>().Where(where).AsNoTracking().ToList();
        }

        /// <summary>
        /// 条件升序查询 带分页 
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public List<T> GetByWhereAsc<orderByT>(Expression<Func<T, bool>> where, Expression<Func<T, orderByT>> orderBy, ref int pageIndex, ref int count, ref int pageCount, int pageSize)
        {
            count = MyDbContext.Set<T>().Where(where).Count(); //总条数
            pageCount = count % pageSize == 0 ? count / pageSize : count / pageSize + 1; //总页数
            if (pageIndex <= 1 || count == 0) pageIndex = 1;
            else if (pageIndex >= pageCount) pageIndex = pageCount;

            var filterCount = (pageIndex - 1) * pageSize; 
            return MyDbContext.Set<T>().Where(where).OrderBy(orderBy).Skip(filterCount).Take(pageSize).ToList();
        }

        /// <summary>
        /// 条件降序查询 带分页 
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public List<T> GetByWhereDesc<orderByT>(Expression<Func<T, bool>> where, Expression<Func<T, orderByT>> orderBy, ref int pageIndex, ref int count, ref int pageCount, int pageSize)
        {
            count = MyDbContext.Set<T>().Where(where).Count(); //总条数
            pageCount = count % pageSize == 0 ? count / pageSize : count / pageSize + 1; //总页数
            if (pageIndex <= 1 || count == 0) pageIndex = 1;
            else if (pageIndex >= pageCount) pageIndex = pageCount;

            var filterCount = (pageIndex - 1) * pageSize;
            return MyDbContext.Set<T>().Where(where).OrderByDescending(orderBy).Skip(filterCount).Take(pageSize).ToList();
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Add(T model)
        {
            MyDbContext.Entry<T>(model).State = EntityState.Added;
            var result = MyDbContext.SaveChanges();
            return result > 0;
        }

        /// <summary>
        /// 修改
        /// 修改的实体 必须主键有值
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Update(T model)
        {
            MyDbContext.Entry<T>(model).State = EntityState.Modified;
            var result = MyDbContext.SaveChanges();
            return result > 0;
        }


        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Delete(T model)
        {
            MyDbContext.Entry<T>(model).State = EntityState.Deleted;
            var result = MyDbContext.SaveChanges();
            return result > 0;
        }
    }
}
