using UnityEngine;

public class CCFadeIn : CCAction
{
    protected float _diff;
    protected float _orgOpacity;

    private SpriteRenderer _spriteRenderer;
    protected float _tarOpacity = 1f;

    public static CCFadeIn Create(float duartion)
    {
        return new CCFadeIn {_tarOpacity = 1f, _duration = duartion};
    }

    public override void setTarget(Transform node)
    {
        _firstTick = true;
        _isEnd = false;
        _target = node;
        _elapsed = 0f;
        _spriteRenderer = node.GetComponent<SpriteRenderer>();
        _orgOpacity = _spriteRenderer.color.a;
        _diff = _tarOpacity - _orgOpacity;
    }

    public override void update(float t)
    {
        if (t == 1f)
        {
            _spriteRenderer.color = new Color(
                _spriteRenderer.color.r,
                _spriteRenderer.color.g,
                _spriteRenderer.color.b,
                (_tarOpacity));
            return;
        }
        _spriteRenderer.color = new Color(
            _spriteRenderer.color.r,
            _spriteRenderer.color.g,
            _spriteRenderer.color.b,
            t*_diff + _orgOpacity);
    }
}