using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
 public   class DB_Session:NSPMsSql<NModel.DB_Session>
    {

     /// <summary>
     /// 删除超过几天
     /// </summary>
        /// <param name="days">超过几天</param>
     /// <returns>成功删除几条</returns>
     public long DelOutTime(int days=6) {

         string execSql = string.Format("Exec  {0}_DelOutTime  @Days={1}  ", ETblName,days);

         return base.DUI(execSql);
     }

     public bool Exists_CID_UserID(String Session_CID, long? Session_UserID)
     {


         string execSql = string.Format("Exec  {0}_Exists_CID_UserID  @Session_CID=N'{1}',@Session_UserID={2}  ", ETblName, Session_CID, Session_UserID);

         int num = 0;
         int.TryParse(base.GetObject(execSql) + "", out num);

         if (num > 0)
         {
             return true;

         }
         else
         {
             return false;

         }



     }


    }
}
