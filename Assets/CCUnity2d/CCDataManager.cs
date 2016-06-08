using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public class CCDataMgr
{
    private static CCDataMgr sharedDateMgr;
    public Dictionary<string, CCFont> FontDictionary;

    private CCDataMgr()
    {
        InitDataDefinitions();
    }

    public static CCDataMgr SharedDateMgr
    {
        get
        {
            if (sharedDateMgr == null)
            {
                sharedDateMgr = new CCDataMgr();
            }
            return sharedDateMgr;
        }
    }

    public static void UnsharedDateMgr()
    {
        sharedDateMgr = null;
    }

    public void InitDataDefinitions()
    {
        FontDictionary = new Dictionary<string, CCFont>();
    }


    public static T LoadData<T>(string key)
    {
        if (PlayerPrefs.HasKey(key))
        {
            var serializer = new XmlSerializer(typeof (T));
            var sr = new StringReader(PlayerPrefs.GetString(key));
            return (T) serializer.Deserialize(sr);
        }
        return default(T);
    }

    public static void SaveData<T>(string key, T source)
    {
        var serializer = new XmlSerializer(typeof (T));
        var sw = new StringWriter();
        serializer.Serialize(sw, source);
        PlayerPrefs.SetString(key, sw.ToString());
    }

    public static void ClearData(string key)
    {
        PlayerPrefs.DeleteKey(key);
    }
}