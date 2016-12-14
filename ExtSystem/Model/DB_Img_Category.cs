using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace NModel{
	 	//db_img_category
		public class DB_Img_Category
	{
   		     
      	/// <summary>
		/// auto_increment
        /// </summary>		
		private long _img_category_id;
        public long Img_category_ID
        {
            get{ return _img_category_id; }
            set{ _img_category_id = value; }
        }        
		/// <summary>
		/// Img_category_Name
        /// </summary>		
		private string _img_category_name;
        public string Img_category_Name
        {
            get{ return _img_category_name; }
            set{ _img_category_name = value; }
        }        
		/// <summary>
		/// Img_category_Name_en
        /// </summary>		
		private string _img_category_name_en;
        public string Img_category_Name_en
        {
            get{ return _img_category_name_en; }
            set{ _img_category_name_en = value; }
        }        
		/// <summary>
		/// Img_category_AddTime
        /// </summary>		
		private DateTime? _img_category_addtime;
        public DateTime? Img_category_AddTime
        {
            get{ return _img_category_addtime; }
            set{ _img_category_addtime = value; }
        }        
		/// <summary>
		/// Img_category_EditTime
        /// </summary>		
		private DateTime? _img_category_edittime;
        public DateTime? Img_category_EditTime
        {
            get{ return _img_category_edittime; }
            set{ _img_category_edittime = value; }
        }        
		/// <summary>
		/// Img_category_SortNo
        /// </summary>		
		private long _img_category_sortno;
        public long Img_category_SortNo
        {
            get{ return _img_category_sortno; }
            set{ _img_category_sortno = value; }
        }        
		/// <summary>
		/// Img_category_Status
        /// </summary>		
		private int? _img_category_status;
        public int? Img_category_Status
        {
            get{ return _img_category_status; }
            set{ _img_category_status = value; }
        }        
		/// <summary>
		/// Img_category_Folder
        /// </summary>		
		private string _img_category_folder;
        public string Img_category_Folder
        {
            get{ return _img_category_folder; }
            set{ _img_category_folder = value; }
        }        
		/// <summary>
		/// Img_category_Size
        /// </summary>		
		private string _img_category_size;
        public string Img_category_Size
        {
            get{ return _img_category_size; }
            set{ _img_category_size = value; }
        }        
		/// <summary>
		/// Img_category_Operate
        /// </summary>		
		private int? _img_category_operate;
        public int? Img_category_Operate
        {
            get{ return _img_category_operate; }
            set{ _img_category_operate = value; }
        }        
		   
	}
}

