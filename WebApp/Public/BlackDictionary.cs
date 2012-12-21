using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using AjaxPro;

using System.Collections;

/// <summary>
/// BlackDictionary 的摘要说明
/// </summary>
public class BlackDictionary
{
    public BlackDictionary()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }

    #region Fields

    private readonly static string keyWord = "word";
    private readonly static int keyWordLen = 4;

    // 二级索引表
    private static Hashtable indexTable = new Hashtable();

    #endregion

    #region Constructor

    #endregion

    #region Properties

    #endregion

    #region Methods

    [AjaxMethod]
    public static string Parse(string value)
    {
        int len = value.Length;
        for (int pos = 0; pos < len; pos++)
        {
            // 遍历所有的文字
            string header = value.Substring(pos, 1);
            if (indexTable.ContainsKey(header))
            {
                Hashtable table2 = (Hashtable)indexTable[header];
                // 遍历头文字下的所有词
                foreach (DictionaryEntry de in table2)
                {
                    int wordLen = int.Parse(((string)de.Key).Substring(keyWordLen));
                    string subWord = value.Length > wordLen ? (value.Length - pos  > wordLen ? value.Substring(pos, wordLen) : value.Substring(pos)) : value;
                    if (((Hashtable)de.Value).ContainsKey(subWord)) 
                        return subWord;
                }
            }
        }
        return "";
    }

    public void AddWord(string word)
    {
        // 获取头文字
        string header = word.Substring(0, 1);
        // 获取头文字索引表
        Hashtable table2;
        if (indexTable.ContainsKey(header))
        {
            table2 = (Hashtable)indexTable[header];
        }
        else
        {
            table2 = new Hashtable();
            indexTable.Add(header, table2);
        }
        // 根据单词字数分配到相应的索引表中
        int length = word.Length;
        string key = keyWord + length;
        Hashtable table3;
        if (table2.ContainsKey(key))
        {
            table3 = (Hashtable)table2[key];
        }
        else
        {
            table3 = new Hashtable();
            table2.Add(key, table3);
        }
        // 将单词加入到索引表中
        if (table3.ContainsKey(word))
        {
            //throw new Exception("单词已经存在。");
        }
        else
        {
            table3.Add(word, word);
        }
    }

    #endregion
}
