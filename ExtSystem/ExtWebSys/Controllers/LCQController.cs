using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExtWebSys.Controllers
{
    public class LCQController : Controller
    {
       

        public ActionResult Index()
        {
            return View();
        }


    
        public ActionResult Login()
        {

          
            byte bt = BLL.Fun.Auto_Login_Admin();
            switch (bt)
            {

                case 1:

                    return RedirectToAction("main", "LCQ");




                case 2:


                    return RedirectToAction("main", "LCQ");


                default:


                    break;

            }

            return View();
        }
        public ActionResult Main()
        {
            byte bt = BLL.Fun.Auto_Login_Admin();
            switch (bt)
            {

                case 1:


                    return View();



                case 2:

                    return View();
                 


                default:
                    return RedirectToAction("login", "LCQ");

            

            }
           
        }
    }
}
