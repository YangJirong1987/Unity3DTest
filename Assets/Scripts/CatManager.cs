using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CatManager  {
    private static CatManager instance;
    public static CatManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new CatManager();
            }
            return instance;
        }
    }
    public List<Sprite> defaultSpriteList = new List<Sprite>();
    List<Sprite> selectedSpriteList = new List<Sprite>();
    public Sprite wCatSprite;
    public Sprite soundOnSprite, soundOffSprite;
    public CatManager()
    {

        for (int i = 0; i < 5; i++)
        {
            defaultSpriteList.Add(Resources.Load<Sprite>("Game/Cat" + (i + 1)));
        }
        for (int i = 0; i < 5; i++)
        {
            selectedSpriteList.Add(Resources.Load<Sprite>("Game/CatOn" + (i + 1)));
        }
        wCatSprite=Resources.Load<Sprite>("Game/SelectedFrame");
        soundOnSprite = Resources.Load<Sprite>("Game/SoundOn");
        soundOffSprite = Resources.Load<Sprite>("Game/SoundOff");
    }


    //对象池
    private List<GameObject> catPoolList = new List<GameObject>();
    //根据对象获加载物体
    public GameObject GetCat(CatType catType)
    {
        GameObject cat;
        for (int i = 0; i < catPoolList.Count; i++)
        {
            var ccCat = catPoolList[i].GetComponent<Cat>();
            if (ccCat.catType == catType)
            {
                cat=catPoolList[i];
                catPoolList.Remove(cat);
                cat.SetActive(true);
                ccCat.ResetWriteCat();
                //catPoolList[i].GetComponent<Cat>().ResetWriteCat();
                return cat;
            }
        }
        //如果这个物体在对象池里，就直接返回，如果不在就创建
        cat = new GameObject("Cat_"+catType.ToString());
        //GameObject obj = Resources.Load<GameObject>("Cat/Cat");
        //cat = GameObject.Instantiate(obj) as GameObject;
        //cat.name = "Cat_" + catType.ToString();
        cat.AddComponent<Cat>().catType=catType;
        SpriteRenderer render = cat.AddComponent<SpriteRenderer>();
        render.sprite = defaultSpriteList[(int)catType];
        render.sortingLayerName = "actor";
        
        return cat;
    }
    public void RemoveCat(GameObject cat)
    {
        cat.SetActive(false);
        catPoolList.Add(cat);
    }
    //获得白猫，建立对象池
    private Queue<GameObject> writeCatPool = new Queue<GameObject>();
    List<GameObject> writeCatList = new List<GameObject>();
    public GameObject GetWriteCat()
    {
        if (writeCatPool.Count>0)
        {
            var cat = writeCatPool.Dequeue();
            if (cat!=null)
            {
                writeCatList.Add(cat);
                cat.SetActive(true);
                return cat;
            }
        }
        GameObject wCat = new GameObject("writeCat");
        wCat.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
        wCat.AddComponent<SpriteRenderer>().sprite = wCatSprite;
        SpriteRenderer render = wCat.GetComponent<SpriteRenderer>();
        render.sortingLayerName = "actor";
        render.sortingOrder = -1;
        writeCatList.Add(wCat);
        return wCat;
    }
    public void RemoveWriteCat()
    {
        for (int i = 0; i < writeCatList.Count; i++)
        {
            writeCatList[i].SetActive(false);
            writeCatPool.Enqueue(writeCatList[i]);
        }
        writeCatList.Clear();    
    }
    //当选中猫的时候，其进行形态转换
    public GameObject SwapSprite(GameObject cat)
    {

        cat.GetComponent<SpriteRenderer>().sprite = selectedSpriteList[(int)cat.GetComponent<Cat>().catType];
        cat.GetComponent<Cat>().isDefault = false;
        return cat;
    }
    //第二次点中其他猫的时候还原当选中猫
    public GameObject ToDefaultSprite(GameObject cat)
    {
        cat.GetComponent<SpriteRenderer>().sprite = defaultSpriteList[(int)cat.GetComponent<Cat>().catType];
        cat.GetComponent<Cat>().isDefault = true;       
        return cat;
    }

}
