using UnityEngine;

public class CCOpacityAction : CCAction
{
    public float _opacity;
    private SpriteRenderer _spriteRenderer;

    public static CCOpacityAction Create(float opacity)
    {
        return new CCOpacityAction {_opacity = opacity,};
    }

    public override void setTarget(Transform node)
    {
        base.setTarget(node);
        _spriteRenderer = node.GetComponent<SpriteRenderer>();
    }

    public override void update(float dt)
    {
        _spriteRenderer.color = new Color(
            _spriteRenderer.color.r,
            _spriteRenderer.color.g,
            _spriteRenderer.color.b,
            _opacity);
        _isEnd = true;
    }
}