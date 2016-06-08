using System;

public class CCEaseSineOut : CCEaseAction
{
    #region Public Methods and Operators

    public static CCEaseSineOut Create(CCAction curAction)
    {
        return new CCEaseSineOut {_other = curAction, _duration = curAction.Duration};
    }

    public override void update(float t)
    {
        _other.update((float) Math.Sin(t*3.1415926535897931/2.0));
    }

    #endregion
}