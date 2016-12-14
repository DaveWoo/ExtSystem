using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Tool
{
   public class NFTool
    {
        /// <summary>
        /// 过滤SQL语句,防止注入
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns>0 - 没有注入, 1 - 有注入 </returns>
        public static int filterSql(string sSql)
        {
            try
            {
                int srcLen, decLen = 0;
                sSql = sSql.ToLower().Trim();
                srcLen = sSql.Length;

                sSql = sSql.Replace("insert", "");

                sSql = sSql.Replace("update", "");
                sSql = sSql.Replace("exec", "");
                sSql = sSql.Replace("delete", "");
                sSql = sSql.Replace("master", "");
                sSql = sSql.Replace("truncate", "");
                sSql = sSql.Replace("declare", "");
                sSql = sSql.Replace("create", "");
                sSql = sSql.Replace("xp_", "no");
                decLen = sSql.Length;
                if (srcLen == decLen) return 0; else return 1;

            }
            catch
            {
                return 1;
            }


        }
        /// <summary>
        ///    "'|and|exec|insert|select|delete|update|count|*|chr|mid|master|truncate|char|declare|xp_cmdshell|drop","exec |insert |select |delete |update |count |chr |mid |master |truncate |char |declare|xp_cmdshell|drop"
        /// </summary> 


        public static List<string> listSqlStr = new List<string>(){
      "'|and|exec|insert|select|delete|update|count|*|chr|mid|master|truncate|char|declare|xp_cmdshell|drop",
      "exec |insert |select |delete |update |count |chr |mid |master |truncate |char |declare|xp_cmdshell|drop"
      
      };



        /**/
        /// <summary>
        /// 分析用户请求是否正常 "'|and|exec|insert|select|delete|update|count|*|chr|mid|master|truncate|char|declare|xp_cmdshell|drop","exec |insert |select |delete |update |count |chr |mid |master |truncate |char |declare|xp_cmdshell|drop"
        /// </summary>
        /// <param name="Str">传入用户提交数据</param>
        /// <returns>返回是否含有SQL注入式攻击代码</returns>
        public static bool ProcessSqlStr(string Str, int type)
        {



            string SqlStr =NFTool.listSqlStr[type];

            bool ReturnValue = true;
            try
            {
                if (!string.IsNullOrEmpty(Str))
                {

                    //  string[] anySqlStr = SqlStr.Split('|');
                    //  foreach (string ss in anySqlStr)


                    Regex regexInsert = new Regex(@"insert\s{1,}into .{0,}\s{1,}values", RegexOptions.IgnoreCase);
                    Regex regexSelect = new Regex(@"select\s.{0,}\s{1,}from", RegexOptions.IgnoreCase);

                    Regex regexUpdate = new Regex(@"update\s{1,}.{0,}set", RegexOptions.IgnoreCase);
                    Regex regexDelete = new Regex(@"DELETE\s{1,}.{0,}from\s", RegexOptions.IgnoreCase);
                    Regex regexDrop = new Regex(@"drop\s.{0,}\s{1,}.{1,}", RegexOptions.IgnoreCase);
                    Regex regexEXECA = new Regex(@"EXEC\(.{1,}", RegexOptions.IgnoreCase);
                    Regex regexEXECB = new Regex(@"EXEC\s.{1,}", RegexOptions.IgnoreCase);
                    Regex regexTruncate = new Regex(@"truncate\s.{1,}", RegexOptions.IgnoreCase);
                    Regex regexCreate = new Regex(@"CREATE\s.{1,}\s", RegexOptions.IgnoreCase);
                    Regex regexALTER = new Regex(@"ALTER\s.{0,}\s{1,}.{1,}", RegexOptions.IgnoreCase);


                     bool isSelect = regexSelect.IsMatch(Str.ToLower());

                    if (isSelect)
                    {
                        ReturnValue = false;
                        return false;
                    }
                    bool isInsert = regexInsert.IsMatch(Str.ToLower());


                    if (isInsert)
                    {
                        ReturnValue = false;
                        return false;
                    }
                    bool isUpdate = regexUpdate.IsMatch(Str.ToLower());

                    if (isUpdate)
                    {
                        ReturnValue = false;
                        return false;
                    }
                    bool isDelete = regexDelete.IsMatch(Str.ToLower());

                    if (isDelete)
                    {
                        ReturnValue = false;
                        return false;
                    }
                    bool isDrop = regexDrop.IsMatch(Str.ToLower());

                    if (isDrop)
                    {
                        ReturnValue = false;
                        return false;
                    }
                    bool isEXECA = regexEXECA.IsMatch(Str.ToLower());

                    if (isEXECA)
                    {
                        ReturnValue = false;
                        return false;
                    }

                    bool isEXECB = regexEXECB.IsMatch(Str.ToLower());
                    if (isEXECB)
                    {
                        ReturnValue = false;
                        return false;
                    }

                    bool isTruncate = regexTruncate.IsMatch(Str.ToLower());
                    if (isTruncate)
                    {
                        ReturnValue = false;
                        return false;

                    }
                    bool isCreate = regexCreate.IsMatch(Str.ToLower());
                    if (isCreate)
                    {
                        ReturnValue = false;
                        return false;
                    }
                    bool isALTER = regexALTER.IsMatch(Str.ToLower());
                    if (isALTER)
                    {
                        ReturnValue = false;
                        return false;
                    }
                }
            }
            catch
            {
                ReturnValue = false;
            }
            return ReturnValue;
        }
    
    }
}
