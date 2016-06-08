using UnityEngine;

public class CCDestroy : CCAction
{
    #region Fields

    #endregion

    #region Public Methods and Operators

    public static CCDestroy Create()
    {
        return new CCDestroy();
    }

    public override void setTarget(Transform node)
    {
        base.setTarget(node);
    }

    public override void update(float dt)
    {
        Object.Destroy(_target.gameObject);
        _isEnd = true;
    }

    #endregion
}