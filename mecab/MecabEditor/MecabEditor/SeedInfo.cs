using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MecabEditor
{
    //Seed情報
    public class SeedInfo
    {
        //メンバ
        public Dictionary<String, Object> dicMembers = new Dictionary<String, Object>();

        //コンストラクタ
        public SeedInfo()
        {
        }

        //CSVから情報を取得
        public Boolean getInfoFromCsv(String strLine)
        {
            Boolean bResult = false;

            try
            {
                String[] strLineSplit = strLine.Split(',');
                dicMembers.Add(XmlDatas.ListItemNames["HYOSO_TYPE"], strLineSplit[0]);
                dicMembers.Add(XmlDatas.ListItemNames["LEFT_CONNECT_STATUS_NO"], int.Parse(strLineSplit[1]));
                dicMembers.Add(XmlDatas.ListItemNames["RIGHT_CONNECT_STATUS_NO"], int.Parse(strLineSplit[2]));
                dicMembers.Add(XmlDatas.ListItemNames["COST"], int.Parse(strLineSplit[3]));
                dicMembers.Add(XmlDatas.ListItemNames["HINSHI"], strLineSplit[4]);
                dicMembers.Add(XmlDatas.ListItemNames["HINSHI_DETAIL_1"], strLineSplit[5]);
                dicMembers.Add(XmlDatas.ListItemNames["HINSHI_DETAIL_2"], strLineSplit[6]);
                dicMembers.Add(XmlDatas.ListItemNames["HINSHI_DETAIL_3"], strLineSplit[7]);
                dicMembers.Add(XmlDatas.ListItemNames["KATSUYO_KEI"], strLineSplit[8]);
                dicMembers.Add(XmlDatas.ListItemNames["KATSUYO_TYPE"], strLineSplit[9]);
                dicMembers.Add(XmlDatas.ListItemNames["BASE_TYPE"], strLineSplit[10]);
                dicMembers.Add(XmlDatas.ListItemNames["YOMI"], strLineSplit[11]);
                dicMembers.Add(XmlDatas.ListItemNames["HATSUON"], strLineSplit[12]);
            }
            catch (IOException ioe)
            {
                Debug.WriteLine("SeedInfoのgetInfoFromCsvで例外が発生しました");
                Debug.WriteLine(ioe.Message);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("SeedInfoのgetInfoFromCsvで例外が発生しました");
                Debug.WriteLine(ex.Message);
            }
            finally
            {
            }

            return bResult;
        }

        //getter, setter
        public Dictionary<String, Object> DictionaryMembers
        {
            get { return dicMembers; }
            set { dicMembers = value; }
        }
    }
}
