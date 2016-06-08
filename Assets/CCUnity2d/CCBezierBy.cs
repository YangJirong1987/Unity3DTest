using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class CCBezierBy:CCAction
{
    public struct ccBezierConfig
    {
        //! end position of the bezier
        public Vector2 endPosition;
        //! Bezier control point 1
        public Vector2 controlPoint_1;
        //! Bezier control point 2
        public Vector2 controlPoint_2;
    }

    protected ccBezierConfig _ccBezierConfig;
    public static CCBezierBy Create(float duartion, ccBezierConfig bezierConfig)
    {
        return new CCBezierBy
        {
            _duration = duartion,
            _ccBezierConfig=bezierConfig,

        };
    }
    protected Vector2 _orgPos;
    public override void setTarget(Transform node)
    {
        _orgPos = node.position;
        base.setTarget(node);
    }

    public override void update(float t)
    {
        float xa = 0;
        float xb = _ccBezierConfig.controlPoint_1.x;
        float xc = _ccBezierConfig.controlPoint_2.x;
        float xd = _ccBezierConfig.endPosition.x;

        float ya = 0;
        float yb = _ccBezierConfig.controlPoint_1.y;
        float yc = _ccBezierConfig.controlPoint_2.y;
        float yd = _ccBezierConfig.endPosition.y;

        float x = bezierat(xa, xb, xc, xd, t);
        float y = bezierat(ya, yb, yc, yd, t);

        _target.position = _orgPos + new Vector2(x, y);
    }

    protected float bezierat(float a, float b, float c, float d, float t)
    {

        return (float)((Math.Pow(1 - t, 3) * a +
                        3 * t * (Math.Pow(1 - t, 2)) * b +
                        3 * Math.Pow(t, 2) * (1 - t) * c +
                        Math.Pow(t, 3) * d));
    }
}