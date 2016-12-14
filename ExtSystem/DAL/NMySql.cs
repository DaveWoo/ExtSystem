using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IDAL;
using Tool;
using System.Data;

namespace DAL
{
  public  class NMySql<N> : MySql,INSQLAbl<N>
    {

     
        public NMySql(string ConfigString,string TableName):base(ConfigString,TableName)
           
        {
           

        }
        public NMySql()
            : base(MySql.MySqlConfigString, typeof(N).Name)
        {


        }
        public NMySql(string ConfigString)
            : base(ConfigString, typeof(N).Name)
        {


        }
        public long Add(List<N> addObj)
        {


            if (base.Add(NFactory.FillDataTable<N>(addObj)))
            {
                return 1;

            };
            return 0;


        }

        public bool Add(N addObj)
        {


            if (base.Add(NFactory.FillDataTable<N>(new List<N>() { addObj })))
            {
                return true;

            };
            return false;
        }

        public bool Edit(string where, List<N> editObj)
        {
            return base.Edit(where, NFactory.FillDataTable<N>(editObj));
        }

        public long Edit(List<long> longID, List<N> editObj)
        {
            return base.Edit(longID, NFactory.FillDataTable<N>(editObj));
        }

        public bool Edit(long ID, N editObj)
        {
            return base.Edit(ID, NFactory.FillDataTable<N>(new List<N>() { editObj }));
                
        }

        public List<N> GetList(string getWhere, string orders)
        {
            
            List<N> nList = NFactory.FillModel<N>(base.GetTable(getWhere,orders));
            
            return nList;
        }

        public List<N> GetList(string getWhere)
        {
            List<N> nList = NFactory.FillModel<N>(base.GetTable(getWhere));

            return nList;
        }

        public List<N> GetList(List<long> listID, string orders)
        {
           
            List<N> nList = NFactory.FillModel<N>(base.GetTable(listID,orders));

            return nList;
        }

        public List<N> GetList(List<long> listID)
        {
           
            List<N> nList = NFactory.FillModel<N>(base.GetTable(listID));

            return nList;
        }

        public N GetModel(string getWhere)
        {
            
            List<N> nList = NFactory.FillModel<N>(base.GetTable(getWhere,1));
            if (nList != null && nList.Count > 0) { return nList[0]; }
            return default(N);
        }

        public N GetModel(long getID)
        {

    
           List<N>  nList=    NFactory.FillModel<N>(base.GetTable(getID));
           if (nList != null && nList.Count > 0) { return nList[0]; }
           return default(N);
        }


        public List<N> GetList(string getWhere, string orders, int top)
        {
            return NFactory.FillModel<N>(base.GetTable(getWhere, top, orders));
        }

        public List<N> GetList(string getWhere, int top)
        {
           
            List<N> nList = NFactory.FillModel<N>(base.GetTable(getWhere, top));

            return nList;
        }
    }
}
