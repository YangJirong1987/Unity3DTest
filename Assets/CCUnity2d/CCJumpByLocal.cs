using System;
using UnityEngine;

public class CCJumpByLocal : CCAction
{
    #region Fields

    public Vector2 _delta;

    public float _height;

    public int _jumps;

    protected Vector2 _orgPos;

    #endregion

    #region Public Methods and Operators

    public static CCJumpByLocal Create(float duartion, Vector2 delta, float height, int jumps)
    {
        return new CCJumpByLocal {_duration = duartion, _delta = delta, _height = height, _jumps = jumps};
    }

    public override void setTarget(Transform node)
    {
        _firstTick = true;
        _isEnd = false;
        _target = node;
        _elapsed = 0f;
        _orgPos = node.localPosition;
    }

    public override void update(float t)
    {
        if (t == 1f)
        {
            _target.localPosition = new Vector2(_delta.x + _orgPos.x, _delta.y + _orgPos.y);
            return;
        }
        float num = _height*Math.Abs((float) Math.Sin(t*3.14159274f*_jumps));
        num += _delta.y*t;
        float num2 = _delta.x*t;
        _target.localPosition = new Vector2(num2 + _orgPos.x, num + _orgPos.y);
    }

    #endregion
}