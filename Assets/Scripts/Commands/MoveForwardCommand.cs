using UnityEngine;
using System.Collections;

public class MoveForwardCommand : ICommand 
{
    Rigidbody _rigidbody;
    Transform _transform;

    public MoveForwardCommand(GameObject agent)
    {
        _transform = agent.GetComponent<Transform>();
        _rigidbody = agent.GetComponent<Rigidbody>();
        Debug.Assert(_rigidbody, "There is no rigibody on the agent.");
    }

    void ICommand.Execute()
    {
        Vector3 _move = _rigidbody.velocity;
        _move = _transform.forward * 1;
        _rigidbody.velocity = _move;
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
