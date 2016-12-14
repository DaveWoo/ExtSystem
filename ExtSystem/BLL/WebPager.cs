using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace BLL
{
    public class WebPager : Controller
    {
       public NModel.WebPager<NModel.DB_Area> LoadData(string ipAddress, string pagename)
       {
           BLL.DB_WebConfig bllwebcfg = new BLL.DB_WebConfig();

           BLL.DB_Area bllArea = new BLL.DB_Area();
           NModel.DB_WebConfig modelwebcfg = bllwebcfg.GetCodeModel(pagename);
           NModel.WebPager<NModel.DB_Area> model_pager = new NModel.WebPager<NModel.DB_Area>();
          // model_pager.OutStr = Tool.NWeb.GetAddresByIp(ipAddress);
           string gocityName = Tool.NTool.GetCookieValue("gocityName");
           if (!string.IsNullOrEmpty(gocityName))
           {

               model_pager.OutStr = this.Server.UrlDecode(gocityName);
           }
           if (string.IsNullOrEmpty(model_pager.OutStr))
           {

               model_pager.OutStr = "广州";
           }
          // model_pager.OutObj = bllArea.GetListByName(model_pager.OutStr + "%");

         

           model_pager.OutWebConfig = modelwebcfg != null ? modelwebcfg : new NModel.DB_WebConfig();
           BLL.DB_Link bll_link = new BLL.DB_Link();
           IList<NModel.DB_Link> List_link = bll_link.GetListByNumAndTop("1", 20);
           model_pager.IDictLink.Add("招聘友情链接", List_link);

           bll_link.Close();
           bllArea.Close();
           bllwebcfg.Close();
           return model_pager;

       }

    }
}
