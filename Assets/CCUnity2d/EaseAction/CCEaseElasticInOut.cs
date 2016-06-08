using System;

public class CCEaseElasticInOut : CCEaseAction
{
    protected float m_fPeriod;

    public static CCEaseElasticInOut Create(CCAction curAction, float fPeriod = .3f)
    {
        return new CCEaseElasticInOut
        {
            m_fPeriod = fPeriod,
            _other = curAction,
            _duration = curAction.Duration
        };
    }


    public override void update(float time)
    {
        _other.update(ElasticInOut(time, m_fPeriod));
    }

    private float ElasticInOut(float time, float period)
    {
        if (time == 0 || time == 1)
        {
            return time;
        }
        time = time*2;
        if (period == 0)
        {
            period = 0.3f*1.5f;
        }

        float s = period/4;

        time = time - 1;
        if (time < 0)
        {
            return (float) (-0.5f*Math.Pow(2, 10*time)*Math.Sin((time - s)*Math.PI*2/period));
        }
        return (float) (Math.Pow(2, -10*time)*Math.Sin((time - s)*Math.PI*2/period)*0.5f + 1);
    }
}