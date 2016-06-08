public class CCEaseBackInOut : CCEaseAction
{
    public static CCEaseBackInOut Create(CCAction curAction)
    {
        return new CCEaseBackInOut
        {
            _other = curAction,
            _duration = curAction.Duration
        };
    }

    public override void update(float time)
    {
        const float overshoot = 1.70158f*1.525f;

        float t;
        time = time*2;
        if (time < 1)
        {
            t = (time*time*((overshoot + 1)*time - overshoot))/2;
        }
        else
        {
            time = time - 2;
            t = (time*time*((overshoot + 1)*time + overshoot))/2 + 1;
        }

        _other.update(t);
    }
}