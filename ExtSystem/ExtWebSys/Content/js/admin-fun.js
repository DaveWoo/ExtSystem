function SetImg(id) {
    var photo = $("." + id).val();
    var pic = (photo + "").split(',');
    
    if (photo.length<=0|| pic.length <= 0) {

        $(".img_" + id).hide();
    } else {
        $(".img_" + id).show();
        $(".img_" + id).attr("src", pic[0]);
    }
   

}


function delHtmlTag(str) {
    return str.replace(/<[^>]+>/g, "");//去掉所有的html标记  
}

function SetComboxClassifyByNum(Num, _id,selectID) {

 
    var thatid = $(_id);
    $(thatid).html("<option value='0'>不限</option>");
    $.ajax({
        url: '/DBData/Data_DB_ClassifyByWhere?num=' + Num, async: false, success: function (dt) {

          
            var cls = eval('(' + dt + ')')
            var rows = cls["rows"];


            for (var i = 0; i < rows.length; i++) {

                var ID = rows[i].Classify_ID
                var Name = rows[i].Classify_Name
                if (ID == selectID) {
                    $(thatid).append("<option value='" + ID + "' selected  >" + Name + "</option>")

                } else {
                    $(thatid).append("<option value='" + ID + "' >" + Name + "</option>")
                }
            }

        }
    })
   
  
}


function SetCheckBoxsClassifyByNum(Num, _id, arrID) {


    var thatid = $(_id);
    var _nid = _id.replace(".", "").replace("#", "")
    $.ajax({
        url: '/DBData/Data_DB_ClassifyByWhere?num=' + Num, async: false, success: function (dt) {


            var cls = eval('(' + dt + ')')
            var rows = cls["rows"];


            for (var i = 0; i < rows.length; i++) {

                var ID = rows[i].Classify_ID
                var Name = rows[i].Classify_Name
                var _isSelect = false;
                for (var arr = 0; arr < arrID.length; arr++) {
                    if (arrID[arr] == ID) {
                        _isSelect = true;

                    }

                }


                if (_isSelect) {
                    $(thatid).append("<label><input  type='checkbox'  value='" + ID + "' class='class" + _nid + "'  name='" + _nid + "'  checked='checked'   />" + Name + "</label>")

                } else {
                    $(thatid).append("<label><input  type='checkbox'  value='" + ID + "'   class='class" + _nid + "'  name='" + _nid + "' >" + Name + "</label>")
                }
            }

        }
    })


}

function SetComlistClassifyByNum(Num, _id, selectID) {
    /*
     <div class="select" id="comlist_Job_Industry_ID">
                                       
                                      
                                    
    
    */
  
    var thatid = $(_id);
    var newID = _id.replace("#", "");
    var comlistID = "comlist_" + newID;
  
    var htmlstr = '<div class="select" id="comlist_'+newID+'">';
    htmlstr += '<a  value="104" id="comlist_a' + newID + '" name="' + newID + '" class="" href="javascript:">不限</a>  <ul></ul></div>';

    $(thatid).html(htmlstr);
    var ul = $("#" + comlistID).find("ul");

   
    var a = $("#" + "comlist_a" + newID);

    $("#" + comlistID).mouseenter(function () {

        $(this).find("ul").show();

    }).mouseleave(function () {

        $(this).find("ul").hide();

    })

 
    $.ajax({
        url: '/DBData/Data_DB_ClassifyByWhere?num=' + Num, async: true, success: function (dt) {


            var cls = eval('(' + dt + ')')
            var rows = cls["rows"];
            ul.width(rows.length * 0.1 * 160)
           
            //rows * 0.1 * 100
            $(ul).html("");
            for (var i = 0; i < rows.length; i++) {

                var ID = rows[i].Classify_ID
                var Name = rows[i].Classify_Name
                if (ID == selectID) {
                    $(ul).append("<li style='font-weight:bold;' value='" + ID + "'  >" + Name + "</li>")
                    a.attr("value", ID);
                    a.html(Name);

                } else {
                    $(ul).append("<li value='" + ID + "' >" + Name + "</li>")
                }



            }

            ul.children("li").width(160)
            ul.children("li").click(function () {
                var Name = $(this).text();
                var ID=$(this).attr("value");
                a.attr("value", ID);
                a.html(Name);
                ul.hide();
            })
        }
    })


}



