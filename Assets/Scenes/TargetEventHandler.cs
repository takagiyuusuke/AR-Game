using Vuforia;
using UnityEngine;
using System.Collections.Generic;

public class TargetEventHandler : MonoBehaviour, ITrackableEventHandler
{
    private TrackableBehaviour mTrackableBehaviour;
    public RandomObjectManager randomObjectManager;

    void Start()
    {
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }
    }

    public void OnTrackableStateChanged(
        TrackableBehaviour.Status previousStatus,
        TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            // ターゲットが認識されたときにオブジェクトを表示
            GameObject assignedObject = randomObjectManager.GetObjectForTarget(mTrackableBehaviour.TrackableName);
            if (assignedObject != null)
            {
                assignedObject.SetActive(true);
            }
        }
        else
        {
            // ターゲットを見失ったときにオブジェクトを非表示
            GameObject assignedObject = randomObjectManager.GetObjectForTarget(mTrackableBehaviour.TrackableName);
            if (assignedObject != null)
            {
                assignedObject.SetActive(false);
            }
        }
    }
}
