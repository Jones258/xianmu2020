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

    }
}