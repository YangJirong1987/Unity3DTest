using UnityEngine;

public class CCRepeatForever : CCAction
{
    #region Fields

    protected CCAction _action;

    #endregion

    #region Public Methods and Operators

    public static CCRepeatForever Create(CCAction action)
    {
        return new CCRepeatForever {_action = action};
    }

    public override void setTarget(Transform node)
    {
        _firstTick = true;
        _isEnd = false;
        _target = node;
        _elapsed = 0f;
        _action.setTarget(_target);
    }

    public override void step(float dt)
    {
        if (_isPause)
        {
            return;
        }
        if (_firstTick)
        {
            _firstTick = false;
            _action.step(0f);
            _elapsed = dt;
        }
        else
        {
            _elapsed += dt;
        }
        _action.step(dt);
        if (_action.IsEnd)
        {
            _action.start();
            _elapsed = 0f;
        }
    }

    #endregion
}