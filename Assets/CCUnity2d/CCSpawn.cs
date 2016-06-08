using System;
using System.Collections.Generic;
using UnityEngine;

public class CCSpawn : CCAction
{
    #region Fields

    protected List<CCAction> _actionList;

    #endregion

    #region Public Methods and Operators

    public static CCSpawn Create(params CCAction[] actions)
    {
        var spawn = new CCSpawn();
        spawn._actionList = new List<CCAction>(actions.Length);
        spawn._actionList.AddRange(actions);
        spawn._duration = 0f;
        for (int i = 0; i < actions.Length; i++)
        {
            spawn._duration = Math.Max(spawn._duration, actions[i].Duration);
        }
        return spawn;
    }

    public override void setTarget(Transform node)
    {
        _firstTick = true;
        _isEnd = false;
        _target = node;
        _elapsed = 0f;
        using (List<CCAction>.Enumerator enumerator = _actionList.GetEnumerator())
        {
            while (enumerator.MoveNext())
            {
                CCAction current = enumerator.Current;
                current.setTarget(_target);
            }
        }
    }

    public override void step(float dt)
    {
        if (_isPause)
        {
            return;
        }
        _elapsed += dt;
        for (int i = 0; i < _actionList.Count; i++)
        {
            if (!_actionList[i].IsEnd)
            {
                _actionList[i].step(dt);
            }
        }
        if (_elapsed >= _duration)
        {
            _isEnd = true;
        }
    }

    #endregion
}