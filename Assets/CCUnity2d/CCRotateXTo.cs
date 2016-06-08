using UnityEngine;

public class CCRotateXTo : CCAction
{
    #region Fields

    protected float _diff;

    protected float _orgAngle;
    public float _tarAngle;

    private float y;

    private float z;

    #endregion

    #region Public Methods and Operators

    public static CCRotateXTo Create(float duartion, float angle)
    {
        return new CCRotateXTo {_tarAngle = angle, _duration = duartion};
    }

    public override void setTarget(Transform node)
    {
        _firstTick = true;
        _isEnd = false;
        _target = node;
        _elapsed = 0f;
        _orgAngle = node.eulerAngles.x;
        _diff = _tarAngle - _orgAngle;
        y = _target.eulerAngles.y;
        z = _target.eulerAngles.z;
    }

    public override void update(float t)
    {
        if (t == 1f)
        {
            _target.eulerAngles = new Vector3(_tarAngle, y, z);
            return;
        }
        _target.eulerAngles = new Vector3(t*_diff + _orgAngle, y, z);
    }

    #endregion
}