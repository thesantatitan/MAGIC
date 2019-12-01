using UnityEngine;
using Vuforia;
using System.Collections;
public class MyPrefabInstantiator : MonoBehaviour, ITrackableEventHandler {
  private TrackableBehaviour mTrackableBehaviour;
  public Transform ball1;
  public Transform ball2;
  public Transform ball3;
  int c=0;
  // Use this for initialization
  void Start ()
  {
    mTrackableBehaviour = GetComponent<TrackableBehaviour>();
    if (mTrackableBehaviour) {
      mTrackableBehaviour.RegisterTrackableEventHandler(this);
    }
  }
  // Update is called once per frame
  //private void onTrackingLost();
  void Update ()
  {
	  if (!(mTrackableBehaviour.CurrentStatus == TrackableBehaviour.Status.DETECTED ||
        mTrackableBehaviour.CurrentStatus == TrackableBehaviour.Status.TRACKED ||
        mTrackableBehaviour.CurrentStatus == TrackableBehaviour.Status.EXTENDED_TRACKED))
	  {
		if(GameObject.Find("ARCamera").GetComponent<spell>().spell_being_done!="circle"){
			GameObject.Find("ARCamera").GetComponent<spell>().spell_being_done="none";
		}
      }
	  if ((mTrackableBehaviour.CurrentStatus == TrackableBehaviour.Status.DETECTED ||
        mTrackableBehaviour.CurrentStatus == TrackableBehaviour.Status.TRACKED ||
        mTrackableBehaviour.CurrentStatus == TrackableBehaviour.Status.EXTENDED_TRACKED))
	  {
		if(GameObject.Find("ARCamera").GetComponent<spell>().spell_being_done=="circle"){
			GameObject.Find("ARCamera").GetComponent<spell>().spell_being_done="none";
			Debug.Log("found");
			OnTrackingFound("circle");
		}
		if(GameObject.Find("ARCamera").GetComponent<spell>().spell_being_done=="exp"){
			GameObject.Find("ARCamera").GetComponent<spell>().spell_being_done="none";
			Debug.Log("found");
			OnTrackingFound("exp");
		}
		if(GameObject.Find("ARCamera").GetComponent<spell>().spell_being_done=="gig"){
			GameObject.Find("ARCamera").GetComponent<spell>().spell_being_done="none";
			Debug.Log("found");
			OnTrackingFound("gig");
		}
      }
	  
  }
  public void OnTrackableStateChanged(
    TrackableBehaviour.Status previousStatus,
    TrackableBehaviour.Status newStatus)
  { 
	//m_PreviousStatus = previousStatus;
  
  } 
  private void OnTrackingFound(string s)
  {
    if (s=="circle")
    {
      Transform myModelTrf = GameObject.Instantiate(ball1) as Transform;
      myModelTrf.parent = mTrackableBehaviour.transform;
      myModelTrf.localPosition = new Vector3(0f, 0f, 0f);
      myModelTrf.localRotation = Quaternion.identity;
      myModelTrf.localScale = new Vector3(1.0f, 1.0f, 1.0f);
      myModelTrf.gameObject.active = true;
    }
	if(s=="exp"){
	  Transform myModelTrf = GameObject.Instantiate(ball2) as Transform;
      myModelTrf.parent = mTrackableBehaviour.transform;
      myModelTrf.localPosition = new Vector3(0f, 0f, 0f);
      myModelTrf.localRotation = Quaternion.identity;
      myModelTrf.localScale = new Vector3(1.0f, 1.0f, 1.0f);
      myModelTrf.gameObject.active = true;
	}
	if(s=="gig"){
	  Transform myModelTrf = GameObject.Instantiate(ball3) as Transform;
      myModelTrf.parent = mTrackableBehaviour.transform;
      myModelTrf.localPosition = new Vector3(0f, 0f, 0f);
      myModelTrf.localRotation = Quaternion.identity;
      myModelTrf.localScale = new Vector3(1.0f, 1.0f, 1.0f);
      myModelTrf.gameObject.active = true;
	}
  }
}