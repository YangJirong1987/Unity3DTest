using UnityEngine;

public class CCJumpTo : CCJumpBy
{
    #region Public Methods and Operators

    public new static CCJumpTo Create(float duartion, Vector2 delta, float height, int jumps)
    {
        return new CCJumpTo {_duration = duartion, _delta = delta, _height = height, _jumps = jumps};
    }


    public override void setTarget(Transform node)
    {
        _firstTick = true;
        _isEnd = false;
        _target = node;
        _elapsed = 0f;
        _orgPos = node.position;
        _delta = new Vector2(_delta.x - _orgPos.x, _delta.y - _orgPos.y);
        _z = node.position.z;
    }

    #endregion
}