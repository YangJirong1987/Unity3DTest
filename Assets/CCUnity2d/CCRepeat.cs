using UnityEngine;

public class CCRepeat : CCAction
{
    #region Fields

    protected CCAction _action;

    protected int _counter;

    protected int _counterLeft;

    #endregion

    #region Public Methods and Operators

    public static CCRepeat Create(CCAction action, int t)
    {
        return new CCRepeat {_action = action, _counter = t, _duration = t*action.Duration};
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
            _elapsed = dt;
            _counterLeft = _counter;
        }
        else
        {
            _elapsed += dt;
        }
        _action.step(dt);
        if (_action.IsEnd)
        {
            _counterLeft--;
            if (_counterLeft > 0)
            {
                dt = _action.Elapsed - _action.Duration;
                _action.start();
                _action.step(dt);
                return;
            }
            _isEnd = true;
        }
    }

    #endregion
}