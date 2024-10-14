using System.Collections.Generic;
using UnityEngine;

public class RandomObjectManager : MonoBehaviour
{
    // ターゲットとオブジェクトの対応リスト
    public List<GameObject> availableObjects;  // 出現する可能性のあるオブジェクトのリスト
    private Dictionary<string, GameObject> targetObjectMap = new Dictionary<string, GameObject>();

    // 各ターゲットにオブジェクトをランダムに割り当てる
    public void AssignRandomObjectsToTargets(List<string> targetNames)
    {
        foreach (string targetName in targetNames)
        {
            GameObject randomObject = availableObjects[Random.Range(0, availableObjects.Count)];
            // GameObject randomObject = availableObjects[1];
            targetObjectMap[targetName] = randomObject;
        }
    }

    // ターゲット名に対応するオブジェクトを取得
    public GameObject GetObjectForTarget(string targetName)
    {
        return targetObjectMap.ContainsKey(targetName) ? targetObjectMap[targetName] : null;
    }
}
