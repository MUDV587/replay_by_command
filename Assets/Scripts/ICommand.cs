using UnityEngine;
using System.Collections;

public interface ICommand
{
    void Execute();
    void Redo();
    void Undo();
}
