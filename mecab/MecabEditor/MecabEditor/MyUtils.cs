using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MecabEditor
{
    //色々な用途に使う関数
    static public class MyUtils
    {
        //コンストラクタ
        static MyUtils()
        {
        }

        //ファイル読取り。どの文字コードで来ても、全てUtf8で返す
        static public Boolean readFilesWithUtf8(String strSourcePath, ref String strResult)
        {
            Boolean bResult = false;
            strResult = String.Empty;
            StreamReader strRdr = null;
            //byte[] byteDatas;

            try
            {
                //ファイルの存在チェック
                if (!System.IO.File.Exists(@strSourcePath))
                {
                    MessageBox.Show("ファイルが存在しません");
                    return bResult;
                }

                //ファイルの読み取り
                //テキストファイルを開く
                System.IO.FileStream fs = new System.IO.FileStream(@strSourcePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                byte[] bs = new byte[fs.Length];
                
                //byte配列に読み込む
                fs.Read(bs, 0, bs.Length);
                fs.Close();

                //文字コードを取得する
                System.Text.Encoding enc = GetCode(bs);

                //utf-8に変換する
                if (!enc.BodyName.Equals("utf-8"))
                {
                    if (enc.BodyName.Equals("iso-2022-jp"))
                    {
                        //strResult = System.Text.Encoding.GetEncoding("iso-2022-jp").GetString(bs);
                        strResult = System.Text.Encoding.GetEncoding(932).GetString(bs);
                        //byteDatas = System.Text.Encoding.GetEncoding("iso-2022-jp").GetBytes(strResult);
                        //strResult = System.Text.Encoding.UTF8.GetString(bs);
                    }
                    else if (enc.BodyName.Equals("euc-jp"))
                    {
                        strResult = System.Text.Encoding.GetEncoding("euc-jp").GetString(bs);
                        //byteDatas = System.Text.Encoding.GetEncoding("euc-jp").GetBytes(strResult);
                        //strResult = System.Text.Encoding.UTF8.GetString(bs);
                    }
                }
                else
                {
                    strResult = System.Text.Encoding.GetEncoding("utf-8").GetString(bs);
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
                if (strRdr != null)
                {
                    strRdr.Close();
                    strRdr = null;
                }
            }

            return bResult;
        }

        //sjisからutf8に変更
        static public String changeSjisToUtf8(String strSource)
        {
            String strResult = String.Empty;
            byte[] byteDatas;

            byteDatas = System.Text.Encoding.GetEncoding(932).GetBytes(strSource);
            strResult = System.Text.Encoding.UTF8.GetString(byteDatas);

            return strResult;
        }

        //Csvファイル出力
        static public Boolean outputCsvFile(String strFileName, List<MSeed> lstMyMSeed)
        {
            Boolean bResult = false;
            StreamWriter strwtr = null;

            try
            {
                strwtr = new StreamWriter(@strFileName, false, Encoding.UTF8);

                lstMyMSeed.ForEach(delegate(MSeed mSeedWork)
                {
                    String strOutputWork = mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["HYOSO_TYPE"]].ToString();
                    Debug.WriteLine(strOutputWork);
                    if (!mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["HINSHI"]].ToString().Equals(String.Empty))
                    {
                        strOutputWork = strOutputWork + "," + mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["LEFT_CONNECT_STATUS_NO"]].ToString() + "," + mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["RIGHT_CONNECT_STATUS_NO"]].ToString() + "," + mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["COST"]].ToString() + "," + mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["HINSHI"]].ToString() + "," + mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["HINSHI_DETAIL_1"]].ToString() + "," + mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["HINSHI_DETAIL_2"]].ToString() + "," + mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["HINSHI_DETAIL_3"]].ToString() + "," + mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["KATSUYO_KEI"]].ToString() + "," + mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["KATSUYO_TYPE"]].ToString() + "," + mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["BASE_TYPE"]].ToString();
                    }

                    if (!mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["YOMI"]].ToString().Equals(String.Empty))
                    {
                        strOutputWork = strOutputWork + "," + mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["YOMI"]].ToString() + "," + mSeedWork.DictionaryMembers[XmlDatas.ListItemNames["HATSUON"]].ToString();
                    }

                    strwtr.WriteLine(strOutputWork);
                });

                MessageBox.Show(String.Format(XmlDatas.ListMessages["INFORMATION_1"], XmlDatas.ListNames["CSV_FILE_LEARN"]));


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

        //形態要素置換情報ファイル取得
        public static Boolean getMorphemeReplaceInfoFromFile(String strFilePath, ref List<MorphemeReplaceInfo> lstMorphemeReplaceinfo)
        {
            Boolean bResult = false;
            StreamReader strrdr = null;

            try
            {
                if (!File.Exists(@strFilePath))
                {
                    MessageBox.Show(String.Format(XmlDatas.ListMessages["ERROR_2"].ToString(), XmlDatas.ListNames["MORPHEME_REPLACE_INFO"].ToString()));
                    return bResult;
                }

                strrdr = new StreamReader(@strFilePath, Encoding.GetEncoding("utf-8"));
                lstMorphemeReplaceinfo.Clear();
                String strLineWork = String.Empty;
                MorphemeReplaceInfo morphemeReplaceInfo = new MorphemeReplaceInfo();
                List<Boolean> bIsRead = new List<Boolean> { false, false, false, false, false, false, false, false, false, false, false };
                

                while (!strrdr.EndOfStream)
                {
                    strLineWork = strrdr.ReadLine();
                    if (strLineWork.Equals(XmlDatas.ListConsts["EOS"]))
                    {
                        lstMorphemeReplaceinfo.Add(morphemeReplaceInfo);
                        morphemeReplaceInfo = new MorphemeReplaceInfo();
                        bIsRead = new List<Boolean> { false, false, false, false, false, false, false, false, false, false, false };
                    }
                    else
                    {
                        if (!bIsRead[0])
                        {
                            morphemeReplaceInfo.MorphemeReplaceInfoName = strLineWork;
                            bIsRead[0] = true;
                        }
                        else if (!bIsRead[1])
                        {
                            morphemeReplaceInfo.ListSourceCount = int.Parse(strLineWork);
                            bIsRead[1] = true;
                        }
                        else if (!bIsRead[2])
                        {
                            MSeed mSeedWork = new MSeed();
                            mSeedWork.getFromCsvFile2(strLineWork);
                            morphemeReplaceInfo.ListSource.Add(mSeedWork);
                            if (morphemeReplaceInfo.ListSourceCount == morphemeReplaceInfo.ListSource.Count)
                                bIsRead[2] = true;
                        }
                        else if (!bIsRead[3])
                        {
                            morphemeReplaceInfo.ListDestinationCount = int.Parse(strLineWork);
                            bIsRead[3] = true;
                        }
                        else if (!bIsRead[4])
                        {
                            MSeed mSeedWork = new MSeed();
                            mSeedWork.getFromCsvFile2(strLineWork);
                            morphemeReplaceInfo.ListDestination.Add(mSeedWork);
                            if (morphemeReplaceInfo.ListDestinationCount == morphemeReplaceInfo.ListDestination.Count)
                                bIsRead[4] = true;
                        }
                        else if (!bIsRead[5])
                        {
                            if (strLineWork.Equals("True"))
                                morphemeReplaceInfo.IsLoop = true;
                            else
                                morphemeReplaceInfo.IsLoop = false;

                            bIsRead[5] = true;
                        }
                        else if (!bIsRead[6])
                        {
                            if (strLineWork.Equals("True"))
                                morphemeReplaceInfo.IsAuto = true;
                            else
                                morphemeReplaceInfo.IsAuto = false;

                            bIsRead[6] = true;
                        }
                        else if (!bIsRead[7])
                        {
                            if (strLineWork.Equals("True"))
                                morphemeReplaceInfo.IsTake = true;
                            else
                                morphemeReplaceInfo.IsTake = false;

                            bIsRead[7] = true;
                        }
                        else if (!bIsRead[8])
                        {
                            if (strLineWork.Equals("True"))
                                morphemeReplaceInfo.IsUseHyosoFile = true;
                            else
                                morphemeReplaceInfo.IsUseHyosoFile = false;

                            bIsRead[8] = true;
                        }
                        else if (!bIsRead[9])
                        {
                            morphemeReplaceInfo.HyosoIndex = int.Parse(strLineWork);

                            bIsRead[9] = true;
                        }
                        else if (!bIsRead[10])
                        {
                            morphemeReplaceInfo.HyosoFilePath = strLineWork;
                            morphemeReplaceInfo.ListHyoso.Clear();
                            StreamReader strrdr2 = null;

                            if (morphemeReplaceInfo.HyosoFilePath.Length > 0)
                            {
                                try
                                {
                                    strrdr2 = new StreamReader(@morphemeReplaceInfo.HyosoFilePath, Encoding.GetEncoding("utf-8"));
                                    while (!strrdr2.EndOfStream)
                                    {
                                        morphemeReplaceInfo.ListHyoso.Add(strrdr2.ReadLine());
                                    }
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
                                    if (strrdr2 != null)
                                    {
                                        strrdr2.Close();
                                        strrdr2.Dispose();
                                    }
                                }
                            }

                            bIsRead[10] = true;
                        }
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

        //形態要素置換情報ファイル出力
        public static Boolean outputMorphemeReplaceInfoToFile(String strFilePath, List<MorphemeReplaceInfo> lstMorphemeReplaceinfo)
        {
            Boolean bResult = false;
            StreamWriter strwtr = null;

            try
            {
                strwtr = new StreamWriter(@strFilePath, false, Encoding.GetEncoding("utf-8"));

                lstMorphemeReplaceinfo.ForEach(delegate(MorphemeReplaceInfo morphemeReplaceInfoEach)
                {
                    strwtr.WriteLine(morphemeReplaceInfoEach.MorphemeReplaceInfoName);
                    strwtr.WriteLine(morphemeReplaceInfoEach.ListSourceCount);

                    morphemeReplaceInfoEach.ListSource.ForEach(delegate(MSeed seedEach)
                    {
                        strwtr.WriteLine(seedEach.DictionaryMembers["HYOSO_TYPE"].ToString() + "," + seedEach.DictionaryMembers["HINSHI"].ToString() + "," + seedEach.DictionaryMembers["HINSHI_DETAIL_1"].ToString() + "," + seedEach.DictionaryMembers["HINSHI_DETAIL_2"].ToString() + "," + seedEach.DictionaryMembers["HINSHI_DETAIL_3"].ToString() + "," + seedEach.DictionaryMembers["KATSUYO_KEI"].ToString() + "," + seedEach.DictionaryMembers["KATSUYO_TYPE"].ToString() + "," + seedEach.DictionaryMembers["BASE_TYPE"].ToString() + "," + seedEach.DictionaryMembers["YOMI"].ToString() + "," + seedEach.DictionaryMembers["HATSUON"].ToString());
                    });

                    strwtr.WriteLine(morphemeReplaceInfoEach.ListDestinationCount);

                    morphemeReplaceInfoEach.ListDestination.ForEach(delegate(MSeed seedEach)
                    {
                        strwtr.WriteLine(seedEach.DictionaryMembers["HYOSO_TYPE"].ToString() + "," + seedEach.DictionaryMembers["HINSHI"].ToString() + "," + seedEach.DictionaryMembers["HINSHI_DETAIL_1"].ToString() + "," + seedEach.DictionaryMembers["HINSHI_DETAIL_2"].ToString() + "," + seedEach.DictionaryMembers["HINSHI_DETAIL_3"].ToString() + "," + seedEach.DictionaryMembers["KATSUYO_KEI"].ToString() + "," + seedEach.DictionaryMembers["KATSUYO_TYPE"].ToString() + "," + seedEach.DictionaryMembers["BASE_TYPE"].ToString() + "," + seedEach.DictionaryMembers["YOMI"].ToString() + "," + seedEach.DictionaryMembers["HATSUON"].ToString());
                    });

                    strwtr.WriteLine(morphemeReplaceInfoEach.IsLoop.ToString());
                    strwtr.WriteLine(morphemeReplaceInfoEach.IsAuto.ToString());
                    strwtr.WriteLine(morphemeReplaceInfoEach.IsTake.ToString());
                    strwtr.WriteLine(morphemeReplaceInfoEach.IsUseHyosoFile.ToString());
                    strwtr.WriteLine(morphemeReplaceInfoEach.HyosoIndex.ToString());
                    strwtr.WriteLine(morphemeReplaceInfoEach.HyosoFilePath);
                    strwtr.WriteLine(XmlDatas.ListConsts["EOS"]);
                });

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

        //CSVファイルの表層系に数字のカンマ区切りが入っている場合に対応する
        public static String[] CsvFileDisposeComma(String[] strSource)
        {
            int intMorphemeCountMax1 = int.Parse(XmlDatas.ListConsts["MORPHEME_COUNT_MAX1"].ToString());
            int intMorphemeCountMax2 = int.Parse(XmlDatas.ListConsts["MORPHEME_COUNT_MAX2"].ToString());
            int intMorphemeCountMaxResult = 0;
            List<String> lstDisposeWork = new List<String>();
            String[] strResult = new String[1] { String.Empty };

            intMorphemeCountMaxResult = intMorphemeCountMax1;

            //末尾とそのひとつ前が*の場合
            int intLengthWork = strSource.Length;
            if (strSource[intLengthWork - 2].Equals("*") && strSource[intLengthWork - 1].Equals("*"))
            {
                //表層系が*でない場合
                if (!strSource[0].Equals("*"))
                {
                    intMorphemeCountMaxResult = intMorphemeCountMax2;
                }
            }

            for (int i = 0; i < intLengthWork; i++)
            {
                lstDisposeWork.Add(strSource[i].ToString());
            }
            
            if (lstDisposeWork.Count > intMorphemeCountMaxResult)
            {
                while (lstDisposeWork.Count > intMorphemeCountMaxResult)
                {
                    lstDisposeWork[0] = lstDisposeWork[0].ToString() + "," + lstDisposeWork[1].ToString();
                    lstDisposeWork.RemoveAt(1);
                }
            }

            Array.Resize(ref strResult, lstDisposeWork.Count);

            int intCountWork = 0;
            lstDisposeWork.ForEach(delegate(String strWorkLine)
            {
                strResult[intCountWork] = strWorkLine;
                intCountWork++;
            });

            return strResult;
        }

        //数値チェック
        public static Boolean IsNumeric(String strSource)
        {
            Boolean bResult = false;
            int iOutput = -1;

            if (strSource.Length == 0)
                return bResult;

            if (!int.TryParse(strSource, out iOutput))
                return bResult;

            bResult = true;

            return bResult;
        }

        /// <summary>
        /// 文字コードを判別する
        /// </summary>
        /// <remarks>
        /// Jcode.pmのgetcodeメソッドを移植したものです。
        /// Jcode.pm(http://openlab.ring.gr.jp/Jcode/index-j.html)
        /// Jcode.pmのCopyright: Copyright 1999-2005 Dan Kogai
        /// </remarks>
        /// <param name="bytes">文字コードを調べるデータ</param>
        /// <returns>適当と思われるEncodingオブジェクト。
        /// 判断できなかった時はnull。</returns>
        public static System.Text.Encoding GetCode(byte[] bytes)
        {
            const byte bEscape = 0x1B;
            const byte bAt = 0x40;
            const byte bDollar = 0x24;
            const byte bAnd = 0x26;
            const byte bOpen = 0x28;    //'('
            const byte bB = 0x42;
            const byte bD = 0x44;
            const byte bJ = 0x4A;
            const byte bI = 0x49;

            int len = bytes.Length;
            byte b1, b2, b3, b4;

            //Encode::is_utf8 は無視

            bool isBinary = false;
            for (int i = 0; i < len; i++)
            {
                b1 = bytes[i];
                if (b1 <= 0x06 || b1 == 0x7F || b1 == 0xFF)
                {
                    //'binary'
                    isBinary = true;
                    if (b1 == 0x00 && i < len - 1 && bytes[i + 1] <= 0x7F)
                    {
                        //smells like raw unicode
                        return System.Text.Encoding.Unicode;
                    }
                }
            }
            if (isBinary)
            {
                return null;
            }

            //not Japanese
            bool notJapanese = true;
            for (int i = 0; i < len; i++)
            {
                b1 = bytes[i];
                if (b1 == bEscape || 0x80 <= b1)
                {
                    notJapanese = false;
                    break;
                }
            }
            if (notJapanese)
            {
                return System.Text.Encoding.ASCII;
            }

            for (int i = 0; i < len - 2; i++)
            {
                b1 = bytes[i];
                b2 = bytes[i + 1];
                b3 = bytes[i + 2];

                if (b1 == bEscape)
                {
                    if (b2 == bDollar && b3 == bAt)
                    {
                        //JIS_0208 1978
                        //JIS
                        return System.Text.Encoding.GetEncoding(50220);
                    }
                    else if (b2 == bDollar && b3 == bB)
                    {
                        //JIS_0208 1983
                        //JIS
                        return System.Text.Encoding.GetEncoding(50220);
                    }
                    else if (b2 == bOpen && (b3 == bB || b3 == bJ))
                    {
                        //JIS_ASC
                        //JIS
                        return System.Text.Encoding.GetEncoding(50220);
                    }
                    else if (b2 == bOpen && b3 == bI)
                    {
                        //JIS_KANA
                        //JIS
                        return System.Text.Encoding.GetEncoding(50220);
                    }
                    if (i < len - 3)
                    {
                        b4 = bytes[i + 3];
                        if (b2 == bDollar && b3 == bOpen && b4 == bD)
                        {
                            //JIS_0212
                            //JIS
                            return System.Text.Encoding.GetEncoding(50220);
                        }
                        if (i < len - 5 &&
                            b2 == bAnd && b3 == bAt && b4 == bEscape &&
                            bytes[i + 4] == bDollar && bytes[i + 5] == bB)
                        {
                            //JIS_0208 1990
                            //JIS
                            return System.Text.Encoding.GetEncoding(50220);
                        }
                    }
                }
            }

            //should be euc|sjis|utf8
            //use of (?:) by Hiroki Ohzaki <ohzaki@iod.ricoh.co.jp>
            int sjis = 0;
            int euc = 0;
            int utf8 = 0;
            for (int i = 0; i < len - 1; i++)
            {
                b1 = bytes[i];
                b2 = bytes[i + 1];
                if (((0x81 <= b1 && b1 <= 0x9F) || (0xE0 <= b1 && b1 <= 0xFC)) &&
                    ((0x40 <= b2 && b2 <= 0x7E) || (0x80 <= b2 && b2 <= 0xFC)))
                {
                    //SJIS_C
                    sjis += 2;
                    i++;
                }
            }
            for (int i = 0; i < len - 1; i++)
            {
                b1 = bytes[i];
                b2 = bytes[i + 1];
                if (((0xA1 <= b1 && b1 <= 0xFE) && (0xA1 <= b2 && b2 <= 0xFE)) ||
                    (b1 == 0x8E && (0xA1 <= b2 && b2 <= 0xDF)))
                {
                    //EUC_C
                    //EUC_KANA
                    euc += 2;
                    i++;
                }
                else if (i < len - 2)
                {
                    b3 = bytes[i + 2];
                    if (b1 == 0x8F && (0xA1 <= b2 && b2 <= 0xFE) &&
                        (0xA1 <= b3 && b3 <= 0xFE))
                    {
                        //EUC_0212
                        euc += 3;
                        i += 2;
                    }
                }
            }
            for (int i = 0; i < len - 1; i++)
            {
                b1 = bytes[i];
                b2 = bytes[i + 1];
                if ((0xC0 <= b1 && b1 <= 0xDF) && (0x80 <= b2 && b2 <= 0xBF))
                {
                    //UTF8
                    utf8 += 2;
                    i++;
                }
                else if (i < len - 2)
                {
                    b3 = bytes[i + 2];
                    if ((0xE0 <= b1 && b1 <= 0xEF) && (0x80 <= b2 && b2 <= 0xBF) &&
                        (0x80 <= b3 && b3 <= 0xBF))
                    {
                        //UTF8
                        utf8 += 3;
                        i += 2;
                    }
                }
            }
            //M. Takahashi's suggestion
            //utf8 += utf8 / 2;

            System.Diagnostics.Debug.WriteLine(
                string.Format("sjis = {0}, euc = {1}, utf8 = {2}", sjis, euc, utf8));
            if (euc > sjis && euc > utf8)
            {
                //EUC
                return System.Text.Encoding.GetEncoding(51932);
            }
            else if (sjis > euc && sjis > utf8)
            {
                //SJIS
                return System.Text.Encoding.GetEncoding(932);
            }
            else if (utf8 > euc && utf8 > sjis)
            {
                //UTF8
                return System.Text.Encoding.UTF8;
            }

            return null;
        }

        //ConboBoxでのindexを連動するArrayListから取得する
        // コンボボックス選択Index取得
        public static int getComboSelectedIndex(ComboBox combo)
        {
            int selected = combo.SelectedIndex;
            if (selected > 0)
                selected--;
            else if (selected < 0)
                selected++;

            return selected;
        }

        // 品詞リストのチェック。なければ追加する
        // lstSrc:品詞リスト
        // hinshiLevel:品詞レベル
        // parentId:親ID
        // hinshiName:品詞名
        // noSpace:空白許可
        public static int checkHinshiList(List<MHinshi> lstSrc, int hinshiLevel, int parentId, String hinshiName, Boolean noSpace)
        {
            String strName = "*";
            Boolean bEdit = false;

            if (hinshiName.Equals(String.Empty))
            {
                if (noSpace)
                {
                    bEdit = true;
                }
            }
            else
            {
                strName = hinshiName;
                bEdit = true;
            }

            int indexHinshi = lstSrc.FindIndex(x => x.Name.Equals(strName) && x.Level == hinshiLevel && x.ParentId == parentId);

            if (bEdit)
            {
                if (indexHinshi < 0)
                {
                    MHinshi mNewHinshi = new MHinshi();
                    mNewHinshi.ParentId = parentId;
                    if (hinshiLevel == 0)
                        mNewHinshi.Id = lstSrc.Count + 1;
                    else
                    {
                        List<MHinshi> lstWrk = lstSrc.FindAll(x => x.Level == hinshiLevel && x.ParentId == parentId);
                        mNewHinshi.Id = (mNewHinshi.ParentId * 100) + lstWrk.Count + 1;
                    }
                    mNewHinshi.Name = strName;
                    mNewHinshi.Level = hinshiLevel;
                    mNewHinshi.DeleteFlg = "0";

                    lstSrc.Add(mNewHinshi);
                }
            }
            
            return indexHinshi;
        }

        // 活用形リストのチェック。なければ追加する
        public static int checkKatsuyoList(List<MKatsuyo> lstSrc, int katsuyoLevel, int parentId, String katsuyoName)
        {
            int indexKatsuyo = lstSrc.FindIndex(x => x.Name.Equals(katsuyoName));

            if (indexKatsuyo < 0)
            {
                MKatsuyo mNewKatsuyo = new MKatsuyo();
                mNewKatsuyo.ParentId = parentId;
                if (katsuyoLevel == 0)
                    mNewKatsuyo.Id = lstSrc.Count + 1;
                else
                    mNewKatsuyo.Id = (mNewKatsuyo.ParentId * 100) + lstSrc.Count + 1;
                mNewKatsuyo.Name = katsuyoName;
                mNewKatsuyo.Level = katsuyoLevel;
                mNewKatsuyo.DeleteFlg = "0";

                lstSrc.Add(mNewKatsuyo);
                indexKatsuyo = lstSrc.Count - 1;
            }
            else
            {
                indexKatsuyo = -1;
            }

            return indexKatsuyo;
        }
    }
}
