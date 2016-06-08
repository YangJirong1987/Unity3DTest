using UnityEngine;

public class CCCallFuncN : CCAction
{
    #region Fields

    protected callback _c;

    #endregion

    #region Delegates

    public delegate void callback(Transform node);

    #endregion

    #region Public Methods and Operators

    public static CCCallFuncN Create(callback c)
    {
        return new CCCallFuncN {_c = c};
    }

    public override void update(float dt)
    {
        _c(_target);
        _isEnd = true;
    }

    #endregion
}