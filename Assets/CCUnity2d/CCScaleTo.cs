using UnityEngine;

public class CCScaleTo : CCAction
{
    #region Fields

    protected Vector2 _delta;
    public Vector2 _endScale;

    protected Vector2 _scale;

    protected Vector2 _startScale;

    #endregion

    #region Public Methods and Operators

    public static CCScaleTo Create(float duartion, Vector2 scale)
    {
        return new CCScaleTo {_endScale = scale, _duration = duartion};
    }

    public static CCScaleTo Create(float duartion, float scaleX, float scaleY)
    {
        return new CCScaleTo {_endScale = new Vector2(scaleX, scaleY), _duration = duartion};
    }

    public static CCScaleTo Create(float duartion, float scale)
    {
        return new CCScaleTo {_endScale = new Vector2(scale, scale), _duration = duartion};
    }

    public override void setTarget(Transform node)
    {
        _firstTick = true;
        _isEnd = false;
        _target = node;
        _elapsed = 0f;
        _startScale = node.localScale;
        _delta = _endScale - _startScale;
    }

    public override void update(float t)
    {
        if (t == 1f)
        {
            _target.localScale = _endScale;
            return;
        }
        _target.localScale = t*_delta + _startScale;
    }

    #endregion
}