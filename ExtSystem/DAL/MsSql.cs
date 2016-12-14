using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DAL
{
    public class MsSql : SQL
    {


        public static string MsSqlConfigString { get { return "dbmssql"; } }


        public MsSql(string TableName)
            : base(MsSql.MsSqlConfigString, TableName)
        {


        }
        public MsSql(string ConfigString, string TableName)
            : base(ConfigString, TableName)
        {


        }


        public SqlCommand MsSqlCmd { get; set; }
        public SqlConnection MsSqlCon { get; set; }



        public override bool Add(System.Data.DataTable addTable)
        {
           
            String sql = "";
            if (this.IsOpenDataBase)
            {
                SqlCommand sqlcmd = this.MsSqlCon.CreateCommand();

                if (addTable != null && addTable.Rows.Count > 0)
                {


                    try
                    {
                        Dictionary<string, object> dictKV = new Dictionary<string, object>();

                        List<object> key = new List<object>();
                        List<object> val = new List<object>();
                        {

                            foreach (DataColumn dc in addTable.Columns)
                            {



                                key.Add(dc.ColumnName);

                            }
                        }
                        {
                            //short counter = 0;
                            foreach (DataRow dr in addTable.Rows)
                            {
                                //  StringBuilder _val = new StringBuilder();
                                foreach (DataColumn dc in addTable.Columns)
                                {


                                    val.Add(dr[dc.ColumnName] + "");


                                }
                                sql = base.InsertCmd(base.TableName, key, val);
                                sqlcmd.CommandText = sql;
                                foreach (string _key in base.listSqlParam.Keys)
                                {

                                    object vl = base.listSqlParam[_key] is DateTime ? (DateTime.Parse(base.listSqlParam[_key] + "")).ToString("yyyy-MM-dd hh:mm:ss") : base.listSqlParam[_key];
                                    sqlcmd.Parameters.AddWithValue(_key, vl);

                                }

                           
                                if (sqlcmd.ExecuteNonQuery() > 0)
                                {
                                    base.msg = "第三步 微软公司数据库增加数据成功 sql语句：" + sql;

                                }
                                else
                                {

                                    base.msg = "第三步 微软公司数据库增加数据失败 sql语句：" + sql;
                                }

                            }

                        }



                    }



                    catch (Exception ex)
                    {
                        base.msg = "第二步 微软公司数据库创建/增加数据sql 语句失败 sql语句：" + sql + "  异常信息：" + ex.Message;

                    }

                }
                else
                {
                    base.msg = "第二步  增加参数为空";


                }

               // this.Close();
            }


            return false;
        }

        public override bool Close()
        {
            if (this.MsSqlCon != null && this.MsSqlCon.State == ConnectionState.Open)
            {

                this.MsSqlCon.Dispose();
                if (this.MsSqlCmd != null)
                    this.MsSqlCmd.Dispose();
                this.MsSqlCon.Close();
                base.msg = "最后->关闭 微软公司数据库 要请继续调用对象Open() 方法连接数据库";
                return true;
            }
            return false;
        }

        public override bool Connect(string sql)
        {

            this.MsSqlCon = new SqlConnection(sql);
            if (this.MsSqlCon != null && MsSqlCon.State == ConnectionState.Closed)
            {
                this.MsSqlCon.Open();


                return true;

            }

            return false;
        }





        public override bool Open()
        {
            try
            {
                this.MsSqlCon = new SqlConnection(base.ConfigStr);
                if (this.MsSqlCon != null && MsSqlCon.State == ConnectionState.Closed)
                {
                    this.MsSqlCon.Open();
                    this.msg = "第一步打开数库库据成功……请继续第二步";
                    base.isOpenDataBase = true;
                    return true;

                }
                else
                {
                    this.msg = "第一步打开数据库失败……请查看    ConfigStr:" + this.ConfigStr + "没有异常信息 ";
                    base.isOpenDataBase = false;
                };
                //this.Open();
            }
            catch (Exception ex)
            {
                base.isOpenDataBase = false;
                this.msg = "第一步打开数据库失败……请查看   ConfigStr:" + this.ConfigStr + " 异常信息: " + ex.Message;
            }
            return false;
        }



        public override DataTable GetTable(long getTableID)
        {

            string[] ArrID = base.TableName.Split('_');

            string IDAttribute = "";
            if (ArrID.Length > 1)
            {
                IDAttribute = ArrID[1] + "_id";
            }
            else
            {
                IDAttribute = "ID";
            }
            ;
            //   return this.GetTable(string.Format("select *  from {0} where {1}={2}  order by {3} limit 0, 1", base.TableName, IDAttribute, getTableID, IDAttribute));

            List<string> keys = new List<string>();

            keys.Add("@ID");



            List<object> values = new List<object>();

            values.Add(getTableID);



            return this.GetTable((string.Format("select  top 1 * from  {0}    where {1}=@ID   order by {2} ", base.TableName, IDAttribute, IDAttribute)),
           keys, values);


        }

        public override long Edit(List<long> editIds, DataTable editTable)
        {
            string[] ArrID = base.TableName.Split('_');
            long iconter = 0;
            StringBuilder inVals = new StringBuilder();
            string IDAttribute = "";
            if (ArrID.Length > 1)
            {
                IDAttribute = ArrID[1] + "_id";
            }
            else
            {
                IDAttribute = "ID";
            }

            if (editIds != null)
            {



                for (int ic = 0; ic < editIds.Count; ic++)
                {

                    inVals.Append(editIds[ic]);
                    if (ic < editIds.Count - 1)
                    {
                        inVals.Append(",");

                    }

                }


            }

            String sql = "";
            if (this.IsOpenDataBase)
            {
                SqlCommand sqlcmd = this.MsSqlCon.CreateCommand();

                if (editTable != null && editTable.Rows.Count > 0)
                {


                    try
                    {
                        Dictionary<string, object> dictKV = new Dictionary<string, object>();

                        List<object> key = new List<object>();

                        {

                            foreach (DataColumn dc in editTable.Columns)
                            {



                                key.Add(dc.ColumnName);

                            }
                        }
                        {



                            //short counter = 0;
                            foreach (DataRow dr in editTable.Rows)
                            {


                                List<object> val = new List<object>();
                                //  StringBuilder _val = new StringBuilder();
                                foreach (DataColumn dc in editTable.Columns)
                                {


                                    val.Add(dr[dc.ColumnName] + "");


                                }
                                sql = base.UpdateCmd(base.TableName, key, val, string.Format(" {1} in ({0})", inVals, IDAttribute));

                                foreach (string _key in base.listSqlParam.Keys)
                                {

                                    object vl = base.listSqlParam[_key] is DateTime ? (DateTime.Parse(base.listSqlParam[_key] + "")).ToString("yyyy-MM-dd hh:mm:ss") : base.listSqlParam[_key];
                                    sqlcmd.Parameters.AddWithValue(_key, vl);

                                }

                                sqlcmd.CommandText = sql;

                                try
                                {
                                    if (sqlcmd.ExecuteNonQuery() > 0)
                                    {
                                        base.msg = "第三步 微软公司数据库增加数据成功 sql语句：" + sql;
                                        iconter++;
                                    }
                                    else
                                    {

                                        base.msg = "第三步 微软公司数据库增加数据失败 sql语句：" + sql;
                                    }
                                }
                                catch { }{ }


                            }

                        }



                    }



                    catch (Exception ex)
                    {
                        base.msg = "第二步 微软公司数据库创建/增加数据sql 语句失败 sql语句：" + sql + "  异常信息：" + ex.Message;

                    }

                }
                else
                {
                    base.msg = "第二步  增加参数为空";


                }

                // this.Close();
            }
           /// this.Close();

            return iconter;
        }

        public override bool Edit(long EidtID, DataTable editTable)
        {
            long ic = this.Edit(new List<long>() { EidtID }, editTable);
            return ic > 0 ? true : false;
        }


        public override DataTable GetTable(List<long> getTableID)
        {

            string[] ArrID = base.TableName.Split('_');

            string IDAttribute = "";
            if (ArrID.Length > 1)
            {
                IDAttribute = ArrID[1] + "_id";
            }
            else
            {
                IDAttribute = "ID";
            }

            if (getTableID != null)
            {


                StringBuilder inVals = new StringBuilder();
                for (int ic = 0; ic < getTableID.Count; ic++)
                {

                    inVals.Append(getTableID[ic]);
                    if (ic < getTableID.Count - 1)
                    {
                        inVals.Append(",");

                    }

                }

                List<string> keys = new List<string>();
                ;
                keys.Add("@inVals");


                List<object> values = new List<object>();

                values.Add(inVals.ToString());


                return this.GetTable(string.Format("select  top {3} *  from {0}  where  {1}  in ({4})  order by {2}", base.TableName, IDAttribute, IDAttribute, getTableID.Count, inVals),
              null, null);

            }

            return new DataTable();

        }


        public override DataTable GetTable(string getsWhere, int top, string orders)
        {
            if (!string.IsNullOrEmpty(getsWhere))
            {

                getsWhere = "where " + getsWhere;

            }
            else { getsWhere = ""; }
            if (top <= 0)
            {

                top = 2000;

            }

            if (string.IsNullOrEmpty(orders)) { orders = ""; }
            return this.GetTable(string.Format("select top {2}  from {0}   {1}  {3}  ", base.TableName, getsWhere, top, orders)
             );
        }

        public override DataTable GetTable(List<long> getTableID, string orders)
        {
            string[] ArrID = base.TableName.Split('_');

            string IDAttribute = "";
            if (ArrID.Length > 1)
            {
                IDAttribute = ArrID[1] + "_id";
            }
            else
            {
                IDAttribute = "ID";
            }

            if (getTableID != null)
            {


                StringBuilder inVals = new StringBuilder();
                for (int ic = 0; ic < getTableID.Count; ic++)
                {

                    inVals.Append(getTableID[ic]);
                    if (ic < getTableID.Count - 1)
                    {
                        inVals.Append(",");

                    }

                }


                if (string.IsNullOrEmpty(orders))
                {

                    orders = "";
                }



                return this.GetTable(string.Format("select top {3}  *  from {0}  where  {1}  in ({4})  {5} ", base.TableName, IDAttribute, IDAttribute, getTableID.Count, inVals, orders),
              null, null);

            }

            return new DataTable();
        }

        public override DataTable GetTable(string getsWhere, int top)
        {

            if (!string.IsNullOrEmpty(getsWhere)) { getsWhere = " where  " + getsWhere; } else { getsWhere = ""; }
            string sql = string.Format("select top {2}  *  from  {0}  {1}        ", base.TableName, getsWhere, top);
            return this.GetTable(sql);
        }

        public override DataTable GetTable(string getsWhere)
        {
            return this.GetTable(getsWhere, null, null);
        }



        public override DataTable GetTable(string getsWhere, List<string> keys, List<object> values)
        {
            DataTable dtData = new DataTable();

            StringBuilder sql = new StringBuilder();
            if (this.IsOpenDataBase)
            {
                try
                {




                    SqlCommand sqlcmd = this.MsSqlCon.CreateCommand();

                    sql.Append(getsWhere);


                    sqlcmd.CommandText = sql + "";

                    if (keys != null)
                        for (int i = 0; i < keys.Count; i++)
                        {

                            sqlcmd.Parameters.AddWithValue(keys[i], values[i]);


                        }

                    SqlDataReader sqr = sqlcmd.ExecuteReader();

                    dtData.Load(sqr);

                    if (dtData != null && dtData.Rows.Count > 0)
                    {
                        base.msg = "第三步 微软公司数据库读取成数据成功 sql语句：" + sql + "读取行数" + dtData.Rows.Count;

                    }
                    else
                    {

                        base.msg = "第三步 微软公司数据库读取数据成功 sql语句：" + sql + "读取行数" + dtData.Rows.Count; ;
                    }
                }
                catch (Exception ex)
                {
                    base.msg = "第二步 微软公司数据库创建/增加数据sql 语句失败 sql语句：" + sql + "  异常信息：" + ex.Message;

                }


            }

          ////  this.Close();
            return dtData;
        }

        public override DataTable GetTable(string getsWhere, string orders)
        {
            if (string.IsNullOrEmpty(orders))
            {

                orders = "";
            }
            if (!string.IsNullOrEmpty(getsWhere)) { getsWhere = " where  " + getsWhere; } else { getsWhere = ""; }
            string sql = string.Format("select *  from  {0}   {1}   {2}    ", base.TableName, getsWhere, orders);
            return this.GetTable(sql);
        }

        public override bool Edit(string editWhere, DataTable editTable)
        {

            StringBuilder inVals = new StringBuilder();


            String sql = "";
            if (this.IsOpenDataBase)
            {
                SqlCommand sqlcmd = this.MsSqlCon.CreateCommand();

                if (editTable != null && editTable.Rows.Count > 0)
                {


                    try
                    {
                        Dictionary<string, object> dictKV = new Dictionary<string, object>();

                        List<object> key = new List<object>();

                        {

                            foreach (DataColumn dc in editTable.Columns)
                            {



                                key.Add(dc.ColumnName);

                            }
                        }
                        {



                            //short counter = 0;
                            foreach (DataRow dr in editTable.Rows)
                            {


                                List<object> val = new List<object>();
                                //  StringBuilder _val = new StringBuilder();
                                foreach (DataColumn dc in editTable.Columns)
                                {


                                    val.Add(dr[dc.ColumnName] + "");


                                }
                                sql = base.UpdateCmd(base.TableName, key, val, editWhere);

                                foreach (string _key in base.listSqlParam.Keys)
                                {

                                    object vl = base.listSqlParam[_key] is DateTime ? (DateTime.Parse(base.listSqlParam[_key] + "")).ToString("yyyy-MM-dd hh:mm:ss") : base.listSqlParam[_key];
                                    sqlcmd.Parameters.AddWithValue(_key, vl);

                                }

                                sqlcmd.CommandText = sql;
                                if (sqlcmd.ExecuteNonQuery() > 0)
                                {
                                    base.msg = "第三步 微软公司数据库增加数据成功 sql语句：" + sql;

                                }
                                else
                                {

                                    base.msg = "第三步 微软公司数据库增加数据失败 sql语句：" + sql;
                                }

                            }

                        }



                    }



                    catch (Exception ex)
                    {
                        base.msg = "第二步 微软公司数据库创建/增加数据sql 语句失败 sql语句：" + sql + "  异常信息：" + ex.Message;

                    }

                }
                else
                {
                    base.msg = "第二步  增加参数为空";


                }

                // this.Close();
            }

           // this.Close();
            return false;
        }

        public override long Del(string delWhere)
        {
            if (!this.IsOpenDataBase)
            {

                return 0;
            }
            String[] ArrID = base.TableName.Split('_');


            int icounter = 0;


            String sql = "";

            {
                try
                {
                    SqlCommand sqlcmd = this.MsSqlCon.CreateCommand();

                    //
                    if (!string.IsNullOrEmpty(delWhere))
                    {

                        delWhere = " where  " + delWhere;

                    }
                    else { delWhere = ""; }
                    sql = string.Format("delete from {0}  {1} ", base.TableName, delWhere);
                    sqlcmd.CommandText = sql;



                    icounter = sqlcmd.ExecuteNonQuery();
                    base.msg = "第二步 微软公司数据库创建/删除数据sql 语句成功 sql语句：" + sql + " 删除行数：" + icounter;

                }





                catch (Exception ex)
                {
                    base.msg = "第二步 微软公司数据库创建/增加数据sql 语句失败 sql语句：" + sql + "  异常信息：" + ex.Message;

                }


            }
           // this.Close();
            return icounter;
        }

        public override long Del(List<long> delListID)
        {

            if (!this.IsOpenDataBase)
            {

                return 0;
            }
            String[] ArrID = base.TableName.Split('_');
            StringBuilder inVals = new StringBuilder();
            string IDAttribute = "";
            if (ArrID.Length > 1)
            {
                IDAttribute = ArrID[1] + "_id";
            }
            else
            {
                IDAttribute = "ID";
            }
            if (delListID != null)
            {



                for (int ic = 0; ic < delListID.Count; ic++)
                {

                    inVals.Append(delListID[ic]);
                    if (ic < delListID.Count - 1)
                    {
                        inVals.Append(",");

                    }

                }
            }
            else
            {
                base.msg = "第二步  参数为空";
                return 0;

            }


            int icounter = 0;


            String sql = "";

            {
                try
                {
                    SqlCommand sqlcmd = this.MsSqlCon.CreateCommand();
                    object[] arr = (object[])Tool.NTool.ConvertArray(delListID.ToArray(), typeof(object));
                    //

                    sql = base.DeleteCmd(base.TableName, new object[] { IDAttribute }, arr);
                    sqlcmd.CommandText = sql;
                    foreach (string _key in base.listSqlParam.Keys)
                    {


                        sqlcmd.Parameters.AddWithValue(_key, base.listSqlParam[_key]);

                    }


                    icounter = sqlcmd.ExecuteNonQuery();
                    base.msg = "第二步 微软公司数据库创建/删除数据sql 语句成功 sql语句：" + sql + " 删除行数：" + icounter;

                }





                catch (Exception ex)
                {
                    base.msg = "第二步 微软公司数据库创建/增加数据sql 语句失败 sql语句：" + sql + "  异常信息：" + ex.Message;

                }


            }
          //  this.Close();
            return icounter;
        }

        public override bool Del(long delID)
        {
            return this.Del(new List<long>() { delID }) > 0 ? true : false;



        }

        public override long DUI(string cmd)
        {
            try
            {
                SqlCommand sqlcmd = this.MsSqlCon.CreateCommand();
                sqlcmd.CommandText = cmd;
                int num = sqlcmd.ExecuteNonQuery();


                return num;
            }
            catch { return 0; }
            finally { 
            
           
            
            };
        }

        public override object GetObject(string cmd)
        {
            try
            {
                SqlCommand sqlcmd = this.MsSqlCon.CreateCommand();
                sqlcmd.CommandText = cmd;

                object obj=   sqlcmd.ExecuteScalar();
               /// this.Close();
                return obj;
               ;
            }
            catch {   return 0; };
        }
    }
}
