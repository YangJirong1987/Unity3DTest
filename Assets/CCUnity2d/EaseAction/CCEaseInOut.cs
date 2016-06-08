using System;

public class CCEaseInOut : CCEaseAction
{
    protected float m_fRate;

    public static CCEaseInOut Create(CCAction curAction, float fRate)
    {
        return new CCEaseInOut
        {
            m_fRate = fRate,
            _other = curAction,
            _duration = curAction.Duration
        };
    }


    public override void update(float time)
    {
        time *= 2;
        float t = 0;
        if (time < 1)
        {
            t = (0.5f*(float) Math.Pow(time, m_fRate));
        }
        else
        {
            t = (1.0f - 0.5f*(float) Math.Pow(2 - time, m_fRate));
        }

        _other.update(t);
    }
}