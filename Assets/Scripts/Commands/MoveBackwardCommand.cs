using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class MoveBackwardCommand : ICommand 
{
    Rigidbody _rigidbody;
    Transform _transform;
    float _deltaTime;
    float _velocity;
    public MoveBackwardCommand(GameObject agent)
    {
        _transform = agent.GetComponent<Transform>();
        _rigidbody = agent.GetComponent<Rigidbody>();

        _deltaTime = 1.0f / 50;
        _velocity = 50;
    }

    void ICommand.Execute()
    {
        Vector3 _move = _rigidbody.velocity;
        _move = _transform.forward * -_velocity * _deltaTime;
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
