using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScoreLayer : MonoBehaviour
{

    private static ScoreLayer instance;
    public static ScoreLayer Instance
    {
        get
        {
            return instance;
        }
    }
    void Awake()
    {
        instance = this;
    }
    public GameObject scorePrfabs;
    void Start()
    {
        //scorePrfabs = Resources.Load<GameObject>("Prefabs/CPiaoScoreLable");

        callBackObj = GameObject.Find("ScoreLayer");

    }
    //private GameObject scoreObj;
    private Vector3 endPoint = new Vector3(0.1f, 3.8f, 0);
    private Vector3[] path = new Vector3[3];
    
    public GameObject AddScorePiao(string score,Vector3 pos,float time)
    {
        scoreEve = int.Parse(score);
        path[0] = pos;
        int ran = Random.Range(2, 5);
        path[1] = new Vector3((endPoint.x - pos.x) / ran, (endPoint.y - pos.y) / ran, 0);
        path[2] = endPoint;
            
        scoreObj = GameObject.Instantiate(scorePrfabs) as GameObject;
        scoreObj.transform.position = pos;
        
        scoreObj.GetComponent<CCLabel>().SetString(score.ToString());
        scoreObj.GetComponent<OnDestroyScore>().Time = time;

        iTween.MoveTo(scoreObj, iTween.Hash("path", path, "movetopath", true, "time", time, "easetype", iTween.EaseType.linear, "oncomplete", "CallBackScore", "oncompletetarget", callBackObj));
        return scoreObj;
    }
    private GameObject callBackObj;//回调函数所在的物体
    GameObject scoreObj;//实例化飘飞的物体
    private int scoreEve;//每一个飘飞的猫的分数
  
    public void CallBackScore()
    {
        Control.Instance.score += scoreEve;
        GameUI.Instance.SetGameScore(Control.Instance.score.ToString());
    }
    public void AddScoreS(int scoren)
    {
        Control.Instance.score += scoren;
        GameUI.Instance.SetGameScore(Control.Instance.score.ToString());
    }


}
