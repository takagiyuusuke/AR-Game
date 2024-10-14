using UnityEngine;
using Vuforia;

public class TargetEventHandler : MonoBehaviour
{
    private ObserverBehaviour mObserverBehaviour;
    public RandomObjectManager randomObjectManager;

    void Start()
    {
        mObserverBehaviour = GetComponent<ObserverBehaviour>();
        if (mObserverBehaviour)
        {
            mObserverBehaviour.OnTargetStatusChanged += OnTargetStatusChanged;
        }

        // ゲーム開始時にオブジェクトを非表示にしておく
        GameObject assignedObject = randomObjectManager.GetObjectForTarget(mObserverBehaviour.TargetName);
        Instantiate(assignedObject, Vector3.zero, Quaternion.identity);
        if (assignedObject != null)
        {
            assignedObject.SetActive(false); // 初期状態で非表示にしておく
        }
    }
    
        void Update()
    {
        // if (!mObserverBehaviour)
        // {
        //     mObserverBehaviour = GetComponent<ObserverBehaviour>();
        //     mObserverBehaviour.OnTargetStatusChanged += OnTargetStatusChanged;
        // }

        // // ゲーム開始時にオブジェクトを非表示にしておく
        // GameObject assignedObject = randomObjectManager.GetObjectForTarget(mObserverBehaviour.TargetName);
        // if (assignedObject != null)
        // {
        //     assignedObject.SetActive(false); // 初期状態で非表示にしておく
        // }
        GameObject assignedObject = randomObjectManager.GetObjectForTarget(mObserverBehaviour.TargetName);
         if (assignedObject != null && mObserverBehaviour != null)
        {
            assignedObject.transform.SetPositionAndRotation(
                mObserverBehaviour.transform.position,
                mObserverBehaviour.transform.rotation
            );
        }
    }


    private void OnTargetStatusChanged(ObserverBehaviour behaviour, TargetStatus targetStatus)
    {
        GameObject assignedObject = randomObjectManager.GetObjectForTarget(mObserverBehaviour.TargetName);
        if (assignedObject == null) return;

        if (
            targetStatus.Status == Status.TRACKED ||
            targetStatus.Status == Status.EXTENDED_TRACKED)
        {
            // ターゲットが認識されたときにオブジェクトの位置をマーカーの位置に設定し、表示する
            // assignedObject.transform.position = mObserverBehaviour.transform.position; // マーカーの位置にオブジェクトを配置
            // assignedObject.transform.rotation = mObserverBehaviour.transform.rotation; // マーカーの回転をオブジェクトに適用（オプション）
            // assignedObject.SetActive(true);
        }
        else
        {
            // ターゲットを見失ったときにオブジェクトを非表示
            assignedObject.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        if (mObserverBehaviour)
        {
            mObserverBehaviour.OnTargetStatusChanged -= OnTargetStatusChanged;
        }
    }
}
