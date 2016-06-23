using UnityEngine;
using System.Collections;

public class TurnLeftCommand : ICommand 
{
    Rigidbody _rigidbody;

    public TurnLeftCommand(GameObject agent)
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
