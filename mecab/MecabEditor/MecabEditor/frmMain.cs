﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MecabEditor
{
    public partial class frmMain : Form
    {
        private List<MSeed> lstMSeed = new List<MSeed>();
        private List<MSeed> lstAllMSeed = new List<MSeed>();
        private List<MSeed> lstMyMSeed = new List<MSeed>();

        //品詞情報など
        private List<MHinshi> lstHinshi = new List<MHinshi>();
        private List<MHinshi> lstHinshiSub1 = new List<MHinshi>();
        private List<MHinshi> lstHinshiSub2 = new List<MHinshi>();
        private List<MHinshi> lstHinshiSub3 = new List<MHinshi>();
        private List<MKatsuyo> lstKatsuyo1 = new List<MKatsuyo>();
        private List<MKatsuyo> lstKatsuyo2 = new List<MKatsuyo>();

        private List<MorphemeReplaceInfo> lstMorphemeReplaceInfo = new List<MorphemeReplaceInfo>();
        private List<NoTranseInfo> lstNoTanseSeed = new List<NoTranseInfo>();     //以後変換しない情報

        private SystemInfo systemInfo = new SystemInfo();

        private String strCoupusFilePath = String.Empty;
        private String strCsvFilePath = String.Empty;
        private Boolean bFileReaded = false;

        private MySqlControl mySqlControl = new MySqlControl();

        
        public frmMain()
        {
            InitializeComponent();
        }

        //フォームを閉じる
        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        //ファイルからコーパスのリストを作成する
        private Boolean createCopusListFromFile(String strFileName)
        {
            StreamReader strReader = null;
            Boolean bResult = false;

            int intEditMode = cmbEditMode.SelectedIndex;

            lstMSeed.Clear();

            try
            {
                strReader = new StreamReader(@strFileName);

                if (intEditMode == 1)
                {
                    if (!createCopusTotalDataList(strReader))
                    {
                        MessageBox.Show("コーパスの集計リストの作成に失敗しました");
                        return bResult;
                    }
                }
                else if (intEditMode == 0)
                {
                    if (!createCopusDataList(strReader))
                    {
                        MessageBox.Show("コーパスのリストの作成に失敗しました");
                        return bResult;
                    }
                }

                lstMSeed.ForEach(delegate(MSeed mSeedWork) 
                {
                    //品詞リストにないものがあれば追加しておく
                    int indexHinshi = MyUtils.checkHinshiList(lstHinshi, 0, -1, mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["HINSHI"]].ToString(), false);
                    if (indexHinshi < 0)
                        indexHinshi = 0;
                    
                    //品詞詳細1リストにないものがあれば追加する
                    int indexHinshiSub1 = MyUtils.checkHinshiList(lstHinshiSub1, 1, lstHinshi[indexHinshi].Id, mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["HINSHI_DETAIL_1"]].ToString(), false);
                    if (indexHinshiSub1 < 0)
                        indexHinshiSub1 = 0;

                    //品詞詳細2リストにないものがあれば追加する
                    int indexHinshiSub2 = MyUtils.checkHinshiList(lstHinshiSub2, 2, lstHinshiSub1[indexHinshiSub1].Id, mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["HINSHI_DETAIL_2"]].ToString(), false);
                    if (indexHinshiSub2 < 0)
                        indexHinshiSub2 = 0;

                    //品詞詳細3リストにないものがあれば追加する
                    int indexHinshiSub3 = MyUtils.checkHinshiList(lstHinshiSub3, 3, lstHinshiSub2[indexHinshiSub2].Id, mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["HINSHI_DETAIL_3"]].ToString(), false);
                    if (indexHinshiSub3 < 0)
                        indexHinshiSub3 = 0;

                    //活用形リストにないものがあれば追加しておく
                    int indexKatsuyo1 = MyUtils.checkKatsuyoList(lstKatsuyo1, 1, lstHinshi[indexHinshi].Id, mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["KATSUYO_KEI"]].ToString());
                    if (indexKatsuyo1 < 0)
                        indexKatsuyo1 = 0;

                    //活用型リストにないものがあれば追加しておく
                    int indexKatsuyo2 = MyUtils.checkKatsuyoList(lstKatsuyo2, 2, lstKatsuyo1[indexKatsuyo1].Id, mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["KATSUYO_TYPE"]].ToString());
                    if (indexKatsuyo2 < 0)
                        indexKatsuyo2 = 0;
                });
            
                if (!setMainListView(intEditMode))
                    return bResult;

                bResult = true;
            }
            catch (IOException ioe)
            {
                MessageBox.Show(ioe.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (strReader != null)
                {
                    strReader.Close();
                    strReader.Dispose();
                }
            }

            return bResult;
        }

        //コーパスの集計データリストの作成
        private Boolean createCopusTotalDataList(StreamReader strReaderWork)
        {
            Boolean bResult = false;

            while (!strReaderWork.EndOfStream)
            {
                String strLine = strReaderWork.ReadLine();
                String[] strLineSplitTab = strLine.Split('\t');

                if (strLineSplitTab.Length > 1)
                {
                    String[] strLineSplitComma = strLineSplitTab[1].Split(',');

                    //既存のリストにないか検索する
                    Boolean bIsExist = false;
                    lstMSeed.ForEach(delegate(MSeed mSeedSearch)
                    {
                        if (mSeedSearch.DictionaryMembers[XmlDatas.ListItemNames["HYOSO_TYPE"]].ToString().Equals(strLineSplitTab[0]) && mSeedSearch.DictionaryMembers[XmlDatas.ListItemNames["HINSHI"]].ToString().Equals(strLineSplitComma[0]))
                        {
                            bIsExist = true;
                            int intCountWork = int.Parse(mSeedSearch.DictionaryMembers[XmlDatas.ListItemNames["DATA_COUNT"]].ToString());
                            intCountWork++;
                            mSeedSearch.DictionaryMembers[XmlDatas.ListItemNames["DATA_COUNT"]] = intCountWork;
                        }
                    });
                    
                    if (!bIsExist)
                    {
                        MSeed mSeedWork = new MSeed();
                        mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["SEED_NO"]] = 0;
                        mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["HYOSO_TYPE"]] = strLineSplitTab[0];
                        mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["LEFT_CONNECT_STATUS_NO"]] = 1000;
                        mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["RIGHT_CONNECT_STATUS_NO"]] = 1000;
                        mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["COST"]] = 100;
                        mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["HINSHI"]] = strLineSplitComma[0];
                        mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["HINSHI_DETAIL_1"]] = strLineSplitComma[1];
                        mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["HINSHI_DETAIL_2"]] = strLineSplitComma[2];
                        mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["HINSHI_DETAIL_3"]] = strLineSplitComma[3];
                        mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["KATSUYO_KEI"]] = strLineSplitComma[4];
                        mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["KATSUYO_TYPE"]] = strLineSplitComma[5];
                        mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["BASE_TYPE"]] = strLineSplitComma[6];
                        mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["YOMI"]] = String.Empty;
                        mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["HATSUON"]] = String.Empty;
                        mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["CREATED_AT"]] = DateTime.Now;
                        mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["UPDATED_AT"]] = DateTime.Now;
                        mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["DELETE_FLAG"]] = 0;
                        mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["DATA_COUNT"]] = 1;

                        if (strLineSplitComma.Length > 8)
                        {
                            mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["YOMI"]] = strLineSplitComma[7];
                            mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["HATSUON"]] = strLineSplitComma[8];
                        }

                        lstMSeed.Add(mSeedWork);
                    }
                }
            }

            bResult = true;

            return bResult;
        }

        //コーパスのデータリスト作成
        private Boolean createCopusDataList(StreamReader strReaderWork)
        {
            Boolean bResult = false;

            while (!strReaderWork.EndOfStream)
            {
                String strLine = strReaderWork.ReadLine();
                MSeed mSeedWork = new MSeed();
                
                if (mSeedWork.getFromCsvFile(@strLine))
                    lstMSeed.Add(mSeedWork);
            }

            bResult = true;

            return bResult;
        }

        //Form表示後の処理
        private void frmMain_Shown(object sender, EventArgs e)
        {
            cmbEditMode.Items.Clear();
            cmbEditMode.Items.Add(XmlDatas.ListNames["EDIT"]);
            cmbEditMode.Items.Add(XmlDatas.ListNames["TOTAL"]);
            cmbEditMode.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEditMode.SelectedIndex = 0;
            cmbEditMode.Enabled = false;

            btnAdd.Enabled = false;
            btnEdit.Enabled = false;
            btnDel.Enabled = false;
            btnSave.Enabled = false;
            上書き保存SToolStripMenuItem.Enabled = false;
            検索FToolStripMenuItem.Enabled = false;

            if (!readSystemInfos())
                return;

            if (systemInfo.ListMembers.Count == 0)
            {
                frmSetSystemInfo frmSetSysInfo = new frmSetSystemInfo();
                frmSetSysInfo.mySystemInfo = systemInfo;
                frmSetSysInfo.ShowDialog();
            }

            if (!setHinshiInfos())
            {
                Close();
                return;
            }

            //学習用CSVファイルを開く
            if (!ReadLearnCsvFiles())
                return;

            //形態要素置換情報を読み込む
            if (!MyUtils.getMorphemeReplaceInfoFromFile(@XmlDatas.ListConsts["MORPHEME_INFO_FILE_PATH"].ToString(), ref lstMorphemeReplaceInfo))
                return;
        }

        //システム情報の読み取り
        private Boolean readSystemInfos()
        {
            Boolean bResult = false;
            String strFileName = XmlDatas.ListConsts["SETTING_INI_FILE_PATH"].ToString();
            StreamReader strrdr = null;

            try
            {
                strrdr = new StreamReader(@strFileName, Encoding.GetEncoding("utf-8"));

                while (!strrdr.EndOfStream)
                {
                    String strLine = strrdr.ReadLine();
                    if (strLine.IndexOf(XmlDatas.ListConsts["SEED_DIRECTORY_PATH_HEADER"].ToString()) >= 0)
                    {
                        strLine = strLine.Replace(XmlDatas.ListConsts["SEED_DIRECTORY_PATH_HEADER"].ToString(), String.Empty);
                        systemInfo.ListMembers[XmlDatas.ListConsts["SEED_DIRECTORY_PATH"]] = strLine;
                    }
                    else if (strLine.IndexOf(XmlDatas.ListConsts["CSV_DIRECTORY_PATH_HEADER"].ToString()) >= 0)
                    {
                        strLine = strLine.Replace(XmlDatas.ListConsts["CSV_DIRECTORY_PATH_HEADER"].ToString(), String.Empty);
                        systemInfo.ListMembers[XmlDatas.ListConsts["CSV_DIRECTORY_PATH"]] = strLine;
                    }
                }

                bResult = true;
            }
            catch (IOException ioe)
            {
                MessageBox.Show(ioe.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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

        //品詞情報などの取得
        private Boolean setHinshiInfos()
        {
            Boolean bResult = false;

            bResult = getHinshiInfosFromDB(0, lstHinshi);
            if (!bResult)
                return bResult;

            Debug.WriteLine("品詞情報設定完了");

            bResult = getHinshiInfosFromDB(1, lstHinshiSub1);
            if (!bResult)
                return bResult;

            Debug.WriteLine("品詞詳細1情報設定完了");

            bResult = getHinshiInfosFromDB(2, lstHinshiSub2);
            if (!bResult)
                return bResult;

            Debug.WriteLine("品詞詳細2情報設定完了");

            bResult = getHinshiInfosFromDB(3, lstHinshiSub3);
            if (!bResult)
                return bResult;

            Debug.WriteLine("品詞詳細3情報設定完了");

            bResult = getKatsuyoInfosFromDB(1, lstKatsuyo1);
            if (!bResult)
                return bResult;

            Debug.WriteLine("活用1情報設定完了");

            bResult = getKatsuyoInfosFromDB(2, lstKatsuyo2);
            if (!bResult)
                return bResult;

            Debug.WriteLine("活用2情報設定完了");

            return bResult;
        }

        //Textファイルから各種情報を取得する
        private Boolean setInfosFromFiles(String strFilePath, ref List<String> lstInfosParam)
        {
            Boolean bResult = false;
            StreamReader strrdr = null;
            String strText = String.Empty;

            try
            {
                lstInfosParam.Clear();
                strrdr = new StreamReader(@strFilePath, Encoding.GetEncoding("utf-8"));
                
                while (!strrdr.EndOfStream)
                {
                    strText = strrdr.ReadLine();
                    if (strText.Length > 0)
                        lstInfosParam.Add(strText);
                }

                bResult = true;
            }
            catch (IOException ioe)
            {
                MessageBox.Show(ioe.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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

        //M_HINSHIから品詞情報を取得する
        private Boolean getHinshiInfosFromDB(int hinshiLevel, List<MHinshi> lstHinshiInfosParam, int parentNo = -1)
        {
            Boolean bResult = false;
            String strText = String.Empty;
            SqlCommand command = new SqlCommand();
            DataTable dt = new DataTable();
            
            try
            {
                lstHinshiInfosParam.Clear();

                strText = "select * from M_HINSHI where HINSHI_LEVEL = " + hinshiLevel.ToString();
                if (parentNo > -1)
                    strText = strText + " and PARENT_HINSHI_NO = " + parentNo.ToString();
                strText = strText + " order by HINSHI_NO";
                dt = mySqlControl.selectForList(strText);
                foreach (DataRow item in dt.Rows)
                {
                    if (item["PARENT_HINSHI_NO"].ToString().Equals(""))
                        parentNo = -1;
                    else
                        parentNo =  int.Parse(item["PARENT_HINSHI_NO"].ToString());
                    MHinshi hinshiWork = new MHinshi(int.Parse(item["HINSHI_NO"].ToString()), item["HINSHI_BASIC"].ToString(),
                        int.Parse(item["HINSHI_LEVEL"].ToString()), parentNo);
                    hinshiWork.CreatedAt = DateTime.Parse(item["CREATED_AT"].ToString());
                    hinshiWork.UpdatedAt = DateTime.Parse(item["UPDATED_AT"].ToString());
                    hinshiWork.DeleteFlg = item["DELETE_FLAG"].ToString();
                    lstHinshiInfosParam.Add(hinshiWork);
                }

                bResult = true;
            }
            catch (IOException ioe)
            {
                MessageBox.Show(ioe.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
            return bResult;
        }

        //M_KATSUYOから活用形情報を取得する
        private Boolean getKatsuyoInfosFromDB(int katsuyoLevel, List<MKatsuyo> lstKatsuyoInfosParam, int parentNo = -1)
        {
            Boolean bResult = false;
            String strText = String.Empty;
            SqlCommand command = new SqlCommand();
            DataTable dt = new DataTable();

            try
            {
                lstKatsuyoInfosParam.Clear();

                strText = "select * from M_KATSUYO where KATSUYO_LEVEL = " + katsuyoLevel.ToString();
                if (parentNo > -1)
                    strText = strText + " and PARENT_KATSUYO_NO = " + parentNo.ToString();
                strText = strText + " order by KATSUYO_NO";
                dt = mySqlControl.selectForList(strText);
                foreach (DataRow item in dt.Rows)
                {
                    if (item["PARENT_KATSUYO_NO"].ToString().Equals(""))
                        parentNo = -1;
                    else
                        parentNo = int.Parse(item["PARENT_KATSUYO_NO"].ToString());
                    MKatsuyo katsuyoWork = new MKatsuyo(int.Parse(item["KATSUYO_NO"].ToString()), item["KATSUYO_BASIC"].ToString(),
                        int.Parse(item["KATSUYO_LEVEL"].ToString()), parentNo);
                    katsuyoWork.CreatedAt = DateTime.Parse(item["CREATED_AT"].ToString());
                    katsuyoWork.UpdatedAt = DateTime.Parse(item["UPDATED_AT"].ToString());
                    katsuyoWork.DeleteFlg = item["DELETE_FLAG"].ToString();
                    lstKatsuyoInfosParam.Add(katsuyoWork);
                }

                bResult = true;
            }
            catch (IOException ioe)
            {
                MessageBox.Show(ioe.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return bResult;
        }

        //学習用Csvファイルの読み取り
        private Boolean ReadLearnCsvFiles()
        {
            Boolean bResult = false;
            StreamReader strReader = null;
            String strFileName = String.Empty;
            int intMorphemeCountMax1 = int.Parse(XmlDatas.ListConsts["MORPHEME_COUNT_MAX1"].ToString());
            int intMorphemeCountMax2 = int.Parse(XmlDatas.ListConsts["MORPHEME_COUNT_MAX2"].ToString());

            OpenFileDialog ofdRead = new OpenFileDialog();
            ofdRead.Title = "学習用CSVファイルを指定してください";
            ofdRead.InitialDirectory = systemInfo.ListMembers["CSV_DIRECTORY_PATH"].ToString();
            ofdRead.FileName = XmlDatas.ListConsts["LEARN_CSV_FILE"].ToString();
            if (ofdRead.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                strFileName = ofdRead.FileName;
                if (!System.IO.File.Exists(@strFileName))
                {
                    MessageBox.Show("ファイルが存在しません");
                    return bResult;
                }
            }
            else
            {
                return bResult;
            }

            try
            {
                lstMyMSeed.Clear();
                strReader = new StreamReader(@strFileName, Encoding.GetEncoding("utf-8"));

                while (!strReader.EndOfStream)
                {
                    String strLine = strReader.ReadLine();
                    String[] strLineSplitComma = strLine.Split(',');
                    if (strLineSplitComma.Length != intMorphemeCountMax1 && strLineSplitComma.Length != intMorphemeCountMax2)
                        strLineSplitComma = MyUtils.CsvFileDisposeComma(strLineSplitComma);
                
                    MSeed mSeedWork = new MSeed();
                    mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["SEED_NO"]] = 0;
                    mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["HYOSO_TYPE"]] = strLineSplitComma[0];
                    mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["LEFT_CONNECT_STATUS_NO"]] = 1000;
                    mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["RIGHT_CONNECT_STATUS_NO"]] = 1000;
                    mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["COST"]] = 1000;
                    mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["HINSHI"]] = String.Empty;
                    mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["HINSHI_DETAIL_1"]] = String.Empty;
                    mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["HINSHI_DETAIL_2"]] = String.Empty;
                    mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["HINSHI_DETAIL_3"]] = String.Empty;
                    mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["KATSUYO_KEI"]] = String.Empty;
                    mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["KATSUYO_TYPE"]] = String.Empty;
                    mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["BASE_TYPE"]] = String.Empty;
                    mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["YOMI"]] = String.Empty;
                    mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["HATSUON"]] = String.Empty;
                    mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["CREATED_AT"]] = DateTime.Now;
                    mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["UPDATED_AT"]] = DateTime.Now;
                    mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["DELETE_FLAG"]] = 0;
                    mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["DATA_COUNT"]] = 1;

                    if (strLineSplitComma.Length > 1)
                    {
                        mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["LEFT_CONNECT_STATUS_NO"]] = int.Parse(strLineSplitComma[1]);
                        mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["RIGHT_CONNECT_STATUS_NO"]] = int.Parse(strLineSplitComma[2]);
                        mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["COST"]] = int.Parse(strLineSplitComma[3]);
                        mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["HINSHI"]] = strLineSplitComma[4];
                        mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["HINSHI_DETAIL_1"]] = strLineSplitComma[5];
                        mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["HINSHI_DETAIL_2"]] = strLineSplitComma[6];
                        mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["HINSHI_DETAIL_3"]] = strLineSplitComma[7];
                        mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["KATSUYO_KEI"]] = strLineSplitComma[8];
                        mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["KATSUYO_TYPE"]] = strLineSplitComma[9];
                        mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["BASE_TYPE"]] = strLineSplitComma[10];

                        if (strLineSplitComma.Length > 11)
                        {
                            mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["YOMI"]] = strLineSplitComma[11];
                            mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["HATSUON"]] = strLineSplitComma[12];
                        }
                    }

                    lstMyMSeed.Add(mSeedWork);

                    //品詞リストにないものがあれば追加しておく
                    int indexHinshi = MyUtils.checkHinshiList(lstHinshi, 0, -1, mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["HINSHI"]].ToString(), false);
                    if (indexHinshi < 0)
                        indexHinshi = 0;
                    
                    //品詞詳細1リストにないものがあれば追加する
                    int indexHinshiSub1 = MyUtils.checkHinshiList(lstHinshiSub1, 1, lstHinshi[indexHinshi].Id, mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["HINSHI_DETAIL_1"]].ToString(), false);
                    if (indexHinshiSub1 < 0)
                        indexHinshiSub1 = 0;

                    //品詞詳細2リストにないものがあれば追加する
                    int indexHinshiSub2 = MyUtils.checkHinshiList(lstHinshiSub2, 2, lstHinshiSub1[indexHinshiSub1].Id, mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["HINSHI_DETAIL_2"]].ToString(), false);
                    if (indexHinshiSub2 < 0)
                        indexHinshiSub2 = 0;
                    
                    //品詞詳細3リストにないものがあれば追加する
                    int indexHinshiSub3 = MyUtils.checkHinshiList(lstHinshiSub3, 3, lstHinshiSub2[indexHinshiSub2].Id, mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["HINSHI_DETAIL_3"]].ToString(), false);
                    if (indexHinshiSub3 < 0)
                        indexHinshiSub3 = 0;
                    
                    //活用形リストにないものがあれば追加しておく
                    int indexKatsuyo1 = MyUtils.checkKatsuyoList(lstKatsuyo1, 1, lstHinshi[indexHinshi].Id, mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["KATSUYO_KEI"]].ToString());
                    if (indexKatsuyo1 < 0)
                        indexKatsuyo1 = 0;

                    //活用型リストにないものがあれば追加しておく
                    int indexKatsuyo2 = MyUtils.checkKatsuyoList(lstKatsuyo2, 2, lstKatsuyo1[indexKatsuyo1].Id, mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["KATSUYO_TYPE"]].ToString());
                    if (indexKatsuyo2 < 0)
                        indexKatsuyo2 = 0;
                }

                //学習用要素のソート
                lstMyMSeed.Sort(delegate(MSeed seedA, MSeed seedB)
                {
                    int intCompA = String.Compare(seedA.DictionaryMembers[XmlDatas.ListItemNames["HINSHI"]].ToString(), seedB.DictionaryMembers[XmlDatas.ListItemNames["HINSHI"]].ToString());
                    if (intCompA == 0)
                    {
                        int intCompB = String.Compare(seedA.DictionaryMembers[XmlDatas.ListItemNames["HINSHI_DETAIL_1"]].ToString(), seedB.DictionaryMembers[XmlDatas.ListItemNames["HINSHI_DETAIL_1"]].ToString());
                        if (intCompB == 0)
                        {
                            int intCompC = String.Compare(seedA.DictionaryMembers[XmlDatas.ListItemNames["HINSHI_DETAIL_2"]].ToString(), seedB.DictionaryMembers[XmlDatas.ListItemNames["HINSHI_DETAIL_2"]].ToString());
                            if (intCompC == 0)
                            {
                                int intCompD = String.Compare(seedA.DictionaryMembers[XmlDatas.ListItemNames["HINSHI_DETAIL_3"]].ToString(), seedB.DictionaryMembers[XmlDatas.ListItemNames["HINSHI_DETAIL_3"]].ToString());
                                if (intCompD == 0)
                                {
                                    int intCompE = String.Compare(seedA.DictionaryMembers[XmlDatas.ListItemNames["HYOSO_TYPE"]].ToString(), seedB.DictionaryMembers[XmlDatas.ListItemNames["HYOSO_TYPE"]].ToString());
                                    return intCompE;
                                }
                                else
                                {
                                    return intCompD;
                                }
                            }
                            else
                            {
                                return intCompC;
                            }
                        }
                        else
                        {
                            return intCompB;
                        }
                    }
                    else
                    {
                        return intCompA;
                    }
                });

                //品詞等ののソート
                lstHinshi.Sort(delegate(MHinshi mHinshi1, MHinshi mHinshi2) { return mHinshi1.Id - mHinshi2.Id; });
                lstHinshiSub1.Sort(delegate(MHinshi mHinshi1, MHinshi mHinshi2) { return mHinshi1.Id - mHinshi2.Id; });
                lstHinshiSub2.Sort(delegate(MHinshi mHinshi1, MHinshi mHinshi2) { return mHinshi1.Id - mHinshi2.Id; });
                lstHinshiSub3.Sort(delegate(MHinshi mHinshi1, MHinshi mHinshi2) { return mHinshi1.Id - mHinshi2.Id; });
                lstKatsuyo1.Sort(delegate(MKatsuyo mKatsuyo1, MKatsuyo mKatsuyo2) { return mKatsuyo1.Id - mKatsuyo2.Id; });
                lstKatsuyo2.Sort(delegate(MKatsuyo mKatsuyo1, MKatsuyo mKatsuyo2) { return mKatsuyo1.Id - mKatsuyo2.Id; });


                bResult = true;
            }
            catch (IOException ioe)
            {
                MessageBox.Show(ioe.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (strReader != null)
                {
                    strReader.Close();
                    strReader.Dispose();
                }
            }

            return bResult;
        }

        //コーパスファイルの出力
        private Boolean outputCopusFile(String strFileName = "")
        {
            Boolean bResult = false;
            StreamWriter strwtr = null;
            String filePath = String.Empty;
            String fileName = String.Empty;

            if (!strCoupusFilePath.Equals(String.Empty))
            {
                int position = strCoupusFilePath.LastIndexOf('\\');
                filePath = strCoupusFilePath.Substring(0, position);
                fileName = strCoupusFilePath.Substring(position + 1, strCoupusFilePath.Length - 1 - (position + 4));
            }

            if (strFileName.Equals(String.Empty))
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Title = "コーパスファイル保存";
                sfd.Filter = "Text File (*.txt)|*.txt|All Files (*.*)|*.*";
                sfd.DefaultExt = "txt";
                if (!filePath.Equals(String.Empty))
                    sfd.InitialDirectory = @filePath;
                if (!fileName.Equals(String.Empty))
                    sfd.FileName = fileName;

                if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    strFileName = sfd.FileName;
                    strCoupusFilePath = sfd.FileName;
                }
                else
                {
                    return bResult;
                }
            }
            
            try
            {
                strwtr = new StreamWriter(@strFileName, false, Encoding.UTF8);

                if (cmbEditMode.SelectedIndex == 0)
                {
                    lstMSeed.ForEach(delegate(MSeed mSeedWork)
                    {
                        String strOutputWork = mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["HYOSO_TYPE"]].ToString();
                        if (!mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["HINSHI"]].ToString().Equals(String.Empty))
                        {
                            strOutputWork = strOutputWork + "\t" + mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["HINSHI"]].ToString() + "," + mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["HINSHI_DETAIL_1"]].ToString() + "," + mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["HINSHI_DETAIL_2"]].ToString() + "," + mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["HINSHI_DETAIL_3"]].ToString() + "," + mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["KATSUYO_KEI"]].ToString() + "," + mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["KATSUYO_TYPE"]].ToString() + "," + mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["BASE_TYPE"]].ToString();
                        }

                        if (!mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["YOMI"]].ToString().Equals(String.Empty))
                        {
                            strOutputWork = strOutputWork + "," + mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["YOMI"]].ToString() + "," + mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["HATSUON"]].ToString();
                        }

                        strwtr.WriteLine(strOutputWork);
                    });

                    MessageBox.Show(String.Format(XmlDatas.ListMessages["INFORMATION_1"], XmlDatas.ListNames["COPUS_EDIT"]));
                }
                else
                {
                    lstMSeed.ForEach(delegate(MSeed mSeedWork)
                    {
                        strwtr.WriteLine(mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["HYOSO_TYPE"]] + "\t" + mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["HINSHI"]] + "," + mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["HINSHI_DETAIL_1"]] + "," + mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["HINSHI_DETAIL_2"]] + "," + mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["HINSHI_DETAIL_3"]] + "," + mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["DATA_COUNT"]]);
                    });

                    MessageBox.Show(String.Format(XmlDatas.ListMessages["INFORMATION_1"], XmlDatas.ListNames["COPUS_COUNT"]));
                }

                bResult = true;
            }
            catch (IOException ioe)
            {
                MessageBox.Show(ioe.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (strwtr != null)
                {
                    strwtr.Close();
                    strwtr.Dispose();
                }
            }

            return bResult;
        }

        //csvファイルの出力
        private Boolean outputCsvFile(String strFilePath = "")
        {
            String filePath = String.Empty;
            String fileName = String.Empty;

            if (!strCsvFilePath.Equals(String.Empty))
            {
                int position = strCsvFilePath.LastIndexOf('\\');
                filePath = strCsvFilePath.Substring(0, position);
                fileName = strCsvFilePath.Substring(position + 1, strCsvFilePath.Length - 1 - (position + 4));
            }

            if (strFilePath.Equals(String.Empty))
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Title = "学習用CSVファイル保存";
                sfd.Filter = "Csv File (*.csv)|*.csv|All Files (*.*)|*.*";
                sfd.DefaultExt = "csv";
                if (!filePath.Equals(String.Empty))
                    sfd.InitialDirectory = @filePath;
                if (!fileName.Equals(String.Empty))
                    sfd.FileName = fileName;

                if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    strFilePath = sfd.FileName;
                    strCsvFilePath = sfd.FileName;
                }
            }
            return MyUtils.outputCsvFile(@strCsvFilePath, lstMyMSeed);
        }

        //コーパスの編集と集計の切り替え
        private void cmbEditMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnAdd.Enabled = false;
            btnEdit.Enabled = false;
            btnDel.Enabled = false;
            btnEos.Enabled = false;
            btnCheck.Enabled = false;
            btnAllReplace.Enabled = false;
            btnSave.Enabled = false;

            if (cmbEditMode.SelectedIndex == 0)
            {
                if (bFileReaded)
                {
                    btnAdd.Enabled = true;
                    btnEdit.Enabled = true;
                    btnDel.Enabled = true;
                    btnEos.Enabled = true;
                    btnCheck.Enabled = true;
                    btnAllReplace.Enabled = true;
                    btnSave.Enabled = true;
                }
            }

            if (lblFilePath.Text.Length > 0)
            {
                if (!createCopusListFromFile(@lblFilePath.Text))
                    return;
            }
        }

        //要素追加
        private void btnAdd_Click(object sender, EventArgs e)
        {
            int intSelected = lstvwMain.SelectedItems[0].Index;

            frmEditSeed frmEdit = new frmEditSeed();
            frmEdit.ListHinshi = lstHinshi;
            frmEdit.ListHinshiSub1 = lstHinshiSub1;
            frmEdit.ListHinshiSub2 = lstHinshiSub2;
            frmEdit.ListHinshiSub3 = lstHinshiSub3;
            frmEdit.ListKatsuyo1 = lstKatsuyo1;
            frmEdit.ListKatsuyo2 = lstKatsuyo2;
            frmEdit.ProcessMode = 0;
            frmEdit.EditMode = 0;
            frmEdit.systemInfo = systemInfo;
            frmEdit.ListMyLearnSeed = lstMyMSeed;
            frmEdit.ShowDialog();

            if (frmEdit.ReturnValue == 1)
            {
                lstMyMSeed.Add(frmEdit.MySeedInfoResult);
                lstMSeed.Insert(intSelected, frmEdit.MySeedInfoResult);
                String[] strLineWork = { frmEdit.MySeedInfoResult.DictionaryMembers[XmlDatas.ListItemNames["HYOSO_TYPE"]].ToString(), frmEdit.MySeedInfoResult.DictionaryMembers[XmlDatas.ListItemNames["HINSHI"]].ToString(), frmEdit.MySeedInfoResult.DictionaryMembers[XmlDatas.ListItemNames["HINSHI_DETAIL_1"]].ToString(), frmEdit.MySeedInfoResult.DictionaryMembers[XmlDatas.ListItemNames["HINSHI_DETAIL_2"]].ToString(), frmEdit.MySeedInfoResult.DictionaryMembers[XmlDatas.ListItemNames["HINSHI_DETAIL_3"]].ToString(), frmEdit.MySeedInfoResult.DictionaryMembers[XmlDatas.ListItemNames["DATA_COUNT"]].ToString() };
                lstvwMain.Items.Insert(intSelected, new ListViewItem(strLineWork));

                if (!setMainListView(0))
                    return;
            }

            frmEdit.Dispose();
        }

        //要素編集
        private void btnEdit_Click(object sender, EventArgs e)
        {
            //選択チェック
            if (lstvwMain.SelectedItems.Count == 0)
            {
                MessageBox.Show(XmlDatas.ListMessages["ERROR_1"]);
                return;
            }

            List<MSeed> lstSelectedSeeds = new List<MSeed>();
            MSeed seedCelected = new MSeed(lstMSeed[lstvwMain.SelectedItems[0].Index]);
            for (int i = 0; i < lstvwMain.SelectedItems.Count; i++)
            {
                lstSelectedSeeds.Add(lstMSeed[lstvwMain.SelectedItems[i].Index]);

                if (i > 0)
                {
                    seedCelected.DictionaryMembers["HYOSO_TYPE"] += lstMSeed[lstvwMain.SelectedItems[i].Index].DictionaryMembers["HYOSO_TYPE"].ToString();
                    seedCelected.DictionaryMembers["BASE_TYPE"] += lstMSeed[lstvwMain.SelectedItems[i].Index].DictionaryMembers["BASE_TYPE"].ToString();
                    seedCelected.DictionaryMembers["YOMI"] += lstMSeed[lstvwMain.SelectedItems[i].Index].DictionaryMembers["YOMI"].ToString();
                    seedCelected.DictionaryMembers["HATSUON"] += lstMSeed[lstvwMain.SelectedItems[i].Index].DictionaryMembers["HATSUON"].ToString();
                }
            }

            //編集画面の表示
            frmEditSeed frmEdit = new frmEditSeed();
            frmEdit.ListHinshi = lstHinshi;
            frmEdit.ListHinshiSub1 = lstHinshiSub1;
            frmEdit.ListHinshiSub2 = lstHinshiSub2;
            frmEdit.ListHinshiSub3 = lstHinshiSub3;
            frmEdit.ListKatsuyo1 = lstKatsuyo1;
            frmEdit.ListKatsuyo2 = lstKatsuyo2;
            frmEdit.systemInfo = systemInfo;
            frmEdit.ListMyLearnSeed = lstMyMSeed;
            frmEdit.MySeedInfo = seedCelected;
            frmEdit.ProcessMode = 0;
            frmEdit.EditMode = 1;
            frmEdit.ShowDialog();

            if (frmEdit.ReturnValue == 1)
            {
                seedCelected = frmEdit.MySeedInfoResult;

                //先ず先頭の要素を置換する
                int intInsertIndex = lstvwMain.SelectedItems[0].Index;
                for (int i = 0; i < lstvwMain.SelectedItems.Count; i++)
                {
                    lstMSeed.RemoveAt(intInsertIndex);
                }
                lstMSeed.Insert(intInsertIndex, seedCelected);

                //次に、同様に置換対象となる要素を洗い出す
                List<int> lstIndexes = new List<int>();
                int intIndexWork = 0;
                while (intIndexWork < lstMSeed.Count)
                {
                    intIndexWork = lstMSeed.FindIndex(intIndexWork, delegate(MSeed mSeedMatch)
                    {
                        if (mSeedMatch.DictionaryMembers["HYOSO_TYPE"].ToString().Equals(lstSelectedSeeds[0].DictionaryMembers["HYOSO_TYPE"].ToString()) && mSeedMatch.DictionaryMembers["HINSHI"].ToString().Equals(lstSelectedSeeds[0].DictionaryMembers["HINSHI"].ToString()))
                            return true;
                        else
                            return false;
                    });

                    if (intIndexWork > 0)
                    {
                        intIndexWork++;
                        int intCountWork = 1;
                        int intEnd = intIndexWork + lstSelectedSeeds.Count - 1;
                        Boolean bHitWork = true;

                        for (int i = intIndexWork; i < intEnd; i++)
                        {
                            if (!lstMSeed[i].DictionaryMembers["HYOSO_TYPE"].ToString().Equals(lstSelectedSeeds[intCountWork].DictionaryMembers["HYOSO_TYPE"].ToString()) || !lstMSeed[i].DictionaryMembers["HINSHI"].ToString().Equals(lstSelectedSeeds[intCountWork].DictionaryMembers["HINSHI"].ToString()))
                                bHitWork = false;

                            intCountWork++;
                        }

                        if (bHitWork)
                        {
                            lstIndexes.Add(intIndexWork);
                            intIndexWork += lstSelectedSeeds.Count;
                        }
                    }
                    else
                        break;


                }

                if (lstIndexes.Count > 0)
                {
                    String strMessageConfirm = String.Format(XmlDatas.ListMessages["CONFIRM_3"], lstIndexes.Count.ToString());
                    if (MessageBox.Show(strMessageConfirm, XmlDatas.ListNames["CONFIRM"], MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                    {
                        intIndexWork = 0;
                        while (intIndexWork < lstMSeed.Count)
                        {
                            intIndexWork = lstMSeed.FindIndex(intIndexWork, delegate(MSeed mSeedMatch)
                            {
                                if (mSeedMatch.DictionaryMembers["HYOSO_TYPE"].ToString().Equals(lstSelectedSeeds[0].DictionaryMembers["HYOSO_TYPE"].ToString()) && mSeedMatch.DictionaryMembers["HINSHI"].ToString().Equals(lstSelectedSeeds[0].DictionaryMembers["HINSHI"].ToString()))
                                    return true;
                                else
                                    return false;
                            });

                            if (intIndexWork > 0)
                            {
                                int intStartWork = intIndexWork;
                                intIndexWork++;
                                int intCountWork = 1;
                                int intEnd = intIndexWork + lstSelectedSeeds.Count - 1;
                                Boolean bHitWork = true;

                                for (int i = intIndexWork; i < intEnd; i++)
                                {
                                    if (!lstMSeed[i].DictionaryMembers["HYOSO_TYPE"].ToString().Equals(lstSelectedSeeds[intCountWork].DictionaryMembers["HYOSO_TYPE"].ToString()) || !lstMSeed[i].DictionaryMembers["HINSHI"].ToString().Equals(lstSelectedSeeds[intCountWork].DictionaryMembers["HINSHI"].ToString()))
                                        bHitWork = false;

                                    intCountWork++;
                                }

                                if (bHitWork)
                                {
                                    for (int i = intStartWork; i < intEnd; i++)
                                    {
                                        lstMSeed.RemoveAt(intStartWork);
                                    }

                                    lstMSeed.Insert(intStartWork, seedCelected);

                                    intIndexWork += lstSelectedSeeds.Count;
                                }
                            }
                            else
                                break;
                        }
                    }
                }

                if (!setMainListView(0))
                    return;

                lstvwMain.SelectedIndices.Clear();
                lstvwMain.Items[intInsertIndex].Selected = true;
                lstvwMain.Select();
                lstvwMain.Items[intInsertIndex].Focused = true;
            }
            else if (frmEdit.ReturnValue == -5)
            {
                if (MessageBox.Show(XmlDatas.ListMessages["CONFIRM_4"], XmlDatas.ListNames["CONFIRM"], MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    //置換対象外情報を追加する
                    NoTranseInfo noTranseInfoWork = new NoTranseInfo();
                    int intInsertIndex = lstvwMain.SelectedItems[0].Index;
                    for (int i = intInsertIndex; i < intInsertIndex + lstvwMain.SelectedItems.Count; i++)
                    {
                        MSeed notransWotk = new MSeed(lstMSeed[i]);
                        noTranseInfoWork.lstNoTransInfo.Add(notransWotk);
                    }

                    //既に追加されていなければ、追加する
                    Boolean IsExist = true;
                    lstNoTanseSeed.ForEach(delegate(NoTranseInfo noTransInfoDelegate)
                    {
                        if (noTranseInfoWork.lstNoTransInfo.Count == noTransInfoDelegate.lstNoTransInfo.Count)
                        {
                            for (int j = 0; j < noTranseInfoWork.lstNoTransInfo.Count; j++)
                            {
                                if (noTranseInfoWork.lstNoTransInfo[j].DictionaryMembers["HYOSO_TYPE"].ToString().Equals(noTransInfoDelegate.lstNoTransInfo[j].DictionaryMembers["HYOSO_TYPE"].ToString()) && noTranseInfoWork.lstNoTransInfo[j].DictionaryMembers["HINSHI"].ToString().Equals(noTransInfoDelegate.lstNoTransInfo[j].DictionaryMembers["HINSHI"].ToString()))
                                {
                                }
                                else
                                {
                                    IsExist = false;
                                }
                            }
                        }
                    });

                    if (!IsExist)
                    {
                        lstNoTanseSeed.Add(noTranseInfoWork);
                    }
                }
            }
        }

        //要素削除
        private void btnDel_Click(object sender, EventArgs e)
        {
            //選択チェック
            if (lstvwMain.SelectedItems.Count == 0)
            {
                MessageBox.Show(XmlDatas.ListMessages["ERROR_1"]);
                return;
            }

            if (MessageBox.Show(XmlDatas.ListMessages["CONFIRM_1"], XmlDatas.ListNames["CONFIRM"], MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int intSelectedCount = lstvwMain.SelectedIndices.Count;
                for (int i = intSelectedCount - 1; i >= 0; i--)
                {
                    int intSelectedIndex = lstvwMain.SelectedIndices[i];
                    lstMSeed.RemoveAt(intSelectedIndex);
                    lstvwMain.Items.RemoveAt(intSelectedIndex);
                }
            }
        }

        //getter, setter
        public List<MSeed> ListAllMSeed
        {
            get { return lstAllMSeed; }
            set { lstAllMSeed = value; }
        }

        public List<MSeed> ListMyMSeed
        {
            get { return lstMyMSeed; }
            set { lstMyMSeed = value; }
        }

        public List<MSeed> ListMSeed
        {
            get { return lstMSeed; }
        }

        //プログラムの終了
        private void 終了XToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        //システム情報の設定
        private void システム設定TToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSetSystemInfo frmSetSysInfo = new frmSetSystemInfo();
            frmSetSysInfo.mySystemInfo = systemInfo;
            frmSetSysInfo.ShowDialog();
        }

        //CSV情報の設定
        private void cSV設定GToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        //保存ボタンクリック時(品詞、CSV、コーパスを全て保存)
        private void btnSave_Click(object sender, EventArgs e)
        {
            //コーパスの保存
            if (outputCopusFile())
            {
            }

            //Csvファイルの保存
            if (outputCsvFile())
            {
            }
        }

        private void 上書き保存SToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //コーパスの保存
            if (outputCopusFile(@strCoupusFilePath))
            {
            }

            //Csvファイルの保存
            if (outputCsvFile(@strCsvFilePath))
            {
            }
        }

        //リストを描画する
        private Boolean setMainListView(int intEditModeWork)
        {
            Boolean bResult = false;

            lstvwMain.Clear();
            lstvwMain.Columns.Add(XmlDatas.ListNames["HYOSO_TYPE"]);
            lstvwMain.Columns[0].Width = 200;
            lstvwMain.Columns.Add(XmlDatas.ListNames["HINSHI"]);
            lstvwMain.Columns.Add(XmlDatas.ListNames["HINSHI_DETAIL_1"]);
            lstvwMain.Columns.Add(XmlDatas.ListNames["HINSHI_DETAIL_2"]);
            lstvwMain.Columns.Add(XmlDatas.ListNames["HINSHI_DETAIL_3"]);

            if (intEditModeWork == 0)
            {
                lstvwMain.Columns.Add(XmlDatas.ListNames["KATSUYO_KEI"]);
                lstvwMain.Columns.Add(XmlDatas.ListNames["KATSUYO_TYPE"]);
                lstvwMain.Columns.Add(XmlDatas.ListNames["BASE_TYPE"]);
                lstvwMain.Columns[7].Width = 200;
                lstvwMain.Columns.Add(XmlDatas.ListNames["YOMI"]);
                lstvwMain.Columns[8].Width = 200;
                lstvwMain.Columns.Add(XmlDatas.ListNames["HATSUON"]);
                lstvwMain.Columns[9].Width = 200;
            }
            else
            {
                lstvwMain.Columns.Add(XmlDatas.ListNames["DATA_COUNT"]);
            }

            lstMSeed.ForEach(delegate(MSeed mSeedWork)
            {
                if (intEditModeWork == 0)
                {
                    String[] strLineWork = { mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["HYOSO_TYPE"]].ToString(), mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["HINSHI"]].ToString(), mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["HINSHI_DETAIL_1"]].ToString(), mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["HINSHI_DETAIL_2"]].ToString(), mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["HINSHI_DETAIL_3"]].ToString(), mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["KATSUYO_KEI"]].ToString(), mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["KATSUYO_TYPE"]].ToString(), mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["BASE_TYPE"]].ToString(), mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["YOMI"]].ToString(), mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["HATSUON"]].ToString() };
                    lstvwMain.Items.Add(new ListViewItem(strLineWork));
                }
                else
                {
                    String[] strLineWork = { mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["HYOSO_TYPE"]].ToString(), mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["HINSHI"]].ToString(), mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["HINSHI_DETAIL_1"]].ToString(), mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["HINSHI_DETAIL_2"]].ToString(), mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["HINSHI_DETAIL_3"]].ToString(), mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["DATA_COUNT"]].ToString() };
                    lstvwMain.Items.Add(new ListViewItem(strLineWork));
                }
            });

            lblCopusInfo.Text = XmlDatas.ListNames["COPUS_NUMBER"] + ":" + lstMSeed.Count.ToString();
            bResult = true;

            return bResult;
        }

        //EOSを1つにまとめる
        private void btnEos_Click(object sender, EventArgs e)
        {
            //EOSは連続している場合は一つにする
            Boolean bIsChecking = false;
            int intCountMax = lstMSeed.Count;

            for (int i = 0; i < intCountMax; i++)
            {
                if (lstMSeed[i].DictionaryMembers["HYOSO_TYPE"].Equals("EOS"))
                {
                    if (bIsChecking)
                    {
                        lstMSeed.RemoveAt(i);
                        i--;
                        intCountMax--;
                    }

                    bIsChecking = true;
                }
                else
                {
                    bIsChecking = false;
                }
            }

            if (!setMainListView(0))
                return;
        }

        //置換情報設定
        private void 置換情報設定WToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMorphemeReplaceInfo frmMorphemeRepInfo = new frmMorphemeReplaceInfo();
            frmMorphemeRepInfo.ListHinshi = lstHinshi;
            frmMorphemeRepInfo.ListHinshiSub1 = lstHinshiSub1;
            frmMorphemeRepInfo.ListHinshiSub2 = lstHinshiSub2;
            frmMorphemeRepInfo.ListHinshiSub3 = lstHinshiSub3;
            frmMorphemeRepInfo.ListKatsuyo1 = lstKatsuyo1;
            frmMorphemeRepInfo.ListKatsuyo2 = lstKatsuyo2;
            frmMorphemeRepInfo.ListMyLearnSeed = lstMyMSeed;
            frmMorphemeRepInfo.ListMorphemeReplaceInfo = lstMorphemeReplaceInfo;
            frmMorphemeRepInfo.ShowDialog();
        }

        //登録してある情報を基に一括置換
        private void btnAllReplace_Click(object sender, EventArgs e)
        {
            int intCurrent = 0;
            int intSourceMax = 0;
            int intMaX = lstMSeed.Count;
            
            foreach (MorphemeReplaceInfo morphemeReplaceInfoEach in lstMorphemeReplaceInfo)
            {
                List<MSeed> lstSelectedSeeds = new List<MSeed>();
                MSeed seedSelected = new MSeed();

                //当たり判定
                Boolean bHit = false;
                int intCheckCount = 0;
                intCurrent = 0;

                if (morphemeReplaceInfoEach.IsTake)
                {
                    while (intCurrent < lstMSeed.Count)
                    {
                        intCheckCount = 0;
                        intSourceMax = intCurrent + morphemeReplaceInfoEach.ListSourceCount;
                        if (intSourceMax > lstMSeed.Count)
                            intSourceMax = lstMSeed.Count;

                        lstSelectedSeeds.Clear();
                        seedSelected = new MSeed();

                        //ループモードでない場合
                        if (!morphemeReplaceInfoEach.IsLoop)
                        {
                            for (int i = intCurrent; i < intSourceMax; i++)
                            {
                                //形態要素置換情報にヒットしているか判定
                                bHit = isHitMorphemeReplaceInfo(morphemeReplaceInfoEach, i, intCheckCount); ;

                                if (!bHit)
                                    break;

                                intCheckCount++;
                            }
                        }
                        else
                        {
                            int intCurrrntWork = intCurrent;
                            intCheckCount = morphemeReplaceInfoEach.ListSourceCount;
                            int intHitCount = 0;
                            Boolean bIsCheckEnd = false;
                            List<Boolean> lstIsExist = new List<Boolean>();
                            for (int i = 0; i < intCheckCount; i++)
                            {
                                lstIsExist.Add(false);
                            }

                            int intWorkCount = 0;

                            while (!bIsCheckEnd)
                            {
                                //形態要素置換情報にヒットしているか判定
                                bHit = isHitMorphemeReplaceInfo(morphemeReplaceInfoEach, intCurrrntWork, intWorkCount);

                                if (bHit)
                                {
                                    intHitCount++;
                                    lstIsExist[intWorkCount] = true;
                                    intCurrrntWork++;
                                    intWorkCount++;
                                }
                                else
                                {
                                    //置換情報内の1つ目の形態要素と一致しない場合は、その置換情報に対する比較処理は終わり。
                                    intWorkCount = 0;
                                    bIsCheckEnd = true;
                                }

                                if (intCurrrntWork > lstMSeed.Count - 1)
                                    bIsCheckEnd = true;
                                else if (intWorkCount == intCheckCount)
                                    bIsCheckEnd = true;
                            }

                            //バグと思われるので修正
                            if (intHitCount > 1 && intHitCount == morphemeReplaceInfoEach.ListSourceCount)
                            {
                                Boolean bIsAllExist = true;
                                lstIsExist.ForEach(delegate(Boolean bIsExistWork)
                                {
                                    if (!bIsExistWork)
                                        bIsAllExist = false;
                                });

                                if (bIsAllExist)
                                {
                                    intSourceMax = intCurrent + intHitCount;
                                    if (intSourceMax > lstMSeed.Count)
                                        intSourceMax = lstMSeed.Count;

                                    bHit = true;
                                }
                            }
                        }

                        //キャンセル対象か確認する
                        if (bHit)
                        {
                            //デバッグ時にヒット判定された対象を出力
                            outputMorphReplaceInfo(morphemeReplaceInfoEach, intCurrent, intSourceMax, lstMSeed);

                            int intHitCount = intSourceMax - intCurrent;

                            lstNoTanseSeed.ForEach(delegate(NoTranseInfo noTransInfoDelegate)
                            {
                                Boolean bIsExist = false;

                                if (noTransInfoDelegate.lstNoTransInfo.Count == intHitCount)
                                {
                                    for (int i = 0; i < intHitCount; i++)
                                    {
                                        if (lstMSeed[intCurrent + i].DictionaryMembers["HYOSO_TYPE"].ToString().Equals(noTransInfoDelegate.lstNoTransInfo[i].DictionaryMembers["HYOSO_TYPE"].ToString()) 
                                            && lstMSeed[intCurrent + i].DictionaryMembers["HINSHI"].ToString().Equals(noTransInfoDelegate.lstNoTransInfo[i].DictionaryMembers["HINSHI"].ToString())
                                            && lstMSeed[intCurrent + i].DictionaryMembers["HINSHI_DETAIL_1"].ToString().Equals(noTransInfoDelegate.lstNoTransInfo[i].DictionaryMembers["HINSHI_DETAIL_1"].ToString())
                                            && lstMSeed[intCurrent + i].DictionaryMembers["HINSHI_DETAIL_2"].ToString().Equals(noTransInfoDelegate.lstNoTransInfo[i].DictionaryMembers["HINSHI_DETAIL_2"].ToString())
                                            && lstMSeed[intCurrent + i].DictionaryMembers["HINSHI_DETAIL_3"].ToString().Equals(noTransInfoDelegate.lstNoTransInfo[i].DictionaryMembers["HINSHI_DETAIL_3"].ToString())
                                            && lstMSeed[intCurrent + i].DictionaryMembers["KATSUYO_KEI"].ToString().Equals(noTransInfoDelegate.lstNoTransInfo[i].DictionaryMembers["KATSUYO_KEI"].ToString())
                                            && lstMSeed[intCurrent + i].DictionaryMembers["KATSUYO_TYPE"].ToString().Equals(noTransInfoDelegate.lstNoTransInfo[i].DictionaryMembers["KATSUYO_TYPE"].ToString())
                                            && lstMSeed[intCurrent + i].DictionaryMembers["BASE_TYPE"].ToString().Equals(noTransInfoDelegate.lstNoTransInfo[i].DictionaryMembers["BASE_TYPE"].ToString())
                                            && lstMSeed[intCurrent + i].DictionaryMembers["YOMI"].ToString().Equals(noTransInfoDelegate.lstNoTransInfo[i].DictionaryMembers["YOMI"].ToString())
                                            && lstMSeed[intCurrent + i].DictionaryMembers["HATSUON"].ToString().Equals(noTransInfoDelegate.lstNoTransInfo[i].DictionaryMembers["HATSUON"].ToString()))
                                        {
                                            bIsExist = true;
                                        }
                                    }
                                }

                                if (bIsExist)
                                {
                                    bHit = false;
                                }
                            });
                        }

                        if (bHit)
                        {
                            seedSelected.DictionaryMembers["HYOSO_TYPE"] = String.Empty;
                            seedSelected.DictionaryMembers["HINSHI"] = "*";
                            seedSelected.DictionaryMembers["HINSHI_DETAIL_1"] = "*";
                            seedSelected.DictionaryMembers["HINSHI_DETAIL_2"] = "*";
                            seedSelected.DictionaryMembers["HINSHI_DETAIL_3"] = "*";
                            seedSelected.DictionaryMembers["KATSUYO_KEI"] = "*";
                            seedSelected.DictionaryMembers["KATSUYO_TYPE"] = "*";
                            seedSelected.DictionaryMembers["BASE_TYPE"] = "*";
                            seedSelected.DictionaryMembers["YOMI"] = String.Empty;
                            seedSelected.DictionaryMembers["HATSUON"] = String.Empty;

                            for (int i = intCurrent; i < intSourceMax; i++)
                            {
                                lstSelectedSeeds.Add(lstMSeed[i]);
                                seedSelected.DictionaryMembers["HYOSO_TYPE"] += lstMSeed[i].DictionaryMembers["HYOSO_TYPE"].ToString();
                                seedSelected.DictionaryMembers["BASE_TYPE"] += lstMSeed[i].DictionaryMembers["BASE_TYPE"].ToString();
                                seedSelected.DictionaryMembers["YOMI"] += lstMSeed[i].DictionaryMembers["YOMI"].ToString();
                                seedSelected.DictionaryMembers["HATSUON"] += lstMSeed[i].DictionaryMembers["HATSUON"].ToString();
                            }

                            if (morphemeReplaceInfoEach.ListDestination[0].DictionaryMembers["HINSHI"].ToString().Length > 0)
                                seedSelected.DictionaryMembers["HINSHI"] = morphemeReplaceInfoEach.ListDestination[0].DictionaryMembers["HINSHI"].ToString();

                            if (morphemeReplaceInfoEach.ListDestination[0].DictionaryMembers["HINSHI_DETAIL_1"].ToString().Length > 0)
                                seedSelected.DictionaryMembers["HINSHI_DETAIL_1"] = morphemeReplaceInfoEach.ListDestination[0].DictionaryMembers["HINSHI_DETAIL_1"].ToString();

                            if (morphemeReplaceInfoEach.ListDestination[0].DictionaryMembers["HINSHI_DETAIL_2"].ToString().Length > 0)
                                seedSelected.DictionaryMembers["HINSHI_DETAIL_2"] = morphemeReplaceInfoEach.ListDestination[0].DictionaryMembers["HINSHI_DETAIL_2"].ToString();


                            if (morphemeReplaceInfoEach.ListDestination[0].DictionaryMembers["HINSHI_DETAIL_3"].ToString().Length > 0)
                                seedSelected.DictionaryMembers["HINSHI_DETAIL_3"] = morphemeReplaceInfoEach.ListDestination[0].DictionaryMembers["HINSHI_DETAIL_3"].ToString();

                            if (morphemeReplaceInfoEach.ListDestination[0].DictionaryMembers["KATSUYO_KEI"].ToString().Length > 0)
                                seedSelected.DictionaryMembers["KATSUYO_KEI"] = morphemeReplaceInfoEach.ListDestination[0].DictionaryMembers["KATSUYO_KEI"].ToString();

                            if (morphemeReplaceInfoEach.ListDestination[0].DictionaryMembers["KATSUYO_TYPE"].ToString().Length > 0)
                                seedSelected.DictionaryMembers["KATSUYO_TYPE"] = morphemeReplaceInfoEach.ListDestination[0].DictionaryMembers["KATSUYO_TYPE"].ToString();

                            int intCurrentWorkMove = intCurrent;
                            if (intCurrentWorkMove < lstvwMain.Items.Count - 5)
                                intCurrentWorkMove += 5;

                            lstvwMain.Items[intCurrentWorkMove].EnsureVisible();

                            //編集画面の表示
                            frmEditSeed frmEdit = new frmEditSeed();
                            frmEdit.ListHinshi = lstHinshi;
                            frmEdit.ListHinshiSub1 = lstHinshiSub1;
                            frmEdit.ListHinshiSub2 = lstHinshiSub2;
                            frmEdit.ListHinshiSub3 = lstHinshiSub3;
                            frmEdit.ListKatsuyo1 = lstKatsuyo1;
                            frmEdit.ListKatsuyo2 = lstKatsuyo2;
                            frmEdit.systemInfo = systemInfo;
                            frmEdit.ListMyLearnSeed = lstMyMSeed;
                            frmEdit.MySeedInfo = seedSelected;
                            frmEdit.ProcessMode = 0;
                            frmEdit.EditMode = 1;

                            if (!morphemeReplaceInfoEach.IsAuto)
                            {
                                frmEdit.ShowDialog();
                            }
                            else
                            {
                                lstMyMSeed.Add(seedSelected);
                            }

                            if (frmEdit.ReturnValue == 1 || morphemeReplaceInfoEach.IsAuto)
                            {
                                if (!morphemeReplaceInfoEach.IsAuto)
                                    seedSelected = frmEdit.MySeedInfoResult;

                                //先ず先頭の要素を置換する
                                for (int i = intCurrent; i < intSourceMax; i++)
                                {
                                    lstMSeed.RemoveAt(intCurrent);
                                }
                                lstMSeed.Insert(intCurrent, seedSelected);

                                //次に、同様に置換対象となる要素を洗い出す
                                List<int> lstIndexes = new List<int>();
                                int intIndexWork = 0;
                                while (intIndexWork < lstMSeed.Count)
                                {
                                    intIndexWork = lstMSeed.FindIndex(intIndexWork, delegate(MSeed mSeedMatch)
                                    {
                                        if (mSeedMatch.DictionaryMembers["HYOSO_TYPE"].ToString().Equals(lstSelectedSeeds[0].DictionaryMembers["HYOSO_TYPE"].ToString()) && mSeedMatch.DictionaryMembers["HINSHI"].ToString().Equals(lstSelectedSeeds[0].DictionaryMembers["HINSHI"].ToString()))
                                            return true;
                                        else
                                            return false;
                                    });

                                    if (intIndexWork > 0)
                                    {
                                        intIndexWork++;
                                        if (intIndexWork >= lstMSeed.Count)
                                            break;
                                        int intCountWork = 1;
                                        int intEnd = intIndexWork + lstSelectedSeeds.Count - 1;
                                        Boolean bHitWork = true;

                                        if (intEnd <= lstMSeed.Count - 1)
                                        {
                                            for (int i = intIndexWork; i < intEnd; i++)
                                            {
                                                if (!lstMSeed[i].DictionaryMembers["HYOSO_TYPE"].ToString().Equals(lstSelectedSeeds[intCountWork].DictionaryMembers["HYOSO_TYPE"].ToString()) || !lstMSeed[i].DictionaryMembers["HINSHI"].ToString().Equals(lstSelectedSeeds[intCountWork].DictionaryMembers["HINSHI"].ToString()))
                                                    bHitWork = false;

                                                intCountWork++;
                                            }

                                            if (bHitWork)
                                            {
                                                lstIndexes.Add(intIndexWork);
                                                intIndexWork += lstSelectedSeeds.Count;
                                            }
                                        }
                                    }
                                    else
                                        break;


                                }

                                if (lstIndexes.Count > 0)
                                {
                                    String strMessageConfirm = seedSelected.DictionaryMembers["HYOSO_TYPE"].ToString() + "   " + String.Format(XmlDatas.ListMessages["CONFIRM_3"], lstIndexes.Count.ToString());
                                    if (MessageBox.Show(strMessageConfirm, XmlDatas.ListNames["CONFIRM"], MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                                    {
                                        intIndexWork = 0;
                                        while (intIndexWork < lstMSeed.Count)
                                        {
                                            intIndexWork = lstMSeed.FindIndex(intIndexWork, delegate(MSeed mSeedMatch)
                                            {
                                                if (mSeedMatch.DictionaryMembers["HYOSO_TYPE"].ToString().Equals(lstSelectedSeeds[0].DictionaryMembers["HYOSO_TYPE"].ToString()) && mSeedMatch.DictionaryMembers["HINSHI"].ToString().Equals(lstSelectedSeeds[0].DictionaryMembers["HINSHI"].ToString()))
                                                    return true;
                                                else
                                                    return false;
                                            });

                                            if (intIndexWork > 0)
                                            {
                                                int intStartWork = intIndexWork;
                                                intIndexWork++;
                                                if (intIndexWork == lstSelectedSeeds.Count)
                                                    break;
                                                int intCountWork = 1;
                                                int intEnd = intIndexWork + lstSelectedSeeds.Count - 1;
                                                Boolean bHitWork = true;

                                                for (int i = intIndexWork; i < intEnd; i++)
                                                {
                                                    if (!lstMSeed[i].DictionaryMembers["HYOSO_TYPE"].ToString().Equals(lstSelectedSeeds[intCountWork].DictionaryMembers["HYOSO_TYPE"].ToString()) || !lstMSeed[i].DictionaryMembers["HINSHI"].ToString().Equals(lstSelectedSeeds[intCountWork].DictionaryMembers["HINSHI"].ToString()))
                                                        bHitWork = false;

                                                    intCountWork++;
                                                }

                                                if (bHitWork)
                                                {
                                                    for (int i = intStartWork; i < intEnd; i++)
                                                    {
                                                        lstMSeed.RemoveAt(intStartWork);
                                                    }

                                                    lstMSeed.Insert(intStartWork, seedSelected);

                                                    intIndexWork += lstSelectedSeeds.Count;
                                                }
                                            }
                                            else
                                                break;
                                        }
                                    }
                                }

                                if (!setMainListView(0))
                                    return;

                                intCurrent += morphemeReplaceInfoEach.ListSourceCount;

                                if (intSourceMax > lstMSeed.Count)
                                    intSourceMax = lstMSeed.Count;
                            }
                            else if (frmEdit.ReturnValue == 2)
                            {
                                intCurrent = lstMSeed.Count;
                                return;
                            }
                            else if (frmEdit.ReturnValue == -5)
                            {
                                if (MessageBox.Show(XmlDatas.ListMessages["CONFIRM_4"], XmlDatas.ListNames["CONFIRM"], MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                                {
                                    //置換対象外情報を追加する
                                    NoTranseInfo noTranseInfoWork = new NoTranseInfo();
                                    for (int i = intCurrent; i < intSourceMax; i++)
                                    {
                                        MSeed notransWotk = new MSeed(lstMSeed[i]);
                                        noTranseInfoWork.lstNoTransInfo.Add(notransWotk);
                                    }

                                    //既に追加されていなければ、追加する
                                    Boolean IsExist = true;
                                    if (lstNoTanseSeed.Count > 0)
                                    {
                                        lstNoTanseSeed.ForEach(delegate(NoTranseInfo noTransInfoDelegate)
                                        {
                                            if (noTranseInfoWork.lstNoTransInfo.Count == noTransInfoDelegate.lstNoTransInfo.Count)
                                            {
                                                for (int j = 0; j < noTranseInfoWork.lstNoTransInfo.Count; j++)
                                                {
                                                    if (noTranseInfoWork.lstNoTransInfo[j].DictionaryMembers["HYOSO_TYPE"].ToString().Equals(noTransInfoDelegate.lstNoTransInfo[j].DictionaryMembers["HYOSO_TYPE"].ToString()) && noTranseInfoWork.lstNoTransInfo[j].DictionaryMembers["HINSHI"].ToString().Equals(noTransInfoDelegate.lstNoTransInfo[j].DictionaryMembers["HINSHI"].ToString()))
                                                    {
                                                    }
                                                    else
                                                    {
                                                        IsExist = false;
                                                    }
                                                }
                                            }
                                        });
                                    }
                                    else
                                    {
                                        IsExist = false;
                                    }

                                    if (!IsExist)
                                    {
                                        lstNoTanseSeed.Add(noTranseInfoWork);
                                    }
                                }
                                else
                                {
                                    intCurrent++;
                                }
                            }
                            else
                            {
                                intCurrent++;
                            }
                        }
                        else
                        {
                            intCurrent++;
                        }
                    }
                }
            }
        }

        /*  指定したコーパスと指定した形態要素置換情報のヒット判定
         *  morphemeReplaceInfo:形態要素置換情報
         *  currentWrk:コーパスの本文中の位置を示すInex
         *  countWrk:形態要素置換情報のソースリストの位置を示すIndex
         */
        private Boolean isHitMorphemeReplaceInfo(MorphemeReplaceInfo morphemeReplaceInfo, int currentWrk, int countWrk)
        {
            Boolean bHit = true;

            if (!morphemeReplaceInfo.ListSource[countWrk].DictionaryMembers["HYOSO_TYPE"].Equals(String.Empty))
            {
                if (!lstMSeed[currentWrk].DictionaryMembers["HYOSO_TYPE"].Equals(morphemeReplaceInfo.ListSource[countWrk].DictionaryMembers["HYOSO_TYPE"]))
                {
                    bHit = false;
                }
            }
            else
            {
                //表層ファイルを使用する場合
                if (morphemeReplaceInfo.IsUseHyosoFile && morphemeReplaceInfo.HyosoIndex == countWrk)
                {
                    Boolean bHit2 = false;

                    morphemeReplaceInfo.ListHyoso.ForEach(delegate(String strHyosoWork)
                    {
                        if (strHyosoWork.Equals(lstMSeed[currentWrk].DictionaryMembers["HYOSO_TYPE"]))
                            bHit2 = true;
                    });

                    if (!bHit2)
                    {
                        bHit = false;
                        return bHit;
                    }
                }
            }

            //置換情報に品詞が含まれているか判定
            if (bHit)
                bHit = isMorphReplaceInfoHit(morphemeReplaceInfo, countWrk, currentWrk, "HINSHI", lstMSeed);

            //置換情報に品詞詳細1が含まれているか判定
            if (bHit)
                bHit = isMorphReplaceInfoHit(morphemeReplaceInfo, countWrk, currentWrk, "HINSHI_DETAIL_1", lstMSeed);

            //置換情報に品詞詳細2が含まれているか判定
            if (bHit)
                bHit = isMorphReplaceInfoHit(morphemeReplaceInfo, countWrk, currentWrk, "HINSHI_DETAIL_2", lstMSeed);

            //置換情報に品詞詳細3が含まれているか判定
            if (bHit)
                bHit = isMorphReplaceInfoHit(morphemeReplaceInfo, countWrk, currentWrk, "HINSHI_DETAIL_3", lstMSeed);

            //置換情報に活用形が含まれているか判定
            if (bHit)
                bHit = isMorphReplaceInfoHit(morphemeReplaceInfo, countWrk, currentWrk, "KATSUYO_KEI", lstMSeed);

            //置換情報に活用型が含まれているか判定
            if (bHit)
                bHit = isMorphReplaceInfoHit(morphemeReplaceInfo, countWrk, currentWrk, "KATSUYO_TYPE", lstMSeed);

            return bHit;
        }

        //置換情報の指定した属性が、形態要素リスト中の要素の指定した属性と同一か判定する
        private Boolean isMorphReplaceInfoHit(MorphemeReplaceInfo morphReplaceInfo, int intCount, int intLstCount, String itemName, List<MSeed> lstMSeedWork)
        {
            Boolean bHit = true;

            if (!morphReplaceInfo.ListSource[intCount].DictionaryMembers[itemName].Equals(String.Empty))
            {
                if (!lstMSeedWork[intLstCount].DictionaryMembers[itemName].Equals(morphReplaceInfo.ListSource[intCount].DictionaryMembers[itemName]))
                {
                    bHit = false;
                }
            }
            return bHit;
        }

        // DEBUG用。Hit判定とされた際の内容を出力する
        [Conditional("DEBUG")]
        private void outputMorphReplaceInfo(MorphemeReplaceInfo morphReplaceInfo, int currentCnt, int maxCnt, List<MSeed> lstMSeedWork)
        {
            Console.WriteLine("Hit判定");
            Console.WriteLine(morphReplaceInfo.MorphemeReplaceInfoName);
            Console.WriteLine("開始行：" + currentCnt);
            Console.WriteLine("終了行：" + maxCnt);
            for (int i = currentCnt; i <= maxCnt; i++)
            {
                Console.WriteLine(lstMSeedWork[i].DictionaryMembers["HYOSO_TYPE"].ToString());
            }
        }

        //検索実行時
        private void 検索FToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSearch frmsearch = new frmSearch(this);
            frmsearch.ShowDialog();

            lstvwMain.Focus();
        }

        //チェック処理
        private void btnCheck_Click(object sender, EventArgs e)
        {
            List<String> lstMessages = new List<String>();

            lstMSeed.ForEach(delegate(MSeed mSeedWork)
            {
                //EOSの場合
                if (mSeedWork.DictionaryMembers["HYOSO_TYPE"].ToString().Equals("EOS"))
                {
                }
                else
                {
                    //品詞のチェック
                    if (mSeedWork.DictionaryMembers["HINSHI"].ToString().Length < 1)
                    {
                        lstMessages.Add(mSeedWork.DictionaryMembers["HYOSO_TYPE"].ToString() + "の品詞がありません");
                    }

                    //品詞詳細1のチェック
                    if (mSeedWork.DictionaryMembers["HINSHI_DETAIL_1"].ToString().Length < 1)
                    {
                        lstMessages.Add(mSeedWork.DictionaryMembers["HYOSO_TYPE"].ToString() + "の品詞詳細1がありません");
                    }

                    //品詞詳細2のチェック
                    if (mSeedWork.DictionaryMembers["HINSHI_DETAIL_2"].ToString().Length < 1)
                    {
                        lstMessages.Add(mSeedWork.DictionaryMembers["HYOSO_TYPE"].ToString() + "の品詞詳細2がありません");
                    }

                    //品詞詳細3のチェック
                    if (mSeedWork.DictionaryMembers["HINSHI_DETAIL_3"].ToString().Length < 1)
                    {
                        lstMessages.Add(mSeedWork.DictionaryMembers["HYOSO_TYPE"].ToString() + "の品詞詳細3がありません");
                    }

                    //活用形のチェック
                    if (mSeedWork.DictionaryMembers["KATSUYO_KEI"].ToString().Length < 1)
                    {
                        lstMessages.Add(mSeedWork.DictionaryMembers["HYOSO_TYPE"].ToString() + "の活用形がありません");
                    }

                    //活用型のチェック
                    if (mSeedWork.DictionaryMembers["KATSUYO_TYPE"].ToString().Length < 1)
                    {
                        lstMessages.Add(mSeedWork.DictionaryMembers["HYOSO_TYPE"].ToString() + "の活用型がありません");
                    }

                    //基本型がある場合
                    if (mSeedWork.DictionaryMembers["BASE_TYPE"].ToString().Length > 0 && !mSeedWork.DictionaryMembers["BASE_TYPE"].ToString().Equals("*"))
                    {
                        //読みのチェック
                        if (mSeedWork.DictionaryMembers["YOMI"].ToString().Length < 1)
                        {
                            lstMessages.Add(mSeedWork.DictionaryMembers["HYOSO_TYPE"].ToString() + "の読みがありません");
                        }

                        //発音のチェック
                        if (mSeedWork.DictionaryMembers["HATSUON"].ToString().Length < 1)
                        {
                            lstMessages.Add(mSeedWork.DictionaryMembers["HYOSO_TYPE"].ToString() + "の発音がありません");
                        }
                    }
                }
            });

            if (lstMessages.Count > 0)
            {
                String strMessage = String.Empty;

                lstMessages.ForEach(delegate(String strWork)
                {
                    strMessage += strWork;
                    strMessage += "\n";
                });
                MessageBox.Show(strMessage);
            }
        }

        //ソースファイルを読み込んで、そのまま保存する
        private void ソース読み込みFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofdRead = new OpenFileDialog();
            ofdRead.Title = "ファイルを指定してください";
            ofdRead.InitialDirectory = systemInfo.ListMembers["SEED_DIRECTORY_PATH"].ToString();
            if (ofdRead.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                String strFileName = ofdRead.FileName;
                if (!System.IO.File.Exists(@strFileName))
                {
                    MessageBox.Show("ファイルが存在しません");
                    return;
                }

                String strResult = String.Empty;
                Boolean bResult = MyUtils.readFilesWithUtf8(@strFileName, ref strResult);

                if (bResult)
                {
                    StreamWriter strtest = null;

                    try {
                        strtest = new StreamWriter("C:\\work\\mecab\\test.txt", false, System.Text.Encoding.GetEncoding("utf-8"));
                        strtest.Write(strResult);
                    }
                    catch (IOException ioe)
                    {
                        MessageBox.Show(ioe.Message);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        if (strtest != null)
                        {
                            strtest.Close();
                            strtest = null;
                        }
                    }
                }
            }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        //編集要素選択時
        private void lstvwMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            int intSelectedCount = lstvwMain.SelectedIndices.Count;
            for (int i = intSelectedCount - 1; i >= 0; i--)
            {
                int intSelectedIndex = lstvwMain.SelectedIndices[i];
                if (lstMSeed[intSelectedIndex].DictionaryMembers["HYOSO_TYPE"].Equals("EOS"))
                {
                    btnEdit.Enabled = false;
                } else {
                    btnEdit.Enabled = true;
                }
            }
        }

        //コーパスファイル読込み
        private void コーパス読込みCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofdRead = new OpenFileDialog();
            ofdRead.Title = "ファイルを指定してください";
            ofdRead.InitialDirectory = systemInfo.ListMembers["SEED_DIRECTORY_PATH"].ToString();
            if (ofdRead.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                String strFileName = ofdRead.FileName;
                if (!System.IO.File.Exists(@strFileName))
                {
                    MessageBox.Show("ファイルが存在しません");
                    return;
                }

                lblFilePath.Text = strFileName;

                if (!createCopusListFromFile(@strFileName))
                    return;

                lblCopusInfo.Text = XmlDatas.ListNames["COPUS_NUMBER"] + ":" + lstMSeed.Count.ToString();
                bFileReaded = true;
                cmbEditMode.Enabled = true;
                cmbEditMode.SelectedIndex = 1;
                cmbEditMode.SelectedIndex = 0;
                上書き保存SToolStripMenuItem.Enabled = true;
                検索FToolStripMenuItem.Enabled = true;
            }

            lstNoTanseSeed.Clear();
        }
    }

    //変換しない情報リスト
    public class NoTranseInfo
    {
        //メンバ
        public List<MSeed> lstNoTransInfo = new List<MSeed>();

        //コンストラクタ
        public NoTranseInfo()
        {
        }
    }

}
