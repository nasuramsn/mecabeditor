using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MecabEditor
{
    public static class XmlDatas
    {
        private static Dictionary<String, String> lstConsts = new Dictionary<String, String>();
        private static Dictionary<String, String> lstItemNames = new Dictionary<String, String>();
        private static Dictionary<String, String> lstNames = new Dictionary<String, String>();
        private static Dictionary<String, String> lstMessages = new Dictionary<String, String>();
        private static Dictionary<String, Color> lstColors = new Dictionary<String, Color>();
        
        static XmlDatas()
        {
            lstConsts.Clear();
            lstItemNames.Clear();
            lstNames.Clear();
            lstMessages.Clear();
            lstColors.Clear();

            //定数の定義読取り
            XmlDocument xmlConstsDoc = new XmlDocument();
            xmlConstsDoc.Load(@"./xmls/Consts.xml");

            XmlNodeList constsList = xmlConstsDoc.SelectNodes(@"//Consts");

            for (int i = 0; i < constsList.Count; i++)
            {
                foreach (XmlNode childNodeWork in constsList[i].ChildNodes)
                {
                    lstConsts.Add(childNodeWork.Name, childNodeWork.InnerText);
                }
            }

            //項目名の定義読取り
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(@"./xmls/ItemNames.xml");
            
            XmlNodeList itemNamesList = xmlDoc.SelectNodes(@"//ItemNames");

            for (int i = 0; i < itemNamesList.Count; i++)
            {
                foreach (XmlNode childNodeWork in itemNamesList[i].ChildNodes)
                {
                    lstItemNames.Add(childNodeWork.Name, childNodeWork.InnerText);
                }
            }

            //名称の定義読取り
            XmlDocument xmlNameDoc = new XmlDocument();
            xmlNameDoc.Load(@"./xmls/Names.xml");

            XmlNodeList namesList = xmlNameDoc.SelectNodes(@"//Names");

            for (int i = 0; i < namesList.Count; i++)
            {
                foreach (XmlNode childNodeWork in namesList[i].ChildNodes)
                {
                    lstNames.Add(childNodeWork.Name, childNodeWork.InnerText);
                }
            }

            //メッセージの定義読取り
            XmlDocument xmlMessageDoc = new XmlDocument();
            xmlMessageDoc.Load(@"./xmls/Message.xml");

            XmlNodeList messagesList = xmlMessageDoc.SelectNodes(@"//Messages");

            for (int i = 0; i < messagesList.Count; i++)
            {
                foreach (XmlNode childNodeWork in messagesList[i].ChildNodes)
                {
                    lstMessages.Add(childNodeWork.Name, childNodeWork.InnerText);
                }
            }

            //色の定義読取り
            XmlDocument xmlColorsDoc = new XmlDocument();
            xmlColorsDoc.Load(@"./xmls/Colors.xml");

            XmlNodeList colorsList = xmlColorsDoc.SelectNodes(@"//Colors");

            for (int i = 0; i < colorsList.Count; i++)
            {
                foreach (XmlNode childNodeWork in colorsList[i].ChildNodes)
                {
                    String[] strSplit = childNodeWork.InnerText.Split(',');
                    Color clrWork = new Color();
                    clrWork = Color.FromArgb(int.Parse(strSplit[0]), int.Parse(strSplit[1]), int.Parse(strSplit[2]));
                    lstColors.Add(childNodeWork.Name, clrWork);
                }
            }
        }

        //定義用
        public static Dictionary<String, String> ListConsts
        {
            get { return lstConsts; }
            set { lstConsts = value; }
        }

        //DB用項目名
        public static Dictionary<String, String> ListItemNames
        {
            get { return lstItemNames; }
            set { lstItemNames = value; }
        }

        //項目名
        public static Dictionary<String, String> ListNames
        {
            get { return lstNames; }
            set { lstNames = value; }
        }

        //メッセージ名
        public static Dictionary<String, String> ListMessages
        {
            get { return lstMessages; }
            set { lstMessages = value; }
        }

        //色
        public static Dictionary<String, Color> ListColors
        {
            get { return lstColors; }
            set { lstColors = value; }
        }
    }
}
