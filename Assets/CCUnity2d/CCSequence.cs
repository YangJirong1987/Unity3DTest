using System.Collections.Generic;
using UnityEngine;

public class CCSequence : CCAction
{
    #region Fields

    protected List<CCAction> _actionList;

    protected int _index;

    #endregion

    #region Public Methods and Operators

    public static CCSequence Create(params CCAction[] actions)
    {
        var cDESequence = new CCSequence();
        cDESequence._actionList = new List<CCAction>(actions.Length);
        cDESequence._actionList.AddRange(actions);
        cDESequence._duration = 0f;
        cDESequence._index = 0;
        for (int i = 0; i < actions.Length; i++)
        {
            cDESequence._duration += actions[i].Duration;
        }
        return cDESequence;
    }

    public override void setTarget(Transform node)
    {
        _firstTick = true;
        _index = 0;
        _isEnd = false;
        _target = node;
        _elapsed = 0f;
        if (_actionList.Count > 0)
        {
            _actionList[0].setTarget(_target);
        }
    }

    public override void step(float dt)
    {
        if (_isPause)
        {
            return;
        }
        _elapsed += dt;
        if (_index < _actionList.Count)
        {
            do
            {
                _actionList[_index].step(dt);
                if (!_actionList[_index].IsEnd)
                {
                    break;
                }
                dt = _actionList[_index].Elapsed - _actionList[_index].Duration;
                _index++;
                if (_index >= _actionList.Count)
                {
                    break;
                }
                _actionList[_index].setTarget(_target);
            } while (dt >= 0f);
        }
        if (_elapsed >= _duration)
        {
            _isEnd = true;
        }
    }

    #endregion
}