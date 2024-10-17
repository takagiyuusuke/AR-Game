using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // TextMeshProを使うための名前空間


public class GameInitializer : MonoBehaviour
{
    public RandomObjectManager randomObjectManager;
    public List<string> targetNames; // Vuforiaのターゲット名のリスト

    public TextMeshProUGUI turnText; // プレイヤーのターンを表示するテキスト
    void Start()
    {
        // ゲーム開始時にオブジェクトを割り当てる
        randomObjectManager.AssignRandomObjectsToTargets(targetNames);
        turnText.text = "turn: Player 1";
        turnText.color = Color.blue;
    }

    void onGUI()
    {
        GUI.Label(new Rect(10, 10, 50, 50), "hello");
    }
}
