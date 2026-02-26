using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseControl : SceneSingleton<MouseControl>
{
    public override string className => "MouseControl";


    [SerializeField] private GameObject sceneCanvas;
    private List<CustomMouseTrigger> customMCList = new List<CustomMouseTrigger>();

    private void Awake()
    {
        if (sceneCanvas == null)
        {
            sceneCanvas = gameObject.scene.GetRootGameObjects()
                .SelectMany(root => root.GetComponentsInChildren<Canvas>(true))
                .FirstOrDefault()?.gameObject;
        }
    }
    private CustomMouseTrigger currentFocus = null;
    private CustomMouseTrigger grabObject;

    void Update()
    {
        if (Mouse.current == null)
        {
            return;
        }

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

        List<CustomMouseTrigger> candidate = new List<CustomMouseTrigger>();
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

        CustomMouseTrigger focus;
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
            if (sceneCanvas == null)
            {
                focus = candidate[0];
            }
            else
            {
                focus = FindCMI(sceneCanvas.transform, candidate);
            }
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

    private CustomMouseTrigger FindCMI(Transform tf, List<CustomMouseTrigger> cmiList)
    {
        if (tf.childCount != 0)
        {
            for (int i = tf.childCount - 1; 0 <= i; i--)
            {
                CustomMouseTrigger childCMI = FindCMI(tf.GetChild(i), cmiList);
                if (childCMI != null)
                {
                    return childCMI;
                }
            }
        }

        CustomMouseTrigger myCMI = tf.GetComponent<CustomMouseTrigger>();
        if (myCMI != null && cmiList.Contains(myCMI))
        {
            return myCMI;
        }

        return null;
    }

    public void Register(CustomMouseTrigger customMC)
    {
        customMCList.Add(customMC);
    }

    public void Grab(CustomMouseTrigger obj)
    {
        grabObject = obj;

        if (currentFocus != null)
        {
            currentFocus.OnExit();
            currentFocus = null;
        }
    }

    public void Dismiss(CustomMouseTrigger customMC)
    {
        customMCList.Remove(customMC);
    }
}