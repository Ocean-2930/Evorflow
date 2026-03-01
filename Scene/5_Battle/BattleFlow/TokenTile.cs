using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class TokenTile : MonoBehaviour, ICustomMouseInterface
{
    private int _xpos;
    private int _ypos;

    public int xpos { get { return _xpos; } }
    public int ypos { get { return _ypos; } }

    private UnitInst_Battle _unit;
    public UnitInst_Battle unit
    {
        get { return _unit; }
        set
        {
            if (_unit == value)
            {
                return;
            }

            if (_unit != null)
            {
                UnitInst_Battle oldUnit = _unit;
                _unit = null;

                if (oldUnit.tile == this)
                {
                    oldUnit.tile = null;
                }
            }

            if (value != null && value.tile != this)
            {
                value.tile = this;
            }

            _unit = value;
        }
    }

    public void SetTilePosition(int xpos, int ypos)
    {
        _xpos = xpos;
        _ypos = ypos;
    }
}
