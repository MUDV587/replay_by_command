using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameWorld : MonoBehaviour 
{
    public enum GameMode
    {
        Playing,
        Replaying
    }
    
    private GameMode _gameMode;
    public GameMode gameMode {
        get { return _gameMode; }
    }

    #region Private Members

    private Replay _replay;
    private Replay.Snapshot _snapshot;
    private InputHandler _input;
    private Controller _heroController;
    
    private uint _gameTick;
    private uint _replayGameTick;
    private float _deltaGameTime;

    [SerializeField]
    int _fixedFPS;

    int _time;
    #endregion

    void Start () 
    {
        // getting hero controller
        _heroController = GameObject.Find("Hero").GetComponent<Controller>();
        Debug.Assert(_heroController, "Hero does not have a Controller Component");

        // creating a replay object
        _replay = new Replay();
       
        // creating an input handler
        _input = new InputHandler();

        // constant delta time since last frame
        _deltaGameTime = 1.0f / 30.0f;

        StartPlaying();
    }

    void Update()
    {
    }

    #region Playing Methods
    public void StartPlaying()
    {
        print("Playing!");
        _gameMode = GameMode.Playing;
        _gameTick = 0;
        _replay.EnableRecording();
        _heroController.Reset();
        InvokeRepeating("ReallyFixedUpdate", 0.0f, _deltaGameTime);
        //InvokeRepeating("PrintGameTick", 0.0f, 1.0f);
        _time = Time.frameCount;
    }

    void PrintGameTick()
    {
        print("GameTick: " + _gameTick.ToString());
    }

    // using a fixed update to make our update deterministic
	void ReallyFixedUpdate () 
    {
        if (_gameMode == GameMode.Playing)
        {
            _gameTick++;

            uint frameCmd = _input.Evaluate();
            if (frameCmd > 0)
            {
                _heroController.Perform(frameCmd);
                _replay.AddSnapshot(_gameTick, frameCmd);
            }
        }
	}

    #endregion

    #region Replaying Methods

    public void StartReplaying()
    {
        // prevents start a replay during another replay
        if (_gameMode == GameMode.Playing)
        {
            int delta = Time.frameCount - _time;
            print("FramePassed " + delta.ToString() + " started: " + _time.ToString() + " ended: " + Time.frameCount);
            Debug.Log(_heroController.transform.position);
            _gameMode = GameMode.Replaying;
            _replay.DisableRecording();
            CancelInvoke("ReallyFixedUpdate");
            _heroController.Reset();
            _snapshot = _replay.GetNextSnapshot();
            _replayGameTick = 0;
            InvokeRepeating("ReallyFixedReplay", 0, _deltaGameTime);
            _time = Time.frameCount;
        }
    }

    public void PrintReplay()
    {
        Replay.Snapshot snapshot = _replay.GetNextSnapshot();
        while(snapshot != null)
        {
            Debug.Log("cmd: " + snapshot.commands.ToString() + " tick: " + snapshot.tick.ToString());
            snapshot = _replay.GetNextSnapshot();
        }
    }

    void ReallyFixedReplay()
    {
        if (_gameMode == GameMode.Replaying)
        {
            _replayGameTick++;

            if (_replayGameTick == _snapshot.tick)
            {
                uint frameCmd = _snapshot.commands;
                _heroController.Perform(frameCmd);
                _snapshot = _replay.GetNextSnapshot();
                if(_snapshot == null)
                {
                    int delta = Time.frameCount - _time;
                    print("FramePassed " + delta.ToString() + " started: " + _time.ToString() + " ended: " + Time.frameCount);
                    Debug.Log("Replay ended");
                    Debug.Log(_heroController.transform.position);
                    CancelInvoke("ReallyFixedReplay");
                    Invoke("StartPlaying", 3.0f);
                }
            }
        }
    }
    #endregion
}
