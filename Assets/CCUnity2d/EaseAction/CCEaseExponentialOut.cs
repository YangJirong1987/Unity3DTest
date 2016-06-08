using System;

public class CCEaseExponentialOut : CCEaseAction
{
    #region Public Methods and Operators

    public static CCEaseExponentialOut Create(CCAction curAction)
    {
        return new CCEaseExponentialOut {_other = curAction, _duration = curAction.Duration};
    }

    public override void update(float t)
    {
        _other.update((t == 1f) ? 1f : (-(float) Math.Pow(2.0, -10f*t/1f) + 1f));
    }

    #endregion
}