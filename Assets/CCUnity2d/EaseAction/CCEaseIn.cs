using System;

public class CCEaseIn : CCEaseAction
{
    protected float m_fRate;

    public static CCEaseIn Create(CCAction curAction, float fRate)
    {
        return new CCEaseIn
        {
            m_fRate = fRate,
            _other = curAction,
            _duration = curAction.Duration
        };
    }


    public override void update(float time)
    {
        _other.update((float) Math.Pow(time, m_fRate));
    }
}