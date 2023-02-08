using UnityEngine;

namespace ArrayVisualizer
{
    public class ElementColor
    {
        private int _elementIndex;
        private Color _elementColor;

        public int ElementIndex => _elementIndex;

        public Color ElementColor1 => _elementColor;


        public static ElementColor Build(int index, Color color)
        {
            ElementColor elementColor = new ElementColor
            {
                _elementColor = color,
                _elementIndex = index
            };
            return elementColor;
        }
    }
}