using UnityEngine;
using System.Collections;

public class TouchLayer : MonoBehaviour
{

    private static TouchLayer instance;
    public static TouchLayer Instance
    {
        get { return instance; }
    }
    void Awake()
    {
        instance = this;
    }
    void Start()
    {

    }

    public static bool isTouch = true;//正在消除的的时候不允许点击，只有等消除完成之后才能在点击有效
    public bool isSelected = false;//第一次点击是否选中
    public void OnUpdate()
    {
        if (isTouch)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                if (!isSelected)//第一次点击如果没有选中，则选中
                {

                    pos = OnTouchCatOne(pos);
                }
                else//第二次选中则消除
                {

                    bool isTouchOnTwo = false;
                    for (int i = 0; i < CatLayer.Instance.friendsList.Count; i++)
                    {
                        Cat cat=CatLayer.Instance.friendsList[i].GetComponent<Cat>();
                        isTouchOnTwo = cat.IsTouchOnTwo(pos);
                        if (isTouchOnTwo)
                        {
                            TextMesh tipText = GameUI.Instance.touchTip.GetComponent<TextMesh>();
                            iTween.FadeTo(GameUI.Instance.touchTip, 1, 0.5f);
                            iTween.FadeTo(GameUI.Instance.touchTip, iTween.Hash("alpha", 0, "time", 1, "delay", 2f));
                            AudioManager.Instance.PlayAudioxiaoDiao();
                            CatLayer.Instance.RemoveSelectedCat();
                            return;
                        }
                    }
                    if (!isTouchOnTwo)
                    {
                        for (int i = 0; i < CatLayer.Instance.friendsList.Count; i++)
                        {
                           
                            Cat cat = CatLayer.Instance.friendsList[i].GetComponent<Cat>();
                            cat.SwapWriteCat();
                            isSelected = false;
                        }
                        CatManager.Instance.RemoveWriteCat();
                        CatLayer.Instance.friendsList.Clear();
                        OnTouchCatOne(pos);
                    }
                }
            }
        }
    }
    /// <summary>
    /// 第一次点击在猫上
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    private Vector3 OnTouchCatOne(Vector3 pos)
    {
        foreach (GameObject cat in CatLayer.Instance.catsList)
        {
            bool isTouchOn = cat.GetComponent<Cat>().TouchOn(pos);
            if (isTouchOn)
            {
                TextMesh tipText=GameUI.Instance.touchTip.GetComponent<TextMesh>();
                iTween.FadeTo(GameUI.Instance.touchTip, 1,0.5f);
                iTween.FadeTo(GameUI.Instance.touchTip, iTween.Hash("alpha", 0, "time", 1, "delay", 2f));
                AudioManager.Instance.PlayAudioSelected();
                isSelected = true;
                break;
            }
        }
        return pos;
    }
}
