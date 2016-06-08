using System;
using UnityEngine;

public class CCButton : MonoBehaviour, ICCSingleTouchable
{
    #region Fields

    public AudioSource _clickSound;

    public Color _downColor = Color.white;

    public Sprite _downElement;

    public Rect _hitRect;
    private bool _isEnabled = true;
    protected bool _isTouchDown = false;

    public Color _overColor = Color.white;

    public Sprite _overElement;

    public bool _shouldUseCustomColors = false;

    public bool _shouldUseCustomHitRect = false;
    private bool _supportsOver;

    public Color _upColor = Color.white;

    public Sprite _upElement;

    public float expansionAmount = .95f;

    private float xsize;

    private float ysize;

    public bool Enabled
    {
        get { return _enabled; }
        set
        {
            _enabled = value;
            if (!_enabled)
            {
                GetComponent<SpriteRenderer>().color = new Color(0.5019608F, 0.5019608F, 0.5019608F, 1);
            }
            else
            {
                GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            }
        }
    }

    #endregion

    #region Delegates

    public delegate void ButtonSignalDelegate(CCButton button);

    #endregion

    #region Public Events

    public event ButtonSignalDelegate SignalPress;

    public event ButtonSignalDelegate SignalRelease;

    public event ButtonSignalDelegate SignalReleaseOutside;

    #endregion

    #region Public Properties

    public int _touchPriority = 10;

    public int TouchPriority
    {
        get { return _touchPriority; }
    }

    #endregion

    #region Public Methods and Operators

    private Color _defaultOverColor;
    private Sprite _defaultOverElement;
    private Color _defaultUpColor;
    private Sprite _defaultUpElement;


    public bool HandleSingleTouchBegan(CCTouch touch)
    {
        _isTouchDown = false;

        if (_hitRect.Contains(UnityEngine. Camera.main.ScreenToWorldPoint(touch.Position)))
        {
            if (_isEnabled)
            {
                if (Enabled)
                {
                    if (_downElement != null)
                    {
                        GetComponent<SpriteRenderer>().sprite = _downElement;
                    }
                    transform.localScale = new Vector3(
                        xsize*expansionAmount,
                        ysize*expansionAmount);
                    if (_shouldUseCustomColors)
                    {
                        GetComponent<SpriteRenderer>().color = _downColor;
                    }
                    if (_clickSound != null)
                    {
                        _clickSound.Play();
                    }
                    if (SignalPress != null)
                    {
                        SignalPress(this);
                    }
                    _isTouchDown = true;
                    return true;
                }
            }
        }

        return false;
    }

    public void HandleSingleTouchCanceled(CCTouch touch)
    {
        _isTouchDown = false;
        if (!_isEnabled)
        {
            return;
        }
        if (!Enabled)
        {
            return;
        }
        GetComponent<SpriteRenderer>().sprite = _upElement;
        if (_shouldUseCustomColors)
        {
            GetComponent<SpriteRenderer>().color = _upColor;
        }
        if (SignalReleaseOutside != null)
        {
            SignalReleaseOutside(this);
        }
        transform.localScale = new Vector3(xsize, ysize);
    }

    public void HandleSingleTouchEnded(CCTouch touch)
    {
        _isTouchDown = false;
        if (!_isEnabled)
        {
            return;
        }
        if (!Enabled)
        {
            return;
        }
        if (_hitRect.Contains(UnityEngine.Camera.main.ScreenToWorldPoint(touch.Position)))
        {
            if (SignalRelease != null)
            {
                SignalRelease(this);
            }
            if (_overElement != null)
            {
                if (!_supportsOver)
                {
                    GetComponent<SpriteRenderer>().sprite = _overElement;
                    if (_shouldUseCustomColors)
                    {
                        GetComponent<SpriteRenderer>().color = _overColor;
                    }
                }
                else
                {
                    GetComponent<SpriteRenderer>().sprite = _upElement;
                    if (_shouldUseCustomColors)
                    {
                        GetComponent<SpriteRenderer>().color = _upColor;
                    }
                }
                _supportsOver = !_supportsOver;
            }
            else
            {
                GetComponent<SpriteRenderer>().sprite = _upElement;
                if (_shouldUseCustomColors)
                {
                    GetComponent<SpriteRenderer>().color = _upColor;
                }
            }
        }
        else
        {
            if (SignalReleaseOutside != null)
            {
                SignalReleaseOutside(this);
            }
        }
        transform.localScale = new Vector3(xsize, ysize);
    }

    public void HandleSingleTouchMoved(CCTouch touch)
    {
        if (!_isEnabled)
        {
            return;
        }
        if (!Enabled)
        {
            return;
        }
        if (_hitRect.Contains(UnityEngine.Camera.main.ScreenToWorldPoint(touch.Position)))
        {
            if (_downElement != null)
            {
                GetComponent<SpriteRenderer>().sprite = _downElement;
            }
            if (_shouldUseCustomColors)
            {
                GetComponent<SpriteRenderer>().color = _downColor;
            }
            _isTouchDown = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = _upElement;
            if (_shouldUseCustomColors)
            {
                GetComponent<SpriteRenderer>().color = _upColor;
            }
            transform.localScale = new Vector3(xsize, ysize);
            _isTouchDown = false;
        }
    }

