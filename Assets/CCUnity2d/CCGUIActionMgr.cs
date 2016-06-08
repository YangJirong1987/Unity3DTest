using System.Collections.Generic;
using UnityEngine;

public class CCGUIActionMgr
{
    private static CCGUIActionMgr sharedActionMgr;

    private readonly List<CCAction> actionList = new List<CCAction>();

    public CCGUIActionMgr()
    {
        sharedActionMgr = this;
    }

    public static CCGUIActionMgr SharedActionMgr
    {
        get { return sharedActionMgr ?? (sharedActionMgr = new CCGUIActionMgr()); }
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
        float t = 0.02f;
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

        if (CCActionMgr.SharedActionManager.Actions.ContainsKey(transform))
        {
            CCActionMgr.SharedActionManager.Actions[transform].Add(action);
        }
        else
        {
            CCActionMgr.SharedActionManager.Actions.Add(transform, new List<CCAction> {action});
        }
    }
}