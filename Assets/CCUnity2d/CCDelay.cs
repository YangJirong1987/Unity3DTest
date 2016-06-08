public class CCDelay : CCAction
{
    #region Public Methods and Operators

    public static CCDelay Create(float duartion)
    {
        return new CCDelay {_duration = duartion};
    }

    #endregion
}