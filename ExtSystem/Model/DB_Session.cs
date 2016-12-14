using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace NModel{
	 	//db_session
		public class DB_Session
	{
   		     
      	/// <summary>
		/// auto_increment
        /// </summary>		
		private long? _session_id;
        public long? Session_ID
        {
            get{ return _session_id; }
            set{ _session_id = value; }
        }        
		/// <summary>
		/// Session_CID
        /// </summary>		
		private string _session_cid;
        public string Session_CID
        {
            get{ return _session_cid; }
            set{ _session_cid = value; }
        }        
		/// <summary>
		/// Session_UserID
        /// </summary>		
		private long? _session_userid;
        public long? Session_UserID
        {
            get{ return _session_userid; }
            set{ _session_userid = value; }
        }        
		/// <summary>
		/// Session_AddTime
        /// </summary>		
		private DateTime? _session_addtime;
        public DateTime? Session_AddTime
        {
            get{ return _session_addtime; }
            set{ _session_addtime = value; }
        }        
		/// <summary>
		/// Session_Status
        /// </summary>		
		private int? _session_status;
        public int? Session_Status
        {
            get{ return _session_status; }
            set{ _session_status = value; }
        }        
		/// <summary>
		/// Session_DB_Name
        /// </summary>		
		private string _session_db_name;
        public string Session_DB_Name
        {
            get{ return _session_db_name; }
            set{ _session_db_name = value; }
        }        
		/// <summary>
		/// Session_EndTime
        /// </summary>		
		private DateTime? _session_endtime;
        public DateTime? Session_EndTime
        {
            get{ return _session_endtime; }
            set{ _session_endtime = value; }
        }        
		   
	}
}