    private void OnEnable()
    {
        _isEnabled = true;
    }

    private void OnDisable()
    {
        _isEnabled = false;
    }


    public void UnSelected()
    {
        GetComponent<SpriteRenderer>().sprite = _upElement;
        if (_shouldUseCustomColors)
        {
            GetComponent<SpriteRenderer>().color = _upColor;
        }
        transform.localScale = new Vector3(xsize, ysize);
        _isTouchDown = false;
    }

    private void DefaultStatus()
    {
        _upElement = _defaultUpElement;
        _overElement = _defaultOverElement;
        _upColor = _defaultUpColor;
        _overColor = _defaultOverColor;
    }

    public void SwapStatus()
    {
        DefaultStatus();
        Sprite sprite = _overElement;
        _overElement = _upElement;
        _upElement = sprite;

        Color color = _overColor;
        _overColor = _upColor;
        _upColor = color;
        GetComponent<SpriteRenderer>().sprite = _upElement;
        if (_shouldUseCustomColors)
        {
            GetComponent<SpriteRenderer>().color = _upColor;
        }
        transform.localScale = new Vector3(xsize, ysize);
        _isTouchDown = false;
    }

    #endregion

    #region Methods

    private const int PixelsToUnits = 100;
    private bool _enabled = true;

    private Vector2 _positionVector2;
    private Vector2 _size;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        xsize = transform.localScale.x;
        ysize = transform.localScale.y;
        if (gameObject.name == "7")
        {
            Debug.Log("7");
        }
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (!_shouldUseCustomHitRect)
        {
            float width = spriteRenderer.sprite.textureRect.width*xsize/PixelsToUnits;
            float height = spriteRenderer.sprite.textureRect.height*ysize/PixelsToUnits;
            Vector3 vector;

            vector = transform.position;

            _hitRect = new Rect(vector.x - width/2f, vector.y - height/2, width, height);
        }

        if (GetComponent<SpriteRenderer>() != null)
        {
            _upElement = GetComponent<SpriteRenderer>().sprite;
        }
        else
        {
            gameObject.AddComponent<SpriteRenderer>().sprite = _upElement;
        }
        CCTouchMgr.SharedTouchManager.AddSingleTouchTarget(this);
        _positionVector2 = transform.position;
        _size = transform.localScale;


        _defaultUpElement = _upElement;
        _defaultOverElement = _overElement;

        _defaultUpColor = _upColor;
        _defaultOverColor = _overColor;
    }

    private void Start()
    {
        //xsize = transform.localScale.x;
        //ysize = transform.localScale.y;
        //spriteRenderer = GetComponent<SpriteRenderer>();
        //if (!_shouldUseCustomHitRect)
        //{
        //    float width = spriteRenderer.sprite.textureRect.width*xsize/PixelsToUnits;
        //    float height = spriteRenderer.sprite.textureRect.height*ysize/PixelsToUnits;
        //    Vector3 vector;

        //    vector = transform.position;

        //    _hitRect = new Rect(vector.x - width/2f, vector.y - height/2, width, height);
        //}

        //if (GetComponent<SpriteRenderer>() != null)
        //{
        //    _upElement = GetComponent<SpriteRenderer>().sprite;
        //}
        //else
        //{
        //    gameObject.AddComponent<SpriteRenderer>().sprite = _upElement;
        //}
        //CCTouchMgr.SharedTouchManager().AddSingleTouchTarget(this);
        //_positionVector2 = transform.position;
        //_size = transform.localScale;
    }

    public void UpdateHitRict()
    {
        _positionVector2 = new Vector2(transform.position.x, transform.position.y);
        xsize = transform.localScale.x;
        ysize = transform.localScale.y;
        if (!_shouldUseCustomHitRect)
        {
            float width = spriteRenderer.sprite.textureRect.width*xsize/PixelsToUnits;
            float height = spriteRenderer.sprite.textureRect.height*ysize/PixelsToUnits;
            Vector3 vector;

            vector = _positionVector2;

            _hitRect = new Rect(vector.x - width/2f, vector.y - height/2, width, height);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (Math.Abs(expansionAmount - 1f) <= 0)
        {
            xsize = transform.localScale.x;
            ysize = transform.localScale.y;
        }
      
        if (_positionVector2 != new Vector2(transform.position.x, transform.position.y))
        {
            _positionVector2 = new Vector2(transform.position.x, transform.position.y);
            if (!_shouldUseCustomHitRect)
            {
                float width = spriteRenderer.sprite.textureRect.width*xsize/PixelsToUnits;
                float height = spriteRenderer.sprite.textureRect.height*ysize/PixelsToUnits;
                Vector3 vector;

                vector = _positionVector2;

                _hitRect = new Rect(vector.x - width/2f, vector.y - height/2, width, height);
            }
        }
    }

    #endregion
}