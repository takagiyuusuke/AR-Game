using System.Collections.Generic;
using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    public RandomObjectManager randomObjectManager;
    public List<string> targetNames; // Vuforiaのターゲット名のリスト

    void Start()
    {
        // ゲーム開始時にオブジェクトを割り当てる
        randomObjectManager.AssignRandomObjectsToTargets(targetNames);
    }
}
