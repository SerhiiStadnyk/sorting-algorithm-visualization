using UnityEngine;

public class ElementColor
{
    public int elementInxe;
    public Color elementColor;

    static public ElementColor Build(int index, Color color) 
    {
        ElementColor elementColor = new ElementColor() 
        {
            elementColor = color,
            elementInxe = index
        };
        return elementColor;
    }
}