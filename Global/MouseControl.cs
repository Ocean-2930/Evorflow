using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseControl : MonoBehaviour
{
    public static MouseControl inst
    {
        get
        {
            if (instance != null)
            {
                return instance;
            }

            GameObject temp = GameObject.Find("MouseControl");

            if (temp == null)
            {
                return null;
            }

            instance = temp.GetComponent<MouseControl>();
            return instance;
        }
    }

    public static MouseControl instance;

    [SerializeField] private GameObject sceneCanvas;
    private List<CustomMouseInterface> customMCList = new List<CustomMouseInterface>();
    private CustomMouseInterface currentFocus = null;
    private CustomMouseInterface grabObject;

    void Update()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();

        if (grabObject != null)
        {
            if (Mouse.current.leftButton.isPressed)
            {
                Vector2 localPos;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    grabObject.transform.parent as RectTransform,
                    mousePos,
                    null,
                    out localPos
                );
                grabObject.gameObject.GetComponent<RectTransform>().localPosition = localPos;
                return;
            }
        }

        List<CustomMouseInterface> candidate = new List<CustomMouseInterface>();
        for (int i = customMCList.Count - 1; 0 <= i; i--)
        {
            if (customMCList[i] == grabObject)
            {
                continue;
            }

            if (!customMCList[i].gameObject.activeInHierarchy)
            {
                continue;
            }

            if (customMCList[i].CheckPos(mousePos))
            {
                candidate.Add(customMCList[i]);
            }
        }

        CustomMouseInterface focus;
        if (candidate.Count == 0)
        {
            focus = null;
        }
        else if (candidate.Count == 1)
        {
            focus = candidate[0];
        }
        else
        {
            focus = FindCMI(sceneCanvas.transform, candidate);
        }

        if (grabObject != null)
        {
            if (focus != null)
            {
                focus.OnDragRecieve(grabObject.gameObject);
            }
            grabObject.OnDragRelease();
            grabObject = null;
        }

        if (focus == null)
        {
            if (currentFocus != null)
            {
                currentFocus.OnExit();
                currentFocus = null;
            }
            return;
        }

        if (currentFocus != focus)
        {
            if (currentFocus != null)
            {
                currentFocus.OnExit();
            }

            currentFocus = focus;
            focus.OnEnter();
        }

        currentFocus.OnHover();

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            currentFocus.OnLeftClick();
        }
        else if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            currentFocus.OnRightClick();
        }
    }

    private CustomMouseInterface FindCMI(Transform tf, List<CustomMouseInterface> cmiList)
    {
        if (tf.childCount != 0)
        {
            for (int i = tf.childCount - 1; 0 <= i; i--)
            {
                CustomMouseInterface childCMI = FindCMI(tf.GetChild(i), cmiList);
                if (childCMI != null)
                {
                    return childCMI;
                }
            }
        }

        CustomMouseInterface myCMI = tf.GetComponent<CustomMouseInterface>();
        if (myCMI != null && cmiList.Contains(myCMI))
        {
            return myCMI;
        }

        return null;
    }

    public void Register(CustomMouseInterface customMC)
    {
        customMCList.Add(customMC);
    }

    public void Grab(CustomMouseInterface obj)
    {
        grabObject = obj;

        if (currentFocus != null)
        {
            currentFocus.OnExit();
            currentFocus = null;
        }
    }

    public void Dismiss(CustomMouseInterface customMC)
    {
        customMCList.Remove(customMC);
    }
}