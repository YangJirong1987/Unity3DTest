using System.Collections.Generic;
using UnityEngine;

public class CCActionMgr
{
    private static CCActionMgr sharedActionMgr;
    public readonly Dictionary<Transform, List<CCAction>> Actions;

    private readonly List<CCAction> actionList;

    public CCActionMgr()
    {
        sharedActionMgr = this;
        Actions = new Dictionary<Transform, List<CCAction>>();
        actionList = new List<CCAction>();
    }

    public static CCActionMgr SharedActionManager
    {
        get { return sharedActionMgr ?? (sharedActionMgr = new CCActionMgr()); }
    }

    public static void UnsharedActionMgr()
    {
        foreach (CCAction action in sharedActionMgr.actionList)
        {
            action.stop();
        }
        sharedActionMgr = null;
    }


    public void Update()
    {
        float t = Time.deltaTime;
        for (int i = 0; i < actionList.Count; i++)
        {
            if (actionList[i].IsEnd)
            {
                actionList.Remove(actionList[i]);
                i--;
            }
            else
            {
                actionList[i].step(t);
                if (actionList[i].IsEnd)
                {
                    actionList.Remove(actionList[i]);
                    i--;
                }
            }
        }
    }

    public void AddAction(Transform transform, CCAction action)
    {
        actionList.Add(action);

        if (Actions.ContainsKey(transform))
        {
            Actions[transform].Add(action);
        }
        else
        {
            Actions.Add(transform, new List<CCAction> {action});
        }
    }
}