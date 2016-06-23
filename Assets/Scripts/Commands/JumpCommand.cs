using UnityEngine;
using System.Collections;

public class JumpCommand : ICommand 
{
    Rigidbody _rigidbody;

    public JumpCommand(GameObject agent)
    {

    }

    void ICommand.Execute()
    { 
    
    }

    void ICommand.Undo()
    {
        //UNUSED
    }

    void ICommand.Redo()
    {
        //UNUSED
    }
}