function SetArea(ProvinceID, CityID, AreaID,dict) {





    function pca(ID,level,selectCode)
    {

        var newID = (ID + "").replace("#", "");
        var _newID = (ID + "").replace("#", "");
        var ulcombox = $("#op_combox_" + newID);
        var comlistID = "comlist_" + newID;

        var htmlstr = '<div class="select" id="comlist_' + newID + '">';
        htmlstr+='<input type="hidden" name="_'+newID+'" />';
        htmlstr += '<a  value="104" id="comlist_a' + newID + '" name="' + newID + '" class="" href="javascript:">选择</a>  <ul class="ul_'+newID+'" ></ul></div>';

        $(ID).html(htmlstr);
        var ul = $("#" + comlistID).find("ul");


        var a = $("#" + "comlist_a" + newID);
        $("#" + comlistID).mouseenter(function () {

            $(this).find("ul").show();

        }).mouseleave(function () {

            $(this).find("ul").hide();

        })


        if(level==1){

       addArea(ul,1,0);
     
        }
      
     function  addArea(ul,level,areacode){

       $(ul).append('<li  val="-1" op="-1" showid="-1">选择全部</li>')

        $.ajax({
            url: '/DBData//Data_DB_Area_Level?level='+level+"&code="+areacode, async:false, success: function (dt) {


               
                var cls = eval('(' + dt + ')')
                 var rows = cls["rows"];
                var total = cls["total"];
              var isdf=  (selectCode == null || selectCode == 0)
                if (level == 1) { 
                ul.width(rows.length * 0.1 * 100)
                ul.height(rows.length * 0.1 * 60)
                if (isdf) {
                    selectCode = "19_0"
                }
                    
                   
                } else if (level == 2) {
                    ul.width(160)
                    ul.height(30)
                    ul.hide();

                    
                    if (isdf) {
                        selectCode = "0_0_19"
                    } 
                }
                else if (level == 3) {
                    ul.width(160)
                    ul.height(30)
                    ul.hide();
                    if (isdf) { 
                        selectCode = "0_0_19_0"


                    } 
                }
                for (var i = 0; i < rows.length; i++) {
                    var Area_Code = rows[i].Area_Code;
                    var Area_Name = rows[i].Area_Name;
                    var Area_ID = rows[i].Area_ID;

                    var idx = (Area_Code + "").indexOf("_")
                    var lstr = (Area_Code + "").substring(0, idx);
                    var nstr = lstr;
                    var len = $.GetLength(Area_Code);


                    var func="";
                    if (level == 1) {

                     

                    }
                    else if (level == 2) {
                        var lindex = (Area_Code + "").lastIndexOf("_")
                        lstr = (Area_Code + "").substring(lindex+1, len);
                        nstr = (Area_Code + "").substring(idx + 1,len) + "_" + (Area_Code + "").substring(0, idx);


                        func='addArea('+(level+1)+',"'+nstr+'")';
                    }
                    else if (level == 3) {

                        lstr = (Area_Code + "").substring(idx + 1, len);
                       
                        nstr = "";
                       
                       
                    }

                    if (Area_Code == selectCode) {


                     
                        a.attr("value", Area_Code);
                        a.html(Area_Name);

                        $(ul).append("<li class='" + newID + lstr + "  " + newID + "  c"+Area_ID+"  '  op='" + nstr + "'       showID='" + lstr + "'    val='" + Area_Code + "'   >" + Area_Name + "</li>")
                       // a.attr("value", Area_Code);
                        
                    
                       
                    } else {
                        $(ul).append("<li class='" + newID + lstr + "   " + newID + "  c"+Area_ID+" '   op='" + nstr + "'     showID='" + lstr + "'   val='" + Area_Code + "' >" + Area_Name + "</li>")
                    }




                }

                 if(level==2){

                  funB(total);
                       }
                  if(level==3){
                  funC(total);
             }
                

              
            }
        });


        }

           var newp = ProvinceID.replace("#", "");
            var newc = CityID.replace("#", "");

            var newa = AreaID.replace("#", "");

          function funB(total) {
                var ccomlistID = "comlist_" + newc;


                 var cul = $("#" + ccomlistID).find("ul");

                 var ap = $("#" + "comlist_a" + newc);
                 var cinput=$("#"+ccomlistID).find("input[name='_"+newc+"']");
                 var ulnewc = $(".ul_" + newc);
                 var liLen= ulnewc.children("li");

                 
               
                    var lcount = liLen.length < 10 ? 10 : liLen.length;


                    ulnewc.width(lcount * 0.2 * 100)
                    ulnewc.height(lcount * 0.1 * 100)
                    cul.children("li").click(function () {

                   
                    var Name = $(this).text();

                    var ID = $(this).attr("val");
                    ap.attr("value", ID);
                    ap.html(Name);
                    cul.hide();
                    $(cinput).attr("value",ID);
                    


                    var op = $(this).attr("op");

                    var _op=  parseInt(op)

                    if(_op==-1)return;
                    var ulnewa= $(".ul_" + newa);

                      ulnewa.html("");
                     addArea(ulnewa,3,op)
                 
                        
                      
                    


                 
                })
          }

        
          function funC(total) {
                var ccomlistID = "comlist_" + newa;

                var cinput=$("#"+ccomlistID).find("input[name='_"+newa+"']");
             


                var cul = $("#" + ccomlistID).children("ul");

                var ap = $("#" + "comlist_a" + newa);

                   var ulnewa = $(".ul_" + newa);
                  var liLen= ulnewa.children("li");

                 
               
                    var lcount = liLen.length < 10 ? 10 : liLen.length;


                    ulnewa.width(lcount * 0.2 * 100)
                    ulnewa.height(lcount * 0.1 * 100)
                    cul.children("li").click(function () {


                    var Name = $(this).text();

                    var ID = $(this).attr("val");
                    ap.attr("value", ID);
                    ap.html(Name);
                    cul.hide();
                    $(cinput).attr("value",ID);
                     


                



                })
          }
        $(function () {


          
       
          function funA()  {
            var ccomlistID = "comlist_" + newp;

           
            var cul = $("#" + ccomlistID).children("ul");

            var ap = $("#" + "comlist_a" + newp);
            var cinput=$("#"+ccomlistID).find("input[name='_"+newp+"']");
            cul.children("li").click(function () {

               
                var Name = $(this).text();
              
                var _ID = $(this).attr("val");
                ap.attr("value", _ID);
               
                ap.html(Name);
                cul.hide();

                $(cinput).attr("value",_ID)
              

                var op = $(this).attr("op");

             
                   var _op=  parseInt(op)

                    if(_op==-1)return;
              

           

                var ulnewc = $(".ul_" + newc);

                 ulnewc.html("");
                addArea(ulnewc,2,op)

              
               
                
            })
            }


        


          if(level==1){
     
          funA();
          }
         

        })

    }
   

   new pca(ProvinceID,1, dict.ProvinceCode);
   new pca(CityID,2, dict.CityCode);
   new pca(AreaID,3, dict.AreaCode);

}





