using UnityEngine;

public class CCMoveTo : CCAction
{
    protected Vector2 _diff;
    protected Vector2 _orgPos;
    public Vector2 _pos;

    private float _z;

    public static CCMoveTo Create(float duartion, Vector2 pos)
    {
        return new CCMoveTo {_pos = pos, _duration = duartion};
    }

    public override void setTarget(Transform node)
    {
        _firstTick = true;
        _isEnd = false;
        _target = node;
        _elapsed = 0f;
        _orgPos = node.localPosition;
        _diff = _pos - _orgPos;
        _z = node.localPosition.z;
    }

    public override void update(float t)
    {
        if (t == 1f)
        {
            _target.localPosition = new Vector3(_pos.x, _pos.y, _z);
            return;
        }
        Vector2 p = t*_diff + _orgPos;
        _target.localPosition = new Vector3(p.x, p.y, _z);
    }
}