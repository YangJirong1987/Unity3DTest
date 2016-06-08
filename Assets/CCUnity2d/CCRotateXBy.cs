using UnityEngine;

public class CCRotateXBy : CCAction
{
    #region Fields

    public float _diff;

    protected float _orgAngle;

    protected float _tarAngle;

    private float y;

    private float z;

    #endregion

    #region Public Methods and Operators

    public static CCRotateXBy Create(float duartion, float angle)
    {
        return new CCRotateXBy {_diff = angle, _duration = duartion};
    }

    public override void setTarget(Transform node)
    {
        _firstTick = true;
        _isEnd = false;
        _target = node;
        _elapsed = 0f;
        _orgAngle = node.eulerAngles.x;
        _tarAngle = _diff + _orgAngle;
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