function SetComlistClassifyByNum(Num, _id, selectID) {
    /*
     <div class="select" id="comlist_Job_Industry_ID">
                                       
                                      
                                    
    
    */

    var thatid = $(_id);
    var newID = _id.replace("#", "");
    var comlistID = "comlist_" + newID;

    var htmlstr = '<div class="select" id="comlist_' + newID + '">';
    htmlstr += '<a  value="104" id="comlist_a' + newID + '" name="' + newID + '" class="" href="javascript:">不限</a>  <ul class="cul_' + newID + '"></ul></div>';

    $(thatid).html(htmlstr);
    var ul = $("#" + comlistID).find("ul");


    var a = $("#" + "comlist_a" + newID);

    $("#" + comlistID).mouseenter(function () {

        $(this).find("ul").show();

    }).mouseleave(function () {

        $(this).find("ul").hide();

    })


    $.ajax({
        url: '/DBData/Data_DB_ClassifyByWhere?num=' + Num, async: false, success: function (dt) {


            var cls = eval('(' + dt + ')')
            var rows = cls["rows"];
            ul.width(rows.length * 0.1 * 160)

            //rows * 0.1 * 100
            $(ul).html("");
            for (var i = 0; i < rows.length; i++) {

                var ID = rows[i].Classify_ID
                var Name = rows[i].Classify_Name
                if (ID == selectID) {
                    $(ul).append("<li style='font-weight:bold;' value='" + ID + "'  >" + Name + "</li>")
                    a.attr("value", ID);
                    a.html(Name);

                } else {
                    $(ul).append("<li value='" + ID + "' >" + Name + "</li>")
                }



            }

            ul.children("li").width(160)
            ul.children("li").click(function () {
                var Name = $(this).text();
                var ID = $(this).attr("value");
                a.attr("value", ID);
                a.html(Name);
                ul.hide();
            })
        }
    })


}



