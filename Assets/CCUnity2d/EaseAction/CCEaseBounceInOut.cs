public class CCEaseBounceInOut : CCEaseAction
{
    public static CCEaseBounceInOut Create(CCAction curAction)
    {
        return new CCEaseBounceInOut
        {
            _other = curAction,
            _duration = curAction.Duration
        };
    }

    public override void update(float time)
    {
        _other.update(BounceInOut(time));
    }

    private float BounceInOut(float time)
    {
        if (time < 0.5f)
        {
            time = time*2;
            return (1 - BounceOut(1 - time))*0.5f;
        }
        return BounceOut(time*2 - 1)*0.5f + 0.5f;
    }

    private float BounceOut(float time)
    {
        if (time < 1/2.75)
        {
            return 7.5625f*time*time;
        }
        if (time < 2/2.75)
        {
            time -= 1.5f/2.75f;
            return 7.5625f*time*time + 0.75f;
        }
        if (time < 2.5/2.75)
        {
            time -= 2.25f/2.75f;
            return 7.5625f*time*time + 0.9375f;
        }

        time -= 2.625f/2.75f;
        return 7.5625f*time*time + 0.984375f;
    }
}