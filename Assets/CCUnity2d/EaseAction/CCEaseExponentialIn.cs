using System;

public class CCEaseExponentialIn : CCEaseAction
{
    #region Public Methods and Operators

    public static CCEaseExponentialIn Create(CCAction CurAction)
    {
        return new CCEaseExponentialIn {_other = CurAction, _duration = CurAction.Duration};
    }

    public override void update(float t)
    {
        _other.update((t == 0f) ? 0f : ((float) Math.Pow(2.0, 10f*(t/1f - 1f)) - 0.001f));
    }

    #endregion
}