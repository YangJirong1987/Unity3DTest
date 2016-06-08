using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class CCBlink:CCAction
{
    protected int _times;
    public static CCBlink Create(float duartion, int times)
    {
        return new CCBlink
        {
            _duration = duartion,
            _times = times,
            
        };
    }

    private Renderer renderer;
    public override void setTarget(Transform node)
    {
        base.setTarget(node);
        renderer=_target.GetComponent<Renderer>();
    }

    public override void update(float t)
    {
        if (t == 1f)
        {
            renderer.enabled=true;
            return;
        }
        float slice = 1f / (float)this._times;
        float m = t % slice;
        renderer.enabled = m > slice / 2;
    }
}