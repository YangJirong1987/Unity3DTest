using System.Collections.Generic;
using UnityEngine;

public struct CCTouch
{
    #region Fields

    public Vector2 DeltaPosition; //this is not accurate

    public float DeltaTime;

    public int FingerId;

    public TouchPhase Phase;

    public Vector2 Position;

    public int TapCount;

    #endregion
}

public interface ICCSingleTouchable
{
    #region Public Properties

    int TouchPriority //FNodes have this defined by default
    { get; }

    #endregion

    #region Public Methods and Operators

    bool HandleSingleTouchBegan(CCTouch touch);

    void HandleSingleTouchCanceled(CCTouch touch);

    void HandleSingleTouchEnded(CCTouch touch);

    void HandleSingleTouchMoved(CCTouch touch);

    #endregion
}

public interface ICCMultiTouchable
{
    #region Public Methods and Operators

    void HandleMultiTouch(CCTouch[] touches);

    #endregion
}

public class CCTouchMgr
{
    #region Static Fields

    public static bool IsEnabled = true;

    /// <summary>
    ///     鼠标模拟Touch
    /// </summary>
    public static bool ShouldMouseEmulateTouch = true;

    private static CCTouchMgr _sharedTouchManager;

    #endregion

    #region Fields

    private readonly List<ICCMultiTouchable> _multiTouchables = new List<ICCMultiTouchable>();

    private readonly List<ICCMultiTouchable> _multiTouchablesToAdd = new List<ICCMultiTouchable>();

    private readonly List<ICCMultiTouchable> _multiTouchablesToRemove = new List<ICCMultiTouchable>();

    private readonly List<ICCSingleTouchable> _singleTouchables = new List<ICCSingleTouchable>();

    private readonly List<ICCSingleTouchable> _singleTouchablesToAdd = new List<ICCSingleTouchable>();

    private readonly List<ICCSingleTouchable> _singleTouchablesToRemove = new List<ICCSingleTouchable>();

    private bool _isUpdating;

    private bool _needsPrioritySort;

    private Vector2 _previousMousePosition = new Vector2(0, 0);

    private ICCSingleTouchable _theSingleTouchable;

    #endregion

    #region Constructors and Destructors

    public CCTouchMgr()
    {
        Input.multiTouchEnabled = true;
#if UNITY_ANDROID
        ShouldMouseEmulateTouch = false;
#endif

#if UNITY_IPHONE
        ShouldMouseEmulateTouch = false;
#endif

#if UNITY_EDITOR
        ShouldMouseEmulateTouch = true;
#endif
    }

    #endregion

    #region Public Methods and Operators

    public static CCTouchMgr SharedTouchManager
    {
        get
        {
            if (_sharedTouchManager == null)
            {
                _sharedTouchManager = new CCTouchMgr();
            }
            return _sharedTouchManager;
        }
    }

    public static void UnsharedTouchManager()
    {
        _sharedTouchManager = null;
    }


    public void AddMultiTouchTarget(ICCMultiTouchable touchable)
    {
        if (_isUpdating)
        {
            if (!_multiTouchablesToAdd.Contains(touchable))
            {
                int index = _multiTouchablesToRemove.IndexOf(touchable);
                if (index != -1)
                {
                    _multiTouchablesToRemove.RemoveAt(index);
                }
                _multiTouchablesToAdd.Add(touchable);
            }
        }
        else
        {
            if (!_multiTouchables.Contains(touchable))
            {
                _multiTouchables.Add(touchable);
            }
        }
    }

    public void AddSingleTouchTarget(ICCSingleTouchable touchable)
    {
        if (_isUpdating)
        {
            if (!_singleTouchablesToAdd.Contains(touchable))
            {
                int index = _singleTouchablesToRemove.IndexOf(touchable);
                if (index != -1)
                {
                    _singleTouchablesToRemove.RemoveAt(index);
                }
                _singleTouchablesToAdd.Add(touchable);
            }
        }
        else
        {
            if (!_singleTouchables.Contains(touchable))
            {
                _singleTouchables.Add(touchable);
            }
        }
        _needsPrioritySort = true;
    }

    public bool DoesTheSingleTouchableExist()
    {
        return (_theSingleTouchable != null);
    }

    public void HandleDepthChange()
    {
        _needsPrioritySort = true;
    }

    //public Vector2 PointToOpenGL(Vector2 vector2)
    //{
    //    return new Vector2(vector2.x * this.xSize, vector2.y * this.ySize) + new Vector2(this.offsetX, this.offsetY);
    //}

    public void RemoveMultiTouchTarget(ICCMultiTouchable touchable)
    {
        if (_isUpdating)
        {
            if (!_multiTouchablesToRemove.Contains(touchable))
            {
                int index = _multiTouchablesToAdd.IndexOf(touchable);
                if (index != -1)
                {
                    _multiTouchablesToAdd.RemoveAt(index);
                }
                _multiTouchablesToRemove.Add(touchable);
            }
        }
        else
        {
            _multiTouchables.Remove(touchable);
        }
    }

    public void RemoveSingleTouchTarget(ICCSingleTouchable touchable)
    {
        if (_isUpdating)
        {
            if (!_singleTouchablesToRemove.Contains(touchable))
            {
                int index = _singleTouchablesToAdd.IndexOf(touchable);
                if (index != -1)
                {
                    _singleTouchablesToAdd.RemoveAt(index);
                }
                _singleTouchablesToRemove.Add(touchable);
            }
        }
        else
        {
            _singleTouchables.Remove(touchable);
        }
    }

