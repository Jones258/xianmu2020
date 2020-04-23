using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Expressions;
using Mods;
using Service;
using xianmu2020.Model;
using System.Web.Security;

namespace xianmu2020.Controllers
{
    public class IndexController : Controller
    {
        public int pageSize
        {
            get { return 2; }
        }
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

        [HttpPost]
        //登录方法
        public ActionResult Login(Admin admin)
        {
            var adminService = new adminService();
            var finUser = adminService.GetByWhere(item => item.UserName.Equals(admin.UserName) && item.Passworld.Equals(admin.Passworld)).FirstOrDefault();
            if (true)
            {
                if (finUser != null)
                {
                    FormsAuthentication.SetAuthCookie(finUser.Id.ToString(), false);
                    return RedirectToAction("Homes", "Index");
                }
                return View("QueryLogin");
            }
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
        public ActionResult QuerySeatingPage()
        {
            //测试 
            ViewBag.Type = new SelectList("");
            return View();
        }

        /// <summary>
        /// 供应商管理
        /// </summary>
        /// <returns></returns>
        public ActionResult QuerySupplierPage()
        {

            //测试 
            ViewBag.Type = new SelectList("");
            return View();
        }

        /// <summary>
        /// 客户管理
        /// </summary>
        /// <returns></returns>
        public ActionResult QueryClientPage()
        {

            return View();
        }

        /// <summary>
        /// 计量单位
        /// </summary>
        /// <returns></returns>
        public ActionResult QueryMeasureUnitPage()
        {

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
        //产品类别查询
        public ActionResult GetProductSort(ProductSortDto dto)
        {
            Expression<Func<ProductSort, bool>> where = item => item.State == 1;
            if (!string.IsNullOrEmpty(dto.ProductSortName))
            {
                where = where.And(item => item.ProductSortName.IndexOf(dto.ProductSortName) != -1 || item.ProductSortId.IndexOf(dto.ProductSortName) != -1);
            }
            var pageIndex = dto.PageIndex;
            var pageCount = 0;
            var count = 0;
            var ProductSortModel = new ProductSortService().GetByWhereAsc(where, item => item.CreationTime, ref pageIndex, ref pageCount, ref count, pageSize);
            var ProductSort = ProductSortModel.Select(item => new {
                ProductSortId = item.ProductSortId, ProductSortName = item.ProductSortName,
                CreationMen = item.CreationMen,
                CreationTime = Convert.ToDateTime(item.CreationTime).ToString("yyyy-MM-dd"),
                Remark = item.Remark
            });
            var result = new { ProductAction = ProductSort, PageIndex = pageIndex, PageCount = pageCount, Count = count };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        //添加产品类别
        public ActionResult GetProductSortAdd(ProductSort productSort)
        {
            productSort.CreationMen = "administrator";
            productSort.CreationTime = DateTime.Now;
            productSort.State = 1;
            var ProductSortModel = new ProductSortService().Add(productSort);
            var addresult = new
            {
                AddProduct = ProductSortModel,
                Msg = ProductSortModel ? "添加成功" : "添加失败！"
            };
            return Json(addresult, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        //删除产品
        public ActionResult GetDeleteProductSort(string id) {
            var deleteProductSort = new ProductSortService();
            var data = deleteProductSort.GetByWhere(item => item.ProductSortId == id).SingleOrDefault();
            data.State = 0;
            deleteProductSort.Update(data);
            return Json(JsonRequestBehavior.AllowGet);
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
            var model = StStorageDJTypeService.GetByWhere(item => item.State == 1);
            model.Insert(0, new ChuBaoPanTuiTypes() { CBPTTid = 0, DaBillTYpeName = "请选择入库单类型" });
            ViewBag.StStorageDJType = new SelectList(model, "CBPTTid", "DaBillTYpeName");

            ViewBag.Type = new SelectList("");
            return View();
        }

        //加载数据方法1
        public ActionResult GetStorage(RequestDto re, int zt) {
            Expression<Func<StStorage, bool>> where = item => item.State == 1;
            if (!string.IsNullOrEmpty(re.StoOrderId))
            {
                where = where.And(item => item.StoOrderId.IndexOf(re.StoOrderId) != -1);
            }
            if (re.StoType != 0)
            {
                where = where.And(item => item.StoType==re.StoType);
            }
            if (!string.IsNullOrEmpty(re.SuppliersType))
            {
                where = where.And(item=>item.SuppliersType.IndexOf(re.SuppliersType)!=-1);
            }
            if (re.Start != null)
            {
                where = where.And(item => item.CreateTime >= re.Start);
            }
            if (re.End != null)
            {
                where = where.And(item => item.CreateTime <= re.End);
            }
            if (re.zt != 0)
            {
                where = where.And(item => item.StStorageState == re.zt);
            }

            var pageIndex = re.PageIndex;
            var pageCount = 0;
            var count = 0;
            var StorageList = new StStorageService().GetByWhereAsc(where, item => item.CreateTime, ref pageIndex, ref pageCount, ref count, pageSize);
            var newform = StorageList.Select(item => new {
                Stoid = item.Stoid,
                StoOrderId = item.StoOrderId, StoType = item.StoType, SuppliersType = item.SuppliersType, GoodsTotal = item.GoodsTotal,
                TotalMoney = item.TotalMoney, StStorageState = item.StStorageState, MakingSingle = item.MakingSingle, CreateTime = Convert.ToDateTime(item.CreateTime).ToString("yyyy-MM-dd")
            });
            var result = new {
                Storage = newform, PageIndex = pageIndex, PageCount = pageCount, Count = count
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        //删除（修改入库单里的状态）
        [HttpPost]
        public ActionResult GetDeleteRK(int id)
        {
            var deleteStStorag = new StStorageService();
            var data = deleteStStorag.GetByWhere(item => item.Stoid == id).SingleOrDefault();
            data.State = 0;
            deleteStStorag.Update(data);
            return Json(JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        //大拇指改状态（审核通过）
        public ActionResult StStorageUpSH(int Stoid,int StStorageStates) {
            var UpShStStorag = new StStorageService();
            var data = UpShStStorag.GetByWhere(item=>item.Stoid==Stoid).SingleOrDefault();
            data.StStorageState = StStorageStates;
            UpShStStorag.Update(data);
            return Json(JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 新增入库单and入库产品视图
        /// </summary>
        /// <returns></returns>
        public ActionResult QueryStorageAdd()
        {
            var model = new StStorageDJTypeService().GetByWhere(item=>item.State==1);
            model.Insert(0, new ChuBaoPanTuiTypes() { CBPTTid = 0, DaBillTYpeName = "请选择入库单类型" });
            ViewBag.StStorageDJType = new SelectList(model, "CBPTTid", "DaBillTYpeName");
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
            var model = new StStorageDJTypeService().GetByWhere(item => item.State==5);
            model.Insert(0,new ChuBaoPanTuiTypes() { CBPTTid=0,DaBillTYpeName= "请选择出库单类型" });
            ViewBag.DeliveryType = new SelectList(model,"CBPTTid", "DaBillTYpeName");
            //测试 
            ViewBag.Type = new SelectList("");
            return View();
        }
        //获取数据
        public ActionResult GetDeliveryData(RequestDto re) {
            Expression<Func<Delivery, bool>> where = item => item.State == 1 && item.CreationTime >= re.Start && item.CreationTime <= re.End;
            if (re.DeliveryTYpe!=0)
            {
                where = where.And(item=>item.DeliveryTYpe==re.DeliveryTYpe);
            }
            if (!string.IsNullOrEmpty(re.DeliveryId))
            {
                where = where.And(item=>item.DeliveryId.IndexOf(re.DeliveryId) !=-1);
            }
            if (re.zt!=0)
            {
                where = where.And(item=>item.DeliveryAuditState==re.zt);
            }
            var pageIndex = re.PageIndex;
            var pageCount = 0;
            var count = 0;
            var DeliveryList = new DeliveryService().GetByWhereAsc(where,item=>item.CreationTime,ref pageIndex,ref pageCount,ref count,pageSize);
            var DeliveryNew = DeliveryList.Select(item=> new {
                Did=item.Did,
                DeliveryId=item.DeliveryId,
                DeliveryTYpe=item.DeliveryTYpe,
                ClientNames=item.ClientNames,
                TotalCount=item.TotalCount,
                TotalMoney=item.TotalMoney,
                DeliveryAuditState=item.DeliveryAuditState,
                OperatingMode=item.OperatingMode,
                PreparedBy=item.PreparedBy,
                CreationTime = Convert.ToDateTime(item.CreationTime).ToString("yyyy-MM-dd")
            });
            var result = new { DeliveryAction = DeliveryNew,  PageIndex = pageIndex, PageCount = pageCount, Count = count };
            return Json(result,JsonRequestBehavior.AllowGet);
        }

        //删除
        [HttpPost]
        public ActionResult DeleteDelivery(int Did)
        {
            var IneventoryServices =new DeliveryService().GetByWhere(item => item.Did == Did).SingleOrDefault();
            IneventoryServices.State = 0;
            new DeliveryService().Update(IneventoryServices);
            return Json(JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        //修改大拇指状态(审核)
        public ActionResult UpDeliveryAuditState(int Did,int DeliveryAuditStats) {
            var UpState = new DeliveryService();
            var data = UpState.GetByWhere(item=>item.Did==Did).SingleOrDefault();
            data.DeliveryAuditState = DeliveryAuditStats;
            UpState.Update(data);
            //var UpResult = new {
            //    UpStateAction = UpState
            //};
            return Json(JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 添加出库单and添加出库产品出库视图
        /// </summary>
        /// <returns></returns>
        public ActionResult QueryAddSingle()
        {
            var StStorageDJTypeService = new StStorageDJTypeService();
            var model = StStorageDJTypeService.GetByWhere(item => item.State == 6);
            model.Insert(0, new ChuBaoPanTuiTypes() { CBPTTid = 0, DaBillTYpeName = "请选择报损类型" });
            ViewBag.StStorageDJType = new SelectList(model, "CBPTTid", "DaBillTYpeName");

            var PService = new ProductGLService();
            var model2 = PService.GetAll();
            model2.Insert(0, new ProductGL() { PGLid = 0, ProductName = "请选择产品" });
            ViewBag.ProductGLType = new SelectList(model2, "PGLid", "ProductName");

            var ClientService = new ClientService();
            var model3 = ClientService.GetAll();
            model3.Insert(0, new Client() { Cid = 0, ClientName = "请选择客户名称" });
            ViewBag.ClientType = new SelectList(model3, "Cid", "ClientName");
            return View();
        }
        public int PageSize
        {
            get { return 2; }
        }
        /// <summary>
        /// 报损管理页面视图
        /// </summary>
        /// <returns></returns>
        public ActionResult QueryBreakagePage()
        {
            var StStorageDJTypeService = new StStorageDJTypeService();
            var model = StStorageDJTypeService.GetByWhere(item => item.State == 6);
            model.Insert(0, new ChuBaoPanTuiTypes() { CBPTTid = 0, DaBillTYpeName = "请选择报损类型" });
            ViewBag.StStorageDJType = new SelectList(model, "CBPTTid", "DaBillTYpeName");

            var PService = new ProductGLService();
            var model2 = PService.GetAll();
            model2.Insert(0, new ProductGL() { PGLid = 0, ProductName = "请选择产品" });
            ViewBag.ProductGLType = new SelectList(model2, "PGLid", "ProductName");

            var ClientService = new ClientService();
            var model3 = ClientService.GetAll();
            model3.Insert(0, new Client() { Cid = 0, ClientName = "请选择客户名称" });
            ViewBag.ClientType = new SelectList(model3, "Cid", "ClientName");
            return View();
        }
        public ActionResult Data(int BGLid)
        {

            var ser = new BreakageGLService();
            var model = ser.GetByWhere(item => item.BGLid.Equals(BGLid));

            var newform = model.Select(item => new
            {
                BGLid = item.BGLid,
                Standby4 = item.Standby4,//报损产品
                Standby3 = item.Standby3,
                BreakageType = item.BreakageType,
                PreparedMan = item.PreparedMan,
                PreparedCount = item.PreparedCount,
                BreakageGLAduitState = item.BreakageGLAduitState,
                CreationMan = item.CreationMan,
                CreationTime = Convert.ToDateTime(item.CreationTime).ToString("yyyy-MM-dd")
            });

            var result = new
            {
                data = newform
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 添加报损单and添加报损产品视图
        /// </summary>
        /// <returns></returns>
        public ActionResult QueryBreakageAdd()
        {
            var StStorageDJTypeService = new StStorageDJTypeService();
            var model = StStorageDJTypeService.GetByWhere(item => item.State == 6);
            model.Insert(0, new ChuBaoPanTuiTypes() { CBPTTid = 0, DaBillTYpeName = "请选择报损类型" });
            ViewBag.StStorageDJType = new SelectList(model, "CBPTTid", "DaBillTYpeName");

            var PService = new ProductGLService();
            var model2 = PService.GetAll();
            model2.Insert(0, new ProductGL() { PGLid = 0, ProductName = "请选择产品" });
            ViewBag.ProductGLType = new SelectList(model2, "PGLid", "ProductName");

            var ClientService = new ClientService();
            var model3 = ClientService.GetAll();
            model3.Insert(0, new Client() { Cid = 0, ClientName = "请选择客户名称" });
            ViewBag.ClientType = new SelectList(model3, "Cid", "ClientName");

            return View();
        }
        [HttpPost]
        public ActionResult QueryChuRuBaosunProductAdd(BreakageGL gl)
        {
            var ser = new BreakageGLService();
            gl.State = 1;
            gl.BreakageGLAduitState = 3;
            var saveResu = ser.Add(gl);
            var result = new
            {
                ActionResult = saveResu,
                Msg = saveResu ? "添加成功" : "添加失败"
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        //报损修改
        [HttpPost]
        public ActionResult UpdateBreakagePage(BreakageGL gl)
        {
            var ser = new BreakageGLService();
            var data = ser.GetByWhere(item => item.BGLid == gl.BGLid).SingleOrDefault();
            gl.Standby1 = 0;
            gl.Standby2 = 0;
            gl.State = 1;
            gl.BreakageGLAduitState = data.BreakageGLAduitState;
            var saveResu = ser.Update(gl);
            var result = new
            {
                ActionResult = saveResu,
                Msg = saveResu ? "修改成功" : "修改失败"
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        
        //加载报损数据方法1
        public ActionResult GetBreakageGL(RequestDto re)
        {
            Expression<Func<BreakageGL, bool>> where = item => item.State == 1;

            var BreakageGLService = new BreakageGLService();
            var pageCount = 0;
            var count = 0;
            var pageIndex = re.PageIndex;
            var BreakageList = BreakageGLService.GetByWhereDesc(where, item => item.CreationTime, ref pageIndex, ref pageCount, ref count, PageSize);

            var newform = BreakageList.Select(item => new
            {
                BGLid = item.BGLid,
                Standby4 = item.Standby4,//报损产品
                Standby3 = item.Standby3,
                BreakageType = item.BreakageType,
                PreparedMan = item.PreparedMan,
                PreparedCount = item.PreparedCount,
                BreakageGLAduitState = item.BreakageGLAduitState,
                CreationMan = item.CreationMan,
                CreationTime = Convert.ToDateTime(item.CreationTime).ToString("yyyy-MM-dd")
            });

            var result = new
            {
                Breakage = newform,
                PageIndex = pageIndex,
                PageCount = pageCount,
                Count = count
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        //审核通过
        public ActionResult GetBreakageData(RequestDto re)
        {
            Expression<Func<BreakageGL, bool>> where = item => item.State == 1 && item.BreakageGLAduitState == 3;

            var BreakageGLService = new BreakageGLService();
            var pageCount = 0;
            var count = 0;
            var pageIndex = re.PageIndex;
            var BreakageList = BreakageGLService.GetByWhereDesc(where, item => item.CreationTime, ref pageIndex, ref pageCount, ref count, PageSize);
            var newform = BreakageList.Select(item => new
            {
                BGLid = item.BGLid,
                Standby4 = item.Standby4,//报损产品
                Standby3 = item.Standby3,
                BreakageType = item.BreakageType,
                PreparedMan = item.PreparedMan,
                PreparedCount = item.PreparedCount,
                BreakageGLAduitState = item.BreakageGLAduitState,
                CreationMan = item.CreationMan,
                CreationTime = Convert.ToDateTime(item.CreationTime).ToString("yyyy-MM-dd")
            });

            var result = new
            {
                Breakage = newform,
                PageIndex = pageIndex,
                PageCount = pageCount,
                Count = count
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        //待审核
        public ActionResult GetBreakageData2(RequestDto re)
        {
            Expression<Func<BreakageGL, bool>> where = item => item.State == 1 && item.BreakageGLAduitState == 1;

            var BreakageGLService = new BreakageGLService();
            var pageCount = 0;
            var count = 0;
            var pageIndex = re.PageIndex;
            var BreakageList = BreakageGLService.GetByWhereDesc(where, item => item.CreationTime, ref pageIndex, ref pageCount, ref count, PageSize);
            var newform = BreakageList.Select(item => new
            {
                BGLid = item.BGLid,
                Standby4 = item.Standby4,//报损产品
                Standby3 = item.Standby3,
                BreakageType = item.BreakageType,
                PreparedMan = item.PreparedMan,
                PreparedCount = item.PreparedCount,
                BreakageGLAduitState = item.BreakageGLAduitState,
                CreationMan = item.CreationMan,
                CreationTime = Convert.ToDateTime(item.CreationTime).ToString("yyyy-MM-dd")
            });

            var result = new
            {
                Breakage = newform,
                PageIndex = pageIndex,
                PageCount = pageCount,
                Count = count
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        //审核未通过
        public ActionResult GetBreakageData3(RequestDto re)
        {
            Expression<Func<BreakageGL, bool>> where = item => item.State == 1 && item.BreakageGLAduitState == 2;

            var BreakageGLService = new BreakageGLService();
            var pageCount = 0;
            var count = 0;
            var pageIndex = re.PageIndex;
            var BreakageList = BreakageGLService.GetByWhereDesc(where, item => item.CreationTime, ref pageIndex, ref pageCount, ref count, PageSize);
            var newform = BreakageList.Select(item => new
            {
                BGLid = item.BGLid,
                Standby4 = item.Standby4,//报损产品
                Standby3 = item.Standby3,
                BreakageType = item.BreakageType,
                PreparedMan = item.PreparedMan,
                PreparedCount = item.PreparedCount,
                BreakageGLAduitState = item.BreakageGLAduitState,
                CreationMan = item.CreationMan,
                CreationTime = Convert.ToDateTime(item.CreationTime).ToString("yyyy-MM-dd")
            });

            var result = new
            {

                Breakage = newform,
                PageIndex = pageIndex,
                PageCount = pageCount,
                Count = count
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        //删除
        public ActionResult BreakageDele(int BGLid)
        {
            var BreakageGLService = new BreakageGLService();
            var data = BreakageGLService.GetByWhere(item => item.BGLid == BGLid).SingleOrDefault();
            data.State = 2;
            var BreakageList = BreakageGLService.Update(data);

            return Json(JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        //审核通过
        public ActionResult BreakageUp(int BGLid, int State)
        {
            var BreakageGLService = new BreakageGLService();
            var data = BreakageGLService.GetByWhere(item => item.BGLid == BGLid).SingleOrDefault();

            data.BreakageGLAduitState = State;
            var BreakageList = BreakageGLService.Update(data);

            return Json(JsonRequestBehavior.AllowGet);
        }
        //搜索
        public ActionResult GetBreakageSS(BreakDto re)
        {
            Expression<Func<BreakageGL, bool>> where = item => item.State == 1;
            if (!string.IsNullOrEmpty(re.Standby3))
            {
                where = where.And(item => item.Standby3.Equals(re.Standby3));
            }
            if (re.Start != null)
            {
                where = where.And(item => item.CreationTime >= re.Start);
            }
            if (re.End != null)
            {
                where = where.And(item => item.CreationTime <= re.End);
            }
            var BreakageGLService = new BreakageGLService();
            var pageCount = 0;
            var count = 0;
            var pageIndex = re.PageIndex;
            var BreakageList = BreakageGLService.GetByWhereDesc(where, item => item.CreationTime, ref pageIndex, ref pageCount, ref count, PageSize);

            var newform = BreakageList.Select(item => new
            {
                BGLid = item.BGLid,
                Standby4 = item.Standby4,//报损产品
                Standby3 = item.Standby3,
                BreakageType = item.BreakageType,
                PreparedMan = item.PreparedMan,
                PreparedCount = item.PreparedCount,
                BreakageGLAduitState = item.BreakageGLAduitState,
                CreationMan = item.CreationMan,
                CreationTime = Convert.ToDateTime(item.CreationTime).ToString("yyyy-MM-dd")
            });

            var result = new
            {
                Breakage1 = newform,
                PageIndex = pageIndex,
                PageCount = pageCount,
                Count = count
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 盘点管理页面视图
        /// </summary>
        /// <returns></returns>
        public ActionResult QueryCheckPage(RequestDto dto)
        {
            //测试 
            ViewBag.Type = new SelectList("");
            return View();
        }
        public ActionResult GetIneventoryData(RequestDto dto) {
            Expression<Func<Ineventory, bool>> where = item => item.State == 1 && item.PreparedTime >= dto.Start && item.PreparedTime <= dto.End;
            if (!string.IsNullOrEmpty(dto.CheckId))
            {
                where = where.And(item => item.CheckId.IndexOf(dto.CheckId)!=-1);
            }
            if (dto.zt!=0)
            {
                where = where.And(item => item.CheckAuditState==dto.zt);
            }
            var pageIndex = dto.PageIndex;
            var pageCount = 0;
            var count = 0;
            var IneventoryService = new IneventoryService().GetByWhereAsc(where, item => item.PreparedTime, ref pageIndex, ref pageCount, ref count, pageSize);
            var newIneventory = IneventoryService.Select(item => new {
                Iid = item.Iid,
                CheckId = item.CheckId,
                CheckType = item.CheckType,
                PreparedTime = Convert.ToDateTime(item.PreparedTime).ToString("yyyy-MM-dd"),
                PreparedMan = item.PreparedMan,
                CheckAuditState =item.CheckAuditState ,
                CreationTime = Convert.ToDateTime(item.CreationTime).ToString("yyyy-MM-dd")
            });
            var result = new
            {
                IneventoryAction = newIneventory,
                PageIndex = pageIndex,
                PageCount = pageCount,
                Count = count
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        //删除
        [HttpPost]
        public ActionResult DeleteIneventory(int Iid ) {
            var IneventoryServices = new IneventoryService().GetByWhere(item=>item.Iid==Iid).SingleOrDefault();
            IneventoryServices.State = 0;
            new IneventoryService().Update(IneventoryServices);
            return Json(JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        //修改大拇指
        public ActionResult UpdateMZ(int Iid, int CheckAuditStates) {
            var IneventoryServices = new IneventoryService().GetByWhere(item => item.Iid == Iid).SingleOrDefault();
            IneventoryServices.CheckAuditState = CheckAuditStates;
            new IneventoryService().Update(IneventoryServices);
            return Json(JsonRequestBehavior.AllowGet);
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
        public ActionResult GetRefun(RequestDto dto, int zt)
        {
            Expression<Func<Refund, bool>> where = item => item.State == 1 && item.PreparedTime >= dto.Start && item.PreparedTime <= dto.End;
            if (!string.IsNullOrEmpty(dto.RefundId))
            {
                where = where.And(item => item.RefundId.IndexOf(dto.RefundId) != -1);
            }
            if (zt != 0)
            {
                where = where.And(item => item.RefundAuditState == zt);
            }
            var pageIndex = dto.PageIndex;
            var pageCount = 0;
            var count = 0;

            var RefundModel = new RefundService().GetByWhereAsc(where,item=>item.PreparedTime,ref pageIndex,ref pageCount,ref count, pageSize);
            var newRefund = RefundModel.Select(item => new {
                Rid = item.Rid,
                RefundId = item.RefundId,
                RefundCount=item.RefundCount,
                Standby1 = item.Standby1,//把这个字段当成退货类型字段
                RefundAuditState = item.RefundAuditState,
                PreparedMan = item.PreparedMan,
                PreparedTime = Convert.ToDateTime(item.PreparedTime).ToString("yyyy-MM-dd")
            });
            var RefundResult = new { RefundAction = newRefund, PageIndex = pageIndex, PageCount = pageCount, Count = count };
            return Json(RefundResult, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        //删除（改状态）
        public ActionResult DelRefund(int Rid) {
            var RefundService = new RefundService().GetByWhere(item => item.Rid == Rid).SingleOrDefault();
            RefundService.State = 0;
            new RefundService().Update(RefundService);
            return Json(JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        //大拇指该状态
        public ActionResult RefundUpSHZT(int Rid,int RefundAuditStates) {
            var RefundService = new RefundService().GetByWhere(item=>item.Rid==Rid).SingleOrDefault();
            RefundService.RefundAuditState = RefundAuditStates;
            new RefundService().Update(RefundService);
            return Json(JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 新增产品退货页面视图
        /// </summary>
        /// <returns></returns>
        public ActionResult QueryRefundThAdd(string kehuid)
        {
            //退货类型
            var RefundTHType = new StStorageDJTypeService().GetByWhere(item=>item.State==4);
            RefundTHType.Insert(0,new ChuBaoPanTuiTypes() {CBPTTid=0,DaBillTYpeName="请选择退货单类型" });
            ViewBag.RefundTHType = new SelectList(RefundTHType, "CBPTTid", "DaBillTYpeName");
            //客户
            var ClientModel = new ClientService().GetByWhere(item=>true);
            ClientModel.Insert(0,new Client() { Cid=0,ClientId="请选择客户编号"});
            ViewBag.ClientModel = new SelectList(ClientModel, "Cid", "ClientId");


            //var Refundlist = new RefundService().GetByWhere(item => item.Rid==Typeid).SingleOrDefault();
            //var result = new
            //{
            //    ClientName = Refundlist.ClientName,
            //    ClientSite = Refundlist.ClientSite,
            //    ClientContactWay = Refundlist.ClientContactWay,
            //    ContactMan = Refundlist.ContactMan
            //};

            //测试 
            ViewBag.Type = new SelectList("");
            //return Json(result,JsonRequestBehavior.AllowGet);
            return View();
        }

        //新增产品退货
        [HttpPost]
        public ActionResult THProduct(Refund refund) {
            var RefundTHService = new RefundService();
            refund.RefundAuditState = 1;
            refund.State = 1;
            refund.Standby1 = 1;
            var AddList = RefundTHService.Add(refund);
            var AddResult = new {
                AddRefundAcion = AddList,
                Msg = AddList ? "添加成功！" : "添加失败！"
            };
            return Json(AddResult,JsonRequestBehavior.AllowGet);
        }
        #endregion

    }
}