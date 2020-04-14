using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Mods;
using Repo;

namespace Service
{
   public class BaseServices<T> where T :class
    {
        BaseDao<T> baseServices = null;
        public BaseDao<T> MyServices {
            get {
                if (baseServices==null)
                {
                    baseServices = new BaseDao<T>();
                }
                return baseServices;
            }
        }

        //查询
        public List<T> GetAll()
        {
            return MyServices.GetAll();
        }
        //条件查询
        public List<T> GetByWhere(Expression<Func<T, bool>> where)
        {
            return MyServices.GetByWhere(where);
        }

        //分页
        public List<T> GetByWhereAsc<orderByT>(Expression<Func<T, bool>> where, Expression<Func<T, orderByT>> orderBy, ref int pageIndex, ref int pageCount, ref int count, int pageSize)
        {
            return MyServices.GetByWhereAsc(where, orderBy, ref pageIndex, ref pageCount, ref count, pageSize);
        }


        //添加
        public bool Add(T modal)
        {
            return MyServices.Add(modal);
        }
        //添加
        public bool Delete(T modal)
        {
            return MyServices.Delete(modal);
        }
        //修改
        public bool Update(T modal)
        {
            return MyServices.Update(modal);
        }
    }
}
