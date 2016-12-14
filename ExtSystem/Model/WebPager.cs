using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Webdiyer.WebControls.Mvc;

namespace NModel
{
    public class WebPager<N>
    {
        public IDictionary<string, IPagedList<NModel.DB_AD>> IDictPagedAd { get; set; }

        public IDictionary<string, IList<NModel.DB_AD>> IDictListAd = null;
        public IDictionary<string, IList<NModel.Admin_Menu>> IDictMenu { get; set; }

        public IDictionary<string, IList<NModel.DB_Link>> IDictLink = new Dictionary<string, IList<NModel.DB_Link>>();
        public IDictionary<string, IList<NModel.DB_Classify>> IDictClassify = new Dictionary<string, IList<NModel.DB_Classify>>();


        public IDictionary<string, NModel.DB_User> IDictUserModel { get; set; }


        public NModel.DB_WebConfig OutWebConfig { set; get; }
        public N OutObj { get; set; }
        public List<N> OutData { get; set; }
        public long? PagerTotal { get; set; }
        public string OutStr { get; set; }
        public string OutHtml { get; set; }

        public string OutJson { get; set; }


    }
}
