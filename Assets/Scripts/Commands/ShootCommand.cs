using UnityEngine;
using System.Collections;

public class ShootCommand : ICommand 
{
    Transform _transform;

    public ShootCommand(GameObject agent)
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
