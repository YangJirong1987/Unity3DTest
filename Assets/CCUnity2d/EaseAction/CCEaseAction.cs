using UnityEngine;

public class CCEaseAction : CCAction
{
    #region Fields

    protected CCAction _other;

    #endregion

    #region Public Methods and Operators

    public override void assignTarget(Transform node)
    {
        _target = node;
        _other.assignTarget(node);
    }

    public override void setTarget(Transform node)
    {
        _firstTick = true;
        _isEnd = false;
        _target = node;
        _elapsed = 0f;
        _other.setTarget(node);
    }

    #endregion
}