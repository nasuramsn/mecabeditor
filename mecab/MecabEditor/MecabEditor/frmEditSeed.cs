using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MecabEditor
{
    public partial class frmEditSeed : Form
    {
        //メンバ
        //private SeedInfo mySeedInfo = new SeedInfo();
        private MSeed mySeedInfo = new MSeed();
        private MSeed mySeedInfoResult = new MSeed();
        private Dictionary<String, Boolean> dicIsEdited = new Dictionary<String, Boolean>();
        private int intProcessMode = -1;            //作業モード 0:形態要素編集 1:形態要素置換情報編集
        private int intEditMode = -1;               //編集モード 0:追加 1:編集
        private int intAnalyzeMode = -1;            //表示モード 0:分析 1:学習済
        private int intReturnValue = -1;

        //品詞情報など
        private List<MHinshi> lstHinshi = new List<MHinshi>();
        private List<MHinshi> lstHinshiSub1 = new List<MHinshi>();
        private List<MHinshi> lstHinshiSub2 = new List<MHinshi>();
        private List<MHinshi> lstHinshiSub3 = new List<MHinshi>();
        private List<MKatsuyo> lstKatsuyo1 = new List<MKatsuyo>();
        private List<MKatsuyo> lstKatsuyo2 = new List<MKatsuyo>();

        //選択時用
        private List<MHinshi> lstHinSub1Selected = new List<MHinshi>();
        private List<MHinshi> lstHinSub2Selected = new List<MHinshi>();
        private List<MHinshi> lstHinSub3Selected = new List<MHinshi>();
        private List<MKatsuyo> lstKatsuyo1Selected = new List<MKatsuyo>();
        private List<MKatsuyo> lstKatsuyo2Selected = new List<MKatsuyo>();
        
        private List<MSeed> lstAnalyzeSeed = new List<MSeed>();
        private List<MSeed> lstMyLearnSeed = new List<MSeed>();
        private List<MSeed> lstMyLearnSeedResult = new List<MSeed>();

        private SystemInfo sysInfo = new SystemInfo();
        private MySqlControl mySqlControl = new MySqlControl();

        public frmEditSeed()
        {
            InitializeComponent();
        }

        //Form表示後
        private void frmEditCsv_Shown(object sender, EventArgs e)
        {
            //品詞の設定
            cmbHinshi.Items.Clear();
            cmbHinshi.Items.Add(String.Empty);
            lstHinshi.ForEach(delegate(MHinshi mHinshiWork)
            {
                cmbHinshi.Items.Add(mHinshiWork.Name);
            });
            cmbHinshi.DropDownStyle = ComboBoxStyle.DropDown;
            cmbHinshi.SelectedIndex = 0;

            //品詞詳細1の設定
            setHinshiComboBoxes(cmbHinshiDetail1, lstHinshiSub1, lstHinSub1Selected, lstHinshi[0].Id, 1);

            //品詞詳細2の設定
            setHinshiComboBoxes(cmbHinshiDetail2, lstHinshiSub2, lstHinSub2Selected, lstHinSub1Selected[0].Id, 2);

            //品詞詳細3の設定
            setHinshiComboBoxes(cmbHinshiDetail3, lstHinshiSub3, lstHinSub3Selected, lstHinSub2Selected[0].Id, 3);

            //活用形の設定
            setKatsuyoComboBoxes(cmbKatsuyoukei, lstKatsuyo1, lstKatsuyo1Selected, lstHinshi[0].Id, 1);
            
            //活用型の設定
            setKatsuyoComboBoxes(cmbKatsuyoType, lstKatsuyo2, lstKatsuyo2Selected, lstKatsuyo1[0].Id, 2);

            lstvwAnalyze.Clear();
            lstvwAnalyze.Columns.Add(XmlDatas.ListNames["HYOSO_TYPE"]);
            lstvwAnalyze.Columns.Add(XmlDatas.ListNames["HINSHI"]);
            lstvwAnalyze.Columns.Add(XmlDatas.ListNames["HINSHI_DETAIL_1"]);
            lstvwAnalyze.Columns.Add(XmlDatas.ListNames["HINSHI_DETAIL_2"]);
            lstvwAnalyze.Columns.Add(XmlDatas.ListNames["HINSHI_DETAIL_3"]);
            lstvwAnalyze.Columns.Add(XmlDatas.ListNames["KATSUYO_KEI"]);
            lstvwAnalyze.Columns.Add(XmlDatas.ListNames["KATSUYO_TYPE"]);
            lstvwAnalyze.Columns.Add(XmlDatas.ListNames["BASE_TYPE"]);
            lstvwAnalyze.Columns.Add(XmlDatas.ListNames["YOMI"]);
            lstvwAnalyze.Columns.Add(XmlDatas.ListNames["HATSUON"]);
            lstvwAnalyze.Columns[0].Width = 150;
            lstvwAnalyze.Columns[7].Width = 150;
            lstvwAnalyze.Columns[8].Width = 150;
            lstvwAnalyze.Columns[9].Width = 150;

            //編集の場合の処理
            if (intEditMode == 1)
            {
                if (!SetInfosFromMSeedToControls(mySeedInfo))
                {
                    Close();
                    return;
                }

                mySeedInfoResult = new MSeed(mySeedInfo);
            }

            ControlButtons(intProcessMode);
        }

        //保存ボタンクリック時
        private void btnSave_Click(object sender, EventArgs e)
        {
            String strMyLearnCsvFilePath = sysInfo.ListMembers["CSV_DIRECTORY_PATH"].ToString() + "\\" + XmlDatas.ListConsts["LEARN_CSV_FILE"].ToString();

            try
            {
                //品詞
                lstHinshi.ForEach(delegate(MHinshi mHinshiWork)
                {
                    if (mHinshiWork.CreatedAt.Equals(new DateTime(0)) && mHinshiWork.UpdatedAt.Equals(new DateTime(0)))
                    {
                        if (mySqlControl.insert("insert into M_HINSHI values (" + mHinshiWork.Id + ", '" + mHinshiWork.Name + "', "
                            + mHinshiWork.Level + ", " + mHinshiWork.ParentId + ", Now(), Now(), " + mHinshiWork.DeleteFlg + ")"))
                        {
                            DataTable dt = mySqlControl.selectForList("select * from M_HINSHI where HINSHI_NO = " + mHinshiWork.Id);
                            foreach (DataRow item in dt.Rows)
                            {
                                mHinshiWork.CreatedAt = DateTime.Parse(item["CREATED_AT"].ToString());
                                mHinshiWork.UpdatedAt = DateTime.Parse(item["UPDATED_AT"].ToString());
                            }
                        }
                    }
                });

                //品詞詳細1
                lstHinSub1Selected.ForEach(delegate(MHinshi mHinshiSub1Work)
                {
                    if (mHinshiSub1Work.CreatedAt.Equals(new DateTime(0)) && mHinshiSub1Work.UpdatedAt.Equals(new DateTime(0)))
                    {
                        if (mySqlControl.insert("insert into M_HINSHI values (" + mHinshiSub1Work.Id + ", '" + mHinshiSub1Work.Name + "', "
                            + mHinshiSub1Work.Level + ", " + mHinshiSub1Work.ParentId + ", Now(), Now(), " + mHinshiSub1Work.DeleteFlg + ")"))
                        {
                            DataTable dt = mySqlControl.selectForList("select * from M_HINSHI where HINSHI_NO = " + mHinshiSub1Work.Id);
                            foreach (DataRow item in dt.Rows)
                            {
                                mHinshiSub1Work.CreatedAt = DateTime.Parse(item["CREATED_AT"].ToString());
                                mHinshiSub1Work.UpdatedAt = DateTime.Parse(item["UPDATED_AT"].ToString());
                            }
                        }
                    }
                });

                //品詞詳細2
                lstHinSub2Selected.ForEach(delegate(MHinshi mHinshiSub2Work)
                {
                    if (mHinshiSub2Work.CreatedAt.Equals(new DateTime(0)) && mHinshiSub2Work.UpdatedAt.Equals(new DateTime(0)))
                    {
                        if (mySqlControl.insert("insert into M_HINSHI values (" + mHinshiSub2Work.Id + ", '" + mHinshiSub2Work.Name + "', "
                            + mHinshiSub2Work.Level + ", " + mHinshiSub2Work.ParentId + ", Now(), Now(), " + mHinshiSub2Work.DeleteFlg + ")"))
                        {
                            DataTable dt = mySqlControl.selectForList("select * from M_HINSHI where HINSHI_NO = " + mHinshiSub2Work.Id);
                            foreach (DataRow item in dt.Rows)
                            {
                                mHinshiSub2Work.CreatedAt = DateTime.Parse(item["CREATED_AT"].ToString());
                                mHinshiSub2Work.UpdatedAt = DateTime.Parse(item["UPDATED_AT"].ToString());
                            }
                        }
                    }
                });

                //品詞詳細3
                lstHinSub3Selected.ForEach(delegate(MHinshi mHinshiSub3Work)
                {
                    if (mHinshiSub3Work.CreatedAt.Equals(new DateTime(0)) && mHinshiSub3Work.UpdatedAt.Equals(new DateTime(0)))
                    {
                        if (mySqlControl.insert("insert into M_HINSHI values (" + mHinshiSub3Work.Id + ", '" + mHinshiSub3Work.Name + "', "
                            + mHinshiSub3Work.Level + ", " + mHinshiSub3Work.ParentId + ", Now(), Now(), " + mHinshiSub3Work.DeleteFlg + ")"))
                        {
                            DataTable dt = mySqlControl.selectForList("select * from M_HINSHI where HINSHI_NO = " + mHinshiSub3Work.Id);
                            foreach (DataRow item in dt.Rows)
                            {
                                mHinshiSub3Work.CreatedAt = DateTime.Parse(item["CREATED_AT"].ToString());
                                mHinshiSub3Work.UpdatedAt = DateTime.Parse(item["UPDATED_AT"].ToString());
                            }
                        }
                    }
                });

                //活用形
                lstKatsuyo1Selected.ForEach(delegate(MKatsuyo mKatsuyou1Work)
                {
                    if (mKatsuyou1Work.CreatedAt.Equals(new DateTime(0)) && mKatsuyou1Work.UpdatedAt.Equals(new DateTime(0)))
                    {
                        if (mySqlControl.insert("insert into M_KATSUYO values (" + mKatsuyou1Work.Id + ", '" + mKatsuyou1Work.Name + "', "
                            + mKatsuyou1Work.Level + ", " + mKatsuyou1Work.ParentId + ", Now(), Now(), " + mKatsuyou1Work.DeleteFlg + ")"))
                        {
                            DataTable dt = mySqlControl.selectForList("select * from M_HINSHI where HINSHI_NO = " + mKatsuyou1Work.Id);
                            foreach (DataRow item in dt.Rows)
                            {
                                mKatsuyou1Work.CreatedAt = DateTime.Parse(item["CREATED_AT"].ToString());
                                mKatsuyou1Work.UpdatedAt = DateTime.Parse(item["UPDATED_AT"].ToString());
                            }
                        }
                    }
                });

                //活用型
                lstKatsuyo2Selected.ForEach(delegate(MKatsuyo mKatsuyou2Work)
                {
                    if (mKatsuyou2Work.CreatedAt.Equals(new DateTime(0)) && mKatsuyou2Work.UpdatedAt.Equals(new DateTime(0)))
                    {
                        if (mySqlControl.insert("insert into M_KATSUYO values (" + mKatsuyou2Work.Id + ", '" + mKatsuyou2Work.Name + "', "
                            + mKatsuyou2Work.Level + ", " + mKatsuyou2Work.ParentId + ", Now(), Now(), " + mKatsuyou2Work.DeleteFlg + ")"))
                        {
                            DataTable dt = mySqlControl.selectForList("select * from M_HINSHI where HINSHI_NO = " + mKatsuyou2Work.Id);
                            foreach (DataRow item in dt.Rows)
                            {
                                mKatsuyou2Work.CreatedAt = DateTime.Parse(item["CREATED_AT"].ToString());
                                mKatsuyou2Work.UpdatedAt = DateTime.Parse(item["UPDATED_AT"].ToString());
                            }
                        }
                    }
                });

                //学習用Csv
                lstMyLearnSeedResult.Clear();
                lstMyLearnSeed.ForEach(delegate(MSeed mSeedWork)
                {
                    lstMyLearnSeedResult.Add(mSeedWork);
                });

                if (!MyUtils.outputCsvFile(strMyLearnCsvFilePath, lstMyLearnSeedResult))
                    return;
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
                
            }

            intReturnValue = 1;
            mySeedInfoResult = new MSeed(mySeedInfo);

            Close();
        }

        //入力チェック
        private Boolean inputCheck()
        {
            Boolean bResult = true;
            List<String> lstMessages = new List<String>();

            //表層系
            txtHyosokei.BackColor = XmlDatas.ListColors["NORMAL_TEXT"];

            if (intProcessMode == 0)
            {
                if (txtHyosokei.Text.Length == 0)
                {
                    lstMessages.Add(String.Format(XmlDatas.ListMessages["ERROR_3"].ToString(), XmlDatas.ListNames["HYOSO_TYPE"].ToString()));
                    txtHyosokei.BackColor = XmlDatas.ListColors["ERROR_TEXT"];
                    bResult = false;
                }
            }

            //品詞
            cmbHinshi.BackColor = XmlDatas.ListColors["NORMAL_TEXT"];

            if (intProcessMode == 0)
            {
                if (cmbHinshi.Text.Length == 0)
                {
                    lstMessages.Add(String.Format(XmlDatas.ListMessages["ERROR_3"].ToString(), XmlDatas.ListNames["HINSHI"].ToString()));
                    cmbHinshi.BackColor = XmlDatas.ListColors["ERROR_TEXT"];
                    bResult = false;
                }
            }

            //品詞詳細1
            cmbHinshiDetail1.BackColor = XmlDatas.ListColors["NORMAL_TEXT"];

            if (intProcessMode == 0)
            {
                if (cmbHinshiDetail1.Text.Length == 0)
                {
                    lstMessages.Add(String.Format(XmlDatas.ListMessages["ERROR_3"].ToString(), XmlDatas.ListNames["HINSHI_DETAIL_1"].ToString()));
                    cmbHinshiDetail1.BackColor = XmlDatas.ListColors["ERROR_TEXT"];
                    bResult = false;
                }
            }

            //品詞詳細2
            cmbHinshiDetail2.BackColor = XmlDatas.ListColors["NORMAL_TEXT"];

            if (intProcessMode == 0)
            {
                if (cmbHinshiDetail2.Text.Length == 0)
                {
                    lstMessages.Add(String.Format(XmlDatas.ListMessages["ERROR_3"].ToString(), XmlDatas.ListNames["HINSHI_DETAIL_2"].ToString()));
                    cmbHinshiDetail2.BackColor = XmlDatas.ListColors["ERROR_TEXT"];
                    bResult = false;
                }
            }

            //品詞詳細3
            cmbHinshiDetail3.BackColor = XmlDatas.ListColors["NORMAL_TEXT"];

            if (intProcessMode == 0)
            {
                if (cmbHinshiDetail3.Text.Length == 0)
                {
                    lstMessages.Add(String.Format(XmlDatas.ListMessages["ERROR_3"].ToString(), XmlDatas.ListNames["HINSHI_DETAIL_3"].ToString()));
                    cmbHinshiDetail3.BackColor = XmlDatas.ListColors["ERROR_TEXT"];
                    bResult = false;
                }
            }

            //活用形
            cmbKatsuyoukei.BackColor = XmlDatas.ListColors["NORMAL_TEXT"];

            if (intProcessMode == 0)
            {
                if (cmbKatsuyoukei.Text.Length == 0)
                {
                    lstMessages.Add(String.Format(XmlDatas.ListMessages["ERROR_3"].ToString(), XmlDatas.ListNames["KATSUYO_KEI"].ToString()));
                    cmbKatsuyoukei.BackColor = XmlDatas.ListColors["ERROR_TEXT"];
                    bResult = false;
                }
            }

            //活用型
            cmbKatsuyoType.BackColor = XmlDatas.ListColors["NORMAL_TEXT"];

            if (intProcessMode == 0)
            {
                if (cmbKatsuyoukei.Text.Length == 0)
                {
                    lstMessages.Add(String.Format(XmlDatas.ListMessages["ERROR_3"].ToString(), XmlDatas.ListNames["KATSUYO_TYPE"].ToString()));
                    cmbKatsuyoType.BackColor = XmlDatas.ListColors["ERROR_TEXT"];
                    bResult = false;
                }
            }

            //基本型
            txtBaseType.BackColor = XmlDatas.ListColors["NORMAL_TEXT"];

            if (intProcessMode == 0)
            {
                if (txtBaseType.Text.Length == 0)
                {
                    lstMessages.Add(String.Format(XmlDatas.ListMessages["ERROR_3"].ToString(), XmlDatas.ListNames["BASE_TYPE"].ToString()));
                    txtBaseType.BackColor = XmlDatas.ListColors["ERROR_TEXT"];
                    bResult = false;
                }
            }

            if (intProcessMode == 1)
            {
                if (cmbHinshi.Text.Length == 0 && cmbHinshiDetail1.Text.Length == 0 && cmbHinshiDetail2.Text.Length == 0 && cmbHinshiDetail3.Text.Length == 0 && cmbKatsuyoukei.Text.Length == 0 && cmbKatsuyoukei.Text.Length == 0 && txtBaseType.Text.Length == 0)
                {
                    lstMessages.Add((XmlDatas.ListMessages["ERROR_6"].ToString()));
                    cmbHinshi.BackColor = XmlDatas.ListColors["ERROR_TEXT"];
                    cmbHinshiDetail1.BackColor = XmlDatas.ListColors["ERROR_TEXT"];
                    cmbHinshiDetail2.BackColor = XmlDatas.ListColors["ERROR_TEXT"];
                    cmbHinshiDetail3.BackColor = XmlDatas.ListColors["ERROR_TEXT"];
                    cmbKatsuyoukei.BackColor = XmlDatas.ListColors["ERROR_TEXT"];
                    cmbKatsuyoType.BackColor = XmlDatas.ListColors["ERROR_TEXT"];
                    txtBaseType.BackColor = XmlDatas.ListColors["ERROR_TEXT"];
                    bResult = false;
                }
            }

            String strMessage = String.Empty;
            if (!bResult)
            {
                lstMessages.ForEach(delegate(String strMessageWork)
                {
                    strMessage = strMessage + strMessageWork + "\n";
                });
                MessageBox.Show(strMessage);
            }

            return bResult;
        }

        //品詞用コンボボックス類設定
        private void setHinshiComboBoxes(ComboBox comboBox, List<MHinshi> srcList, List<MHinshi> destList, int parentId, int level)
        {
            destList.Clear();
            comboBox.Items.Clear();
            comboBox.Items.Add(String.Empty);
            srcList.ForEach(delegate(MHinshi mHinshiWork)
            {
                if (mHinshiWork.ParentId == parentId)
                {
                    destList.Add(mHinshiWork);
                    comboBox.Items.Add(mHinshiWork.Name);
                }
            });
            // 該当データがない場合
            if (destList.Count == 0)
            {
                MHinshi wrkHinshi = new MHinshi();
                wrkHinshi.ParentId = parentId;
                wrkHinshi.Id = wrkHinshi.ParentId * 100 + destList.Count + 1;
                wrkHinshi.Level = level;
                wrkHinshi.Name = "*";
                wrkHinshi.CreatedAt = DateTime.Now;
                wrkHinshi.UpdatedAt = DateTime.Now;
                wrkHinshi.DeleteFlg = "0";
                destList.Add(wrkHinshi);
                srcList.Add(wrkHinshi);
                comboBox.Items.Add(wrkHinshi.Name);
            }
            comboBox.DropDownStyle = ComboBoxStyle.DropDown;
            comboBox.SelectedIndex = 0;
        }

        // 品詞リストとコンボボックスのチェック。なければ品詞リストとコンボボックスに追加する
        private int checkHinshiListWithCombo(ComboBox comboBox, List<MHinshi> lstSrc, int hinshiLevel, int parentId, String hinshiName, List<MHinshi> lstDest = null)
        {
            // 以下で "indexHinshi = lstDest.Count;"となっているのは、lstDestにはComboBoxにある空白がないため
            int indexHinshi = -1;
            if (lstDest != null)
                indexHinshi = lstDest.FindIndex(x => x.Name.Equals(hinshiName));
            else
                indexHinshi = lstSrc.FindIndex(x => x.Name.Equals(hinshiName));

            if (indexHinshi > -1)
                comboBox.SelectedIndex = indexHinshi + 1;
            else
            {
                if (!hinshiName.Equals(String.Empty)) 
                {
                    MHinshi mNewHinshi = new MHinshi();
                    mNewHinshi.ParentId = parentId;
                    if (hinshiLevel == 0)
                        mNewHinshi.Id = lstSrc.Count;
                    else
                        mNewHinshi.Id = (mNewHinshi.ParentId * 100) + lstDest.Count + 1;
                    mNewHinshi.Name = hinshiName;
                    mNewHinshi.Level = hinshiLevel;
                    mNewHinshi.CreatedAt = DateTime.Now;
                    mNewHinshi.UpdatedAt = DateTime.Now;
                    mNewHinshi.DeleteFlg = "0";

                    lstSrc.Add(mNewHinshi);
                    comboBox.Items.Add(hinshiName);
                    indexHinshi = lstSrc.Count;
                    if (lstDest != null)
                    {
                        lstDest.Add(mNewHinshi);
                        indexHinshi = lstDest.Count;
                    }
                    comboBox.SelectedIndex = indexHinshi;
                }
            }

            return indexHinshi;
        }

        //活用用コンボボックス類設定
        private void setKatsuyoComboBoxes(ComboBox comboBox, List<MKatsuyo> srcList, List<MKatsuyo> destList, int parentId, int level)
        {
            destList.Clear();
            comboBox.Items.Clear();
            comboBox.Items.Add(String.Empty);
            srcList.ForEach(delegate(MKatsuyo mKatsuyoWork)
            {
                if (mKatsuyoWork.ParentId == parentId)
                {
                    destList.Add(mKatsuyoWork);
                    comboBox.Items.Add(mKatsuyoWork.Name);
                }
            });
            // 該当データがない場合
            if (destList.Count == 0)
            {
                MKatsuyo wrkKatsuyo = new MKatsuyo();
                wrkKatsuyo.ParentId = parentId;
                wrkKatsuyo.Id = wrkKatsuyo.ParentId * 100 + destList.Count + 1;
                wrkKatsuyo.Level = level;
                wrkKatsuyo.Name = "*";
                wrkKatsuyo.CreatedAt = DateTime.Now;
                wrkKatsuyo.UpdatedAt = DateTime.Now;
                wrkKatsuyo.DeleteFlg = "0";
                destList.Add(wrkKatsuyo);
                srcList.Add(wrkKatsuyo);
                comboBox.Items.Add(wrkKatsuyo.Name);
            }
            comboBox.DropDownStyle = ComboBoxStyle.DropDown;
            comboBox.SelectedIndex = 0;
        }

        // 活用リストとコンボボックスのチェック。なければ活用リストとコンボボックスに追加する
        private int checkKatsuyoListWithCombo(ComboBox comboBox, List<MKatsuyo> lstSrc, int katsuyoLevel, int parentId, String katsuyoName, List<MKatsuyo> lstDest = null)
        {
            // 以下で "indexKatsuyo = lstDest.Count;"となっているのは、lstDestにはComboBoxにある空白がないため
            int indexKatsuyo = -1;
            if (lstDest != null)
                indexKatsuyo = lstDest.FindIndex(x => x.Name.Equals(katsuyoName));
            else
                indexKatsuyo = lstSrc.FindIndex(x => x.Name.Equals(katsuyoName));

            if (indexKatsuyo > -1)
                comboBox.SelectedIndex = indexKatsuyo + 1;
            else
            {
                MKatsuyo mNewKatsuyo = new MKatsuyo();
                mNewKatsuyo.ParentId = parentId;
                if (katsuyoLevel == 0)
                    mNewKatsuyo.Id = lstSrc.Count;
                else
                    mNewKatsuyo.Id = (mNewKatsuyo.ParentId * 100) + lstDest.Count + 1;
                mNewKatsuyo.Name = katsuyoName;
                mNewKatsuyo.Level = katsuyoLevel;
                mNewKatsuyo.CreatedAt = DateTime.Now;
                mNewKatsuyo.UpdatedAt = DateTime.Now;
                mNewKatsuyo.DeleteFlg = "0";

                lstSrc.Add(mNewKatsuyo);
                comboBox.Items.Add(katsuyoName);
                indexKatsuyo = lstSrc.Count;
                if (lstDest != null)
                {
                    lstDest.Add(mNewKatsuyo);
                    indexKatsuyo = lstDest.Count;
                }
                comboBox.SelectedIndex = indexKatsuyo;
            }

            return indexKatsuyo;
        }

        //閉じるボタンクリック時
        private void btnClose_Click(object sender, EventArgs e)
        {
            mySeedInfo = new MSeed(mySeedInfoResult);

            if (chkNoTranse.Checked)
                intReturnValue = -5;

            Close();
        }

        //getter, setter

        //Seed情報
        //public SeedInfo MySeedInfo
        public MSeed MySeedInfo
        {
            get { return mySeedInfo; }
            set { mySeedInfo = value; }
        }

        //品詞リスト
        public List<MHinshi> ListHinshi
        {
            get { return lstHinshi; }
            set { lstHinshi = value; }
        }

        //品詞詳細1リスト
        public List<MHinshi> ListHinshiSub1
        {
            get { return lstHinshiSub1; }
            set { lstHinshiSub1 = value; }
        }

        //品詞詳細2リスト
        public List<MHinshi> ListHinshiSub2
        {
            get { return lstHinshiSub2; }
            set { lstHinshiSub2 = value; }
        }

        //品詞詳細3リスト
        public List<MHinshi> ListHinshiSub3
        {
            get { return lstHinshiSub3; }
            set { lstHinshiSub3 = value; }
        }

        //活用形リスト
        public List<MKatsuyo> ListKatsuyo1
        {
            get { return lstKatsuyo1; }
            set { lstKatsuyo1 = value; }
        }

        //活用型リスト
        public List<MKatsuyo> ListKatsuyo2
        {
            get { return lstKatsuyo2; }
            set { lstKatsuyo2 = value; }
        }

        //品詞詳細1リスト選択後
        public List<MHinshi> ListHinSub1Selected
        {
            get { return lstHinSub1Selected; }
            set { lstHinSub1Selected = value; }
        }

        //品詞詳細2リスト選択後
        public List<MHinshi> ListHinSub2Selected
        {
            get { return lstHinSub2Selected; }
            set { lstHinSub2Selected = value; }
        }

        //品詞詳細3リスト選択後
        public List<MHinshi> ListHinSub3Selected
        {
            get { return lstHinSub3Selected; }
            set { lstHinSub3Selected = value; }
        }

        //活用形リスト選択後
        public List<MKatsuyo> ListKatsuyo1Selected
        {
            get { return lstKatsuyo1Selected; }
            set { lstKatsuyo1Selected = value; }
        }

        //活用型リスト選択後
        public List<MKatsuyo> ListKatsuyo2Selected
        {
            get { return lstKatsuyo2Selected; }
            set { lstKatsuyo2Selected = value; }
        }

        //作業モード
        public int ProcessMode
        {
            set { intProcessMode = value; }
        }

        //編集モード
        public int EditMode
        {
            get { return intEditMode; }
            set { intEditMode = value; }
        }

        //学習用リスト
        public List<MSeed> ListMyLearnSeed
        {
            set { lstMyLearnSeed = value; }
        }

        //学習用リスト(保存用)
        public List<MSeed> ListMyLearnSeedResult
        {
            get { return lstMyLearnSeedResult; }
        }

        //表層系テキストボックスの値
        /*public String TxtHyosokei
        {
            get { return txtHyosokei.Text; }
            set { txtHyosokei.Text = value; }
        }

        //左文脈IDテキストボックスの値
        public String TxtLeftConnectStatusNo
        {
            get { return txtLeftConnectStatusNo.Text }
            set { txtLeftConnectStatusNo.Text = value; }
        }

        //右文脈IDテキストボックスの値
        public String TxtRightConnectStatusNo
        {
            get { return txtRightConnectStatusNo.Text }
            set { txtRightConnectStatusNo.Text = value; }
        }

        //コストテキストボックスの値
        public String TxtCost
        {
            get { return txtCost.Text }
            set { txtCost.Text = value; }
        }*/

        //返り値
        public int ReturnValue
        {
            get { return intReturnValue; }
            set { intReturnValue = value; }
        }

        //システム情報
        public SystemInfo systemInfo
        {
            set { sysInfo = value; }
        }

        //編集結果
        public MSeed MySeedInfoResult
        {
            get { return mySeedInfoResult; }
            set { mySeedInfoResult = value; }
        }

        //解析開始
        private void btnAnalyze_Click(object sender, EventArgs e)
        {
            String strBatchWork = Application.StartupPath + "\\" + "temp.bat";
            String strSourceWork = Application.StartupPath + "\\" + "temp.txt";
            String strDistWork = Application.StartupPath + "\\" + "tempdist.txt";
            
            StreamWriter strwtrBatchWork = null;
            StreamWriter strwtrSourceWork = null;
            StreamReader strrdrDistWork = null;

            try
            {
                strwtrSourceWork = new StreamWriter(@strSourceWork, false, Encoding.GetEncoding("utf-8"));
                strwtrSourceWork.WriteLine(txtHyosokei.Text);
                strwtrSourceWork.Close();

                strwtrBatchWork = new StreamWriter(@strBatchWork, false, Encoding.GetEncoding("sjis"));
                strwtrBatchWork.WriteLine("C:\\MeCab\\bin\\mecab.exe -a " + strSourceWork + " -o " + strDistWork);
                strwtrBatchWork.WriteLine("Exit");
                strwtrBatchWork.Close();

                var startInfo = new ProcessStartInfo();
                startInfo.FileName = System.Environment.GetEnvironmentVariable("ComSpec");
                startInfo.CreateNoWindow = true;
                startInfo.UseShellExecute = false;
                startInfo.Arguments = "/c " + @strBatchWork;

                Process process = Process.Start(startInfo);
                process.WaitForExit();
                process.Close();

                strrdrDistWork = new StreamReader(@strDistWork, Encoding.GetEncoding("utf-8"));
                int intCountWork = 0;
                String strLineWork = String.Empty;
                lstAnalyzeSeed.Clear();
                while (!strrdrDistWork.EndOfStream)
                {
                    strLineWork = strrdrDistWork.ReadLine();

                    if (intCountWork > 0)
                    {
                        MSeed seedWork = new MSeed();
                        if (seedWork.getFromCsvFile(strLineWork))
                        {
                            lstAnalyzeSeed.Add(seedWork);
                        }
                    }

                    intCountWork++;
                }

                intAnalyzeMode = 0;
                lstvwAnalyze.Items.Clear();
                lstAnalyzeSeed.ForEach(delegate(MSeed seedWork)
                {
                    String[] strLineList = { seedWork.DictionaryMembers[XmlDatas.ListItemNames["HYOSO_TYPE"]].ToString(), seedWork.DictionaryMembers[XmlDatas.ListItemNames["HINSHI"]].ToString(), seedWork.DictionaryMembers[XmlDatas.ListItemNames["HINSHI_DETAIL_1"]].ToString(), seedWork.DictionaryMembers[XmlDatas.ListItemNames["HINSHI_DETAIL_2"]].ToString(), seedWork.DictionaryMembers[XmlDatas.ListItemNames["HINSHI_DETAIL_3"]].ToString(), seedWork.DictionaryMembers[XmlDatas.ListItemNames["KATSUYO_KEI"]].ToString(), seedWork.DictionaryMembers[XmlDatas.ListItemNames["KATSUYO_TYPE"]].ToString(), seedWork.DictionaryMembers[XmlDatas.ListItemNames["BASE_TYPE"]].ToString(), seedWork.DictionaryMembers[XmlDatas.ListItemNames["YOMI"]].ToString(), seedWork.DictionaryMembers[XmlDatas.ListItemNames["HATSUON"]].ToString() };
                    lstvwAnalyze.Items.Add(new ListViewItem(strLineList));
                });
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
                if (strwtrBatchWork != null)
                {
                    strwtrBatchWork.Close();
                    strwtrBatchWork.Dispose();
                }

                if (strwtrSourceWork != null)
                {
                    strwtrSourceWork.Close();
                    strwtrSourceWork.Dispose();
                }

                if (strrdrDistWork != null)
                {
                    strrdrDistWork.Close();
                    strrdrDistWork.Dispose();
                }
            }
        }

        //追加ボタン
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (inputCheck())
            {
                //Seed情報設定
                mySeedInfo.DictionaryMembers["HYOSO_TYPE"] = txtHyosokei.Text;
                mySeedInfo.DictionaryMembers["HINSHI"] = cmbHinshi.Text;
                mySeedInfo.DictionaryMembers["HINSHI_DETAIL_1"] = cmbHinshiDetail1.Text;
                mySeedInfo.DictionaryMembers["HINSHI_DETAIL_2"] = cmbHinshiDetail2.Text;
                mySeedInfo.DictionaryMembers["HINSHI_DETAIL_3"] = cmbHinshiDetail3.Text;
                mySeedInfo.DictionaryMembers["KATSUYO_KEI"] = cmbKatsuyoukei.Text;
                mySeedInfo.DictionaryMembers["KATSUYO_TYPE"] = cmbKatsuyoType.Text;
                mySeedInfo.DictionaryMembers["BASE_TYPE"] = txtBaseType.Text;
                mySeedInfo.DictionaryMembers["YOMI"] = txtYomi.Text;
                mySeedInfo.DictionaryMembers["HATSUON"] = txtHatsuon.Text;

                if (intProcessMode == 0)
                {
                    if (IsAlreadyExist(mySeedInfo))
                        return;
                }

                //品詞リストにないものがあれば追加しておく。
                int indexHinshi = MyUtils.checkHinshiList(lstHinshi, 0, -1, mySeedInfo.DictionaryMembers[XmlDatas.ListItemNames["HINSHI"]].ToString());
                if (indexHinshi > -1)
                {
                    lstHinSub1Selected.Add(lstHinshi[lstHinshi.Count - 1]);
                }
                else
                {
                    indexHinshi = 0;
                }
                
                //品詞詳細1リストにないものがあれば追加する
                int indexHinshiSub1 = MyUtils.checkHinshiList(lstHinshiSub1, 1, lstHinshi[indexHinshi].Id, mySeedInfo.DictionaryMembers[XmlDatas.ListItemNames["HINSHI_DETAIL_1"]].ToString());
                if (indexHinshiSub1 > -1)
                {
                    lstHinSub1Selected.Add(lstHinshiSub1[lstHinshiSub1.Count - 1]);
                }
                else
                {
                    indexHinshiSub1 = 0;
                }

                //品詞詳細2リストにないものがあれば追加する
                int indexHinshiSub2 = MyUtils.checkHinshiList(lstHinshiSub2, 2, lstHinshiSub1[indexHinshiSub1].Id, mySeedInfo.DictionaryMembers[XmlDatas.ListItemNames["HINSHI_DETAIL_2"]].ToString());
                if (indexHinshiSub2 > -1)
                {
                    lstHinSub2Selected.Add(lstHinshiSub2[lstHinshiSub2.Count - 1]);
                }
                else
                {
                    indexHinshiSub2 = 0;
                }

                //品詞詳細3リストにないものがあれば追加する
                int indexHinshiSub3 = MyUtils.checkHinshiList(lstHinshiSub3, 3, lstHinshiSub2[indexHinshiSub2].Id, mySeedInfo.DictionaryMembers[XmlDatas.ListItemNames["HINSHI_DETAIL_3"]].ToString());
                if (indexHinshiSub3 > -1)
                {
                    lstHinSub3Selected.Add(lstHinshiSub3[lstHinshiSub3.Count - 1]);
                }
                else
                {
                    indexHinshiSub3 = 0;
                }

                //活用形リストにないものがあれば追加しておく
                int indexKatsuyo1 = MyUtils.checkKatsuyoList(lstKatsuyo1, 1, lstHinshi[indexHinshi].Id, mySeedInfo.DictionaryMembers[XmlDatas.ListItemNames["KATSUYO_KEI"]].ToString());
                if (indexKatsuyo1 > -1) {
                    lstKatsuyo1Selected.Add(lstKatsuyo1[lstKatsuyo1.Count - 1]);
                }
                else
                {
                    indexKatsuyo1 = 0;
                }

                //活用型リストにないものがあれば追加しておく
                int indexKatsuyo2 = MyUtils.checkKatsuyoList(lstKatsuyo2, 2, lstKatsuyo1[indexKatsuyo1].Id, mySeedInfo.DictionaryMembers[XmlDatas.ListItemNames["KATSUYO_TYPE"]].ToString());
                if (indexKatsuyo2 > -1) {
                    lstKatsuyo2Selected.Add(lstKatsuyo2[lstKatsuyo2.Count - 1]);
                }
                else
                {
                    indexKatsuyo2 = 0;
                }

                if (intProcessMode == 0)
                {
                    //学習用の形態要素リストに追加
                    lstMyLearnSeed.Add(mySeedInfo);
                }
                else
                {
                    //Seed情報設定
                    intReturnValue = 1;
                    mySeedInfoResult = new MSeed(mySeedInfo);

                    Close();
                }
            }
        }

        //解析リストか、学習用のリストにないことを確認する
        private Boolean IsAlreadyExist(MSeed mSeedWork)
        {
            Boolean bResult = false;
            Boolean bHit = false;

            lstAnalyzeSeed.ForEach(delegate(MSeed mSeedDel)
            {
                if (mSeedWork.DictionaryMembers["HYOSO_TYPE"].ToString().Equals(mSeedDel.DictionaryMembers["HYOSO_TYPE"].ToString()) && mSeedWork.DictionaryMembers["HINSHI"].ToString().Equals(mSeedDel.DictionaryMembers["HINSHI"].ToString()))
                    bHit = true;
            });

            if (bHit)
            {
                MessageBox.Show(String.Format(XmlDatas.ListMessages["ERROR_5"].ToString(), XmlDatas.ListNames["MORPHEME"].ToString()));
                bResult = true;
            }

            bHit = false;
            lstMyLearnSeed.ForEach(delegate(MSeed mSeedDel)
            {
                if (mSeedWork.DictionaryMembers["HYOSO_TYPE"].ToString().Equals(mSeedDel.DictionaryMembers["HYOSO_TYPE"].ToString()) && mSeedWork.DictionaryMembers["HINSHI"].ToString().Equals(mSeedDel.DictionaryMembers["HINSHI"].ToString()))
                    bHit = true;
            });

            if (bHit)
            {
                MessageBox.Show(String.Format(XmlDatas.ListMessages["ERROR_5"].ToString(), XmlDatas.ListNames["MORPHEME"].ToString()));
                bResult = true;
            }

            return bResult;
        }


        //選択をクリック時
        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (lstvwAnalyze.SelectedItems.Count != 1)
            {
                MessageBox.Show(String.Format(XmlDatas.ListMessages["ERROR_4"].ToString(), XmlDatas.ListNames["MORPHEME"].ToString(), "1"));
                return;
            }

            MSeed seedWork = new MSeed();
            if (intAnalyzeMode == 0)
                seedWork = lstAnalyzeSeed[lstvwAnalyze.SelectedItems[0].Index];
            else
                seedWork = lstMyLearnSeed[lstvwAnalyze.SelectedItems[0].Index];

            if (!SetInfosFromMSeedToControls(seedWork))
                return;
        }

        //MSeedの情報を基に、各コントロールに値を設定する
        private Boolean SetInfosFromMSeedToControls(MSeed seedSetInfo)
        {
            Boolean bResult = false;

            txtHyosokei.Text = seedSetInfo.DictionaryMembers[XmlDatas.ListItemNames["HYOSO_TYPE"]].ToString();
            if (intProcessMode == 0)
            {
                txtLeftConnectStatusNo.Text = seedSetInfo.DictionaryMembers[XmlDatas.ListItemNames["LEFT_CONNECT_STATUS_NO"]].ToString();
                txtRightConnectStatusNo.Text = seedSetInfo.DictionaryMembers[XmlDatas.ListItemNames["RIGHT_CONNECT_STATUS_NO"]].ToString();
                txtCost.Text = seedSetInfo.DictionaryMembers[XmlDatas.ListItemNames["COST"]].ToString();
            }
            else
            {
                txtLeftConnectStatusNo.Text = String.Empty;
                txtRightConnectStatusNo.Text = String.Empty;
                txtCost.Text = String.Empty;
            }

            //品詞
            int intHinshiSelected = checkHinshiListWithCombo(cmbHinshi, lstHinshi, 0, -1, seedSetInfo.DictionaryMembers[XmlDatas.ListItemNames["HINSHI"]].ToString(), null);
            if (intHinshiSelected < 0)
                intHinshiSelected = 0;
            
            //品詞詳細1
            int intHinshiSub1Selected = checkHinshiListWithCombo(cmbHinshiDetail1, lstHinshiSub1,
                1, lstHinshi[intHinshiSelected].Id, seedSetInfo.DictionaryMembers[XmlDatas.ListItemNames["HINSHI_DETAIL_1"]].ToString(), lstHinSub1Selected);
            if (intHinshiSub1Selected < 0)
                intHinshiSub1Selected = 0;
            
            //品詞詳細2
            int intHinshiSub2Selected = checkHinshiListWithCombo(cmbHinshiDetail2, lstHinshiSub2,
                2, lstHinshiSub1[intHinshiSub1Selected].Id, seedSetInfo.DictionaryMembers[XmlDatas.ListItemNames["HINSHI_DETAIL_2"]].ToString(), lstHinSub2Selected);
            if (intHinshiSub2Selected < 0)
                intHinshiSub2Selected = 0;

            //品詞詳細3
            int intHinshiSub3Selected = checkHinshiListWithCombo(cmbHinshiDetail3, lstHinshiSub3,
                3, lstHinshiSub2[intHinshiSub2Selected].Id, seedSetInfo.DictionaryMembers[XmlDatas.ListItemNames["HINSHI_DETAIL_3"]].ToString(), lstHinSub3Selected);
            if (intHinshiSub3Selected < 0)
                intHinshiSub3Selected = 0;

            //活用形
            int intKatsuyoSelected = checkKatsuyoListWithCombo(cmbKatsuyoukei, lstKatsuyo1,
                1, lstHinshi[intHinshiSelected].Id, seedSetInfo.DictionaryMembers[XmlDatas.ListItemNames["KATSUYO_KEI"]].ToString(), lstKatsuyo1Selected);
            if (intKatsuyoSelected < 0)
                intKatsuyoSelected = 0;
            
            //活用型
            int intKatsuyoTypeSelected = checkKatsuyoListWithCombo(cmbKatsuyoType, lstKatsuyo1,
                2, lstKatsuyo1[intKatsuyoSelected].Id, seedSetInfo.DictionaryMembers[XmlDatas.ListItemNames["KATSUYO_TYPE"]].ToString(), lstKatsuyo2Selected);
            if (intKatsuyoTypeSelected < 0)
                intKatsuyoTypeSelected = 0;
            
            txtBaseType.Text = seedSetInfo.DictionaryMembers[XmlDatas.ListItemNames["BASE_TYPE"]].ToString();
            txtYomi.Text = seedSetInfo.DictionaryMembers[XmlDatas.ListItemNames["YOMI"]].ToString();
            txtHatsuon.Text = seedSetInfo.DictionaryMembers[XmlDatas.ListItemNames["HATSUON"]].ToString();

            bResult = true;

            return bResult;
        }

        //設定
        private void btnSet_Click(object sender, EventArgs e)
        {
            if (inputCheck())
            {
                //Seed情報設定
                mySeedInfo.DictionaryMembers["HYOSO_TYPE"] = txtHyosokei.Text;
                mySeedInfo.DictionaryMembers["HINSHI"] = cmbHinshi.Text;
                mySeedInfo.DictionaryMembers["HINSHI_DETAIL_1"] = cmbHinshiDetail1.Text;
                mySeedInfo.DictionaryMembers["HINSHI_DETAIL_2"] = cmbHinshiDetail2.Text;
                mySeedInfo.DictionaryMembers["HINSHI_DETAIL_3"] = cmbHinshiDetail3.Text;
                mySeedInfo.DictionaryMembers["KATSUYO_KEI"] = cmbKatsuyoukei.Text;
                mySeedInfo.DictionaryMembers["KATSUYO_TYPE"] = cmbKatsuyoType.Text;
                mySeedInfo.DictionaryMembers["BASE_TYPE"] = txtBaseType.Text;
                mySeedInfo.DictionaryMembers["YOMI"] = txtYomi.Text;
                mySeedInfo.DictionaryMembers["HATSUON"] = txtHatsuon.Text;

            }
            else
            {
                return;
            }

            intReturnValue = 1;
            mySeedInfoResult = new MSeed(mySeedInfo);

            Close();
        }

        //学習済がクリックされた場合
        private void btnLearned_Click(object sender, EventArgs e)
        {
            intAnalyzeMode = 1;
            int intCurrentWork = -1;

            lstvwAnalyze.Items.Clear();
            lstMyLearnSeed.ForEach(delegate(MSeed seedWork)
            {
                String[] strLineList = { seedWork.DictionaryMembers[XmlDatas.ListItemNames["HYOSO_TYPE"]].ToString(), seedWork.DictionaryMembers[XmlDatas.ListItemNames["HINSHI"]].ToString(), seedWork.DictionaryMembers[XmlDatas.ListItemNames["HINSHI_DETAIL_1"]].ToString(), seedWork.DictionaryMembers[XmlDatas.ListItemNames["HINSHI_DETAIL_2"]].ToString(), seedWork.DictionaryMembers[XmlDatas.ListItemNames["HINSHI_DETAIL_3"]].ToString(), seedWork.DictionaryMembers[XmlDatas.ListItemNames["KATSUYO_KEI"]].ToString(), seedWork.DictionaryMembers[XmlDatas.ListItemNames["KATSUYO_TYPE"]].ToString(), seedWork.DictionaryMembers[XmlDatas.ListItemNames["BASE_TYPE"]].ToString(), seedWork.DictionaryMembers[XmlDatas.ListItemNames["YOMI"]].ToString(), seedWork.DictionaryMembers[XmlDatas.ListItemNames["HATSUON"]].ToString() };
                lstvwAnalyze.Items.Add(new ListViewItem(strLineList));
                if (seedWork.DictionaryMembers[XmlDatas.ListItemNames["HYOSO_TYPE"]].ToString().Equals(txtHyosokei.Text) && seedWork.DictionaryMembers[XmlDatas.ListItemNames["HINSHI"]].ToString().Equals(cmbHinshi.Text))
                {
                    intCurrentWork = lstvwAnalyze.Items.Count - 1;
                }
            });

            if (intCurrentWork > -1)
            {
                lstvwAnalyze.EnsureVisible(intCurrentWork);
                lstvwAnalyze.SelectedIndices.Clear();
                lstvwAnalyze.Items[intCurrentWork].Selected = true;
                lstvwAnalyze.Focus();
            }
        }

        //ボタン制御
        //intProcWork 0:形態要素編集 1:形態要素置換情報
        private void ControlButtons(int intProcWork)
        {
            Boolean bEnabled = true;

            btnStop.Visible = true;
            if (intProcWork == 1)
            {
                btnStop.Visible = false;
                bEnabled = false;
            }

            btnSave.Enabled = bEnabled;
            btnSet.Enabled = bEnabled;
            btnSelect.Enabled = bEnabled;
            btnAnalyze.Enabled = bEnabled;
            btnLearned.Enabled = bEnabled;
            chkNoTranse.Visible = bEnabled;
        }

        //中断ボタン
        private void btnStop_Click(object sender, EventArgs e)
        {
            intReturnValue = 2;
            Close();
        }

        //品詞変更時
        private void cmbHinshi_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbHinshiDetail1.Items.Clear();
            cmbHinshiDetail2.Items.Clear();
            cmbHinshiDetail3.Items.Clear();
            cmbKatsuyoukei.Items.Clear();
            cmbKatsuyoType.Items.Clear();
            int selected = MyUtils.getComboSelectedIndex(cmbHinshi);
            setHinshiComboBoxes(cmbHinshiDetail1, lstHinshiSub1, lstHinSub1Selected, lstHinshi[selected].Id, 1);
            setKatsuyoComboBoxes(cmbKatsuyoukei, lstKatsuyo1, lstKatsuyo1Selected, lstHinshi[selected].Id, 1);
        }

        //品詞1変更時
        private void cmbHinshiDetail1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selected = MyUtils.getComboSelectedIndex(cmbHinshiDetail1);
            setHinshiComboBoxes(cmbHinshiDetail2, lstHinshiSub2, lstHinSub2Selected, lstHinSub1Selected[selected].Id, 2);
        }

        //品詞2変更時
        private void cmbHinshiDetail2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selected = MyUtils.getComboSelectedIndex(cmbHinshiDetail2);
            setHinshiComboBoxes(cmbHinshiDetail3, lstHinshiSub3, lstHinSub3Selected, lstHinSub2Selected[selected].Id, 3);
        }

        //品詞3変更時
        private void cmbHinshiDetail3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        //活用形変更時
        private void cmbKatsuyoukei_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selected = MyUtils.getComboSelectedIndex(cmbKatsuyoukei);
            setKatsuyoComboBoxes(cmbKatsuyoType, lstKatsuyo2, lstKatsuyo2Selected, lstKatsuyo1[cmbKatsuyoukei.SelectedIndex].Id, 2);
        }

        //活用型変更時
        private void cmbKatsuyoType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }

}
