using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MecabEditor
{
    //システムが使用する情報
    public class SystemInfo
    {
        //メンバ
        public Dictionary<String, Object> lstMembers = new Dictionary<String, Object>();

        //コンストラクタ
        public SystemInfo()
        {
            if (!getInfosFromFile())
                return;
        }

        //ファイルから情報を取得する
        private Boolean getInfosFromFile()
        {
            Boolean bResult = false;
            StreamReader strrdr = null;

            try
            {
                lstMembers.Clear();

                String strFilePath = XmlDatas.ListConsts["SETTING_INI_FILE_PATH"];
                if (!System.IO.File.Exists(@strFilePath))
                    return bResult;

                strrdr = new StreamReader(@strFilePath, Encoding.GetEncoding("utf-8"));

                while (!strrdr.EndOfStream)
                {
                    String strLine = strrdr.ReadLine();
                    if (strLine.IndexOf(XmlDatas.ListConsts["SEED_DIRECTORY_PATH_HEADER"]) > 0)
                    {
                        strLine = strLine.Substring(XmlDatas.ListConsts["SEED_DIRECTORY_PATH_HEADER"].Length - 1, strLine.Length - XmlDatas.ListConsts["SEED_DIRECTORY_PATH_HEADER"].Length);
                        lstMembers.Add(XmlDatas.ListConsts["SEED_DIRECTORY_PATH"], strLine);
                    }
                }

                bResult = true;
            }
            catch (IOException ioe)
            {
                Debug.WriteLine(ioe.Message);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            finally
            {
                if (strrdr != null)
                {
                    strrdr.Close();
                    strrdr.Dispose();
                }
            }

            return bResult;
        }

        //getter, setter
        public Dictionary<String, Object> ListMembers
        {
            get { return lstMembers; }
            set { lstMembers = value; }
        }
    }
}
