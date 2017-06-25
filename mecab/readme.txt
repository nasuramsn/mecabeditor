MecabEditorについて        2017/06/25


■概要
Mecabのipadicのコーパス学習用に使用するために作成中です。
コーパスファイルとcsvファイルの生成を行います。
UIの不備、バグなどまだ多々ある状態です。
気が付いたら都度都度直しています。


■機能概要
1.Mecabのipadicのコーパスファイルを読み込む
2.形態要素の編集を行う
3.学習後のコーパスファイルとcsvファイルを出力する
4.形態要素の品詞と活用形・活用型はMySQLに出力する


■環境
Windows7 SP1 64bit
Microsoft Visual C# 2012
Microsoft .Net Framework 4.5
MySQL 5.5
MySQL.Data 6.9.9.0
mecab 0.996


■ソース関連
MecabEditor\bin\Debug配下(※releaseは未使用)
1.libmecab.dll mecab用ライブラリファイル
2.morphemeInfo.txt 形態要素置換情報ファイル。一括置換時に使用。サンプルです。
3.setting.ini システム設定ファイル
4.SushiTanni.txt 一括置換時の表層系設定ファイル。morphemeInfo.txtの中で未使用なら不要です。サンプルです。
5.SushiTime.txt 一括置換時の表層系設定ファイル。morphemeInfo.txtの中で未使用なら不要です。サンプルです。
6.xmls xmlファイルは全てこのフォルダ配下にコピーします。


■学習用CSVファイル
1.mylean.csv setting.iniで指定した学習用CSVファイル格納フォルダ配下にConsts.xmlのLEARN_CSV_FILEタグの値です。
　保存時に使用します。


■DB関連
1.DB設計.xlsx db設計書です。
2.create_tables.txt DDLです。
3.mylean_hinshi.xlsx 品詞と活用形のテーブルにinsertするデータ作成時のファイルです。


■マニュアル
MecabEditorマニュアル.pdf 簡単なマニュアルです。
