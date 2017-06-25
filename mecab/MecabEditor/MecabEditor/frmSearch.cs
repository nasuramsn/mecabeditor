using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MecabEditor
{
    public partial class frmSearch : Form
    {
        //メンバ
        private frmMain frmMyParent = new frmMain();
        private int intNextRowWork = 0;


        public frmSearch()
        {
            InitializeComponent();
        }

        public frmSearch(frmMain frmParent)
        {
            InitializeComponent();
            frmMyParent = frmParent;
        }

        //フォームが表示された後
        private void frmSearch_Shown(object sender, EventArgs e)
        {
            cmbMode.Items.Clear();
            cmbMode.Items.Add(XmlDatas.ListNames["MATCH_ALL"].ToString());
            cmbMode.Items.Add(XmlDatas.ListNames["MATCH_PART"].ToString());
            cmbMode.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMode.SelectedIndex = 0;
        }

        //上ボタンクリック時
        private void btnUp_Click(object sender, EventArgs e)
        {
            ListView lstvwWork = (ListView)frmMyParent.Controls["lstvwMain"];

            if (intNextRowWork > -1)
            {
                lstvwWork.Items[intNextRowWork].BackColor = XmlDatas.ListColors["LISTVIEW_NORMAL_ROW_BACKCOLOR"];
                lstvwWork.Items[intNextRowWork].ForeColor = XmlDatas.ListColors["LISTVIEW_NORMAL_ROW_FORECOLOR"];
            }

            int intCurrentRow = 0;

            if (lstvwWork.SelectedIndices.Count > 0)
            {
                intCurrentRow = lstvwWork.SelectedIndices[0];
                intCurrentRow--;

                if (intCurrentRow < 0)
                    intCurrentRow = 0;
            }

            intNextRowWork = SearchLastFromSeedList(intCurrentRow);

            if (intNextRowWork > -1)
            {
                lstvwWork.SelectedItems.Clear();
                lstvwWork.Items[intNextRowWork].Selected = true;
                lstvwWork.EnsureVisible(intNextRowWork);
                lstvwWork.Focus();
                lstvwWork.Items[intNextRowWork].BackColor = XmlDatas.ListColors["LISTVIEW_SELECTED_ROW_BACKCOLOR"];
                lstvwWork.Items[intNextRowWork].ForeColor = XmlDatas.ListColors["LISTVIEW_SELECTED_ROW_FORECOLOR"];
            }
        }

        //下ボタンクリック時
        private void btnDown_Click(object sender, EventArgs e)
        {
            ListView lstvwWork = (ListView)frmMyParent.Controls["lstvwMain"];

            if (intNextRowWork > -1)
            {
                lstvwWork.Items[intNextRowWork].BackColor = XmlDatas.ListColors["LISTVIEW_NORMAL_ROW_BACKCOLOR"];
                lstvwWork.Items[intNextRowWork].ForeColor = XmlDatas.ListColors["LISTVIEW_NORMAL_ROW_FORECOLOR"];
            }

            int intCurrentRow = 0;
            
            if (lstvwWork.SelectedIndices.Count > 0)
            {
                intCurrentRow = lstvwWork.SelectedIndices[0];
                intCurrentRow++;
            }

            //int intNextRowWork = SearchFromSeedList(intCurrentRow);
            intNextRowWork = SearchFromSeedList(intCurrentRow);

            if (intNextRowWork > -1)
            {
                lstvwWork.SelectedItems.Clear();
                lstvwWork.Items[intNextRowWork].Selected = true;
                lstvwWork.EnsureVisible(intNextRowWork);
                lstvwWork.Focus();
                lstvwWork.Items[intNextRowWork].BackColor = XmlDatas.ListColors["LISTVIEW_SELECTED_ROW_BACKCOLOR"];
                lstvwWork.Items[intNextRowWork].ForeColor = XmlDatas.ListColors["LISTVIEW_SELECTED_ROW_FORECOLOR"];
            }
        }

        //閉じるボタンクリック時
        private void btnClose_Click(object sender, EventArgs e)
        {
            ListView lstvwWork = (ListView)frmMyParent.Controls["lstvwMain"];

            if (intNextRowWork > -1)
            {
                lstvwWork.SelectedItems.Clear();
                lstvwWork.Items[intNextRowWork].BackColor = XmlDatas.ListColors["LISTVIEW_NORMAL_ROW_BACKCOLOR"];
                lstvwWork.Items[intNextRowWork].ForeColor = XmlDatas.ListColors["LISTVIEW_NORMAL_ROW_FORECOLOR"];
                lstvwWork.Items[intNextRowWork].Selected = true;
                lstvwWork.EnsureVisible(intNextRowWork);
                lstvwWork.Focus();
            }

            Close();
        }

        //Seedのリストから、テキストに一致するものを見つける
        private int SearchFromSeedList(int intStart)
        {
            int intResult = -1;

            //入力チェック
            if (txtText.Text.Equals(String.Empty))
            {
                MessageBox.Show(XmlDatas.ListMessages["ERROR_6"]).ToString();
            }
            else
            {
                int intFindWorkRow = -1;
                intFindWorkRow = frmMyParent.ListMSeed.FindIndex(intStart, delegate(MSeed seedWork)
                 {
                     //完全一致の場合
                     if (cmbMode.SelectedIndex == 0)
                     {
                         if (seedWork.DictionaryMembers["HYOSO_TYPE"].ToString().Equals(txtText.Text))
                             return true;
                         else
                             return false;
                     }
                     //部分一致の場合
                     else
                     {
                         if (seedWork.DictionaryMembers["HYOSO_TYPE"].ToString().IndexOf(txtText.Text) > -1)
                             return true;
                         else
                             return false;
                     }
                 });

                if (intFindWorkRow > -1 && (intFindWorkRow != intStart || intFindWorkRow < frmMyParent.ListMSeed.Count))
                    intResult = intFindWorkRow;
                else
                {
                    MessageBox.Show(XmlDatas.ListMessages["ERROR_7"].ToString());
                }
            }

            return intResult;
        }

        //Seedのリストから、テキストに一致する直前のものを見つける
        private int SearchLastFromSeedList(int intLast)
        {
            int intResult = -1;

            //入力チェック
            if (txtText.Text.Equals(String.Empty))
            {
                MessageBox.Show(XmlDatas.ListMessages["ERROR_6"]).ToString();
            }
            else
            {
                int intFindWorkRow = -1;
                intFindWorkRow = frmMyParent.ListMSeed.FindLastIndex(intLast, intLast, delegate(MSeed seedWork)
                {
                    //完全一致の場合
                    if (cmbMode.SelectedIndex == 0)
                    {
                        if (seedWork.DictionaryMembers["HYOSO_TYPE"].ToString().Equals(txtText.Text))
                            return true;
                        else
                            return false;
                    }
                    //部分一致の場合
                    else
                    {
                        if (seedWork.DictionaryMembers["HYOSO_TYPE"].ToString().IndexOf(txtText.Text) > -1)
                            return true;
                        else
                            return false;
                    }
                });

                if (intFindWorkRow > -1 && (intFindWorkRow != intLast || intFindWorkRow < frmMyParent.ListMSeed.Count))
                    intResult = intFindWorkRow;
                else
                {
                    MessageBox.Show(XmlDatas.ListMessages["ERROR_7"].ToString());
                }
            }

            return intResult;
        }
    }
}
