using UnityEngine;

public class InputHandler
{
    private static bool blockInput = false;
    public static bool BlockInput { get => blockInput; set => blockInput = value; }

    public static float GetAxis(string axisName)
    {
        if (blockInput)
        {
            return 0;
        }

        return Input.GetAxis(axisName);
    }
    
    public static bool GetMouseButtonDown(int button)
    {
        if (blockInput)
        {
            return false;
        }
        
        return Input.GetMouseButtonDown(button);
    }

    public static bool GetMouseButton(int button)
    {
        if (blockInput)
        {
            return false;
        }
        
        return Input.GetMouseButton(button);
    }
    
    public static bool GetMouseButtonUp(int button)
    {
        if (blockInput)
        {
            return false;
        }
        
        return Input.GetMouseButtonUp(button);
    }

    public static bool GetKeyDown(KeyCode key)
    {
        if (blockInput)
        {
            return false;
        }
        
        return Input.GetKey(key);
    }
}