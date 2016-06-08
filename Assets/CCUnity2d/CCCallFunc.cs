public class CCCallFunc : CCAction
{
    #region Fields

    public callback _c;

    #endregion

    #region Delegates

    public delegate void callback();

    #endregion

    #region Public Methods and Operators

    public static CCCallFunc Create(callback c)
    {
        return new CCCallFunc {_c = c};
    }

    public override void update(float dt)
    {
        _c();
        _isEnd = true;
    }

    #endregion
}