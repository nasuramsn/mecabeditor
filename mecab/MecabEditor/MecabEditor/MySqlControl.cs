using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System.Data;

namespace MecabEditor
{
    //MySQLを管理するクラス
    public class MySqlControl
    {
        //メンバ
        private MySqlConnection conn;
        private String strUser = String.Empty;
        private String strPassword = String.Empty;
        private String strDatabase = String.Empty;
        private String strHost = String.Empty;

        //コンストラクタ
        public MySqlControl()
        {
        }

        //接続
        public Boolean connect(String user, String password, String database, String host)
        {
            Boolean bResult = false;
            String strConnect = String.Empty;

            try
            {
                strConnect = "userid="+user+"; password="+password+"; database="+database+"; Host="+host;
                conn = new MySqlConnection(strConnect);
                conn.Open();
                bResult = true;
            }
            catch (MySqlException mex)
            {
                MessageBox.Show(mex.Message);
            }
            
            return bResult;
        }

        //切断
        public Boolean disconnect()
        {
            Boolean bResult = false;

            try
            {
                conn.Close();
                bResult = true;
            }
            catch (MySqlException mex)
            {
                MessageBox.Show(mex.Message);
            }

            return bResult;
        }

        // insert
        // inset文を実行します
        public Boolean insert(String sqlSentence)
        {
            MySqlCommand command = new MySqlCommand();
            DataTable dt = new DataTable();
            List<Type> returnList = new List<Type>();

            try
            {
                this.connect(XmlDatas.ListConsts["MYSQL_USER_ID"].ToString(), XmlDatas.ListConsts["MYSQL_PASSWORD"].ToString(),
                    XmlDatas.ListConsts["MYSQL_DATABASE"].ToString(), XmlDatas.ListConsts["MYSQL_HOST"].ToString());

                // SELECT文を設定します。
                command.CommandText = sqlSentence;
                command.Connection = this.conn;

                // SQLを実行します。
                command.ExecuteNonQuery();

                this.disconnect();
            }
            catch (MySqlException me)
            {
                MessageBox.Show(me.Message);
                return false;
            }

            return true;
        }

        // selectForList
        // select文の結果をListで返す
        public DataTable selectForList(String sqlSentence)
        {

            MySqlCommand command = new MySqlCommand();
            DataTable dt = new DataTable();
            List<Type> returnList = new List<Type>();

            try
            {
                this.connect(XmlDatas.ListConsts["MYSQL_USER_ID"].ToString(), XmlDatas.ListConsts["MYSQL_PASSWORD"].ToString(),
                    XmlDatas.ListConsts["MYSQL_DATABASE"].ToString(), XmlDatas.ListConsts["MYSQL_HOST"].ToString());

                // SELECT文を設定します。
                command.CommandText = sqlSentence;
                command.Connection = this.conn;

                // SQLを実行します。
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                adapter.Fill(dt);

                this.disconnect();
            }
            catch (MySqlException me)
            {
                MessageBox.Show(me.Message);
                return null;
            }

            return dt;
        }

        //getter, setter
        public MySqlConnection mysqlConn
        {
            get { return conn; }
            set { conn = value; }
        }
    }
}
