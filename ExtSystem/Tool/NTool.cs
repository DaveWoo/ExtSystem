using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;

namespace Tool
{
  public  class NTool
  {  /// 要转换的数组
      /// 转换后的数组类型
      public static Array ConvertArray(Array srcArray, System.Type elmType)
      {
          if (srcArray == null)
              throw new ArgumentNullException("srcArray");
          int len = srcArray.Length;
          Array a = Array.CreateInstance(elmType, len);
          for (int i = 0; i < len; i++)
              a.SetValue(srcArray.GetValue(i), i);
          return a;
      }

    
        /// <summary>
        /// 两个数组是否相等
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns>真假</returns>
        public static bool isLenEquals(object[] key, object[] value)
        {
            if (key != null && value != null && key.Length == value.Length)
            {
                return true;

            }
            return false;

        }
        /// <summary>
        /// 长度大于0返回真
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>真假</returns>
        public static bool hasData(object[] obj)
        {
            if (obj != null && obj.Length > 0)
            {

                return true;

            }

            return false;

        }
        /// <summary>
        /// 长度大于0并 大于needCount给的数
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="needCount"></param>
        /// <returns>真假</returns>
        public static bool hasData(object[] obj, int needCount)
        {
            if (obj != null && obj.Length > 0 && obj.Length > needCount)
            {

                return true;

            }

            return false;

        }
        public static string getProjectPath()
        {

            return HttpRuntime.AppDomainAppPath.ToString();
        }



        public static object HConvertByType(object val, Type type)
        {

            try
            {
                Type valtype = type;
                string pptypeName = "";
                pptypeName = valtype.FullName.ToLower();
                if (pptypeName.LastIndexOf("system.int") != -1)
                {
                    if (pptypeName.LastIndexOf("64") != -1)
                    {
                        return Convert.ToInt64(val);

                    }
                    else if (pptypeName.LastIndexOf("32") != -1)
                    {
                        return Convert.ToInt32(val);
                    }


                }
                else if (pptypeName == "system.datetime")
                {
                    return Convert.ToDateTime(val);
                }
                else if (pptypeName == "system.string")
                {
                    return Convert.ToString(val);
                }
                else if (pptypeName == "system.decimal")
                {
                    return Convert.ToDecimal(val);
                }
                else if (pptypeName.IndexOf("nullable") != -1)
                {

                    if (pptypeName.LastIndexOf("int") != -1)
                    {
                        try { return (int?)val; }
                        catch
                        {

                            return Int64.Parse(val + "");
                        }

                    }
                    else if (pptypeName.LastIndexOf("datetime") != -1)
                    {
                        string dobj = (val + "").Replace("/", "-");
                        try
                        {
                            return (DateTime?)DateTime.Parse(dobj);
                        }
                        catch (Exception ex)
                        {

                            return null;
                        }

                    }
                    else if (pptypeName.LastIndexOf("string") != -1)
                    {
                        return Convert.ToString(val);
                    }
                    else if (pptypeName.LastIndexOf("decimal") != -1)
                    {
                        try
                        {

                            return Convert.ToDecimal(val);

                        }catch(Exception ex){
                            return null;
                        
                        }
                      
                    }
                }

            }
            catch
            {


            }
            return val;
        }


        public static object GetNNType(object val, System.Reflection.PropertyInfo pi)
        {
            if (string.IsNullOrEmpty(val + ""))
            {

                return "NULL";
            }
            Type tp = pi.PropertyType;
            ///   object _o = FDispose.HConvertByType(val, tp);
            Type valtype = tp;
            string pptypeName = "";
            pptypeName = valtype.FullName.ToLower();

            try
            {
                if (pptypeName.LastIndexOf("int") != -1)
                {
                    return Convert.ToInt32(val);
                }
                else if (pptypeName.LastIndexOf("datetime") != -1)
                {
                    return "''" + Convert.ToDateTime(val).ToString("yyyy-MM-dd hh:mm:ss") + "''";
                }
                else if (pptypeName.LastIndexOf("string") != -1)
                {
                    return "N''" + Convert.ToString(val) + "''"; ;
                }
                else if (pptypeName.LastIndexOf("decimal") != -1)
                {
                    return Convert.ToDecimal(val);
                }

            }
            catch (Exception ex)
            {
                if (pptypeName.IndexOf("nullable") != -1)
                {


                    if (pptypeName.LastIndexOf("int") != -1)
                    {
                        if (pptypeName.LastIndexOf("64") != -1)
                        {
                            long oLong = 0;
                            long.TryParse(val + "", out oLong);
                            return oLong;
                        }
                        else
                        {
                            return (int?)val;

                        }

                    }
                    else if (pptypeName.LastIndexOf("datetime") != -1)
                    {
                        string dobj = (val + "").Replace("/", "-");
                        try
                        {
                            return "''" + (DateTime?)DateTime.Parse(dobj) + "''";
                        }
                        catch (Exception exx)
                        {

                            return null;
                        }

                    }
                    else if (pptypeName.LastIndexOf("string") != -1)
                    {
                        return "N''" + Convert.ToString(val) + "''"; ;
                    }
                    else if (pptypeName.LastIndexOf("decimal") != -1)
                    {
                        return (decimal?)val;
                    }
                }
            }



