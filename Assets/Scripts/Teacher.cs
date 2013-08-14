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
	
	public bool moveDestinationReached = false;
	
	public Dictionary<State, int> stateWeightMap = new Dictionary<State, int>(){
		{ State.NOTHING, 1 },
		{ State.GOTO, 1 },
		{ State.SCAN, 1 },
	};
	
	public State _currentState = State.NOTHING;
	
	public NavMeshAgent navAgent;
	public List<AudioClip> footsteps;
	
	public List<WatchPoint> positions;
	public List<WatchPoint> lookAtPositions;
	
	void Awake() {
		navAgent = GetComponent<NavMeshAgent>();
	}
	
	void LookAt()
	{
		var lookAtPos = RandomHelper.pickWeightedRandom(lookAtPositions, (point) => point.weight);
		var rotateTime = 1f;
		transform.localEularAnglesTo(rotateTime, Quaternion.LookRotation(lookAtPos.transform.position - transform.position).eulerAngles, false);
		Invoke("DoSomething", rotateTime);
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
		if (_currentState == State.GOTO && moveDestinationReached == false && 
			Vector3.Distance(transform.position, navAgent.destination) < 0.01f)
		{
			moveDestinationReached = true;
			LookAt();
		}
	}
	
	void GotoSomewhere()
	{
		moveDestinationReached = false;
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
	
	public float CalculateInViewFactor(Vector3 p)
	{
		var localP = transform.InverseTransformPoint(p);
		var angle = Quaternion.Angle(Quaternion.LookRotation(localP), Quaternion.LookRotation(Vector3.forward));
		var af = 1f - Mathf.Clamp01(angle / viewAngle);
		var lf = Mathf.Clamp01(localP.magnitude / viewLength);
		return Mathf.Clamp01(af * lf);
	}
	
	void PlayRandomSFX(){
		if (audio.isPlaying || _currentState != State.GOTO) return; 
		else {
			audio.clip = RandomHelper.pickRandom(footsteps);
			audio.Play();
			}
		}
}