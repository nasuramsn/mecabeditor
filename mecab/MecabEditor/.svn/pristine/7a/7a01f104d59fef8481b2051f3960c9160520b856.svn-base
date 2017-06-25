using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MecabEditor
{
    //形態要素置換情報
    public class MorphemeReplaceInfo
    {
        //メンバ
        private String strMorphemeReplaceInfoName = String.Empty;       //形態要素置換情報名
        private int intListSourceCount = -1;                            //置換前の形態要素リスト要素数
        private List<MSeed> lstSource = new List<MSeed>();              //置換前の形態要素リスト
        private int intListDestinationCount = -1;                       //置換後の形態要素リスト要素数
        private List<MSeed> lstDestination = new List<MSeed>();         //置換後の形態要素リスト
        private Boolean bIsLoop = false;                                //置換前の形態要素ループフラグ
        private Boolean bIsAuto = false;                                //自動変換フラグ
        private Boolean bIsTake = false;                                //適用対象フラグ
        private Boolean bIsUseHyosoFile = false;                        //表層系をファイル指定対象フラグ
        private int intHyosoIndex = -1;                                 //表層系対象のSeedインデックス
        private String strHyosoFilePath = String.Empty;                 //表層系のファイルパス
        private List<String> lstHyoso = new List<String>();             //表層系のリスト


        //コンストラクタ
        public MorphemeReplaceInfo()
        {
        }

        //コピーコンストラクタ
        public MorphemeReplaceInfo(MorphemeReplaceInfo source)
        {
            this.strMorphemeReplaceInfoName = source.MorphemeReplaceInfoName;
            this.intListSourceCount = source.ListSourceCount;
            this.lstSource = new List<MSeed>(source.ListSource);
            this.intListDestinationCount = source.intListDestinationCount;
            this.lstDestination = new List<MSeed>(source.ListDestination);
            this.bIsLoop = source.IsLoop;
            this.bIsAuto = source.IsAuto;
            this.bIsTake = source.IsTake;
            this.bIsUseHyosoFile = source.IsUseHyosoFile;
            this.intHyosoIndex = source.HyosoIndex;
            this.strHyosoFilePath = source.strHyosoFilePath;
            this.lstHyoso = new List<String>(source.ListHyoso);
        }


        //getter, setter

        //形態要素置換情報名
        public String MorphemeReplaceInfoName
        {
            get { return strMorphemeReplaceInfoName; }
            set { strMorphemeReplaceInfoName = value; }
        }

        //置換前の形態要素リスト要素数
        public int ListSourceCount
        {
            get { return intListSourceCount; }
            set { intListSourceCount = value; }
        }

        //置換前の形態要素リスト
        public List<MSeed> ListSource
        {
            get { return lstSource; }
            set { lstSource = value; }
        }

        //置換後の形態要素リスト要素数
        public int ListDestinationCount
        {
            get { return intListDestinationCount; }
            set { intListDestinationCount = value; }
        }

        //置換後の形態要素リスト
        public List<MSeed> ListDestination
        {
            get { return lstDestination; }
            set { lstDestination = value; }
        }

        //置換前の形態要素ループフラグ
        public Boolean IsLoop
        {
            get { return bIsLoop; }
            set { bIsLoop = value; }
        }

        //自動変換フラグ
        public Boolean IsAuto
        {
            get { return bIsAuto; }
            set { bIsAuto = value; }
        }

        //適用対象フラグ
        public Boolean IsTake
        {
            get { return bIsTake; }
            set { bIsTake = value; }
        }

        //表層系をファイル指定対象フラグ
        public Boolean IsUseHyosoFile
        {
            get { return bIsUseHyosoFile; }
            set { bIsUseHyosoFile = value; }
        }

        //表層系ファイルパス
        public String HyosoFilePath
        {
            get { return strHyosoFilePath; }
            set { strHyosoFilePath = value; }
        }

        //表層系リスト
        public List<String> ListHyoso
        {
            get { return lstHyoso; }
            set { lstHyoso = value; }
        }

        //表層系対象のSeedインデックス
        public int HyosoIndex
        {
            get { return intHyosoIndex; }
            set { intHyosoIndex = value; }
        }
    }
}
