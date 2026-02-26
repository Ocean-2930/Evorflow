using UnityEngine;

public interface ICustomMouseInterface
{
    public void OnEnter() { }
    public void OnHover() { }
    public void OnExit() { }
    public void OnRightClick() { }
    public void OnLeftClick() { }
    public void OnDragRelease() { }
    public void OnDragRecieve(GameObject obj) { }
}

/*
public class CustomMouseTrigger : MonoBehaviour
{
    public Vector2 LD
    {
        get
        {
            Vector3[] corners = new Vector3[4];
            gameObject.GetComponent<RectTransform>().GetWorldCorners(corners);
            return RectTransformUtility.WorldToScreenPoint(null, corners[0]);
        }
    }

    public Vector2 RU
    {
        get
        {
            Vector3[] corners = new Vector3[4];
            gameObject.GetComponent<RectTransform>().GetWorldCorners(corners);
            return RectTransformUtility.WorldToScreenPoint(null, corners[2]);
        }
    }

    public void Awake()
    {
        MouseControl.inst.Register(this);
    }

    public virtual void OnEnter() { }

    public virtual void OnHover() { }

    public virtual void OnExit() { }

    public virtual void OnRightClick() { }

    public virtual void OnLeftClick() { }

    public virtual void OnDragRelease() { }

    public virtual void OnDragRecieve(GameObject obj) { }

    public void StartDrag()
    {
        MouseControl.inst.Grab(this);
    }

    public bool CheckPos(Vector2 pos)
    {
        bool rbool = LD.x <= pos.x && LD.y <= pos.y;
        rbool = rbool && (pos.x <= RU.x && pos.y <= RU.y);
        return rbool;
    }

    public void OnDestroy()
    {
        if(MouseControl.inst != null)
        {
            MouseControl.inst.Dismiss(this);
        }       
    }
}

public class CustomMouseComponent : CustomMouseTrigger
{
    private ICustomMouseTrigger target;

    public void Initialize(ICustomMouseTrigger input)
    {
        target = input;
    }

    public override void OnEnter() { target.OnEnter(); }
    public override void OnHover() { target.OnHover(); }
    public override void OnExit() { target.OnExit(); }
    public override void OnRightClick() { target.OnRightClick(); }
    public override void OnLeftClick() { target.OnLeftClick(); }
    public override void OnDragRelease() { target.OnDragRelease(); }
    public override void OnDragRecieve(GameObject obj) { target.OnDragRecieve(obj); }
}

public interface ICustomMouseTrigger
{
    public void OpenInterface(GameObject obj)
    {
        CustomMouseComponent cp = obj.AddComponent<CustomMouseComponent>();
        cp.Initialize(this);
    }

    public virtual void OnEnter() { }
    public virtual void OnHover() { }
    public virtual void OnExit() { }
    public virtual void OnRightClick() { }
    public virtual void OnLeftClick() { }
    public virtual void OnDragRelease() { }
    public virtual void OnDragRecieve(GameObject obj) { }
}
*/

/*
public abstract class Dragable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }
}
*/