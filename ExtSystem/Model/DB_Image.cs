using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace NModel{
	 	//db_image
		public class DB_Image:DB_Img_Category
	{
   		     
      	/// <summary>
		/// auto_increment
        /// </summary>		
		private long _image_id;
        public long Image_ID
        {
            get{ return _image_id; }
            set{ _image_id = value; }
        }        
		/// <summary>
		/// Image_Url
        /// </summary>		
		private string _image_url;
        public string Image_Url
        {
            get{ return _image_url; }
            set{ _image_url = value; }
        }        
		/// <summary>
		/// Image_Name
        /// </summary>		
		private string _image_name;
        public string Image_Name
        {
            get{ return _image_name; }
            set{ _image_name = value; }
        }        
		/// <summary>
		/// Image_Size
        /// </summary>		
		private string _image_size;
        public string Image_Size
        {
            get{ return _image_size; }
            set{ _image_size = value; }
        }        
		/// <summary>
		/// Image_Type
        /// </summary>		
		private string _image_type;
        public string Image_Type
        {
            get{ return _image_type; }
            set{ _image_type = value; }
        }        
		/// <summary>
		/// Image_Belongs_ID
        /// </summary>		
		private long _image_belongs_id;
        public long Image_Belongs_ID
        {
            get{ return _image_belongs_id; }
            set{ _image_belongs_id = value; }
        }        
		/// <summary>
		/// Image_AddTime
        /// </summary>		
		private DateTime _image_addtime;
        public DateTime Image_AddTime
        {
            get{ return _image_addtime; }
            set{ _image_addtime = value; }
        }        
		/// <summary>
		/// Image_EditTime
        /// </summary>		
		private DateTime _image_edittime;
        public DateTime Image_EditTime
        {
            get{ return _image_edittime; }
            set{ _image_edittime = value; }
        }        
		/// <summary>
		/// Image_Status
        /// </summary>		
		private int _image_status;
        public int Image_Status
        {
            get{ return _image_status; }
            set{ _image_status = value; }
        }        
		/// <summary>
		/// Image_Operate
        /// </summary>		
		private int _image_operate;
        public int Image_Operate
        {
            get{ return _image_operate; }
            set{ _image_operate = value; }
        }        
		/// <summary>
		/// Image_SortNo
        /// </summary>		
		private long _image_sortno;
        public long Image_SortNo
        {
            get{ return _image_sortno; }
            set{ _image_sortno = value; }
        }        
		/// <summary>
		/// Image_Category_ID
        /// </summary>		
		private long _image_category_id;
        public long Image_Category_ID
        {
            get{ return _image_category_id; }
            set{ _image_category_id = value; }
        }        
		   
	}
}

