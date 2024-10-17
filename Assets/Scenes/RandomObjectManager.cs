using System.Collections.Generic;
using UnityEngine;

public class RandomObjectManager : MonoBehaviour
{
    // ターゲットとオブジェクトの対応リスト
    public List<GameObject> availableObjects;  // 出現する可能性のあるオブジェクトのリスト
    private Dictionary<string, GameObject> targetObjectMap = new Dictionary<string, GameObject>();

    // 各ターゲットにオブジェクトをランダムに割り当てる
    int randomNumber = 0;
    public void AssignRandomObjectsToTargets(List<string> targetNames)
    {
        foreach (string targetName in targetNames)
        {
            // int randomNumber = Random.Range(0, availableObjects.Count);
            GameObject randomObject = availableObjects[randomNumber];
            Debug.Log("らんだむなんばー" + randomNumber.ToString());
            // GameObject randomObject = availableObjects[1];
            targetObjectMap[targetName] = randomObject;
            
            randomNumber ++;
        }
    }

    // ターゲット名に対応するオブジェクトを取得
    public GameObject GetObjectForTarget(string targetName)
    {
        return targetObjectMap.ContainsKey(targetName) ? targetObjectMap[targetName] : null;
    }
}
