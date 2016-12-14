using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tool
{
 public   class NJson<T>
    {


        public delegate string SetDelegateResult(List<T> _menu, T t, int? rows, int layer, int Level);

        public delegate dynamic SetProcessResult(T newValue, T oldValue, List<T> _menu, int layer, int Level);
        private T _oldValue;
        public static int breakLevelNum = 500;
        /// <summary>
        /// 下级
        /// </summary>
        public int Level = 0;
        /// <summary>
        /// 顶层
        /// </summary>
        private int Layer = 0;

        public Dictionary<int, int> dictLayerLevel = new Dictionary<int, int>();
        public string JsonNoLevel(SetDelegateResult setMothod, SetProcessResult SetP, List<T> _menu)
        {


            StringBuilder sbStr = new StringBuilder();


            sbStr.Append("[");

            if (NTool.IsLtNULL<T>(_menu))
            {





                List<T> _listFirst = NTool.SelectListData<T>(_menu, (Predicate<T>)SetP(default(T), default(T), _menu, -1, -1));

                if (NTool.IsLtNULL<T>(_listFirst))
                {



                    int l1 = 0;

                    foreach (T _chlidModel in _listFirst)
                    {


                        //一级

                        l1++;
                        if (l1 == 1)
                        {
                            sbStr.Append("{");
                        }
                        else
                        {
                            sbStr.Append(",{");
                        }
                        // Level = l1;


                        Level = 2;

                        Layer++;

                        sbStr.Append(JsonNoLevel(_chlidModel, setMothod, SetP, _menu, -1, Layer, Level));





                        sbStr.Append("} ");

                    }


                }

            }

            sbStr.Append(" ] ");


            return sbStr.ToString();
        }




        public string JsonNoLevel(T _chlidModel, SetDelegateResult setMothod, SetProcessResult SetP, List<T> _menu, int? Row, int Layer, int Level)
        {


            StringBuilder sbStr = new StringBuilder();

            List<T> __chlidList = NTool.SelectListData<T>
            (_menu, (Predicate<T>)SetP(_chlidModel, _oldValue, _menu, Layer, Level));
            sbStr.Append(setMothod(_menu, _chlidModel, __chlidList != null ? __chlidList.Count : 0, Layer, Level));


            if (NTool.IsLtNULL<T>(_menu))
            {

                {



                    if (NTool.IsLtNULL<T>(__chlidList))
                    {
                        Level++;
                        int CwGo = 0;

                        //二级
                        sbStr.Append(",\"children\":[  ");

                        foreach (T chlidModel in __chlidList)
                        {


                            CwGo++;
                            if (CwGo == 1)
                            {
                                sbStr.Append("{");
                            }
                            else
                            {
                                sbStr.Append(",{");
                            }



                            _oldValue = chlidModel;


                            string lastStr = JsonNoLevel(chlidModel, setMothod, SetP, _menu, -1, Layer, Level);


                            sbStr.Append(lastStr);
                            sbStr.Append("} ");




                        }



                        sbStr.Append("] ");


                    }


                }


            }

            return (sbStr + "");


        }
    }
}
