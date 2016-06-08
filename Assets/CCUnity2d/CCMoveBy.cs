using UnityEngine;

public class CCMoveBy : CCAction
{
    #region Fields

    public Vector2 _diff;

    protected float _lastTime;

    protected Vector2 _orgPos;

    protected Vector2 _pos;

    #endregion

    #region Public Methods and Operators

    private float _z;

    public static CCMoveBy Create(float duartion, Vector2 pos)
    {
        return new CCMoveBy {_diff = pos, _duration = duartion, _lastTime = 0f};
    }

    public override void setTarget(Transform node)
    {
        _firstTick = true;
        _isEnd = false;
        _target = node;
        _elapsed = 0f;
        _orgPos = node.localPosition;
        _pos = _orgPos + _diff;
        _z = node.localPosition.z;
    }

    public override void update(float t)
    {
        Vector2 position = _target.localPosition;
        Vector2 p = (t - _lastTime)*_diff + position;
        _target.localPosition = new Vector3(p.x, p.y, _z);
        _lastTime = t;
    }

    #endregion
}