            return val;


        }
        public static object GetNType(object val, System.Reflection.PropertyInfo pi)
        {
            if (string.IsNullOrEmpty(val + ""))
            {

                return "NULL";
            }
            Type tp = pi.PropertyType;
            ///   object _o = FDispose.HConvertByType(val, tp);
            Type valtype = tp;
            string pptypeName = "";
            pptypeName = valtype.FullName.ToLower();

            try
            {
                if (pptypeName.LastIndexOf("int") != -1)
                {
                    return Convert.ToInt32(val);
                }
                else if (pptypeName.LastIndexOf("datetime") != -1)
                {
                    return "'" + Convert.ToDateTime(val).ToString("yyyy-MM-dd hh:mm:ss") + "'";
                }
                else if (pptypeName.LastIndexOf("string") != -1)
                {
                    return "N'" + Convert.ToString(val) + "'"; ;
                }
                else if (pptypeName.LastIndexOf("decimal") != -1)
                {
                    return Convert.ToDecimal(val);
                }

            }
            catch (Exception ex)
            {
                if (pptypeName.IndexOf("nullable") != -1)
                {


                    if (pptypeName.LastIndexOf("int") != -1)
                    {
                        if (pptypeName.LastIndexOf("64") != -1)
                        {
                            long oLong = 0;
                            long.TryParse(val + "", out oLong);
                            return oLong;
                        }
                        else
                        {
                            return (int?)val;

                        }

                    }
                    else if (pptypeName.LastIndexOf("datetime") != -1)
                    {
                        string dobj = (val + "").Replace("/", "-");
                        try
                        {
                            return "'" + (DateTime?)DateTime.Parse(dobj) + "'";
                        }
                        catch (Exception exx)
                        {

                            return null;
                        }

                    }
                    else if (pptypeName.LastIndexOf("string") != -1)
                    {
                        return "N'" + Convert.ToString(val) + "'"; ;
                    }
                    else if (pptypeName.LastIndexOf("decimal") != -1)
                    {
                        return (decimal?)val;
                    }
                }
            }



            return val;


        }

        public static object GetNNType(object val, string typeName)
        {
            if (string.IsNullOrEmpty(val + ""))
            {

                return "NULL";
            }

            string pptypeName = typeName.ToLower();


            try
            {
                if (pptypeName.LastIndexOf("int") != -1)
                {
                    return Convert.ToInt32(val);
                }
                else if (pptypeName.LastIndexOf("datetime") != -1)
                {
                    return "''" + Convert.ToDateTime(val) + "''";
                }
                else if (pptypeName.LastIndexOf("string") != -1)
                {
                    return "N''" + Convert.ToString(val) + "''"; ;
                }
                else if (pptypeName.LastIndexOf("decimal") != -1)
                {
                    return Convert.ToDecimal(val);
                }

            }
            catch (Exception ex)
            {
                if (pptypeName.IndexOf("nullable") != -1)
                {


                    if (pptypeName.LastIndexOf("int") != -1)
                    {
                        if (pptypeName.LastIndexOf("64") != -1)
                        {
                            long oLong = 0;
                            long.TryParse(val + "", out oLong);
                            return oLong;
                        }
                        else
                        {
                            return (int?)val;

                        }

                    }
                    else if (pptypeName.LastIndexOf("datetime") != -1)
                    {
                        string dobj = (val + "").Replace("/", "-");
                        try
                        {
                            return "''" + (DateTime?)DateTime.Parse(dobj) + "''";
                        }
                        catch (Exception exx)
                        {

                            return null;
                        }

                    }
                    else if (pptypeName.LastIndexOf("string") != -1)
                    {
                        return "N''" + Convert.ToString(val) + "''"; ;
                    }
                    else if (pptypeName.LastIndexOf("decimal") != -1)
                    {
                        return (decimal?)val;
                    }
                }
            }



