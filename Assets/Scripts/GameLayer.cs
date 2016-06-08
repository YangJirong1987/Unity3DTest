using UnityEngine;
using System.Collections;

public class GameLayer : MonoBehaviour
{
    private static GameLayer instance;
    public static GameLayer Instance
    {
        get { return instance; }
    }
    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        TouchLayer.Instance.OnUpdate();
    }

    public void AddScore(int score)
    {        
        Control.Instance.score += score;
        GameUI.Instance.SetGameScore(Control.Instance.score.ToString());
    }
    public void OnTouchTip(int count)
    {
        int score=Control.Instance.ScoreWithStartNumber(count);
        string str = "消除" + count + "个猫猫可获得" + score + "分";
        GameUI.Instance.SetTouchTip(str);
    }

}
