using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constants
{
    public static float RGB = 0.00392156862f;
    public static Color BLUE = new Color(43 * RGB, 177 * RGB, 204 * RGB); //Hex: #2bb1cc
    public static Color RED = new Color(242 * RGB, 63 * RGB, 63 * RGB); //Hex: #f23f3f
    public static Color PURPLE = new Color(149 * RGB, 72 * RGB, 217 * RGB); //Hex: #9548d9
    public static Color GREEN = new Color(63 * RGB, 194 * RGB, 56 * RGB); //Hex: #3fc238
    public static Color GRAY = new Color(117 * RGB, 117 * RGB, 117 * RGB); //Hex: #757575

    public static Color GetBackgroundColor(Vector2 growth)
    {
        if (growth.x >= 0 && growth.y >= 0)
        {
            return BLUE;
        }
        else if (growth.x >= 0 || growth.y >= 0)
        {
            return PURPLE;
        }
        else
        {
            return RED;
        }
    }
}
