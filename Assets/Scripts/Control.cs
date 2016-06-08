
using System;
using System.Collections;
using UnityEngine;

public class Control {
    private static Control instance;
    public static Control Instance
    {
        get
        {
            if (instance==null)
            {
                instance = new Control();
            }
            return instance;
        }
    }
    /// <summary>
    /// 计算分数
    /// </summary>
    /// <param name="num">几个</param>
    /// <returns>分数</returns>
    public int ScoreWithStartNumber(int num)
    {
        return num * num * 5;
    }
    // Math.Max(0, 2000 - num * num * 15);
    public int JiangliScoreWithRemain(int num)
    {
        return Math.Max(0, 2000 - num * num * 15);
    }
    private int lv = 1;
    public int Lv
    {
        get { return lv; }
        set { lv = value; }
    }
    private int mscore;
    public bool isPassTargetGoal = true;
    public int score { 
        get { return mscore; } 
        set { mscore = value; 
        if (mscore>highScore)
        {
            highScore = mscore;
            PlayerPrefs.SetInt("highScore", highScore);
            GameUI.Instance.SetHighScore(highScore);            
        }
        if (mscore >= GetTargetScore())
        {
            //GameUI.Instance.targetGoal
            //GameUI.Instance.targetGoal.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            ////iTween.ScaleTo(GameUI.Instance.targetGoal,iTween.Hash("scale",new Vector3(1.2f,1.2f,1.2f),"time",0.2f,"looptype",iTween.LoopType.pingPong)) ;
            //iTween.ScaleTo(GameUI.Instance.targetGoal, new Vector3(1.2f, 1.2f, 1.2f), 0.2f);
            //iTween.ScaleTo(GameUI.Instance.targetGoal, iTween.Hash("scale",new Vector3(1.2f,1.2f,1.2f),"time",0.2f,"delay",0.2f));
            if (isPassTargetGoal)
            {
                ParticleLayer.Instance.AddParticlePool(Vector3.zero, CatType.Red);
                ParticleLayer.Instance.AddParticlePool(Vector3.zero, CatType.Green);
                ParticleLayer.Instance.AddParticlePool(Vector3.zero, CatType.Yellow);
                ParticleLayer.Instance.AddParticlePool(Vector3.zero, CatType.Blue);
                ParticleLayer.Instance.AddParticlePool(Vector3.zero, CatType.Purple);
                isPassTargetGoal = false;
            }
        }
        } 
    }


    public int magicFish { get; set; }
    public int highScore = 0;
    public int HighScore
    {
        set { highScore = value;
        GameUI.Instance.SetHighScore(highScore);
        }
        get { return highScore; }
    }

    

    public long GetTargetScore()
    {
        
        switch (this.lv)
        {
            case 1:
                return 1000L;
            case 2:
                return 3000L;
            case 3:
            case 4:
            case 5:
                return (long)(6000 + (this.Lv - 3) * 2000);
            case 6:
            case 7:
            case 8:
                return (long)(13000 + (this.Lv - 6) * 2000);          

            default:
                return (long)(20000 + (this.Lv - 9) * 2000);
        }
        } 


}
