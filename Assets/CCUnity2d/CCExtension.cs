using UnityEngine;

public static class CCExtension
{
    public static void StopAllAction(this Transform s)
    {
        if (!CCActionMgr.SharedActionManager.Actions.ContainsKey(s))
        {
            return;
        }
        foreach (CCAction action in CCActionMgr.SharedActionManager.Actions[s])
        {
            action.stop();
        }
    }

    public static void RunAction(this Transform s, CCAction action)
    {
        CCActionMgr.SharedActionManager.AddAction(s, action);
        action.setTarget(s);
    }

    public static void RunGUIAction(this Transform s, CCAction action)
    {
        CCGUIActionMgr.SharedActionMgr.AddAction(s, action);
        action.setTarget(s);
    }
}