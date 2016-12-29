using System.Collections.Generic;
using DAL;

namespace BLL
{
	public class DB_Area : NSPMsSql<NModel.DB_Area>
	{
		public List<NModel.DB_Area> GetListNextLevel(int level)
		{
			string execSql = string.Format("Exec  {0}_GetListNextLevel @Level={1} ", ETblName, level);
			return base.GetList(execSql);
		}
	}
}