            return val;


        }
        public static object GetNType(object val,string typeName)
        {
            if (string.IsNullOrEmpty(val + ""))
            {

                return "NULL";
            }

            string pptypeName = typeName.ToLower();
          

            try
            {
                if (pptypeName.LastIndexOf("int") != -1)
                {
                    return Convert.ToInt32(val);
                }
                else if (pptypeName.LastIndexOf("datetime") != -1)
                {
                    return "'" + Convert.ToDateTime(val) + "'";
                }
                else if (pptypeName.LastIndexOf("string") != -1)
                {
                    return "N'" + Convert.ToString(val) + "'"; ;
                }
                else if (pptypeName.LastIndexOf("decimal") != -1)
                {
                    return Convert.ToDecimal(val);
                }

            }
            catch (Exception ex)
            {
                if (pptypeName.IndexOf("nullable") != -1)
                {


                    if (pptypeName.LastIndexOf("int") != -1)
                    {
                        if (pptypeName.LastIndexOf("64") != -1)
                        {
                            long oLong = 0;
                            long.TryParse(val + "", out oLong);
                            return oLong;
                        }
                        else
                        {
                            return (int?)val;

                        }

                    }
                    else if (pptypeName.LastIndexOf("datetime") != -1)
                    {
                        string dobj = (val + "").Replace("/", "-");
                        try
                        {
                            return "'" + (DateTime?)DateTime.Parse(dobj) + "'";
                        }
                        catch (Exception exx)
                        {

                            return null;
                        }

                    }
                    else if (pptypeName.LastIndexOf("string") != -1)
                    {
                        return "N'" + Convert.ToString(val) + "'"; ;
                    }
                    else if (pptypeName.LastIndexOf("decimal") != -1)
                    {
                        return (decimal?)val;
                    }
                }
            }



            return val;


        }

        public static object GetNType(object val, Type tp)
        {


            
            if (string.IsNullOrEmpty(val + ""))
            {

                return "NULL";
            }

            string pptypeName = tp.Name.ToLower();


            try
            {
                if (pptypeName.LastIndexOf("int") != -1)
                {
                    return Convert.ToInt32(val);
                }
                else if (pptypeName.LastIndexOf("datetime") != -1)
                {
                    return "'" + Convert.ToDateTime(val).ToString("yyyy-MM-dd hh:mm:ss") +"'";
                }
                else if (pptypeName.LastIndexOf("string") != -1)
                {
                    return "N'" + Convert.ToString(val) + "'"; ;
                }
                else if (pptypeName.LastIndexOf("decimal") != -1)
                {
                    return Convert.ToDecimal(val);
                }

            }
            catch (Exception ex)
            {
                if (pptypeName.IndexOf("nullable") != -1)
                {


                    if (pptypeName.LastIndexOf("int") != -1)
                    {
                        if (pptypeName.LastIndexOf("64") != -1)
                        {
                            long oLong = 0;
                            long.TryParse(val + "", out oLong);
                            return oLong;
                        }
                        else
                        {
                            return (int?)val;

                        }

                    }
                    else if (pptypeName.LastIndexOf("datetime") != -1)
                    {
                        string dobj = (val + "").Replace("/", "-");
                        try
                        {
                            return "'" + (DateTime?)DateTime.Parse(dobj) + "'";
                        }
                        catch (Exception exx)
                        {

                            return null;
                        }

                    }
                    else if (pptypeName.LastIndexOf("string") != -1)
                    {
                        return "N'" + Convert.ToString(val) + "'"; ;
                    }
                    else if (pptypeName.LastIndexOf("decimal") != -1)
                    {
                        return (decimal?)val;
                    }
                }
            }



            return val;


        }

        public static DataColumn NewTableColumnProperty(String ColumnName, string DataType)
        {
           
            DataColumn Column = new DataColumn();

            try
            {
                Column.DataType = System.Type.GetType(DataType);
                Column.ColumnName = ColumnName;
            }
            catch (Exception ext) {

                new Exception("参数名：string DataType 的值:" + DataType + " 异常信息：" + ext.Message);
            
            }
         
            //Column.AutoIncrement = true;

            return Column;
        }



