using System;

public class CCEaseExponentialInOut : CCEaseAction
{
    #region Public Methods and Operators

    public static CCEaseExponentialInOut Create(CCAction curAction)
    {
        return new CCEaseExponentialInOut {_other = curAction, _duration = curAction.Duration};
    }

    public override void update(float t)
    {
        t /= 0.5f;
        if (t < 1f)
        {
            t = 0.5f*(float) Math.Pow(2.0, 10f*(t - 1f));
        }
        else
        {
            t = 0.5f*(-(float) Math.Pow(2.0, -10f*(t - 1f)) + 2f);
        }
        _other.update(t);
    }

    #endregion
}