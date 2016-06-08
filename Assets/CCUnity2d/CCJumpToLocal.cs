using UnityEngine;

public class CCJumpToLocal : CCJumpByLocal
{
    #region Public Methods and Operators

    public new static CCJumpToLocal Create(float duartion, Vector2 delta, float height, int jumps)
    {
        return new CCJumpToLocal {_duration = duartion, _delta = delta, _height = height, _jumps = jumps};
    }

    public override void setTarget(Transform node)
    {
        _firstTick = true;
        _isEnd = false;
        _target = node;
        _elapsed = 0f;
        _orgPos = node.localPosition;
        _delta = new Vector2(_delta.x - _orgPos.x, _delta.y - _orgPos.y);
    }

    #endregion
}