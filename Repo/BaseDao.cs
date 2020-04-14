using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mods;
using System.Data.Entity;
using System.Linq.Expressions;

namespace Repo
{
   public class BaseDao<T> where T :class
    {
        DbContext dbcontext = null;
        public DbContext MydbContext {
            get {
                if (dbcontext==null)
                {
                    dbcontext = new DbContext("name=WarehouseSystemEntities");
                }
                return dbcontext;
            }
        }

        //查询
        public List<T> GetAll() {
            return MydbContext.Set<T>().ToList();
        }
        //条件查询
        public List<T> GetByWhere(Expression<Func<T,bool>>where) {
            return MydbContext.Set<T>().Where(where).ToList();
        }

        //分页查询
        public List<T> GetByWhereAsc<orderByT>(Expression<Func<T, bool>> where, Expression<Func<T, orderByT>> orderBy, ref int pageIndex, ref int pageCount, ref int count, int pageSize)
        {
            count = MydbContext.Set<T>().Where(where).Count();  //总条数
            pageCount = count % pageSize == 0 ? count / pageSize : count / pageSize + 1;  //总页数
            if (pageIndex <= 1 || count == 0) pageIndex = 1;
            else if (pageIndex >= pageCount) pageIndex = pageCount;
            var filterCount = (pageIndex - 1) * pageSize;
            return MydbContext.Set<T>().Where(where).OrderBy(orderBy).Skip(filterCount).Take(pageSize).ToList();
        }



        //添加
        public bool Add(T modal) {
            MydbContext.Entry<T>(modal).State = EntityState.Added;
            return MydbContext.SaveChanges() > 0;
        }
        //添加
        public bool Delete(T modal)
        {
            MydbContext.Entry<T>(modal).State = EntityState.Deleted;
            return MydbContext.SaveChanges() > 0;
        }
        //修改
        public bool Update(T modal)
        {
            MydbContext.Entry<T>(modal).State = EntityState.Modified;
            return MydbContext.SaveChanges() > 0;
        }



    }
}