function SetJobcfn(AID, BID, CID, dict) {


    //$(AID).html("<option value='0'>所</option>")


    function pca(ID, level, selectCode) {

        var newID = (ID + "").replace("#", "");
        var _newID = (ID + "").replace("#", "");
        var ulcombox = $("#op_combox_" + newID);
        var comlistID = "comlist_" + newID;

        var htmlstr = '<div class="select" id="comlist_' + newID + '">';
        htmlstr += '<a  value="104" id="comlist_a' + newID + '" name="' + newID + '" class="" href="javascript:">选择分类</a>  <ul class="ul_' + newID + '" ></ul></div>';

        $(ID).html(htmlstr);
        var ul = $("#" + comlistID).find("ul");


        var a = $("#" + "comlist_a" + newID);
        $("#" + comlistID).mouseenter(function () {

            $(this).find("ul").show();

        }).mouseleave(function () {

            $(this).find("ul").hide();

        })



        $.ajax({
            url: '/DBData/Data_DB_JobcfnByLevel?level=' + level, async: false, success: function (dt) {



                var cls = eval('(' + dt + ')')
                var rows = cls["rows"];
                var isdf = (selectCode == null || selectCode == 0)
                if (level == 1) {
                    ul.width(rows.length * 0.4 * 140)
                    ul.height(rows.length * 0.2 * 60)
                 
         

                } else if (level == 2) {
                    ul.width(160)
                    ul.height(30)
                    ul.hide();
                  

                }
                else if (level == 3) {
                    ul.width(160)
                    ul.height(30)
                    ul.hide();

                
                  
                }
                for (var i = 0; i < rows.length; i++) {
                  
                    var Jobcfn_X = rows[i].Jobcfn_X;
                    var Jobcfn_Y = rows[i].Jobcfn_Y;

                    var Jobcfn_Z = rows[i].Jobcfn_Z;
                    var Jobcfn_Name = rows[i].Jobcfn_Name;
                    var Jobcfn_ID = rows[i].Jobcfn_ID
                    var x = Jobcfn_X;
                    var y = Jobcfn_Y
                    var z =parseInt(Jobcfn_Z);
                     
                    var nstr = "", lstr="";
                    if (level == 1)
                    {
                        nstr=  x + "_" + (z+1);
                    }
                    else if (level == 2) {
                        lstr = x + "_" + (z);
                        nstr = x + "" + y + "_" + (z + 1);
                    }
                    else if (level == 3) {

                        lstr = x + "_" + (z);
                        nstr = "0";
                       // nstr = x + "" + y + "_" + (z + 1);
                    }
                   

                    if (Jobcfn_ID == selectCode) {



                        a.attr("value", Jobcfn_ID);
                        a.html(Jobcfn_Name);

                        $(ul).append("<li class='" + newID + lstr + "  " + newID + "'  showID='" + lstr + "'    op='" + nstr + "'   val='" + Jobcfn_ID + "'  >" + Jobcfn_Name + "</li>")
                        // a.attr("value", Area_Code);


                    } else {
                        $(ul).append("<li class='" + newID + lstr + "    " + newID + " '     showID='" + lstr + "'     op='" + nstr + "'  val='" + Jobcfn_ID + "' >" + Jobcfn_Name + "</li>")
                    }




                }





            }
        });


        $(function () {


            var newp = AID.replace("#", "");
            var newc = BID.replace("#", "");

            var newa = CID.replace("#", "");

            function funA() {
                var ccomlistID = "comlist_" + newp;


                var cul = $("#" + ccomlistID).children("ul");

                var ap = $("#" + "comlist_a" + newp);


                cul.children("li").click(function () {


                    var Name = $(this).text();

                    var _ID = $(this).attr("val");
                    ap.attr("value", _ID);

                    ap.html(Name);
                    cul.hide();




                    var op = $(this).attr("op");



                    $("." + newc).hide();
                    var removeOjb = $("." + newc + op);


                    var ulnewc = $(".ul_" + newc);
                    var lcount = removeOjb.length < 10 ? 10 : removeOjb.length;

                    ulnewc.width(lcount * 0.2 * 100)
                    ulnewc.height(lcount * 0.2 * 100)

                    removeOjb.show();
                })
            }


            function funB() {
                var ccomlistID = "comlist_" + newc;


                var cul = $("#" + ccomlistID).find("ul");

                var ap = $("#" + "comlist_a" + newc);


                cul.children("li").click(function () {


                    var Name = $(this).text();

                    var ID = $(this).attr("val");
                    ap.attr("value", ID);
                    ap.html(Name);
                    cul.hide();




                    var op = $(this).attr("op");



                    $("." + newa).hide();

                    var removeOjb = $("." + newa + op);
                    var ulnewc = $(".ul_" + newa);
                    var comlist = $("#" + newa);
                    //_Job_Area_Code

                    if (removeOjb.length <= 0) {

                        comlist.hide();
                    } else {

                        comlist.show();
                    }
                    var lcount = removeOjb.length < 10 ? 10 : removeOjb.length;


                    ulnewc.width(lcount * 0.2 * 100)
                    ulnewc.height(lcount * 0.2 * 100)


                    removeOjb.show();
                })
            }


            function funC() {
                var ccomlistID = "comlist_" + newa;


                var cul = $("#" + ccomlistID).children("ul");

                var ap = $("#" + "comlist_a" + newa);


                cul.children("li").click(function () {


                    var Name = $(this).text();

                    var ID = $(this).attr("val");
                    ap.attr("value", ID);
                    ap.html(Name);
                    cul.hide();








                })
            }


            funA();
            funB();
            funC();

        })

    }


    pca(AID, 1, dict.A);
    pca(BID, 2, dict.B);
    pca(CID, 3, dict.C);

}


