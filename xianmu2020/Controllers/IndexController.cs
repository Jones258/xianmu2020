using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Expressions;
using Mods;
using Service;
using xianmu2020.Model;

namespace xianmu2020.Controllers
{
    public class IndexController : Controller
    {
        // GET: Index
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Findex()
        {
            //https://github.com/Jones258/xianmu2020.git
            return View();
        }

        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Homes()
        {
            return View();
        }

        /// <summary>
        /// 登录页面
        /// </summary>
        /// <returns></returns>
        public ActionResult QueryLogin()
        {
            return View();
        }

        #region 系统设置
        /// <summary>
        /// 员工管理页面视图
        /// </summary>
        /// <returns></returns>
        public ActionResult QueryEmployeesPage()
        {
            //测试 
            ViewBag.Type = new SelectList("");
            return View();
        }

        /// <summary>
        /// 角色管理页面视图
        /// </summary>
        /// <returns></returns>
        public ActionResult QueryRolePage()
        {

            return View();
        }

        /// <summary>
        /// 部门管理页面视图
        /// </summary>
        /// <returns></returns>
        public ActionResult QuerySectionPage()
        {

            return View();
        }

        /// <summary>
        /// 菜单管理页面视图
        /// </summary>
        /// <returns></returns>
        public ActionResult QueryMenuPage()
        {

            //测试 
            ViewBag.Type = new SelectList("");
            return View();
        }
        //权限
        public ActionResult Qx()
        {
            return View();
        }
        #endregion


        #region 基本资料

        /// <summary>
        /// 库位管理
        /// </summary>
        /// <returns></returns>
        public ActionResult QuerySeatingPage() {
            //测试 
            ViewBag.Type = new SelectList("");
            return View();
        }

        /// <summary>
        /// 供应商管理
        /// </summary>
        /// <returns></returns>
        public ActionResult QuerySupplierPage() {

            //测试 
            ViewBag.Type = new SelectList("");
            return View();
        }

        /// <summary>
        /// 客户管理
        /// </summary>
        /// <returns></returns>
        public ActionResult QueryClientPage() {

            return View();
        }

        /// <summary>
        /// 计量单位
        /// </summary>
        /// <returns></returns>
        public ActionResult QueryMeasureUnitPage() {

            return View();
        }

        /// <summary>
        /// 产品类别
        /// </summary>
        /// <returns></returns>
        public ActionResult QueryProductSortPage()
        {
            return View();
        }

        /// <summary>
        /// 产品管理
        /// </summary>
        /// <returns></returns>
        public ActionResult QueryProductGLPage()
        {
            //测试 
            ViewBag.Type = new SelectList("");
            return View();
        }






        #endregion


        #region 仓库作业
        /// <summary>
        /// 入库管理页面视图
        /// </summary>
        /// <returns></returns>
        public ActionResult QueryStorage()
        {
            var StStorageDJTypeService = new StStorageDJTypeService();
            var model = StStorageDJTypeService.GetAll();
            model.Insert(0, new ChuBaoPanTuiTypes() {CBPTTid=0,DaBillTYpeName="请选择入库单类型" });
            ViewBag.StStorageDJType = new SelectList(model, "CBPTTid", "DaBillTYpeName");

            ViewBag.Type = new SelectList("");
            return View();
        }

        //加载数据方法1
        public ActionResult GetStorage(RequestDto re) {
            Expression<Func<StStorage, bool>> where = item => item.State==1;
            if (!string.IsNullOrEmpty(re.StoOrderId))
            {
                where = where.And(item=>item.StoOrderId.IndexOf(re.StoOrderId)!=-1);
            }
            if (re.Start!=null)
            {
                where = where.And(item => item.CreateTime >= re.Start);
            }
            if (re.End!=null)
            {
                where = where.And(item=>item.CreateTime<= re.End);
            }
            
            var StorageService = new StStorageService();
            var StorageList = StorageService.GetByWhere(where);
            var newform = StorageList.Select(item => new {
                StoOrderId = item.StoOrderId,StoType = item.StoType,SuppliersType = item.SuppliersType,GoodsTotal=item.GoodsTotal,
                TotalMoney = item.TotalMoney,StStorageState = item.StStorageState,MakingSingle = item.MakingSingle, CreateTime = Convert.ToDateTime(item.CreateTime).ToString("yyyy-MM-dd")
            });
            var result = new {
                Storage = newform
            };
            return Json(result,JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 新增入库单and入库产品视图
        /// </summary>
        /// <returns></returns>
        public ActionResult QueryStorageAdd()
        {
            //测试 
            ViewBag.Type = new SelectList("");
            return View();
        }

        /// <summary>
        /// 出库管理页面视图
        /// </summary>
        /// <returns></returns>
        public ActionResult QueryDelivery()
        {
            //测试 
            ViewBag.Type = new SelectList("");
            return View();
        }

        /// <summary>
        /// 添加出库单and添加出库产品出库视图
        /// </summary>
        /// <returns></returns>
        public ActionResult QueryAddSingle()
        {
            //测试 
            ViewBag.Type = new SelectList("");
            return View();
        }

        /// <summary>
        /// 报损管理页面视图
        /// </summary>
        /// <returns></returns>
        public ActionResult QueryBreakagePage()
        {
            //测试 
            ViewBag.Type = new SelectList("");
            return View();
        }

        /// <summary>
        /// 添加报损单and添加报损产品视图
        /// </summary>
        /// <returns></returns>
        public ActionResult QueryBreakageAdd()
        {
            //测试 
            ViewBag.Type = new SelectList("");
            return View();
        }

        /// <summary>
        /// 盘点管理页面视图
        /// </summary>
        /// <returns></returns>
        public ActionResult QueryCheckPage()
        {
            //测试 
            ViewBag.Type = new SelectList("");
            return View();
        }

        /// <summary>
        /// 新增盘点and盘点目标页面视图
        /// </summary>
        /// <returns></returns>
        public ActionResult QueryCheckDanCheckMbAdd()
        {
            //测试 
            ViewBag.Type = new SelectList("");
            return View();
        }

        /// <summary>
        /// 退货管理页面视图
        /// </summary>
        /// <returns></returns>
        public ActionResult QueryRefundPage()
        {
            //测试 
            ViewBag.Type = new SelectList("");
            return View();
        }

        /// <summary>
        /// 新增产品退货页面视图
        /// </summary>
        /// <returns></returns>
        public ActionResult QueryRefundThAdd()
        {
            //测试 
            ViewBag.Type = new SelectList("");
            return View();
        }
        #endregion
       
    }
}