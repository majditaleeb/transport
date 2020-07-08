using AutoMapper;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Transport.Models;

namespace Transport.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {
        private TransportDBEntities newTranspore;
        public HomeController()
        {
            newTranspore = new TransportDBEntities();
        }

        //...........list all..............
        public ActionResult Index()
        {
            if (Session["userId"] != null)
            {
                try
                {
                    var getList = newTranspore.Informaions.OrderByDescending(a=>a.dateTravel).ToList();
                    if (getList.Any())
                    {
                        var list = Mapper.Map<List<HomeIndexMV>>(getList);
                        return View(list);
                    }
                    else 
                    {
                        return View();
                    }
                }
                catch
                {
                    return RedirectToAction("Error", "Home");
                }
            }
            else
            {
                try
                {
                    return RedirectToRoute(new { Controller = "LoginLogout", Action = "Login" });
                }
                catch
                {
                    return RedirectToAction("Error", "Home");
                }
            }
        }
        //.......search in list...............
        [HttpPost]
        public ActionResult SearchTraveller(string SearchText)
        {
            if (Session["userId"] != null)
            {
                try
                {
                        var getList = newTranspore.Informaions.Where(x => x.Traveller.travellerIdentifiy.Contains(SearchText)).OrderByDescending(a => a.dateTravel).ToList();
                        var list = Mapper.Map<List<HomeIndexMV>>(getList);
                        return PartialView("SearchListPartial", list);
                }
                catch
                {
                    return RedirectToAction("Error", "Home");
                }
            }
            else
            {
                try
                {
                    return RedirectToRoute(new { Controller = "LoginLogout", Action = "Login" });
                }
                catch
                {
                    return RedirectToAction("Error", "Home");
                }
            }
        }
        //............Creat new Travl ...................
        public ActionResult CreatNewTravel()
        {
            if (Session["userId"] != null)
            {
                try
                {
                    ViewBag.DDL = GetDropDawn();
                    return View();
                }
                catch
                {
                    return RedirectToAction("Error", "Home");
                }
            }
            else
            {
                try
                {
                    return RedirectToRoute(new { Controller = "LoginLogout", Action = "Login" });
                }
                catch
                {
                    return RedirectToAction("Error", "Home");
                }
            }
        }
        // test it please  beso
        public ActionResult CreateNew() {
            return View();
        }
        [HttpPost]
        public ActionResult CreatNewTravel(HomeIndexMV newRecord)
        {
            if (Session["userId"] != null)
            {
                try
                {
                    if (!ModelState.IsValid)
                    {
                        ModelState.AddModelError("", "هنالك قيم خاطئة تم إدخالها");
                        ViewBag.DDL = GetDropDawn();
                        return View(newRecord);
                    }
                    else 
                    {
                        if (newRecord != null)
                        {

                            //Car newCar = new Car()
                            //{
                            //    carName = newRecord.carName
                            //};
                            //newTranspore.Cars.Add(newCar);
                            Car newCar = new Car();
                            if (newRecord.carName != null)
                            {
                                var convertToInt1 = int.Parse(newRecord.carName);
                                newRecord.Car = newTranspore.Cars.Find(convertToInt1);
                            }
                            else if (newRecord.anotherCarName != null)
                            {
                                newCar.carName = newRecord.anotherCarName;
                                newTranspore.Cars.Add(newCar);
                            }
                            else
                            {
                                ModelState.AddModelError("", "يرجى إختيار رقم المركبة أو أخرى رقم المركبة");
                                ViewBag.DDL = GetDropDawn();
                                return View(newRecord);
                            }

                            Traveller newTravName = new Traveller();
                            if (newRecord.travellerName != null && newRecord.travellerIdentifiy != null)
                            {
                                var convertToInt2 = int.Parse(newRecord.travellerName);
                                newRecord.Traveller = newTranspore.Travellers.Find(convertToInt2);
                            }
                            else if (newRecord.anotherTravellerName != null && newRecord.anotherTravellerIdentifiy != null)
                            {
                                newTravName.travellerName = newRecord.anotherTravellerName;
                                newTravName.travellerIdentifiy = newRecord.anotherTravellerIdentifiy;
                                newTranspore.Travellers.Add(newTravName);
                            }
                            else
                            {
                                ModelState.AddModelError("", " يرجى إختيار (الاسم و رقم الهوية) أو (أخرى الاسم و أخرى رقم الهوية) ");
                                ViewBag.DDL = GetDropDawn();
                                return View(newRecord);
                            }

                            From newFrom = new From();
                            if (newRecord.fromName != null)
                            {
                                var convertToInt1 = int.Parse(newRecord.fromName);
                                newRecord.From = newTranspore.Froms.Find(convertToInt1);
                            }
                            else if (newRecord.anotherFromName != null)
                            {
                                newFrom.fromName = newRecord.anotherFromName;                             
                                newTranspore.Froms.Add(newFrom);
                            }
                            else
                            {
                                ModelState.AddModelError("", "يرجى إختيار من أو أخرى من");
                                ViewBag.DDL = GetDropDawn();
                                return View(newRecord);
                            }

                            To newTo = new To();
                            if (newRecord.toName != null)
                            {
                                var convertToInt2 = int.Parse(newRecord.toName);
                                newRecord.To = newTranspore.Toes.Find(convertToInt2);
                            }
                            else if (newRecord.anotherToName != null)
                            {
                                newTo.toName = newRecord.anotherToName;
                                newTranspore.Toes.Add(newTo);
                            }
                            else
                            {
                                ModelState.AddModelError("", "يرجى إختيار إلى أو أخرى إلى");
                                ViewBag.DDL = GetDropDawn();
                                return View(newRecord);
                            }
                            

                            //Traveller newTrav = new Traveller()
                            //{
                            //    travellerName=newRecord.travellerName,
                            //    travellerIdentifiy=newRecord.travellerIdentifiy
                            //};
                            //newTranspore.Travellers.Add(newTrav);
                            
                            newTranspore.SaveChanges();

                            int fromId = 0;
                            if (newRecord.fromName != null)
                            {
                                fromId = newRecord.From.fromId;
                            }
                            else if (newRecord.anotherFromName != null)
                            {
                                fromId = newFrom.fromId;
                            }

                            var toId = 0;
                            if (newRecord.toName != null)
                            {
                                toId = newRecord.To.toId;
                            }
                            else if (newRecord.anotherToName != null)
                            {
                                toId = newTo.toId;
                            }

                            var travId = 0;
                            if (newRecord.travellerName != null)
                            {
                                travId = newRecord.Traveller.travellerId;
                            }
                            else if (newRecord.anotherTravellerName != null)
                            {
                                travId = newTravName.travellerId;
                            }

                            var carId = 0;
                            if (newRecord.carName != null)
                            {
                                carId = newRecord.Car.carId;
                            }
                            else if (newRecord.anotherCarName != null)
                            {
                                carId = newCar.carId;
                            }



                            //var carId = newCar.carId;
                            //var TraveId = newTrav.travellerId;
                            var userId = int.Parse(Session["userId"].ToString());
                            Informaion newInfo = new Informaion()
                            {
                                CashOrReserve=newRecord.CashOrReserve,
                                dateTravel=DateTime.Now,
                                carId= carId,
                                fromId= fromId,
                                toId= toId,
                                travellerId= travId,
                                userid= userId
                            };
                            if (newRecord.CashOrReserve == true)
                            {
                                newInfo.CashValue = newRecord.CashValue;
                            }
                            else
                            {
                                newInfo.CashValue = -(newRecord.CashValue);
                            }
                            newTranspore.Informaions.Add(newInfo);
                            newTranspore.SaveChanges();
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            ModelState.AddModelError("", "هنالك قيم خاطئة تم إدخالهاز");
                            ViewBag.DDL = GetDropDawn();
                            return View(newRecord);
                        }
                    }
                }
                catch
                {
                    return RedirectToAction("Error", "Home");
                }
            }
            else
            {
                try
                {
                    return RedirectToRoute(new { Controller = "LoginLogout", Action = "Login" });
                }
                catch
                {
                    return RedirectToAction("Error", "Home");
                }
            }
        }

