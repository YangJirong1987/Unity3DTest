using System;

public class CCEaseOut : CCEaseAction
{
    protected float m_fRate;

    public static CCEaseOut Create(CCAction curAction, float fRate)
    {
        return new CCEaseOut
        {
            m_fRate = fRate,
            _other = curAction,
            _duration = curAction.Duration
        };
    }


    public override void update(float time)
    {
        _other.update((float) (Math.Pow(time, 1/m_fRate)));
    }
}