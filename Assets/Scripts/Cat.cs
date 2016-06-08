using UnityEngine;
using System.Collections;
using System;

public class Cat : MonoBehaviour
{

    /// <summary>
    /// 每只猫所具有的属性1、类型、2位置
    /// </summary>
    public CatType catType { get; set; }
    private Point mPoint;
    public Point point
    {
        get { return mPoint; }
        set
        {
            mPoint = value;
            SetPositionPoint();
        }
    }

    private void SetPositionPoint()
    {
        this.transform.localPosition = new Vector2(mPoint.X * 72 + 72f / 2, mPoint.Y * 72 + 72f / 2) / 100;
    }
   /// <summary>
   /// 第一点击是否选中物体
   /// </summary>
   /// <param name="pos"></param>
   /// <returns></returns>
    public bool TouchOn(Vector3 pos)
    {
        Rect rect = new Rect(this.transform.position.x - 0.72f / 2, transform.position.y - 0.72f / 2, 0.72f, 0.72f);
        bool isTouchOn = rect.Contains(pos);
        if (isTouchOn)
        {
            //根据点击到的猫进行递归
            CatLayer.Instance.GetFriends(mPoint);
            //第一次选中给与提示
            GameLayer.Instance.OnTouchTip(CatLayer.Instance.friendsList.Count);
            CatLayer.Instance.SelectedFirst();

        }
        return isTouchOn;
    }
    /// <summary>
    /// 第二次点击是否选中
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    public bool IsTouchOnTwo(Vector3 pos)
    {
        Rect rect = new Rect(this.transform.position.x - 0.72f / 2, transform.position.y - 0.72f / 2, 0.72f, 0.72f);
        bool isTouchOnTwo = rect.Contains(pos);
        return isTouchOnTwo;
    }


    private float t=0.2f;
    internal float RunToY(Point point)
    {
        float time=t*Mathf.Abs(this.point.Y-point.Y);
        this.transform.RunAction(CCSequence.Create(CCMoveTo.Create(time, GetGoalPoint(point)),
        CCCallFunc.Create(() =>
        {
            this.point = point;
            CatLayer.Instance.UpDatePoint(this.gameObject);
        })));
        mPoint = point;
        return time;        
    }
    private Vector2 GetGoalPoint(Point point)
    {
        return new Vector2(point.X * 72 + 72f / 2, point.Y * 72 + 72f / 2) / 100;
    }


    internal float RunToX(Point point)
    {
        float time = t * Mathf.Abs(this.point.X - point.X);
        this.transform.RunAction(CCSequence.Create(CCMoveTo.Create(time, GetGoalPoint(point)),
        CCCallFunc.Create(() =>
        {
            this.point = point;
            CatLayer.Instance.UpDatePoint(this.gameObject);
        })));
        mPoint = point;
        return time;    
    }
    const float offSetY = 12.80f;//屏幕的高
    public float StartDrop(float t)
    {
        
        GetGoalPoint(mPoint);
        Vector3 pos=this.transform.localPosition;
        this.transform.localPosition = new Vector3(pos.x, pos.y+offSetY, pos.z);
        float time=UnityEngine.Random.Range(0, 0.3f);
        this.transform.RunAction(CCSequence.Create(CCDelay.Create(time + t), CCMoveTo.Create(0.5f, GetGoalPoint(mPoint)), CCCallFunc.Create(SetPositionPoint)));
        //iTween.MoveTo(this.gameObject, iTween.Hash("y", GetGoalPoint(mPoint).y, "delay", time + t, "islocal",true, "time", 0.8f, "easetype",iTween.EaseType.linear,"oncomplete", "SetPositionPoint"));       
        return time;
    }

    /// <summary>
    /// 给选中的猫加上白色的背景
    /// </summary>
    public bool isDefault=true;//是否可以进行形态转换

    public void ResetWriteCat()
    {
        isDefault = true;
        this.transform.StopAllAction();
       // CatManager.Instance.ToDefaultSprite(this.gameObject);
    }
    public void SwapWriteCat()
    {
        if (isDefault)
        {
           
            GameObject cat=CatManager.Instance.GetWriteCat();
            //SetPositionPoint();
            cat.transform.parent = this.transform;
            cat.transform.localPosition = Vector3.zero;
            CatManager.Instance.SwapSprite(this.gameObject);
            var action = CCJumpBy.Create(0.2f,new Vector2(0, 0), 0.1f, 1);
            this.transform.RunAction(action);

        }
        else
        {
            CatManager.Instance.ToDefaultSprite(this.gameObject);
        }
        //CatManager.Instance.GetWriteCat().transform.parent = this.transform;
    }

    public void RemoveParticle()
    {
        ParticleLayer.Instance.AddParticlePool(this.transform.position,this.catType);
    }
}
