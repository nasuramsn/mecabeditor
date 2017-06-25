using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MecabEditor
{
    //CSV関連の情報
    public class CsvInfos
    {
        //メンバ
        private String strFileName = String.Empty;              //CSVファイル名
        private String strKey = String.Empty;                   //CSVリスト上のキー
        private List<String> lstHinshi = new List<String>();    //振り分け条件となる品詞情報


        //コンストラクタ
        public CsvInfos()
        {
        }


        //getter, setter

        //CSVファイル名
        public String FileName
        {
            get { return strFileName; }
            set { strFileName = value; }
        }

        //CSVリスト上のキー
        public String Key
        {
            get { return strKey; }
            set { strKey = value; }
        }

        //振り分け条件となる品詞情報
        public List<String> ListHinshi
        {
            get { return lstHinshi; }
            set { lstHinshi = value; }
        }
    }
}
