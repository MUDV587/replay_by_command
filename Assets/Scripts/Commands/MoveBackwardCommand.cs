using UnityEngine;
using System.Collections;

public class MoveBackwardCommand : ICommand 
{
    Rigidbody _rigidbody;

    public MoveBackwardCommand(GameObject agent)
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
