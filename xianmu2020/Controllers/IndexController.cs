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
            var ProductSort = ProductSortModel.Select(item => new
            {
                ProductSortId = item.ProductSortId,
                ProductSortName = item.ProductSortName,
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
        public ActionResult GetDeleteProductSort(string id)
        {
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

            var PService = new ProductGLService();
            var model2 = PService.GetAll();
            model2.Insert(0, new ProductGL() { PGLid = 0, ProductName = "请选择产品" });
            ViewBag.ProductGLType = new SelectList(model2, "PGLid", "ProductName");

            var SupplierService = new SupplierService();
            var model3 = SupplierService.GetAll();
            model3.Insert(0, new Supplier() { Sid = 0, SupplierName = "请选择供应商名称" });
            ViewBag.SupplierType = new SelectList(model3, "Sid", "SupplierName");

            ViewBag.Type = new SelectList("");
            return View();
        }

        //加载数据方法1
        public ActionResult GetStorage(RequestDto re, int zt)
        {
            Expression<Func<StStorage, bool>> where = item => item.State == 1;
            if (!string.IsNullOrEmpty(re.StoOrderId))
            {
                where = where.And(item => item.StoOrderId.IndexOf(re.StoOrderId) != -1);
            }
            if (re.StoType != 0)
            {
                where = where.And(item => item.StoType == re.StoType);
            }
            if (!string.IsNullOrEmpty(re.SuppliersType))
            {
                where = where.And(item => item.SuppliersType.IndexOf(re.SuppliersType) != -1);
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
            var newform = StorageList.Select(item => new
            {
                Stoid = item.Stoid,
                StoOrderId = item.StoOrderId,
                StoType = item.StoType,
                SuppliersType = item.SuppliersType,
                GoodsTotal = item.GoodsTotal,
                TotalMoney = item.TotalMoney,
                StStorageState = item.StStorageState,
                MakingSingle = item.MakingSingle,
                CreateTime = Convert.ToDateTime(item.CreateTime).ToString("yyyy-MM-dd")
            });
            var result = new
            {
                Storage = newform,
                PageIndex = pageIndex,
                PageCount = pageCount,
                Count = count
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
        public ActionResult StStorageUpSH(int Stoid, int StStorageStates)
        {
            var UpShStStorag = new StStorageService();
            var data = UpShStStorag.GetByWhere(item => item.Stoid == Stoid).SingleOrDefault();
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
            var model = new StStorageDJTypeService().GetByWhere(item => item.State == 1);
           model.Insert(0, new ChuBaoPanTuiTypes() { CBPTTid = 0, DaBillTYpeName = "请选择入库单类型" });
            ViewBag.StStorageDJType = new SelectList(model, "CBPTTid", "DaBillTYpeName");

            var PService = new ProductGLService();
            var model2 = PService.GetAll();
            model2.Insert(0, new ProductGL() { PGLid = 0, ProductName = "请选择产品" });
            ViewBag.ProductGLType = new SelectList(model2, "PGLid", "ProductName");

            var SupplierService = new SupplierService();
            var model3 = SupplierService.GetAll();
            model3.Insert(0, new Supplier() { Sid = 0, SupplierName = "请选择供应商名称" });
            ViewBag.SupplierType = new SelectList(model3, "Sid", "SupplierName");
            //测试 
            return View();
        }
        //查询供应商
        public ActionResult QueryGys(int id)
        {
            Expression<Func<Supplier, bool>> where = item => item.State == 1 && item.Sid == id;
            var SupplierService = new SupplierService();
            var Supplierlist = SupplierService.GetByWhere(where);

            var newform = Supplierlist.Select(item => new
            {
                Standby1 = item.Standby1,
                Standby2 = item.Standby2,
                Standby3 = item.Standby3,
                Standby4 = item.Standby4,
                Sid = item.Sid,
                SupplierId = item.SupplierId,
                SupplierType = item.SupplierType,
                SupplierName = item.SupplierName,
                Phone = item.Phone,
                Fax = item.Fax,
                Email = item.Email,
                Contacts = item.Contacts,
                Site = item.Site,
                Describe = item.Describe,
                State = item.State
            });

            var result = new
            {
                data = newform
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        //修改查询
        public ActionResult QueUpt(int id)
        {
            Expression<Func<StStorage, bool>> where = item => item.State == 1 && item.Stoid == id;

            var StorageList = new StStorageService().GetByWhere(where);
            var newform = StorageList.Select(item => new
            {
                Stoid = item.Stoid,
                StoOrderId = item.StoOrderId,
                StoType = item.StoType,
                SuppliersType = item.SuppliersType,
                GoodsTotal = item.GoodsTotal,
                TotalMoney = item.TotalMoney,
                StStorageState = item.StStorageState,
                MakingSingle = item.MakingSingle,
                Standby4 = item.Standby4,//产品
                CreateTime = Convert.ToDateTime(item.CreateTime).ToString("yyyy-MM-dd"),
                SuppliersId = item.SuppliersId,
                SuppliersName = item.SuppliersName,
                SuppliersPeople = item.SuppliersPeople,
                SuppliersContact = item.SuppliersContact,
                OrdersTime = item.OrdersTime,
                Remark = item.Remark
            });
            var result = new
            {
                Storage = newform
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        //入库修改
        [HttpPost]
        public ActionResult QueryRkUpdate(StStorage gl)
        {
            var ser = new StStorageService();
            gl.State = 1;
            gl.Standby1 = 1;
            gl.Standby2 = 1;
            gl.Standby3 = null;
            gl.StStorageState = 3;
            var saveResu = ser.Update(gl);
            var result = new
            {
                ActionResult = saveResu,
                Msg = saveResu ? "修改成功" : "修改失败"
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        //入库添加
        [HttpPost]
        public ActionResult QueryRkAdd(StStorage gl)
        {
            var ser = new StStorageService();
            gl.State = 1;
            gl.Standby1 = 1;
            gl.Standby2 = 1;
            gl.Standby3 = null;
            gl.StStorageState = 3;
            var saveResu = ser.Add(gl);
            var result = new
            {
                ActionResult = saveResu,
                Msg = saveResu ? "添加成功" : "添加失败"
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 出库管理页面视图
        /// </summary>
        /// <returns></returns>
        public ActionResult QueryDelivery()
        {
            var model = new StStorageDJTypeService().GetByWhere(item => item.State == 5);
            model.Insert(0, new ChuBaoPanTuiTypes() { CBPTTid = 0, DaBillTYpeName = "请选择出库单类型" });
            ViewBag.DeliveryType = new SelectList(model, "CBPTTid", "DaBillTYpeName");

            var PService = new ProductGLService();
            var model2 = PService.GetAll();
            model2.Insert(0, new ProductGL() { PGLid = 0, ProductName = "请选择产品" });
            ViewBag.ProductGLType = new SelectList(model2, "PGLid", "ProductName");

            var ClientService = new ClientService();
            var model3 = ClientService.GetAll();
            model3.Insert(0, new Client() { Cid = 0, ClientName = "请选择客户名称" });
            ViewBag.ClientType = new SelectList(model3, "Cid", "ClientName");
            //测试 
            ViewBag.Type = new SelectList("");
            return View();
        }
        //获取数据
        public ActionResult GetDeliveryData(RequestDto re)
        {
            Expression<Func<Delivery, bool>> where = item => item.State == 1 && item.CreationTime >= re.Start && item.CreationTime <= re.End;
            if (re.DeliveryTYpe != 0)
            {
                where = where.And(item => item.DeliveryTYpe == re.DeliveryTYpe);
            }
            if (!string.IsNullOrEmpty(re.DeliveryId))
            {
                where = where.And(item => item.DeliveryId.IndexOf(re.DeliveryId) != -1);
            }
            if (re.zt != 0)
            {
                where = where.And(item => item.DeliveryAuditState == re.zt);
            }
            var pageIndex = re.PageIndex;
            var pageCount = 0;
            var count = 0;
            var DeliveryList = new DeliveryService().GetByWhereAsc(where, item => item.CreationTime, ref pageIndex, ref pageCount, ref count, pageSize);
            var DeliveryNew = DeliveryList.Select(item => new
            {
                Did = item.Did,
                DeliveryId = item.DeliveryId,
                DeliveryTYpe = item.DeliveryTYpe,
                ClientNames = item.ClientNames,
                TotalCount = item.TotalCount,
                TotalMoney = item.TotalMoney,
                DeliveryAuditState = item.DeliveryAuditState,
                OperatingMode = item.OperatingMode,
                PreparedBy = item.PreparedBy,
                CreationTime = Convert.ToDateTime(item.CreationTime).ToString("yyyy-MM-dd")
            });
            var result = new { DeliveryAction = DeliveryNew, PageIndex = pageIndex, PageCount = pageCount, Count = count };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        //删除
        [HttpPost]
        public ActionResult DeleteDelivery(int Did)
        {
            var IneventoryServices = new DeliveryService().GetByWhere(item => item.Did == Did).SingleOrDefault();
            IneventoryServices.State = 0;
            new DeliveryService().Update(IneventoryServices);
            return Json(JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        //修改大拇指状态(审核)
        public ActionResult UpDeliveryAuditState(int Did, int DeliveryAuditStats)
        {
            var UpState = new DeliveryService();
            var data = UpState.GetByWhere(item => item.Did == Did).SingleOrDefault();
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
            model2.Insert(0, new ProductGL() { PGLid = 0, ProductName = "" });
            ViewBag.ProductGLType = new SelectList(model2, "PGLid", "ProductName");

            var ClientService = new ClientService();
            var model3 = ClientService.GetAll();
            model3.Insert(0, new Client() { Cid = 0, ClientName = "" });
            ViewBag.ClientType = new SelectList(model3, "Cid", "ClientName");
            return View();
        }

        //修改查询
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

        //添加
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
        //待审核
        public ActionResult GetBreakageData2(RequestDto re)
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
        //审核未通过
        public ActionResult GetBreakageData3(RequestDto re)
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
            //下拉框产品类别
            var ProductLB = new ProductSortService();
            var ProductLBList = ProductLB.GetByWhere(item => item.State == 1);
            ViewBag.Lbs = new SelectList(ProductLBList, "PSid", "ProductSortName");
            //下拉框产品
            var ProductGuanLi = new ProductGLService().GetByWhere(item => item.State == 1);
            ViewBag.Cps = new SelectList(ProductGuanLi, "PGLid", "ProductName");       
            //测试 
            ViewBag.Type = new SelectList("");
            return View();
        }
        public ActionResult GetIneventoryData(RequestDto dto)
        {
            Expression<Func<Ineventory, bool>> where = item => item.State == 1 && item.PreparedTime >= dto.Start && item.PreparedTime <= dto.End;
            if (!string.IsNullOrEmpty(dto.CheckId))
            {
                where = where.And(item => item.CheckId.IndexOf(dto.CheckId) != -1);
            }
            if (dto.zt != 0)
            {
                where = where.And(item => item.CheckAuditState == dto.zt);
            }
            var pageIndex = dto.PageIndex;
            var pageCount = 0;
            var count = 0;
            var IneventoryService = new IneventoryService().GetByWhereAsc(where, item => item.PreparedTime, ref pageIndex, ref pageCount, ref count, pageSize);
            var newIneventory = IneventoryService.Select(item => new
            {
                Iid = item.Iid,
                CheckId = item.CheckId,
                CheckType = item.CheckType,
                Standby3 = item.Standby3,
                Standby4 = item.Standby4,
                PreparedTime = Convert.ToDateTime(item.PreparedTime).ToString("yyyy-MM-dd"),
                PreparedMan = item.PreparedMan,
                CheckAuditState = item.CheckAuditState,
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
        public ActionResult DeleteIneventory(int Iid)
        {
            var IneventoryServices = new IneventoryService().GetByWhere(item => item.Iid == Iid).SingleOrDefault();
            IneventoryServices.State = 0;
            new IneventoryService().Update(IneventoryServices);
            return Json(JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        //修改大拇指
        public ActionResult UpdateMZ(int Iid, int CheckAuditStates)
        {
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
            //下拉框产品类别
            var ProductLB = new ProductSortService();
            var ProductLBList = ProductLB.GetByWhere(item => item.State == 1);
            ProductLBList.Insert(0, new ProductSort() { PSid = 0, ProductSortName = "请选择产品类别" });
            ViewBag.Lbs = new SelectList(ProductLBList, "PSid", "ProductSortName");
            //下拉框产品
            var ProductGuanLi = new ProductGLService().GetByWhere(item => item.State == 1);
            ProductGuanLi.Insert(0,new ProductGL() { PGLid=0,ProductName="请选择盘点产品" });
            ViewBag.Cps = new SelectList(ProductGuanLi, "PGLid", "ProductName");

            //测试 
            ViewBag.Type = new SelectList("");
            return View();
        }

        [HttpPost]
        //添加盘点单
        public ActionResult GetAddIneventory(Ineventory Ineven) {
            Ineven.CheckAuditState = 1;
            Ineven.CreationTime = DateTime.Now;
            Ineven.State = 1;
            Ineven.CheckType = 3;
            var AddIneventory = new IneventoryService().Add(Ineven);
            var AddResult = new {
                AddIneventoryAction = AddIneventory,
                Msg= AddIneventory ? "添加成功！": "添加失败！"
            };
            return Json(AddResult,JsonRequestBehavior.AllowGet);
        }
        //修改先查看
        public ActionResult QueryUpdateIneventory(int Iid) {
            var QueryUpIneventory = new IneventoryService().GetByWhere(item=>item.Iid==Iid);
            var UpIneventoryList = QueryUpIneventory.Select(item=> new {
                Iid=item.Iid,
                CheckId = item.CheckId,
                PreparedTime = Convert.ToDateTime(item.PreparedTime).ToString("yyyy-MM-dd"),
                man=item.PreparedMan,
                Standby3 = item.Standby3,
                Standby4 = item.Standby4,
                Remark = item.Remark
            });
            var UpResult = new {
                UpIneventory= UpIneventoryList
            };
            return Json(UpResult,JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        //修改
        public ActionResult UpdateIneventory(Ineventory ineventory) {
            var UpIneventory = new IneventoryService();
            var UpModel = UpIneventory.GetByWhere(item => item.Iid == ineventory.Iid).SingleOrDefault();
            ineventory.CheckType = UpModel.CheckType;
            ineventory.CreationTime = DateTime.Now;
            ineventory.PreparedMan = UpModel.PreparedMan;
            ineventory.CheckAuditState = UpModel.CheckAuditState;
            ineventory.State = UpModel.State;
            ineventory.Standby1 = 0;
            ineventory.Standby2 = 0;
            var UpdatePd = UpIneventory.Update(ineventory);
            var UpResult = new {
                IneventoryAction = UpdatePd,
                Msg = UpdatePd ? "修改成功！":"修改失败！"
            };
            return Json(UpResult,JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 退货管理页面视图
        /// </summary>
        /// <returns></returns>
        public ActionResult QueryRefundPage()
        {
            var StStorageDJTypeService = new StStorageDJTypeService();
            var model = StStorageDJTypeService.GetByWhere(item => item.State == 2);
            model.Insert(0, new ChuBaoPanTuiTypes() { CBPTTid = 0, DaBillTYpeName = "请选择退货类型" });
            ViewBag.RefundTHType = new SelectList(model, "CBPTTid", "DaBillTYpeName");

            var PService = new ProductGLService();
            var model2 = PService.GetAll();
            model2.Insert(0, new ProductGL() { PGLid = 0, ProductName = "请选择产品" });
            ViewBag.ProductGLType = new SelectList(model2, "PGLid", "ProductName");

            var ClientService = new ClientService();
            var model3 = ClientService.GetAll();
            model3.Insert(0, new Client() { Cid = 0, ClientName = "请选择客户名称" });
            ViewBag.ClientType = new SelectList(model3, "Cid", "ClientName");
            //测试 
            ViewBag.Type = new SelectList("");
            return View();
        }
        //退货修改
        [HttpPost]
        public ActionResult UpdateRefund(Refund gl)
        {
            var ser = new RefundService();
            var data = ser.GetByWhere(item => item.Rid == gl.Rid).SingleOrDefault();
            data.Rid = gl.Rid;
            data.RefundId = gl.RefundId;
            data.RefundCount = gl.RefundCount;
            data.ClientId = gl.ClientId;
            data.ClientName = gl.ClientName;
            data.ClientSite = gl.ClientSite;
            data.ClientContactWay = gl.ClientContactWay;
            data.ContactMan = gl.ContactMan;
            data.PreparedMan = gl.PreparedMan;
            data.Remark = gl.Remark;
            data.PreparedTime = gl.PreparedTime;
            data.Standby1 = gl.Standby1;
            data.Standby4 = gl.Standby4;
            data.Standby2 = 0;
            data.Standby3 = "空";
            data.State = 1;
            gl.RefundAuditState = data.RefundAuditState;
            var saveResu = ser.Update(data);
            var result = new
            {
                ActionResult = saveResu,
                Msg = saveResu ? "修改成功" : "修改失败"
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        //修改查询
        public ActionResult RefundData(int id)
        {

            var ser = new RefundService();
            var model = ser.GetByWhere(item => item.Rid == id);

            var newform = model.Select(item => new
            {
                Rid = item.Rid,
                RefundId = item.RefundId,
                RefundCount = item.RefundCount,
                ClientId = item.ClientId,
                ClientName = item.ClientName,
                ClientSite = item.ClientSite,
                ClientContactWay = item.ClientContactWay,
                ContactMan = item.ContactMan,
                PreparedMan = item.PreparedMan,
                RefundAuditState = item.RefundAuditState,
                Remark = item.Remark,
                PreparedTime = Convert.ToDateTime(item.PreparedTime).ToString("yyyy-MM-dd"),
                State = item.State,
                Standby1 = item.Standby1,
                Standby2 = item.Standby2,
                Standby3 = item.Standby3,
                Standby4 = item.Standby4
            });

            var result = new
            {
                data = newform
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        //查询客户
        public ActionResult QueryKH(int id)
        {
            Expression<Func<Client, bool>> where = item => item.State == 1 && item.Cid == id;
            var SupplierService = new ClientService();
            var Supplierlist = SupplierService.GetByWhere(where);

            var newform = Supplierlist.Select(item => new
            {
                Cid = item.Cid,
                ClientId = item.ClientId,
                ClientName = item.ClientName,
                Phone = item.Phone,
                Fax = item.Fax,
                Email = item.Email,
                CreationTime = item.CreationTime,
                State = item.State,
                Standby1 = item.Standby1,
                Standby2 = item.Standby2,
                Standby3 = item.Standby3,
                Standby4 = item.Standby4

            });

            var result = new
            {
                data = newform
            };
            return Json(result, JsonRequestBehavior.AllowGet);
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

            var RefundModel = new RefundService().GetByWhereAsc(where, item => item.PreparedTime, ref pageIndex, ref pageCount, ref count, pageSize);
            var newRefund = RefundModel.Select(item => new
            {
                Rid = item.Rid,
                RefundId = item.RefundId,
                RefundCount = item.RefundCount,
                Standby1 = item.Standby1,//把这个字段当成退货类型字段
                Standby4 = item.Standby4,//把这个字段当成退货产品
                RefundAuditState = item.RefundAuditState,
                PreparedMan = item.PreparedMan,
                PreparedTime = Convert.ToDateTime(item.PreparedTime).ToString("yyyy-MM-dd")
            });
            var RefundResult = new { RefundAction = newRefund, PageIndex = pageIndex, PageCount = pageCount, Count = count };
            return Json(RefundResult, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        //删除（改状态）
        public ActionResult DelRefund(int Rid)
        {
            var RefundService = new RefundService().GetByWhere(item => item.Rid == Rid).SingleOrDefault();
            RefundService.State = 0;
            new RefundService().Update(RefundService);
            return Json(JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        //大拇指该状态
        public ActionResult RefundUpSHZT(int Rid, int RefundAuditStates)
        {
            var RefundService = new RefundService().GetByWhere(item => item.Rid == Rid).SingleOrDefault();
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
            var RefundTHType = new StStorageDJTypeService().GetByWhere(item => item.State == 2);
            RefundTHType.Insert(0, new ChuBaoPanTuiTypes() { CBPTTid = 0, DaBillTYpeName = "请选择退货单类型" });
            ViewBag.RefundTHType = new SelectList(RefundTHType, "CBPTTid", "DaBillTYpeName");

            var PService = new ProductGLService();
            var model2 = PService.GetAll();
            model2.Insert(0, new ProductGL() { PGLid = 0, ProductName = "请选择产品" });
            ViewBag.ProductGLType = new SelectList(model2, "PGLid", "ProductName");

            //客户
            var ClientService = new ClientService();
            var model3 = ClientService.GetAll();
            model3.Insert(0, new Client() { Cid = 0, ClientName = "请选择客户名称" });
            ViewBag.ClientType = new SelectList(model3, "Cid", "ClientName");
            return View();
        }

        //新增产品退货
        [HttpPost]
        public ActionResult THProduct(Refund refund)
        {
            var RefundTHService = new RefundService();
            refund.RefundAuditState = 1;
            refund.State = 1;
            refund.Standby3 = "";
            var AddList = RefundTHService.Add(refund);
            var AddResult = new
            {
                AddRefundAcion = AddList,
                Msg = AddList ? "添加成功！" : "添加失败！"
            };
            return Json(AddResult, JsonRequestBehavior.AllowGet);
        }
        #endregion
       
    }
}