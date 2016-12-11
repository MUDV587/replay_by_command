using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class TurnLeftCommand : ICommand 
{
    Rigidbody _rigidbody;
    Vector3 _rotation;
    float _deltaTime;
    float _velocity;

    public TurnLeftCommand(GameObject agent)
    {
        _rigidbody = agent.GetComponent<Rigidbody>();

        _deltaTime = 1.0f / 50;
        _velocity = 45;
    }

    void ICommand.Execute()
    {
        _rotation = _rigidbody.rotation.eulerAngles;
        _rotation.y -= _velocity * _deltaTime;
        _rigidbody.MoveRotation(Quaternion.Euler(_rotation));
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
