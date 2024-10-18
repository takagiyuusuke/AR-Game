using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // TextMeshProを使うための名前空間
using UnityEngine.EventSystems;
using System;


public class GameInitializer : MonoBehaviour
{
    public RandomObjectManager randomObjectManager;
    public List<string> targetNames; // Vuforiaのターゲット名のリスト

    public TextMeshProUGUI turnText; // プレイヤーのターンを表示するテキスト
    public TextMeshProUGUI description;

    private GameObject[] selected = new GameObject[2];

    void SetTransparency(GameObject obj, float newOpacity){
        Renderer objectRenderer = obj.GetComponent<Renderer>();
        // マテリアルのレンダリングモードをTransparentに変更
        objectRenderer.material.SetOverrideTag("RenderType", "Transparent");
        objectRenderer.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        objectRenderer.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        objectRenderer.material.SetInt("_ZWrite", 0);
        objectRenderer.material.DisableKeyword("_ALPHATEST_ON");
        objectRenderer.material.EnableKeyword("_ALPHABLEND_ON");
        objectRenderer.material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        objectRenderer.material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;

        // 現在のマテリアルの色を取得
        Color objectColor = objectRenderer.material.color;
        

        // float hidden_alpha = 0.3f;
        // // アルファ値を0<->1で切り替える
        // if (objectColor. a == hidden_alpha)
        // objectColor.a = 1.0f;
        // else
        // objectColor.a = hidden_alpha;
        objectColor.a = newOpacity;

        // 変更した色をマテリアルに適用
        objectRenderer.material.color = objectColor;
    }
    void Start()
    {
        // ゲーム開始時にオブジェクトを割り当てる
        randomObjectManager.AssignRandomObjectsToTargets(targetNames);
        turnText.text = "turn: Player 1";
        turnText.color = Color.blue;
    }

    void Update()
    {
        OnClick();
    }

    void OnClick()
    {
        // タッチまたはマウスクリックの判定
        if (Input.GetMouseButtonDown(0) )
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                Debug.Log("UIボタンが押されました");
                //画面のターンを変更
                
                if (description.text == "Wrong!!") {
                    SetTransparency(selected[0], 1);
                    SetTransparency(selected[1], 1);
                    if (turnText.text == "turn: Player 1")  {
                        turnText.text = "turn: Player 2";
                        turnText.color = Color.red;
                    } else {
                        turnText.text = "turn: Player 1";
                        turnText.color = Color.blue;
                    }
                }
                description.text = "Please click card! (2 cards left)";
                Array.Clear(selected, 0, selected.Length);
                return; // UIが押された場合はここで処理を終了
            }
            
            // タッチ位置からレイを生成
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // レイキャストでオブジェクトにヒットしたか確認
            if (Physics.Raycast(ray, out hit))
            {
                GameObject touchedObject = hit.collider.gameObject;
                // string card = touchedObject.name[5..8];
                Debug.Log("Touched object: " + touchedObject.name[5..8]);

                // オブジェクトのRendererを取得
                Renderer objectRenderer = touchedObject.GetComponent<Renderer>();

                if (objectRenderer != null)
                {   
                    SetTransparency(touchedObject, 0.0f);
                    if (selected[0] == null) {
                        selected[0] = touchedObject;
                        description.text = "Please click card! (1 cards left)";
                    } else {
                        selected[1] = touchedObject;
                        if (touchedObject.name[5] == selected[0].name[5]){
                            description.text = "Correct!!";
                        } else {
                            description.text = "Wrong!!";
                        }
                    }
                }
                else
                {
                    Debug.LogWarning("Rendererが見つかりません！");
                }
            }
        }  
    }
}
