using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
        #endregion


        #region 仓库作业
        /// <summary>
        /// 入库管理页面视图
        /// </summary>
        /// <returns></returns>
        public ActionResult QueryStorage()
        {
            //测试 
            ViewBag.Type = new SelectList("");
            return View();
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