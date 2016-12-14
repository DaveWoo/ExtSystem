using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tool;
namespace ExtWebSys.Controllers
{
    public class DrawController : Controller
    {
        //
        // GET: /Draw/
        public ActionResult DrawTextImage()
        {
            string w = this.Request.QueryString["w"];
            string h=this.Request.QueryString["h"];
            string text=this.Request.QueryString["text"];
            string color=this.Request.QueryString["color"];
            string fontsize=this.Request.QueryString["fontsize"];
            string bgColor=this.Request.QueryString["bgcolor"];
            Color _clr = System.Drawing.ColorTranslator.FromHtml(color);
            Color _bgclr = System.Drawing.ColorTranslator.FromHtml(bgColor);

        this.Response.ContentType = "image/png";
        try {
            Bitmap bp = NImage.DrawText(text, _clr, new NModel.EnImage(int.Parse(w), int.Parse(h),_bgclr), new Font("方正", int.Parse(fontsize)));

            bp.Save(this.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Png);
          
        
        }catch(Exception ex){}
        return View();
        
        }

        public ActionResult Index()
        {
            return View();
        }

    }
}
