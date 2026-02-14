using System.Collections.Generic;
using UnityEngine;

public class CustomMouseMaskedTrigger : CustomMouseTrigger
{
    [SerializeField] private GameObject maskobj;
    private RectTransform _mask;
    private RectTransform mask
    {
        get
        {
            if (_mask != null)
            {
                return _mask;
            }

            if (maskobj != null)
            {
                _mask = maskobj.GetComponent<RectTransform>();
                return _mask;
            }

            return null;
        }
    }

    public override bool CheckPos(Vector2 screenPos)
    {
        if (!base.CheckPos(screenPos))
        {
            return false;
        }

        if (mask != null)
        {
            if (!RectTransformUtility.RectangleContainsScreenPoint(mask, screenPos, null)) {
                return false;
            }
        }

        return true;
    }

    public void SetMask(GameObject obj)
    {
        _mask = obj.GetComponent<RectTransform>();
    }
}
