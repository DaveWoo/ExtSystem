using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NModel
{
  public  class DB_Classify:SeftModel
  {
      public long? Classify_ID { get; set; }
      public string Classify_Num { get; set; }
      public string Classify_Name { get; set; }
      public string Classify_Name_En { get; set; }

      public long? Classify_X { get; set; }
      public long? Classify_Y { get; set; }
      public long? Classify_Z { get; set; }

      public DateTime? Classify_AddTime { get; set; }
      public DateTime? Classify_EditTime { get; set; }

      public long? Classify_SortNo { get; set; }

      public int? Classify_Status { get; set; }

      public int? Classify_Operate { get; set; }

      public int? Classify_IsHot { get; set; }

      public string Classify_Url { get; set; }


      public string Classify_Code { get; set; }

    }
}
