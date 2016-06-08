using UnityEngine;

public class CCFadeOut : CCFadeIn
{
    public new static CCFadeOut Create(float duartion)
    {
        return new CCFadeOut {_tarOpacity = 0f, _duration = duartion};
    }

    public override void setTarget(Transform node)
    {
        _tarOpacity = 0f;
        base.setTarget(node);
    }
}