using System.Collections.Generic;
using UnityEngine;

public class CCFont : MonoBehaviour
{
    private readonly Dictionary<string, Sprite> fontDic = new Dictionary<string, Sprite>();

    private readonly List<KeyValuePair<char, Font>> removePool = new List<KeyValuePair<char, Font>>();
    public List<Sprite> Fonts = new List<Sprite>();
    public List<string> Keys = new List<string>();

    public bool OpenTheReuse = true;

    public bool ShouldUseCustomWidth = false;
    public int SortingLayerIndex = 0;
    public string SortingLayerName = "UI";

    public float Width = 0;

    private void Awake()
    {
        for (int i = 0; i < Keys.Count; i++)
        {
            fontDic.Add(Keys[i], Fonts[i]);
        }
        CCDataMgr.SharedDateMgr.FontDictionary.Add(name, this);
    }

    public float GetWidth()
    {
        return ShouldUseCustomWidth ? Width : Fonts[0].rect.width;
    }


    public GameObject CharObjByKey(char key = new char())
    {
        GameObject obj = null;
        bool has = false;
        if (OpenTheReuse)
        {
            for (int i = 0; i < removePool.Count; i++)
            {
                KeyValuePair<char, Font> kv = removePool[i];
                if (kv.Key == key)
                {
                    has = true;
                    obj = kv.Value.FontObj;
                    removePool.Remove(kv);
                    break;
                }
            }
            for (int i = removePool.Count - 1; i >= 0; i--)
            {
                KeyValuePair<char, Font> kv = removePool[i];
                kv.Value.Index++;
                if (kv.Value.Index > 5)
                {
                    Destroy(kv.Value.FontObj);
                    removePool.Remove(kv);
                }
            }
        }


        if (!has)
        {
            obj = new GameObject(key.ToString());
            obj.AddComponent<SpriteRenderer>().sprite = fontDic[key.ToString()];
        }
        obj.SetActive(true);
        return obj;
    }

    public void Debug()
    {
        for (int i = 0; i < removePool.Count; i++)
        {
            UnityEngine.Debug.Log(removePool[i].Value.Index + ":" + removePool[i].Value.FontObj.name);
        }
    }

    public void DestroyCharObjs(List<GameObject> charObjects)
    {
        for (int i = 0; i < charObjects.Count; i++)
        {
            var spriteRenderer = charObjects[i].GetComponent<SpriteRenderer>();
            if (spriteRenderer == null)
            {
                Destroy(charObjects[i]);
            }
            else
            {
                bool has = false;
                if (OpenTheReuse)
                {
                    foreach (var sprite in fontDic)
                    {
                        if (sprite.Value.name == spriteRenderer.sprite.name)
                        {
                            removePool.Add(new KeyValuePair<char, Font>(sprite.Key[0],
                                new Font {FontObj = charObjects[i], Index = 0}));
                            charObjects[i].transform.parent = transform;
                            charObjects[i].SetActive(false);
                            has = true;
                            break;
                        }
                    }
                }
                if (!has)
                {
                    Destroy(charObjects[i]);
                }
            }
        }
        charObjects.Clear();
    }

    public class Font
    {
        public int Index { get; set; }
        public GameObject FontObj { get; set; }
    }
}