        public static DataTable MakeNewTable(string tableName, bool isSetFirstPrimaryKey = true, params string[] arrKeysAndValues)
        {
            // Create a new DataTable titled 'Names.'
            DataTable namesTable = new DataTable(tableName);




            byte counter = 0;
            if (isSetFirstPrimaryKey&&counter!=2)
            {
                counter = 2;

                DataColumn idColumn = NewTableColumnProperty(arrKeysAndValues[0], arrKeysAndValues[1]);
                idColumn.AutoIncrement = true;
                namesTable.Columns.Add(idColumn);

                DataColumn[] keys = new DataColumn[1];
                keys[0] = idColumn;
                namesTable.PrimaryKey = keys;
                

            }
            for (short icounter = counter; icounter < arrKeysAndValues.Length / 2; icounter++)
            {

                DataColumn nameColumn = NewTableColumnProperty(arrKeysAndValues[icounter*2], arrKeysAndValues[(icounter + 1)*2-1]);
                namesTable.Columns.Add(nameColumn);

            }


            return namesTable;
        }

      
        /// <summary>
        ///设置cookies时间
        /// </summary>
        /// <param name="name"> key</param>
        /// <param name="val"></param>
        /// <param name="outTime">过期时间</param>
        /// <returns></returns>
        public static bool AddCookie(string name, string val, double outTime) { 
        
            HttpCookie hc=new HttpCookie(name,val);
            hc.Expires = DateTime.Now.AddHours(outTime);
           
            HttpContext.Current.Response.Cookies.Add(hc);

            return false;
        
        }
        
        public static string GetCookieValue(string name)
        {

            HttpCookie hc = HttpContext.Current.Request.Cookies.Get(name);
         
            if (hc != null) {


            return  hc.Value;
            
            }



            return "";
        
        }

        public static string ObjectToJsonStr(object obj)
        {
          
               JavaScriptSerializer serializer = new JavaScriptSerializer();

               return serializer.Serialize(obj);
        
        }
        public static bool ModelSetVal<T>(ref T modelVal, Dictionary<string, string> vl)
        {


            if (!IsDcNULL<string, string>(vl))
                return false;

            Type type = typeof(T);
            object obj = type.Assembly.CreateInstance(type.FullName);
            foreach (PropertyInfo pi in obj.GetType().GetProperties())
            {
                string val = "";
                string name = "";
                try
                {

                    KeyValuePair<string, string> kvp =

                    (KeyValuePair<string, string>)vl.Where(a => a.Key.ToLower().Trim().Equals(pi.Name.ToLower().Trim())).Skip(0).Take(1).
                    FirstOrDefault();


                    val = kvp.Value + "";
                    name = "" + pi.Name;

                }
                catch (Exception ex)
                {

                }
                Type tp = pi.PropertyType;



                if (string.IsNullOrEmpty(val))
                {

                    continue;
                }

                object _o = NTool.HConvertByType(val, tp);

                pi.SetValue(obj, _o, null);
            }


            modelVal = (T)obj;

            return true;



        }


        public static T ModelSetVal<T>(Dictionary<string, string> vl)
        {
            Type type = typeof(T);
            object obj = type.Assembly.CreateInstance(type.FullName);

            if (!IsDcNULL<string, string>(vl))
                return (T)obj;


            foreach (PropertyInfo pi in obj.GetType().GetProperties())
            {
                string val = "";
                string name = "";
                try
                {

                    KeyValuePair<string, string> kvp =

                    (KeyValuePair<string, string>)vl.Where(a => a.Key.ToLower().Trim().Equals(pi.Name.ToLower().Trim())).Skip(0).Take(1).
                    FirstOrDefault();


                    val = kvp.Value + "";
                    name = "" + pi.Name;

                }
                catch (Exception ex)
                {

                }
                Type tp = pi.PropertyType;



                if (string.IsNullOrEmpty(val))
                {

                    continue;
                }

                object _o = NTool.HConvertByType(val, tp);

                pi.SetValue(obj, _o, null);
            }


            return (T)obj;





        }


        public static void SetModelFieldVal(PropertyInfo[] pi, ref Object obj, string keyPostOrGet, string val)
        {
            List<PropertyInfo> kvp = pi.Where(a => a.Name.ToLower().Trim().Equals(keyPostOrGet.ToLower().Trim())).Skip(0).Take(1).ToList();


            if (kvp != null && kvp.Count > 0)
            {





                Type tp = kvp[0].PropertyType;


                if (string.IsNullOrEmpty(val))
                {

                    return;
                }

                object _o = NTool.HConvertByType(val, tp);

               

                kvp[0].SetValue(obj, _o, null);

            }



        }
   


