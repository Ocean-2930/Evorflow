using UnityEngine;
using UnityEngine.InputSystem;

public class CustomScrollUI_Manual : CustomMouseInterface
{
    [System.Serializable]
    private struct SerialField
    {
        public GameObject windowObj;
        public GameObject scrollObj;
        public float padding;
        public float scrollSpeed;
    }

    [SerializeField] private SerialField scrollProperty = new SerialField();

    protected GameObject windowObj => scrollProperty.windowObj;
    private RectTransform buff_windowTransform;
    protected RectTransform windowTransform
    {
        get
        {
            if (buff_windowTransform != null) { return buff_windowTransform; }
            buff_windowTransform = windowObj.GetComponent<RectTransform>();
            return buff_windowTransform;
        }
    }


    protected GameObject scrollObj => scrollProperty.scrollObj;
    private RectTransform buff_scrollTransform;
    protected RectTransform scrollTransform
    {
        get
        {
            if (buff_scrollTransform != null) { return buff_scrollTransform; }
            buff_scrollTransform = scrollObj.GetComponent<RectTransform>();
            return buff_scrollTransform;
        }
    }

    protected float padding => scrollProperty.padding;
    protected float scrollSpeed => scrollProperty.scrollSpeed;

    private float contentlen = 0.0f;
    private float position = 0.0f;

    public override void OnHover()
    {
        float scroll = Mouse.current.scroll.ReadValue().y;
        if (0 < scroll)
        {
            ScrollUp();
        }
        else if (scroll < 0)
        {
            ScrollDown();
        }
    }

    public void AddContent(GameObject obj)
    {
        if (obj.transform.parent == scrollObj) { return; }

        RectTransform rect = obj.GetComponent<RectTransform>();
        rect.SetParent(scrollTransform);
        float ylen = rect.sizeDelta.y;

        if (contentlen == 0.0f)
        {
            rect.anchoredPosition = new Vector3(0, -ylen * 0.5f, 0);
            contentlen = ylen;
        }
        else
        {
            rect.anchoredPosition = new Vector3(0, -(contentlen + padding + ylen * 0.5f), 0);
            contentlen = contentlen + padding + ylen;
        }
    }

    public void RemoveContent(GameObject obj)
    {
        if (obj.transform.parent != scrollObj) { return; }

        contentlen -= obj.GetComponent<RectTransform>().sizeDelta.y;
        Destroy(obj);

        if (contentlen == 0)
        {
            ToTop();
            return;
        }

        float clen = 0.0f;
        float plen = 0.0f;
        for (int i = 0; i < scrollObj.transform.childCount; i++)
        {
            RectTransform rect = scrollObj.transform.GetChild(i).GetComponent<RectTransform>();
            float ylen = rect.sizeDelta.y * 0.5f;
            clen = clen + plen + (plen == 0.0f ? 0 : 1) * padding + ylen;
            rect.anchoredPosition = new Vector3(0, -clen, 0);
            plen = ylen;
        }

        contentlen = clen + plen;
        float maxlen = contentlen - windowTransform.rect.height;
        position = (maxlen < position) ? maxlen : position;
        scrollTransform.anchoredPosition = new Vector3(0, position, 0);
    }

    public void ToTop()
    {
        position = 0.0f;
        scrollTransform.anchoredPosition = new Vector3(0, 0, 0);
    }

    public void ToBottom()
    {
        float maxlen = contentlen - windowTransform.rect.height;
        position = (windowTransform.rect.height < contentlen) ? maxlen : 0;
        scrollTransform.anchoredPosition = new Vector3(0, position, 0);
    }

    public bool ScrollUp()
    {
        float np = position - scrollSpeed;
        position = (np < 0) ? 0 : np;
        scrollTransform.anchoredPosition = new Vector3(0, position, 0);
        return 0 <= np;
    }

    public bool ScrollDown()
    {
        if (contentlen < windowTransform.rect.height)
        {
            ToTop();
            return false;
        }

        float np = position + scrollSpeed;
        float maxlen = contentlen - windowTransform.rect.height;
        position = (maxlen < np) ? maxlen : np;
        scrollTransform.anchoredPosition = new Vector3(0, position, 0);
        return np <= maxlen;
    }
}
