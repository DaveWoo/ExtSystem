using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace NModel{
	 	//db_log
		public class DB_Log
	{
   		     
      	/// <summary>
		/// auto_increment
        /// </summary>		
		private long _log_id;
        public long Log_ID
        {
            get{ return _log_id; }
            set{ _log_id = value; }
        }        
		/// <summary>
		/// Log_OperateCharacter
        /// </summary>		
		private string _log_operatecharacter;
        public string Log_OperateCharacter
        {
            get{ return _log_operatecharacter; }
            set{ _log_operatecharacter = value; }
        }        
		/// <summary>
		/// Log_UserName
        /// </summary>		
		private string _log_username;
        public string Log_UserName
        {
            get{ return _log_username; }
            set{ _log_username = value; }
        }        
		/// <summary>
		/// Log_TypeName
        /// </summary>		
		private string _log_typename;
        public string Log_TypeName
        {
            get{ return _log_typename; }
            set{ _log_typename = value; }
        }        
		/// <summary>
		/// Log_URL
        /// </summary>		
		private string _log_url;
        public string Log_URL
        {
            get{ return _log_url; }
            set{ _log_url = value; }
        }        
		/// <summary>
		/// Log_Ip
        /// </summary>		
		private string _log_ip;
        public string Log_Ip
        {
            get{ return _log_ip; }
            set{ _log_ip = value; }
        }        
		/// <summary>
		/// Log_AddTime
        /// </summary>		
		private DateTime? _log_addtime;
        public DateTime? Log_AddTime
        {
            get{ return _log_addtime; }
            set{ _log_addtime = value; }
        }        
		/// <summary>
		/// Log_EditTime
        /// </summary>		
		private DateTime? _log_edittime;
        public DateTime? Log_EditTime
        {
            get{ return _log_edittime; }
            set{ _log_edittime = value; }
        }        
		   
	}
}

