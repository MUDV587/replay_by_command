using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class MoveForwardCommand : ICommand 
{
    Rigidbody _rigidbody;
    Transform _transform;
    float _deltaTime;
    float _velocity;

    public MoveForwardCommand(GameObject agent)
    {
        _transform = agent.GetComponent<Transform>();
        _rigidbody = agent.GetComponent<Rigidbody>();

        _deltaTime = 1.0f / 30.0f;
        _velocity = 30;
    }

    void ICommand.Execute()
    {
        Vector3 _move = _rigidbody.velocity;
        _move = _transform.forward * _velocity * _deltaTime;
        _rigidbody.velocity = _transform.forward * _velocity * _deltaTime;
        //Debug.Log(_rigidbody.velocity);
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