        public static string SetJsParameter(object obj,string mothodName)
        {
            Type tpe = obj.GetType();
            PropertyInfo[] pi = tpe.GetProperties();
           // object obj = tpe.Assembly.CreateInstance(tpe.FullName);
            StringBuilder str = new StringBuilder();
            str.AppendFormat("  var {0}_{1}_class = $(\".{0}_{1}\")  ", mothodName, obj.GetType().Name);

            str.AppendFormat(" var   {0}_{1}_Len= {0}_{1}_class.Length  ", mothodName, obj.GetType().Name);
            str.AppendFormat("");


            foreach(PropertyInfo pInfo in pi)
            {

                
                str.AppendFormat("  var {0}_{1}_class = $(.{0}_{1})  ", mothodName, obj.GetType().Name);


            }
            return "";

           
        
        }

      
        /// <summary>
        /// 选取list的数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataSources"></param>
        /// <param name="pde"></param>
        /// <returns></returns>
        public static List<T> SelectListData<T>(List<T> dataSources, Predicate<T> pde)
        {

            if (dataSources != null && dataSources.Count > 0)
            {

                return dataSources.FindAll(pde);

            }

            return null;

        }

        /// <summary>
        /// session 是否相等
        /// </summary>
        /// <param name="codeName">sessionName</param>
        /// <param name="original">原始值</param>
        /// <returns></returns>
        public static bool IsSessionEquals(object codeName, object original)
        {

            if (HttpContext.Current.Session[codeName + ""] != null && original != null)
            {

                bool isEquals = (HttpContext.Current.Session[codeName + ""] + "").Equals(original.ToString().ToLower());
                if (isEquals)
                {

                    return true;


                }





            }

            return false;

        }
        /// <summary>
        /// 有数据是等于指定长度 返回真
        /// </summary>
        /// <typeparam name="T"> 类型</typeparam>
        /// <param name="obj"> 数组对象 </param>
        /// <returns></returns>
        public static bool IsArrEqualLen<T>(T[] obj,int equalsLen)
        {

            if (obj != null && obj.Length >= equalsLen)
            {

                return true;

            }


            return false;


        }
        /// <summary>
        /// 有数据返回真
        /// </summary>
        /// <typeparam name="T"> 类型</typeparam>
        /// <param name="obj"> 数组对象 </param>
        /// <returns></returns>
        public static bool IsArrNULL<T>(T[] obj)
        {

            if (obj != null && obj.Length > 0)
            {

                return true; 
          
            }


            return false;


        }
        /// <summary>
        /// 有数据返回真
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static bool IsDcNULL<T, V>(IDictionary<T, V> list)
        {


            if (list != null && list.Count > 0)
            {
                return true;

            }

            return false;

        }
       /// <summary>
        /// 有数据返回真
       /// </summary>
       /// <typeparam name="T"></typeparam>
       /// <param name="list"></param>
       /// <returns></returns>
        public static bool IsLtNULL<T>(IList<T> list)
        {


            if (list != null && list.Count > 0)
            {
                return true;

            }

            return false;

        }



        /// <summary>
        /// 判断数组所有值，只要有一个等于eqVal 假 没有值也返回假
        /// </summary>
        /// <typeparam name="T"> 类型</typeparam>
        /// <param name="eqVal"> 对比较值 </param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static bool IsValEquals<T>(T eqVal ,params T[] t)
        {
            if (eqVal == null||IsArrNULL<T>(t))
            {

                return false;

            }

            bool isflag = true;
            foreach (T outT in t)
            {

                if (eqVal.Equals(outT))
                {
                    isflag = false;
                    break;
                 
                }
               
                 
            }

            return isflag;
        
        }
        /// 要转换的数组
        
        public static void GetClassNameObj(string className)
        {

        Type tp=   Type.GetType(className);

       
          
            
             
        
        }

        /// <summary>
        ///  数组转换类型
        /// </summary>
        /// <typeparam name="outT">(转换目标类型)</typeparam>
        /// <typeparam name="inT">源类型</typeparam>
        /// <param name="aarStr"></param>
        /// <returns> outT</returns>
        public static outT[] ArrayToAll<outT, inT>(inT[] aarStr)
        {
            outT[] delIntArr = Array.ConvertAll(aarStr, new Converter<inT, outT>(ToType<outT, inT>));


            return delIntArr;
        }

        public static T ToType<T,inT>(inT argItem)
        {

         

            return (T)Convert.ChangeType(argItem, typeof(T));

        }


    

    }
}