        //.............id maybe changed to fname+lname
        //.....................history....................
        public ActionResult History(string id) {
            if (Session["userId"] != null)
            {
                try
                {
                    if (id !=null)
                    {
                        var getList = newTranspore.Informaions.Where(a=>a.Traveller.travellerIdentifiy.Contains(id)).OrderByDescending(a=>a.dateTravel).ToList();
                        if (getList.Any())
                        {
                            var list = Mapper.Map<List<HomeIndexMV>>(getList);
                            return View(list);
                        }
                        else
                        {
                            return View();
                        }
                    }
                    else
                    {
                        return RedirectToAction("Error", "Home");
                    }
               
                }
                catch
                {
                    return RedirectToAction("Error", "Home");
                }
            }
            else
            {
                try
                {
                    return RedirectToRoute(new { Controller = "LoginLogout", Action = "Login" });
                }
                catch
                {
                    return RedirectToAction("Error", "Home");
                }
            }
        }
        //.............pay reservation...............
        public ActionResult Pay(int? idGet)
        {
            if (Session["userId"] != null)
            {
              try
                {
                    bool result = false;
                        if (idGet != null && idGet > 0)
                        {
                            var findRecord = newTranspore.Informaions.Find(idGet);
                            findRecord.CashOrReserve = true;
                            findRecord.CashValue = -(findRecord.CashValue);
                            newTranspore.Entry(findRecord).State = EntityState.Modified;
                            newTranspore.SaveChanges();
                            result = true;
                        return Json(result, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(result, JsonRequestBehavior.AllowGet);
                        }        
                }
                catch
                {
                    return RedirectToAction("Error", "Home");
                }
            }
            else
            {
                try
                {
                    return RedirectToRoute(new { Controller = "LoginLogout", Action = "Login" });
                }
                catch
                {
                    return RedirectToAction("Error", "Home");
                }
            }
        }

        //...........................Reportes..........................
        //...........................Excel files.......................

        public ActionResult ExportToExcelCash(ReportsFromToMV FTDate)
        {
                if (Session["userId"] != null)
                { 
                    try
                    {
                        int check = 0;
                        string Filename = "Report" + string.Format("{0:yyyy-MM-dd_HH-mm-ss-tt}", DateTimeOffset.Now) + ".xls";
                        string FolderPath = System.Web.HttpContext.Current.Server.MapPath("~/CashReportExcel");
                        string FilePath = System.IO.Path.Combine(FolderPath, Filename);
                        //Checking: If file name exists in server then remove from server.
                        if (System.IO.File.Exists(FilePath))
                        {
                            System.IO.File.Delete(FilePath);
                        }

                        var list = new List<Informaion>();

                        if ((FTDate.From.Day == 1 && FTDate.From.Month == 1 && FTDate.From.Year == 0001) ||
                              (FTDate.To.Day == 1 && FTDate.To.Month == 1 && FTDate.To.Year == 0001))
                        {
                            //list = door.TravellerInformations.Where(a => a.dateOfTravel.Value.Day == DateTime.Now.Day).ToList();
                            check = 1;
                            return Json(check, JsonRequestBehavior.AllowGet);
                        }
                        else if (FTDate.From.Date > FTDate.To.Date)
                        {
                            check = 2;
                            return Json(check, JsonRequestBehavior.AllowGet);
                        }
                        else if (FTDate.To.Date > DateTime.Now.Date)
                        {
                            check = 3;
                            return Json(check, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {

                            list = newTranspore.Informaions.Where(a => a.dateTravel.Value.Day >= FTDate.From.Day && a.dateTravel.Value.Month >= FTDate.From.Month && a.dateTravel.Value.Year >= FTDate.From.Year && a.dateTravel.Value.Day <= FTDate.To.Day && a.dateTravel.Value.Month <= FTDate.To.Month && a.dateTravel.Value.Year <= FTDate.To.Year).ToList();
                        }

                        ExcelPackage pck = new ExcelPackage();

                        ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Sheet1");

                        ws.Cells["A1"].Value = "التاريخ";
                        ws.Cells["B1"].Value = string.Format("{0:yyyy-MM-dd_HH-mm-ss-tt}", DateTimeOffset.Now);

                        ws.Cells["A2"].Value = "النوع";
                        ws.Cells["B2"].Value = "تقرير شامل";

                        ws.Cells["A3"].Value = "رقم الهوية";
                        ws.Cells["B3"].Value = "الاسم";
                        ws.Cells["C3"].Value = "من";
                        ws.Cells["D3"].Value = "الى";

                        ws.Cells["E3"].Value = "رقم المركبة";
                        ws.Cells["F3"].Value = "كاش";
                        ws.Cells["G3"].Value = "قيمة الكاش";
                        ws.Cells["H3"].Value = "التاريخ";
              
                        ws.Cells["j3"].Value = "موظف تسجيل النقل";



                        int rowStart = 4;
                        foreach (var item in list)
                        {
                            ws.Cells[string.Format("A{0}", rowStart)].Value = item.Traveller.travellerIdentifiy;
                            ws.Cells[string.Format("B{0}", rowStart)].Value = item.Traveller.travellerName;
                            ws.Cells[string.Format("C{0}", rowStart)].Value = item.From.fromName;
                            ws.Cells[string.Format("D{0}", rowStart)].Value = item.To.toName;
                            ws.Cells[string.Format("E{0}", rowStart)].Value = item.Car.carName;
                            ws.Cells[string.Format("F{0}", rowStart)].Value = item.CashOrReserve;
                            ws.Cells[string.Format("G{0}", rowStart)].Value = item.CashValue;
                            ws.Cells[string.Format("H{0}", rowStart)].Value = item.dateTravel.Value.ToShortDateString();
                            ws.Cells[string.Format("J{0}", rowStart)].Value = item.User.userFname +" "+ item.User.userLname;

                            rowStart++;
                        }
                        ws.Cells["A:AZ"].AutoFitColumns();


                        byte[] ExcelBytes = pck.GetAsByteArray();
                        using (Stream file = System.IO.File.OpenWrite(FilePath)) //FileMode.Create,FileAccess.Write
                        {
                            file.Write(ExcelBytes, 0, ExcelBytes.Length);
                        }
                        check = 4;
                        return Json(check, JsonRequestBehavior.AllowGet);

                    }
                    catch
                    {
                        return RedirectToAction("Error", "Home");
                    }
                }
                else
                {
                    try
                    {
                        return RedirectToRoute(new { Controller = "LoginLogout", Action = "Login" });
                    }
                    catch
                    {
                        return RedirectToAction("Error", "Home");
                    }
                }
        }

        //.............drop dawn lists....................
        //...............................................
        private DropDawnCollection GetDropDawn() {
            DropDawnCollection newDrop = new DropDawnCollection()
            {
                fromNameDDl = new SelectList(newTranspore.Froms.ToList(), "fromId", "fromName"),
                toNameDDl = new SelectList(newTranspore.Toes.ToList(), "toId", "toName"),
                carNameDDl= new SelectList(newTranspore.Cars.ToList(), "carId", "carName"),
                travellerNameDDl= new SelectList(newTranspore.Travellers.ToList(), "travellerId", "travellerName"),
                travellerIdentifyDDl= new SelectList(newTranspore.Travellers.ToList(), "travellerId", "travellerIdentifiy")
            };
            return newDrop;
        }

        //..........get identify............................
        //..................................................
        //public ActionResult GetIdentify(int? getIdentif) {
        //    if (Session["userId"] != null)
        //    {
        //        try
        //        {
        //            return View();
        //        }
        //        catch
        //        {
        //            return RedirectToAction("Error", "Home");
        //        }
        //    }
        //    else
        //    {
        //        try
        //        {
        //            return RedirectToRoute(new { Controller = "LoginLogout", Action = "Login" });
        //        }
        //        catch
        //        {
        //            return RedirectToAction("Error", "Home");
        //        }
        //    }
        //}

        public ActionResult Error()
        {
            if (Session["userId"] != null)
            {
                try
                {
                    return View();
                }
                catch
                {
                    return RedirectToAction("Error", "Home");
                }
            }
            else
            {
                try
                {
                    return RedirectToRoute(new { Controller = "LoginLogout", Action = "Login" });
                }
                catch
                {
                    return RedirectToAction("Error", "Home");
                }
            }
        }

        public ActionResult ErrorNotAllowed()
        {
            if (Session["userId"] != null)
            {
                try
                {
                    return View();
                }
                catch
                {
                    return RedirectToAction("Error", "Home");
                }
            }
            else
            {
                try
                {
                    return RedirectToRoute(new { Controller = "LoginLogout", Action = "Login" });
                }
                catch
                {
                    return RedirectToAction("Error", "Home");
                }
            }
        }


        public ActionResult Service()
        {

            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Home()
        {



            return View();
        }

        public ActionResult gallary()
        {

            return View();
        }
        public ActionResult Contact()
        {

            return View();
        }


    }
}