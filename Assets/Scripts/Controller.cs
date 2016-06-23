using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Controller : MonoBehaviour 
{
    Rigidbody _rigidbody;

    private ICommand _wKeyCommand;
    private ICommand _sKeyCommand;
    private ICommand _aKeyCommand;
    private ICommand _dKeyCommand;
    private ICommand _returnKeyCommand;
    private ICommand _spaceKeyCommand;

    void Awake()
    {
        GameObject go = this.gameObject;
        _wKeyCommand = new MoveForwardCommand(go);
        _sKeyCommand = new MoveBackwardCommand(go);
        _aKeyCommand = new TurnLeftCommand(go);
        _dKeyCommand = new TurnRightCommand(go);
        _returnKeyCommand = new ShootCommand(go);
        _spaceKeyCommand = new JumpCommand(go);

        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Reset()
    {
        _rigidbody.position = Vector3.zero;
        _rigidbody.rotation = Quaternion.identity;
    }

	public void Perform(uint cmds)
    {
        if ((cmds & (uint)CommandCode.W) > 0)
            _wKeyCommand.Execute();

        if ((cmds & (uint)CommandCode.S) > 0)
            _sKeyCommand.Execute();

        if ((cmds & (uint)CommandCode.A) > 0)
            _aKeyCommand.Execute();

        if ((cmds & (uint)CommandCode.D) > 0)
            _dKeyCommand.Execute();

        if ((cmds & (uint)CommandCode.RETURN) > 0)
            _returnKeyCommand.Execute();

        if ((cmds & (uint)CommandCode.SPACE) > 0)
            _spaceKeyCommand.Execute();
    }
}
