using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CatLayer : MonoBehaviour
{


    private static CatLayer instance;
    public static CatLayer Instance
    {
        get
        {
            return instance;
        }
    }
    public List<GameObject> catsList = new List<GameObject>();
    public Dictionary<Point, GameObject> catsDic = new Dictionary<Point, GameObject>();
    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        
        Control.Instance.HighScore = PlayerPrefs.GetInt("highScore");
        
        StartCatsDrop();
    }
    /// <summary>
    /// 猫的下落效果
    /// </summary>
    private void StartCatsDrop()
    {
        catsDic.Clear();
        catsList.Clear();
  
        CatManager.Instance.RemoveWriteCat();
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                CatType type = (CatType)Random.Range((int)CatType.Red, (int)CatType.Purple + 1);
                GameObject cat = CatManager.Instance.GetCat(type);
                cat.transform.parent = CatLayer.Instance.transform;
                cat.GetComponent<Cat>().point = new Point(i, j);

                catsList.Add(cat);
                catsDic.Add(cat.GetComponent<Cat>().point, cat);
            }
        }
        for (int x = 0; x < 10; x++)
        {
            float t = 0;//下一个掉落的时间
            for (int y = 0; y < 10; y++)
            {
                GameObject cat = catsDic[new Point(x, y)];
                //AudioManager.Instance.PlayAudioDrop();
                float time = cat.GetComponent<Cat>().StartDrop(t);
                t += time;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public List<GameObject> friendsList = new List<GameObject>();//存储周边相同的cat
    public void GetFriends(Point point)//递归寻找周边相同的
    {
        Cat cat = catsDic[point].transform.GetComponent<Cat>();
        FindOneAround(catsDic[point]);
        List<GameObject> list = FindOneAround(catsDic[point]);
        foreach (GameObject item in list)
        {
            Cat catl = item.GetComponent<Cat>();
            if (catl.catType == cat.catType)
            {
                if (!friendsList.Contains(item))
                {
                    friendsList.Add(item);
                    GetFriends(catl.point);
                }
            }
        }


    }
    /// <summary>
    /// 找到周边四个
    /// </summary>
    /// <param name="cat"></param>
    /// <returns></returns>
    public List<GameObject> FindOneAround(GameObject cat)
    {
        List<GameObject> cats = new List<GameObject>();
        foreach (GameObject item in catsList)
        {
            Point point1 = item.GetComponent<Cat>().point;
            Point point2 = cat.GetComponent<Cat>().point;
            int dy = Mathf.Abs(point1.Y - point2.Y);
            int dx = Mathf.Abs(point1.X - point2.X);
            if (dx == 1 && dy == 0)
            {
                cats.Add(item);
            }
            else if (dx == 0 && dy == 1)
            {
                cats.Add(item);
            }
        }
        return cats;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="friends">周边相同的猫的集合</param>
    internal void RemoveCat(List<GameObject> friends)
    {
        // int score=Control.Instance.ScoreWithStartNumber(friends.Count);
        //GameLayer.Instance.AddScore(score);

        TouchLayer.isTouch = false;
        List<int> columnList = new List<int>();//存储被消掉的列
        foreach (GameObject item in friends)
        {
            catsList.Remove(item);
            Point key = item.GetComponent<Cat>().point;
            catsDic[key] = null;
            CatManager.Instance.RemoveCat(item);
            columnList.Add(key.X);
        }
        float maxTime = 0;
        foreach (int column in columnList)
        {
            float t = RepairRaw(column);//找到每一空余的列，调整行；
            if (maxTime < t)
            {
                maxTime = t;
            }
        }
        //要等到行的操作完成后，才进行列的操作
        this.transform.RunAction(CCSequence.Create(CCDelay.Create(maxTime), CCCallFunc.Create(RepairColumn)));

    }
    public void RepairColumn()
    {
        int num = 0;
        float time = 0;
        for (int x = 0; x < 10; x++)
        {
            bool isNull = true;//当为true的时候说明整列都是空的
            for (int y = 0; y < 10; y++)
            {
                Point key = new Point(x, y);
                if (catsDic[key] != null)
                {
                    isNull = false;
                }
            }
            if (isNull)
            {
                num++;
            }
            if (!isNull)
            {
                if (num > 0)
                {
                    for (int y = 0; y < 10; y++)
                    {
                        Point key = new Point(x, y);
                        int dx = key.X - num;
                        GameObject cat = catsDic[key];
                        catsDic[key] = null;
                        if (cat != null)
                        {
                            time = cat.GetComponent<Cat>().RunToX(new Point(dx, y));
                        }

                    }
                }
            }
        }
        var action = CCSequence.Create(CCDelay.Create(time), CCCallFunc.Create(() =>
        {
            if (IsGameOver())
            {
                //分数奖励
                JiangLiScore();
                StartCoroutine(IsPassGameLv());

            }
            TouchLayer.isTouch = true;

        }));
        this.transform.RunAction(action);
    }

    private void JiangLiScore()
    {
        //游戏GameOver的效果
        for (int i = 0; i < catsList.Count; i++)
        {
            catsList[i].GetComponent<Cat>().RemoveParticle();
            CatManager.Instance.RemoveCat(catsList[i]);
        }
        int score = Control.Instance.JiangliScoreWithRemain(catsList.Count);
        GameObject obj = ScoreLayer.Instance.AddScorePiao(score.ToString(), new Vector3(-2.6f, -8f, 0), 2.5f);
        if (obj != null)
        {
            obj.transform.localScale = new Vector3(2, 2, 2);
        }

    }
    /// <summary>
    /// 是否过关
    /// </summary>
    
    IEnumerator IsPassGameLv()
    {
        yield return new WaitForSeconds(1f);
        if (Control.Instance.score > Control.Instance.GetTargetScore())
        {

            iTween.ScaleTo(GameUI.Instance.guoGuantext.gameObject, new Vector3(1.3f, 1.3f, 1.3f), 2);
            iTween.ScaleTo(GameUI.Instance.guoGuantext.gameObject, iTween.Hash("scale", Vector3.zero, "delay", 4, "time", 2, "easetype", iTween.EaseType.linear));
            //CatsDrop();
            AudioManager.Instance.PlayAudioGuoGuan();
            StartCoroutine(DelayDrop());
            Control.Instance.isPassTargetGoal = true;
           
        }
        else
        {
            AudioManager.Instance.PlayAudioGameOver();
            GameUI.Instance.UIGameObjece.SetActive(true);
            iTween.ScaleTo(GameUI.Instance.UIGameObjece, new Vector3(1, 1, 1), 1);
            iTween.RotateTo(GameUI.Instance.UIGameObjece, new Vector3(0, 0, 0), 1);
            //StartCoroutine(StartDelayDrop());
            Control.Instance.isPassTargetGoal = true;
        }
    }
    public IEnumerator StartDelayDrop()
    {
        yield return new WaitForSeconds(3f);

        Control.Instance.score = 0;
        Control.Instance.Lv = 1;
        GameUI.Instance.SetTargetGoal((int)Control.Instance.GetTargetScore());
        GameUI.Instance.SetLv(Control.Instance.Lv);
        GameUI.Instance.SetGameScore("0");
        StartCatsDrop();
        
    }
    IEnumerator DelayDrop()
    {
        yield return new WaitForSeconds(5.5f);
        Control.Instance.score = 0;
        Control.Instance.Lv++;
        GameUI.Instance.SetTargetGoal((int)Control.Instance.GetTargetScore());
        GameUI.Instance.SetLv(Control.Instance.Lv);
        GameUI.Instance.SetGameScore("0");
        StartCatsDrop();
    }
    public void CatsDropDown()
    {
        Control.Instance.score = 0;
        Control.Instance.Lv=1;
        GameUI.Instance.SetTargetGoal((int)Control.Instance.GetTargetScore());
        GameUI.Instance.SetLv(Control.Instance.Lv);
        GameUI.Instance.SetGameScore("0");
        StartCatsDrop();
    }

    public bool IsGameOver()
    {
        for (int i = 0; i < catsList.Count; i++)
        {
            Point mPoint = catsList[i].GetComponent<Cat>().point;
            friendsList.Clear();
            GetFriends(mPoint);
            if (friendsList.Count >= 2)
            {
                friendsList.Clear();
                return false;
            }
        }
        return true;
    }
    //行的缩进
    public float RepairRaw(int dx)//dx 被消除掉的每一个x
    {

        float maxTime = 0;
        int num = 0;
        for (int y = 0; y < 10; y++)
        {
            Point key = new Point(dx, y);

            if (catsDic[key] == null)
            {
                num++;
            }
            else
            {
                if (num > 0)
                {
                    int dy = key.Y - num;//调整的量
                    GameObject cat = catsDic[key];//空的位置上方的猫
                    catsDic[key] = null;
                    float t = cat.GetComponent<Cat>().RunToY(new Point(dx, dy));//空的位置上方的猫沿着Y移动
                    if (maxTime < t)
                    {
                        maxTime = t;
                    }
                }
            }
        }
        return maxTime;
    }

    internal void UpDatePoint(GameObject cat)
    {
        Point key = cat.GetComponent<Cat>().point;
        catsDic[key] = cat;


    }

    internal void SelectedFirst()
    {
        List<GameObject> friends = friendsList;
        for (int i = 0; i < friends.Count; i++)
        {
            friends[i].GetComponent<Cat>().SwapWriteCat();
        }
    }

    //删除已经选中的猫
    public void RemoveSelectedCat()
    {
        int totalScore = Control.Instance.ScoreWithStartNumber(CatLayer.Instance.friendsList.Count);
        int aveScore = (int)(totalScore / CatLayer.Instance.friendsList.Count);
        foreach (var cat in CatLayer.Instance.friendsList)
        {
            float time = Random.Range(1f, 3f);
            ScoreLayer.Instance.AddScorePiao(aveScore.ToString(), cat.transform.position, time);
            cat.GetComponent<Cat>().RemoveParticle();
        }
        CatLayer.Instance.RemoveCat(CatLayer.Instance.friendsList);
        CatLayer.Instance.friendsList.Clear();
        TouchLayer.Instance.isSelected = false;
    }


}
