using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IDAL
{
  public  interface INSQLAbl<N>
    {

N  GetModel(long getID);  //获取ID一个模型
N  GetModel(String getCmd); //获取在那里一个模型


List<N> GetList(List<long> listID   );//获取ID列表多个模型
List<N> GetList(List<long> listID ,String orders  ); //获取列表ID和排序

List<N> GetList(String getWhere, int top);
List<N> GetList(String getCmd);
List<N> GetList(String getWhere ,String orders ); //获取在那里和排序的泛型列表

List<N> GetList(String getWhere, String orders,int top);
bool Edit(long ID, N editObj);//编辑ID，设置对象
long Edit(List<long> longID ,List<N> editObj); // 编辑列表ID,设置泛型列表
bool Edit(String where ,List<N> editObj); // 编辑在那里，设置泛型列表
bool Add(N addObj); //增加一个模型
long Add(List<N> addObj  ); //增加泛型列表


    }
}
