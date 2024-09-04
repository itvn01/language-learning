using System.Collections;
using System.Collections.Generic;
using SimpleSQL;
using UnityEngine;
namespace M1Game 
{
    public class SQLManager : SingletonMB<SQLManager>
    {
        public SimpleSQL.SimpleSQLManager dbManager;

        private void Start() {
            Test();
        }

        private void Test()
        {
            var listKanji = GetListKanjiInfoByListLesson(new List<int>() { 2, 3 });
            // for (int i = 0; i < listKanji.Count; i++)
            // {
            //     var kanji_info = listKanji[i];
            //     Debug.Log(kanji_info.w_id + " => " + kanji_info.kanji + " => " + kanji_info.hanviet);
            // }

            var kanji = listKanji[0];
            var listWords = GetListWordInfoByKanjiInfo(kanji);
            Debug.Log("Result => " + listWords[0].kanji);
            Debug.Log("Result => " + listWords[1].kanji);
            // Debug.Log("Result => " + listWords[2].kanji);
        }

        #region KANJI-INFO
        public KanjiInfo GetKanjiInfoById(int w_id)
        {
            bool recordExists;
            string query = "SELECT * FROM kanji WHERE w_id = " + w_id.ToString();
            Debug.Log("query => " + query);
            KanjiInfo firstRecord = dbManager.QueryFirstRecord<KanjiInfo>(out recordExists, query);
            if (recordExists)
                return firstRecord;
            else
                return new KanjiInfo();
        }

        public List<KanjiInfo> GetListKanjiInfoByLesson(int lesson)
        {
            string query = "SELECT * FROM kanji WHERE lesson = " + lesson.ToString();
            Debug.Log("query => " + query);
            List<KanjiInfo> listKanji = dbManager.Query<KanjiInfo>(query);
            return listKanji;
        }

        public List<KanjiInfo> GetListKanjiInfoByListLesson(List<int> listLesson)
        {
            string lessonStr = "(";
            int total = listLesson.Count;
            for (int i = 0; i < total; i++)
            {
                lessonStr += (i < total - 1 ? (listLesson[i].ToString() + ", ") : (listLesson[i].ToString()));
            }
            lessonStr += " )";
            string query = "SELECT * FROM kanji WHERE lesson IN " + lessonStr;
            Debug.Log("query => " + query);
            List<KanjiInfo> listKanji = dbManager.Query<KanjiInfo>(query);
            return listKanji;
        }

        #endregion

        #region WORD-INFO

        public List<WordInfo> GetListWordInfoByListId(List<int> listID)
        {
            string idStr = "(";
            int total = listID.Count;
            for (int i = 0; i < total; i++)
            {
                idStr += (i < total - 1 ? (listID[i].ToString() + ", ") : (listID[i].ToString()));
            }
            idStr += " )";
            string query = "SELECT * FROM word WHERE w_id IN " + idStr;
            Debug.Log("query => " + query);
            List<WordInfo> listWords = dbManager.Query<WordInfo>(query);
            return listWords;
        }

        public List<WordInfo> GetListWordInfoByKanjiInfo(KanjiInfo kanjiInfo)
        {
            var word = kanjiInfo.word.Replace(";", ",");
            string query = $"SELECT * FROM word WHERE w_id IN ({word})";
            Debug.Log("query => " + query);
            List<WordInfo> listWords = dbManager.Query<WordInfo>(query);
            return listWords;
        }

        public WordInfo GetWordInfoById(int w_id)
        {
            bool recordExists;
            string query = "SELECT * FROM word WHERE w_id = " + w_id.ToString();
            Debug.Log("query => " + query);
            WordInfo firstRecord = dbManager.QueryFirstRecord<WordInfo>(out recordExists, query);
            if (recordExists)
                return firstRecord;
            else
                return new WordInfo();
        }

        #endregion
    }

    public class KanjiInfo
    {
        // The w_id field is set as the primary key in the SQLite database,
        // so we reflect that here with the PrimaryKey attribute
        [PrimaryKey]
        public string w_id { get; set; }
        public string lesson { get; set; }
        public string kanji { get; set; }
        public string hanviet { get; set; }
        public string pinyin { get; set; }
        public string word { get; set; }
        public string goiy { get; set; }

        public bool IsValid => kanji.Length > 0 && hanviet.Length > 0;
    }

    public class WordInfo
    {
        // The w_id field is set as the primary key in the SQLite database,
        // so we reflect that here with the PrimaryKey attribute
        [PrimaryKey]
        public string w_id { get; set; }
        public string kanji { get; set; }
        public string pinyin { get; set; }
        public string hanviet { get; set; }
        public string vn { get; set; }
        public bool IsValid => kanji.Length > 0 && hanviet.Length > 0;

    }

}
