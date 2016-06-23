using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Replay
{
    public class Snapshot
    {
        public uint tick, commands;
        public Snapshot(uint _tick, uint _commands)
        {
            tick = _tick;
            commands = _commands;
        }
    }

    private Queue<Snapshot> _snapshots;
    private bool _isRecording; 

    public Replay()
    {
        _snapshots = new Queue<Snapshot>();
        _isRecording = false;
    }

    public void EnableRecording()
    {
        _isRecording = true;
    }

    public void DisableRecording()
    {
        _isRecording = false;
    }

    public void AddSnapshot(uint gameTick, uint commands)
    {
        Debug.Assert (_isRecording, "Replay is not in recording mode.");

        _snapshots.Enqueue(new Snapshot(gameTick, commands));
    }

    public Snapshot GetNextSnapshot()
    {
        Debug.Assert(!_isRecording, "Replay is in recording mode, stop recording before you consume it.");

        Snapshot ss = null;
        if(_snapshots.Count > 0)
            ss = _snapshots.Dequeue();
        return ss;
    }
	
}
