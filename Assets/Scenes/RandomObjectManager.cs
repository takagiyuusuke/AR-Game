using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Security.Cryptography;


public class RandomObjectManager : MonoBehaviour
{
    // ターゲットとオブジェクトの対応リスト
    public List<GameObject> availableObjects;  // 出現する可能性のあるオブジェクトのリスト
    private Dictionary<string, GameObject> targetObjectMap = new Dictionary<string, GameObject>();

    void ShuffleArray(int[] array)
    {
        for (int i = array.Length - 1; i > 0; i--)
        {
            int randomIndex = UnityEngine.Random.Range(0, i + 1);
            int temp = array[i];
            array[i] = array[randomIndex];
            array[randomIndex] = temp;
        }
    }
    
    public void AssignRandomObjectsToTargets(List<string> targetNames)
    {
        int[] arr = {0, 1, 2, 3, 4, 5}; // 3組のペア
        ShuffleArray(arr); // 配列をシャッフル
        int indexOfArray = 0;
        foreach (string targetName in targetNames)
        {
            GameObject randomObject = availableObjects[arr[indexOfArray]];
            Debug.Log("らんだむなんばー" + arr[indexOfArray].ToString());
            // GameObject randomObject = availableObjects[1];
            targetObjectMap[targetName] = randomObject;
            
            indexOfArray ++;
        }
    }

    // ターゲット名に対応するオブジェクトを取得
    public GameObject GetObjectForTarget(string targetName)
    {
        return targetObjectMap.ContainsKey(targetName) ? targetObjectMap[targetName] : null;
    }
}
