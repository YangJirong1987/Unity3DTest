using System;

public class CCEaseSineIn : CCEaseAction
{
    #region Public Methods and Operators

    public static CCEaseSineIn Create(CCAction CurAction)
    {
        return new CCEaseSineIn {_other = CurAction, _duration = CurAction.Duration};
    }

    public override void update(float t)
    {
        _other.update(-1f*(float) Math.Cos(t*3.1415926535897931/2.0) + 1);
    }

    #endregion
}