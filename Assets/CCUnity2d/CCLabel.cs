using System.Collections.Generic;
using UnityEngine;

public class CCLabel : MonoBehaviour
{
    public enum LabelType
    {
        TEXT_ALIGNMENT_CENTER,
        TEXT_ALIGNMENT_LEFT,
        TEXT_ALIGNMENT_RIGHT,
    }

    public LabelType Alignment;

    public Color Color = Color.white;

    public string FontName;
    public int SortingLayerIndex = -99999;
    public string SortingLayerName = null;

    public string Str;


    private string oldStr = "";

    private void Awake()
    {
    }

    private void Start()
    {
        var spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            CCFont font = CCDataMgr.SharedDateMgr.FontDictionary[FontName];
            foreach (Sprite t in font.Fonts)
            {
                if (spriteRenderer.sprite.name == t.name)
                {
                    spriteRenderer.enabled = false;
                    break;
                }
            }
        }

        SetString(Str);
    }

    private void Update()
    {
    }

    public void SetString(string str)
    {
        if (str == "")
        {
            return;
        }
        CCFont font = CCDataMgr.SharedDateMgr.FontDictionary[FontName];
        if (oldStr == str)
        {
            font.CharObjByKey();
            return;
        }
        Str = str;

        var objs = new List<GameObject>();
        for (int i = 0; i < transform.childCount; i++)
        {
            objs.Add(transform.GetChild(i).gameObject);
        }
        font.DestroyCharObjs(objs);
        objs = new List<GameObject>();

        float w = 0;
        float width = font.GetWidth();
        for (int i = 0; i < str.Length; i++)
        {
            objs.Add(font.CharObjByKey(str[i]));
            float x = 0;
            if (Alignment == LabelType.TEXT_ALIGNMENT_LEFT)
            {
                if (i != 0)
                {
                    w += width/100f;
                    x = w;
                }
            }
            else if (Alignment == LabelType.TEXT_ALIGNMENT_RIGHT)
            {
                if (i != 0)
                {
                    w += width/100f;
                }
                x = -(str.Length - 1)*width/100f + w;
            }
            else if (Alignment == LabelType.TEXT_ALIGNMENT_CENTER)
            {
                x = width*(i + 0.5f)/100f - width*str.Length/2f/100f;
            }

            objs[i].GetComponent<SpriteRenderer>().color = Color;
            if (SortingLayerIndex == -99999 && string.IsNullOrEmpty(SortingLayerName))
            {
                objs[i].GetComponent<SpriteRenderer>().sortingLayerName = font.SortingLayerName;
                objs[i].GetComponent<SpriteRenderer>().sortingOrder = font.SortingLayerIndex;
            }
            else
            {
                objs[i].GetComponent<SpriteRenderer>().sortingLayerName = SortingLayerName;
                objs[i].GetComponent<SpriteRenderer>().sortingOrder = SortingLayerIndex;
            }

            objs[i].transform.parent = transform;
            objs[i].transform.localPosition = new Vector3(x, 0, 0);
            objs[i].transform.localScale = new Vector3(1, 1, 1);
        }
    }
}