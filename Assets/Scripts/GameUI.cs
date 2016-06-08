using UnityEngine;
using System.Collections;



public class GameUI : MonoBehaviour
{

    private static GameUI instance;
    public static GameUI Instance
    {
        get { return instance; }
    }
    public GameObject highScore;
    public GameObject magicFish;
    public GameObject stage;
    public GameObject targetGoal,touchTip;
    public GameObject CCLableObj;
    private TextMesh highScoreText, magicFishText, stageText, targetGoalText,touchTipText;
    private CCLabel ccGameScore;
   
    //public GameObject Test;
    public GameObject guoGuantext;
    void Awake()
    {
        instance = this;
        //iTween.ScaleTo(Test, iTween.Hash("scale", new Vector3(1.5f, 1.5f, 1.5f), "time", 10));
        //iTween.MoveTo(Test, iTween.Hash(""));
        highScoreText = highScore.GetComponent<TextMesh>();
    }
    void Start()
    {

       
        magicFishText = magicFish.GetComponent<TextMesh>();
        stageText = stage.GetComponent<TextMesh>();
        targetGoalText = targetGoal.GetComponent<TextMesh>();
        touchTipText = touchTip.GetComponent<TextMesh>();
        ccGameScore = CCLableObj.GetComponent<CCLabel>();       
    }

    public void SetHighScore(int score)
    {
        highScoreText.text = score.ToString();
    }
    public void SetLv(int lv)
    {
        stageText.text = lv.ToString();
    }
    public void SetMagicFish(int fish)
    {
        magicFishText.text = fish.ToString();
    }
    public void SetTargetGoal(int target)
    {
        targetGoalText.text = target.ToString();
    }
    public void SetTouchTip(string str)
    {
        touchTipText.text = str;
    }
    public void SetGameScore(string str)
    {
        ccGameScore.SetString(str);
    }

    public GameObject UIGameObjece;

    //按钮点击事件
    public void ReplaceGame (){
        AudioManager.Instance.PlayAudioReplaceAudio();
        //Control.Instance.score = 0;
        //Control.Instance.Lv = 1;
        //Control.Instance.GetTargetScore();
        //Debug.LogError("Control.Instance.score:" + Control.Instance.score + ";Control.Instance.Lv:" + Control.Instance.Lv);
        //Debug.LogError("Control.Instance.GetTargetScore():" + Control.Instance.GetTargetScore());
        //SetGameScore("0");
        //SetTargetGoal((int)Control.Instance.GetTargetScore());
        //SetLv(1);
        iTween.ScaleTo(UIGameObjece, Vector3.zero, 1f);
        iTween.RotateTo(UIGameObjece, new Vector3(0, 0, 720f), 1f);
        CatLayer.Instance.CatsDropDown();
        StartCoroutine(DelayerGameObject());
    }
    public void GameOver()
    {
        Application.Quit();
    }
    IEnumerator DelayerGameObject()
    {
        yield return new WaitForSeconds(2);
        UIGameObjece.SetActive(false);
    }

}
