using UnityEngine;

public class CCColorAction : CCAction
{
    public float _b;
    public float _g;
    public float _r;

    private SpriteRenderer _spriteRenderer;

    public static CCColorAction Create(float r, float g, float b)
    {
        return new CCColorAction {_r = r, _g = g, _b = b};
    }

    public override void setTarget(Transform node)
    {
        base.setTarget(node);
        _spriteRenderer = node.GetComponent<SpriteRenderer>();
    }

    public override void update(float dt)
    {
        _spriteRenderer.color = new Color(_r, _g, _b);
        _isEnd = true;
    }
}