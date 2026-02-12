using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class CustomMouseTrigger : MonoBehaviour
{
    private ICustomMouseInterface _targetInterface;
    private RectTransform _rectTransform;

    public RectTransform RectTrans
    {
        get
        {
            if (_rectTransform == null) _rectTransform = GetComponent<RectTransform>();
            return _rectTransform;
        }
    }

    private void Awake()
    {
        _targetInterface = GetComponent<ICustomMouseInterface>();

        if (_targetInterface != null)
        {
            MouseControl.inst.Register(this);
        }
    }

    private void OnDestroy()
    {
        if (MouseControl.inst != null)
        {
            MouseControl.inst.Dismiss(this);
        }
    }

    public bool CheckPos(Vector2 screenPos)
    {
        return RectTransformUtility.RectangleContainsScreenPoint(RectTrans, screenPos, null);
    }

    public void OnEnter() => _targetInterface?.OnEnter();
    public void OnHover() => _targetInterface?.OnHover();
    public void OnExit() => _targetInterface?.OnExit();
    public void OnRightClick() => _targetInterface?.OnRightClick();
    public void OnLeftClick() => _targetInterface?.OnLeftClick();
    public void OnDragRelease() => _targetInterface?.OnDragRelease();
    public void OnDragRecieve(GameObject obj) => _targetInterface?.OnDragRecieve(obj);

    public void StartDrag()
    {
        MouseControl.inst.Grab(this);
    }
}
