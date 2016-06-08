using System;
using UnityEngine;

public class CCAction
{
    #region Fields

    public float _duration;

    protected float _elapsed;

    protected bool _firstTick;

    protected bool _isEnd;

    protected bool _isPause;

    protected Transform _target;

    #endregion

    #region Public Properties

    public float Duration
    {
        get { return _duration; }
    }

    public float Elapsed
    {
        get { return _elapsed; }
    }

    public bool IsEnd
    {
        get { return _isEnd; }
        private set
        {
            _isEnd = value;
            CCActionMgr.SharedActionManager.Actions[_target].Remove(this);
        }
    }

    #endregion

    #region Public Methods and Operators

    public virtual void assignTarget(Transform node)
    {
        _target = node;
    }

    public virtual void pause()
    {
        _isPause = true;
    }

    public virtual void resume()
    {
        _isPause = false;
    }

    public virtual void setTarget(Transform node)
    {
        _firstTick = true;
        _isEnd = false;
        _target = node;
        _elapsed = 0f;
    }

    public virtual void start()
    {
        if (_target != null)
        {
            setTarget(_target);
        }
    }

    public virtual void step(float dt)
    {
        if (!_target.gameObject.activeInHierarchy)
        {
            return;
        }
        if (_isPause)
        {
            return;
        }

        if (_firstTick)
        {
            _firstTick = false;
            update(0f);
            _elapsed = dt;
        }
        else
        {
            _elapsed += dt;
        }
        if (!_isEnd)
        {
            update(Math.Min(1f, _elapsed/_duration));
        }
        if (_elapsed >= _duration)
        {
            _isEnd = true;
        }
    }

    public virtual void stop()
    {
        _isEnd = true;
    }

    public virtual void update(float time)
    {
    }

    #endregion

    #region Methods

    private void Start()
    {
        _firstTick = true;
        _isPause = false;
        start();
    }

    // Update is called once per frame
    private void Update()
    {
        step(Time.deltaTime);
    }

    #endregion

    // Use this for initialization
}