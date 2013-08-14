using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]
public class Teacher : MonoBehaviour {
	public float viewAngle;
	public float viewLength;

	public enum State {
		NOTHING, GOTO, SCAN,
	};
	
	public Dictionary<State, int> stateWeightMap = new Dictionary<State, int>(){
		{ State.NOTHING, 1 },
		{ State.GOTO, 1 },
		{ State.SCAN, 1 },
	};
	
	public State _currentState = State.NOTHING;
	
	public NavMeshAgent navAgent;
	public List<AudioClip> footsteps;
	
	public List<WatchPoint> positions;
	
	void Awake() {
		navAgent = GetComponent<NavMeshAgent>();
	}
	
	void DoSomething()
	{
		var nextState = RandomHelper.pickWeightedRandom(stateWeightMap);
		_currentState = nextState;
		switch(_currentState)
		{
			case State.NOTHING:
			{
				Invoke("DoSomething", RandomHelper.next(1f, 5f));
				break;
			}
			case State.GOTO:
			{
				GotoSomewhere();
				break;
			}
			case State.SCAN:
			{
				Invoke("DoSomething", RandomHelper.next(1f, 5f));
				break;
			}
		}
	}
	
	// Use this for initialization
	void Start () {
		DoSomething();
	}
	
	// Update is called once per frame
	void Update () {
		PlayRandomSFX();
				
		// end reached?
		if (_currentState == State.GOTO && Vector3.Distance(transform.position, navAgent.destination) < 0.01f)
		{
			DoSomething();
		}
	}
	
	void GotoSomewhere()
	{
		var gotoPos = RandomHelper.pickWeightedRandom(positions, (point) => point.weight);
		navAgent.destination = gotoPos.transform.position;
	}
	
	void OnDrawGizmos() 
	{
		var localL = transform.InverseTransformPoint(transform.position + new Vector3(viewLength, 0f, 0f)).magnitude;
		var h = Vector3.up * 1f;
		
		var to = transform.TransformPoint(Vector3.forward * localL);
		
		var a = Quaternion.Euler(0f, viewAngle / 2f, 0f) * Vector3.forward * localL;
		var b = Quaternion.Euler(0f, -viewAngle / 2f, 0f) * Vector3.forward * localL;
		
		var toA = transform.TransformPoint(a);
		var toB = transform.TransformPoint(b);
		
		Gizmos.color = Color.red;
		Gizmos.DrawLine(transform.position + h, to + h);
		
		Gizmos.color = Color.yellow;
		Gizmos.DrawLine(transform.position + h, toA + h);
		Gizmos.DrawLine(transform.position + h, toB + h);
	}
	
	void PlayRandomSFX(){
		if (audio.isPlaying) return; 
		else {
			audio.clip = RandomHelper.pickRandom(footsteps);
			audio.Play();
			}
		}
}