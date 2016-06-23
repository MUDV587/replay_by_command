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
    private InputHandler _input;
    private Controller _heroController;
    
    private uint _gameTick;
    private float _deltaGameTime;

    [SerializeField]
    int _fixedFPS;

    #endregion

    void Start () 
    {
        // getting hero controller
        _heroController = GameObject.Find("Hero").GetComponent<Controller>();
        Debug.Assert(_heroController, "Hero does not have a Controller Component");

        // creating a replay object
        _replay = new Replay();
        _replay.EnableRecording();

        // creating an input handler
        _input = new InputHandler();

        // constant delta time since last frame
        _deltaGameTime = 1.0f / 50;

        StartPlaying();
    }

    #region Playing Methods
    public void StartPlaying()
    {
        _gameMode = GameMode.Playing;
        _gameTick = 0;
        StopCoroutine(Replaying());
        _heroController.Reset();
        //InvokeRepeating("ReallyFixedUpdate", 0.0f, _deltaGameTime);
        //InvokeRepeating("PrintGameTick", 0.0f, 1.0f);
    }

    void PrintGameTick()
    {
        print("GameTick: " + _gameTick.ToString());
    }


    // using a fixed update to make our update deterministic
	void FixedUpdate () 
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
            _gameMode = GameMode.Replaying;
            _replay.DisableRecording();
            //CancelInvoke("ReallyFixedUpdate");
            _heroController.Reset();
            StartCoroutine(Replaying());
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

    IEnumerator Replaying()
    {
        Replay.Snapshot snapshot = _replay.GetNextSnapshot();
        while(snapshot != null)
        {
            // wait until next command
            yield return new WaitForSeconds(snapshot.tick * _deltaGameTime);
            _heroController.Perform(snapshot.commands);
            snapshot = _replay.GetNextSnapshot();
        }
        yield return new WaitForSeconds(3.0f);
        StartPlaying();
    }
    #endregion
}
