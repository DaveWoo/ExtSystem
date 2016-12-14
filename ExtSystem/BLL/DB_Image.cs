using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tool;

namespace BLL
{
   public class DB_Image : NSPMsSql<NModel.DB_Image>
    {

       public NModel.DB_Image GetModelByBelongsID(long? id) {
           string execSql = string.Format("Exec  {0}_ GetModelByBelongsID @ID={1} ", ETblName, id);

           List<NModel.DB_Image> Tmodel = base.GetList(execSql);
           if (NTool.IsLtNULL<NModel.DB_Image>(Tmodel))
           {

               return (NModel.DB_Image)Tmodel[0];

           }
           return null;
       
       }
       /// <summary>
       /// 删除用户所属ID
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
       public bool   DelByBelongsID(long? id){



           string execSql = string.Format("Exec  {0}_DelByBelongsID  @ID={1} ", ETblName, id);
           return base.DUI(execSql)>0?true:false;

           
       }
    }
}
