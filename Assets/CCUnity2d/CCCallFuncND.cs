using UnityEngine;

public class CCCallFuncND : CCAction
{
    #region Fields

    protected callback _c;

    protected object _data;

    #endregion

    #region Delegates

    public delegate void callback(Transform node, object data);

    #endregion

    #region Public Methods and Operators

    public static CCCallFuncND Create(callback c, object data)
    {
        return new CCCallFuncND {_c = c, _data = data};
    }

    public override void update(float dt)
    {
        _c(_target, _data);
        _isEnd = true;
    }

    #endregion
}