using System;

public class CCEaseElasticOut : CCEaseAction
{
    protected float m_fPeriod;

    public static CCEaseElasticOut Create(CCAction curAction, float fPeriod = .3f)
    {
        return new CCEaseElasticOut
        {
            m_fPeriod = fPeriod,
            _other = curAction,
            _duration = curAction.Duration
        };
    }


    public override void update(float time)
    {
        _other.update(ElasticOut(time, m_fPeriod));
    }

    private static float ElasticOut(float time, float period)
    {
        if (time == 0 || time == 1)
        {
            return time;
        }
        float s = period/4;
        return (float) (Math.Pow(2, -10*time)*Math.Sin((time - s)*Math.PI*2f/period) + 1);
    }
}