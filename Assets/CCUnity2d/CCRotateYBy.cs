using UnityEngine;

public class CCRotateYBy : CCAction
{
    #region Fields

    public float _diff;

    protected float _orgAngle;

    protected float _tarAngle;

    private float x;

    private float z;

    #endregion

    #region Public Methods and Operators

    public static CCRotateYBy Create(float duartion, float angle)
    {
        return new CCRotateYBy {_diff = angle, _duration = duartion};
    }

    public override void setTarget(Transform node)
    {
        _firstTick = true;
        _isEnd = false;
        _target = node;
        _elapsed = 0f;
        _orgAngle = node.eulerAngles.y;
        _tarAngle = _diff + _orgAngle;
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