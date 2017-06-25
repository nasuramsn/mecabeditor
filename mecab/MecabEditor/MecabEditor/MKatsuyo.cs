﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MecabEditor
{
    //活用形用マスタクラス
    public class MKatsuyo
    {
        //メンバー
        private int id = -1;                            // 活用形を一意に定義するid
        private String name = String.Empty;  // 品詞名
        private int level = -1;                        // 品詞のレベル。0:最上位 1:子供。以下同様
        private int parentId = -1;                  // 親のid
        private DateTime createdAt;             // 作成日
        private DateTime updatedAt;            // 更新日
        private String deleteFlg = "";            // 削除フラグ

        //コンストラクタ
        public MKatsuyo()
        {
        }

        public MKatsuyo(int id, String name, int level, int parentId)
        {
            this.id = id;
            this.name = name;
            this.level = level;
            this.parentId = parentId;
        }

        //getter, setter

        //ID
        public int Id
        {
            get { return this.id; }
            set { this.id = value; }
        }

        //名称
        public String Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        //レベル
        public int Level
        {
            get { return this.level; }
            set { this.level = value; }
        }

        //親ID
        public int ParentId
        {
            get { return this.parentId; }
            set { this.parentId = value; }
        }

        //作成日
        public DateTime CreatedAt
        {
            get { return this.createdAt; }
            set { this.createdAt = value; }
        }

        //更新日
        public DateTime UpdatedAt
        {
            get { return this.updatedAt; }
            set { this.updatedAt = value; }
        }

        //削除フラグ
        public String DeleteFlg
        {
            get { return this.deleteFlg; }
            set { this.deleteFlg = value; }
        }
    }
}
