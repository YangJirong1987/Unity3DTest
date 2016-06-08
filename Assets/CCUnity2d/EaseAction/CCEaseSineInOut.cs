using System;

public class CCEaseSineInOut : CCEaseAction
{
    #region Public Methods and Operators

    public static CCEaseSineInOut Create(CCAction CurAction)
    {
        return new CCEaseSineInOut {_other = CurAction, _duration = CurAction.Duration};
    }

    public override void update(float t)
    {
        _other.update(-0.5f*((float) Math.Cos(3.1415926535897931*t) - 1f));
    }

    #endregion
}