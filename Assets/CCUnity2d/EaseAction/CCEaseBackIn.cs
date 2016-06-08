public class CCEaseBackIn : CCEaseAction
{
    public static CCEaseBackIn Create(CCAction curAction)
    {
        return new CCEaseBackIn
        {
            _other = curAction,
            _duration = curAction.Duration
        };
    }

    public override void update(float t)
    {
        const float overshoot = 1.70158f;
        _other.update(t*t*((overshoot + 1)*t - overshoot));
    }
}