using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace BLL
{
    public class NewsPager : WebPager
    {
        public NModel.NewsPager LoadConfig(string pagename, string linknum = "1", string linkdictName = "友情链接")
        {

            BLL.DB_WebConfig bllwebcfg = new BLL.DB_WebConfig();

         
            NModel.DB_WebConfig modelwebcfg = bllwebcfg.GetCodeModel(pagename);
            NModel.NewsPager model_pager = new NModel.NewsPager();
         

          
            bllwebcfg.Close();

            model_pager.OutWebConfig = modelwebcfg != null ? modelwebcfg : new NModel.DB_WebConfig();
            BLL.DB_Link bll_link = new BLL.DB_Link();

            IList<NModel.DB_Link> List_link = bll_link.GetListByNumAndTop(linknum, 20);
            model_pager.IDictLink.Add(linkdictName, List_link);

            bll_link.Close();
            return model_pager;
        }

    }
}
