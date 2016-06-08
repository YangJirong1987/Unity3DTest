using UnityEngine;

public class CCRotateTo : CCAction
{
    #region Fields

    protected float _diff;

    protected float _orgAngle;
    public float _tarAngle;

    #endregion

    #region Public Methods and Operators

    public static CCRotateTo Create(float duartion, float angle)
    {
        return new CCRotateTo {_tarAngle = angle, _duration = duartion};
    }

    public override void setTarget(Transform node)
    {
        _firstTick = true;
        _isEnd = false;
        _target = node;
        _elapsed = 0f;
        _orgAngle = node.eulerAngles.z;
        _diff = _tarAngle - _orgAngle;
    }

    public override void update(float t)
    {
        if (t == 1f)
        {
            _target.eulerAngles = new Vector3(
                _target.eulerAngles.x,
                _target.eulerAngles.y,
                _tarAngle);
            ;
            return;
        }
        _target.eulerAngles = new Vector3(
            _target.eulerAngles.x,
            _target.eulerAngles.y,
            t*_diff + _orgAngle);
    }

    #endregion
}