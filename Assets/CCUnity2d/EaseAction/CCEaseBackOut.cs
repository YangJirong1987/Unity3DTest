public class CCEaseBackOut : CCEaseAction
{
    public static CCEaseBackOut Create(CCAction curAction)
    {
        return new CCEaseBackOut
        {
            _other = curAction,
            _duration = curAction.Duration
        };
    }

    public override void update(float time)
    {
        const float overshoot = 1.70158f;

        time = time - 1;
        float t = time*time*((overshoot + 1)*time + overshoot) + 1;

        _other.update(t);
    }
}