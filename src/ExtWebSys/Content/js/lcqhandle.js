(function ($) {
    $.NSysBrowser = { ie: false, firefox: false, chrome: false, opera: false, safari: false }
    $.fn.NVal = function (nval) {
        var vDict = this[0];

        for (var vDictKey in vDict) {
            if (nval[vDictKey] != null && nval[vDictKey] != '') {

                vDict[vDictKey] = nval[vDictKey];

            }
            else { vDict[vDictKey] = nval; }

        }

        return vDict;
    }


    $.NIsBrowser = function () {




        var ua = navigator.userAgent.toLowerCase();

        var s;

        (s = ua.match(/msie ([\d.]+)/)) ? $.NSysBrowser.ie = s[1] :

        (s = ua.match(/firefox\/([\d.]+)/)) ? $.NSysBrowser.firefox = s[1] :

        (s = ua.match(/chrome\/([\d.]+)/)) ? $.NSysBrowser.chrome = s[1] :

        (s = ua.match(/opera.([\d.]+)/)) ? $.NSysBrowser.opera = s[1] :

        (s = ua.match(/version\/([\d.]+).*safari/)) ? $.NSysBrowser.safari = s[1] : 0;







        return $.NSysBrowser;
    }
  $.fn.NSetHtml=    function(content){
           
               var _setObj=this;
               var _url=/url:.{2,700}/g
               var isUrl=  _url.test(content);
               
               if( isUrl){
               var getSplitURL=  content.toLowerCase().split("url:")
          
                  $(_setObj).html("<iframe width='100%^' frameborder='0' height='100%' src='"+getSplitURL[1]+"'></iframe>")
                  
              }else{
                  
                   $(_setObj).html(content)
                  
                  
              }
               
               
           }
           
    $.fn.NAutoCenter = function (that) {

        var fn_this = this;;

        $.extend($, {
            AC: function (that,fn_this) {
                var _win_w = $(that).width();
                var _win_h = $(that).height();
                var _obj_w = $(fn_this).width();
                var _obj_h = $(fn_this).height();
              
                var _obj_hw = _obj_w / 2;
                var _obj_hh = _obj_h / 2;
                var offset_left = _win_w / 2 - _obj_hw;
                var offset_top = _win_h / 2 - _obj_hh;
                $(fn_this).offset({ left: offset_left, top: offset_top });
              
            },OnResize:function(_that){}
        })
        $(that).resize(function () {

           $.AC(that,fn_this)
           $.OnResize(that);
          
        })
     
        $.AC(that, fn_this);
    
    }

    $.fn.NIsNunberChange = function (defaultvalue) {
      
        $(this).change(function (e) {
         
            if (isNaN(this.value)) {

                this.value = defaultvalue;

            }
        });



    }

   $.URLEncode= function URLencode(sStr) {
        return escape(sStr).replace(/\+/g, '%2B').replace(/\"/g, '%22').replace(/\'/g, '%27').replace(/\//g, '%2F');
   }


 
   $.GetLength = function (str) {
       ///<summary>获得字符串实际长度，中文2，英文1</summary>
       ///<param name="str">要获得长度的字符串</param>
       var realLength = 0, len = str.length, charCode = -1;
       for (var i = 0; i < len; i++) {
           charCode = str.charCodeAt(i);
           if (charCode >= 0 && charCode <= 128) realLength += 1;
           else realLength += 2;
       }
       return realLength;
   };

})(jQuery);