using UnityEngine;
using System.Collections;

public class TurnRightCommand : ICommand 
{
    Rigidbody _rigidbody;

    public TurnRightCommand(GameObject agent)
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
