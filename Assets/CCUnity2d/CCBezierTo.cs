using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class CCBezierTo:CCBezierBy
{
    public static CCBezierTo Create(float duartion, ccBezierConfig bezierConfig)
    {
        return new CCBezierTo
        {
            _duration = duartion,
            _ccBezierConfig = bezierConfig,
        };
    }
    public override void setTarget(Transform node)
    {
        base.setTarget(node);

        _ccBezierConfig.controlPoint_1 = _ccBezierConfig.controlPoint_1 - _orgPos;
        _ccBezierConfig.controlPoint_2 = _ccBezierConfig.controlPoint_2 - _orgPos;
        _ccBezierConfig.endPosition = _ccBezierConfig.endPosition - _orgPos;
    }
}