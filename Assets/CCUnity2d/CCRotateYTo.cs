using UnityEngine;

public class CCRotateYTo : CCAction
{
    #region Fields

    protected float _diff;

    protected float _orgAngle;
    public float _tarAngle;

    private float x;

    private float z;

    #endregion

    #region Public Methods and Operators

    public static CCRotateYTo Create(float duartion, float angle)
    {
        return new CCRotateYTo {_tarAngle = angle, _duration = duartion};
    }

    public override void setTarget(Transform node)
    {
        _firstTick = true;
        _isEnd = false;
        _target = node;
        _elapsed = 0f;
        _orgAngle = node.eulerAngles.y;
        _diff = _tarAngle - _orgAngle;
        x = node.eulerAngles.x;
        z = node.eulerAngles.z;
    }

    public override void update(float t)
    {
        if (t == 1f)
        {
            _target.eulerAngles = new Vector3(x, _tarAngle, z);
            return;
        }
        _target.eulerAngles = new Vector3(x, t*_diff + _orgAngle, z);
    }

    #endregion
}