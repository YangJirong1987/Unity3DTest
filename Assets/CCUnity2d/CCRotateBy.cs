using UnityEngine;

public class CCRotateBy : CCAction
{
    #region Fields

    public float _diff;

    protected float _orgAngle;

    protected float _tarAngle;

    #endregion

    #region Public Methods and Operators

    public static CCRotateBy Create(float duartion, float angle)
    {
        return new CCRotateBy {_diff = angle, _duration = duartion};
    }

    public override void setTarget(Transform node)
    {
        _firstTick = true;
        _isEnd = false;
        _target = node;
        _elapsed = 0f;
        _orgAngle = node.eulerAngles.z;
        _tarAngle = _diff + _orgAngle;
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