$.fn.BoxList = function () {


    var that = this;

   
   
    var _ulfind = $(that).find("ul")
    for (var i = 1; i < _ulfind.length; i++) {
        _ulfind.eq(i).width(0);
        _ulfind.eq(i).height(0);
       
    }
  
    $(this).mouseenter(function () {

        var _ulObj = $(this).find("ul");
        var _ulliObj = _ulObj.children("li");

       
        var _wUl = _ulliObj.length <= 10 ? 180 : _ulliObj.length * 0.1 * 140;

        var _indexThat = $(that).index(this);
        var _divThat = $($(that).get(_indexThat));
       
        var _thatUl = $($(that).find("ul").get(_indexThat))


        $(_thatUl).children("li").unbind().click(function () {


            var text = $(this).text();
            var val = $(this).attr("val");
            if (val == null) {
                val = $(this).attr("value")
            }
               
            var op = $(this).attr("op")

            var _indexAppend = (_indexThat + 1);
            if (_indexAppend <= that.length) {

                var _thatAppend = $($(that).find("ul").get(_indexAppend))
                $(_thatAppend).children("li").hide();
                $(_thatAppend).children("li[showid='-1']").show();
                var _liShow = $(_thatAppend).children("li[showid='" + op + "']")

               
              _liShow.show();
              var _wwUl = _liShow.length <= 10 ? 140 :  Math.ceil(_liShow.length * 0.1) * 280;

              var _hhUl = 8 * 28;
              _thatAppend.width(_wwUl);
              _thatAppend.height(_hhUl);
            }
          
            var _aThat = $("#comlist_a" + _divThat.attr("id"))

            var _hiddenObj = that.find("input[name='_" + _divThat.attr("id") + "']")
            
            _hiddenObj.val(val);
            _aThat.attr("value", val);

            _aThat.html(text)

        })
       
     
        _ulObj.show();
        $(_ulfind[0]).width(_wUl);

    }).mouseleave(function () {

        $(this).find("ul").hide();

    })

}