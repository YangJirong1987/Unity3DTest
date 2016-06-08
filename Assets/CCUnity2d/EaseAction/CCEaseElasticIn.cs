using System;

public class CCEaseElasticIn : CCEaseAction
{
    protected float m_fPeriod;

    public static CCEaseElasticIn Create(CCAction curAction, float fPeriod = .3f)
    {
        return new CCEaseElasticIn
        {
            m_fPeriod = fPeriod,
            _other = curAction,
            _duration = curAction.Duration
        };
    }


    public override void update(float time)
    {
        _other.update(ElasticIn(time, m_fPeriod));
    }

    public float ElasticIn(float time, float period)
    {
        if (time == 0 || time == 1)
        {
            return time;
        }
        float s = period/4;
        time = time - 1;
        return -(float) (Math.Pow(2, 10*time)*Math.Sin((time - s)*Math.PI*2.0f/period));
    }
}