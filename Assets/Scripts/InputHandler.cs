using UnityEngine;
using System.Collections;

public enum CommandCode : uint
{
    W = 1,
    S = 1 << 1,   // 2
    A = 1 << 2,   // 4
    D = 1 << 3,   // 8
    RETURN = 1 << 4,   // 16
    SPACE = 1 << 5    // 32
}

public class InputHandler 
{
    public InputHandler()
    {
    }

    public uint Evaluate()
    {
        uint frameCmd = 0;
        if (Input.GetKey(KeyCode.W))
            frameCmd |= (uint)CommandCode.W;

        if (Input.GetKey(KeyCode.S))
            frameCmd |= (uint)CommandCode.S;

        if (Input.GetKey(KeyCode.A))
            frameCmd |= (uint)CommandCode.A;

        if (Input.GetKey(KeyCode.D))
            frameCmd |= (uint)CommandCode.D;

        if (Input.GetKey(KeyCode.Return))
            frameCmd |= (uint)CommandCode.RETURN;

        if (Input.GetKey(KeyCode.Space))
            frameCmd |= (uint)CommandCode.SPACE;

        return frameCmd;
    }
}