    public void Update()
    {
        if (!IsEnabled)
        {
            return;
        }

        _isUpdating = true;

        if (_needsPrioritySort)
        {
            UpdatePrioritySorting();
        }

        bool wasMouseTouch = false;
        var mouseTouch = new CCTouch();

        if (ShouldMouseEmulateTouch)
        {
            mouseTouch.Position = new Vector2(
                (Input.mousePosition.x),
                (Input.mousePosition.y));

            mouseTouch.FingerId = 0;
            mouseTouch.TapCount = 1;
            mouseTouch.DeltaTime = Time.deltaTime;

            if (Input.GetMouseButtonDown(0))
            {
                mouseTouch.DeltaPosition = new Vector2(0, 0);
                _previousMousePosition = mouseTouch.Position;

                mouseTouch.Phase = TouchPhase.Began;
                wasMouseTouch = true;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                mouseTouch.DeltaPosition = new Vector2(
                    mouseTouch.Position.x - _previousMousePosition.x,
                    mouseTouch.Position.y - _previousMousePosition.y);
                _previousMousePosition = mouseTouch.Position;
                mouseTouch.Phase = TouchPhase.Ended;
                wasMouseTouch = true;
            }
            else if (Input.GetMouseButton(0))
            {
                mouseTouch.DeltaPosition = new Vector2(
                    mouseTouch.Position.x - _previousMousePosition.x,
                    mouseTouch.Position.y - _previousMousePosition.y);
                _previousMousePosition = mouseTouch.Position;

                mouseTouch.Phase = TouchPhase.Moved;
                wasMouseTouch = true;
            }
        }

        int touchCount = Input.touchCount;
        int offset = 0;

        if (wasMouseTouch)
        {
            touchCount++;
        }

        var touches = new CCTouch[touchCount];

        if (wasMouseTouch)
        {
            touches[0] = mouseTouch;
            offset = 1;
        }

        for (int i = 0; i < Input.touchCount; ++i)
        {
            Touch sourceTouch = Input.GetTouch(i);
            var resultTouch = new CCTouch();

            resultTouch.DeltaPosition = new Vector2(
                sourceTouch.deltaPosition.x,
                sourceTouch.deltaPosition.y);
            resultTouch.DeltaTime = sourceTouch.deltaTime;
            resultTouch.FingerId = sourceTouch.fingerId + offset;
            resultTouch.Phase = sourceTouch.phase;
            resultTouch.Position = new Vector2(
                (sourceTouch.position.x),
                (sourceTouch.position.y));
            resultTouch.TapCount = sourceTouch.tapCount;

            touches[i + offset] = resultTouch;
        }

        int singleTouchableCount = _singleTouchables.Count;

        int lowestFingerId = int.MaxValue;

        for (int t = 0; t < touchCount; t++)
        {
            CCTouch touch = touches[t];
            if (touch.FingerId < lowestFingerId)
            {
                lowestFingerId = touch.FingerId;
            }
        }

        for (int t = 0; t < touchCount; t++)
        {
            CCTouch touch = touches[t];
            if (touch.FingerId == lowestFingerId) // we only care about the first touch for the singleTouchables
            {
                if (touch.Phase == TouchPhase.Began)
                {
                    for (int s = 0; s < singleTouchableCount; s++)
                    {
                        ICCSingleTouchable singleTouchable = _singleTouchables[s];

                        if (singleTouchable.HandleSingleTouchBegan(touch))
                            //the first touchable to return true becomes theSingleTouchable
                        {
                            _theSingleTouchable = singleTouchable;
                            break;
                        }
                    }
                }
                else if (touch.Phase == TouchPhase.Ended)
                {
                    if (_theSingleTouchable != null)
                    {
                        _theSingleTouchable.HandleSingleTouchEnded(touch);
                    }
                    _theSingleTouchable = null;
                }
                else if (touch.Phase == TouchPhase.Canceled)
                {
                    if (_theSingleTouchable != null)
                    {
                        _theSingleTouchable.HandleSingleTouchCanceled(touch);
                    }
                    _theSingleTouchable = null;
                }
                else //moved or stationary
                {
                    if (_theSingleTouchable != null)
                    {
                        _theSingleTouchable.HandleSingleTouchMoved(touch);
                    }
                }

                break; //break out from the foreach, once we've found the first touch we don't care about the others
            }
        }

        if (touchCount > 0)
        {
            int multiTouchableCount = _multiTouchables.Count;
            for (int m = 0; m < multiTouchableCount; m++)
            {
                _multiTouchables[m].HandleMultiTouch(touches);
            }
        }

        //now add or remove anything that was changed while we were looping through

        for (int s = 0; s < _singleTouchablesToRemove.Count; s++)
        {
            _singleTouchables.Remove(_singleTouchablesToRemove[s]);
        }

        for (int s = 0; s < _singleTouchablesToAdd.Count; s++)
        {
            _singleTouchables.Add(_singleTouchablesToAdd[s]);
        }

        for (int m = 0; m < _multiTouchablesToRemove.Count; m++)
        {
            _multiTouchables.Remove(_multiTouchablesToRemove[m]);
        }

        for (int m = 0; m < _multiTouchablesToAdd.Count; m++)
        {
            _multiTouchables.Add(_multiTouchablesToAdd[m]);
        }

        _singleTouchablesToRemove.Clear();
        _singleTouchablesToAdd.Clear();
        _multiTouchablesToRemove.Clear();
        _multiTouchablesToAdd.Clear();

        _isUpdating = false;
    }

    #endregion

    #region Methods

    private static int PriorityComparison(ICCSingleTouchable a, ICCSingleTouchable b)
    {
        return b.TouchPriority - a.TouchPriority;
    }

    private void UpdatePrioritySorting()
    {
        _needsPrioritySort = false;
        _singleTouchables.Sort(PriorityComparison);
    }

    #endregion
}