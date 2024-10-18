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

    void Update()
    {
        OnClick();
    }

    void OnClick()
    {
        // タッチまたはマウスクリックの判定
        if (Input.GetMouseButtonDown(0) )
        {
            // タッチ位置からレイを生成
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // レイキャストでオブジェクトにヒットしたか確認
            if (Physics.Raycast(ray, out hit))
            {
                GameObject touchedObject = hit.collider.gameObject;
                Debug.Log("Touched object: " + touchedObject.name);

                // オブジェクトのRendererを取得
                Renderer objectRenderer = touchedObject.GetComponent<Renderer>();

                if (objectRenderer != null)
                {   
                    //画面のターンを変更
                    if (turnText.text == "turn: Player 1")
                    {
                        turnText.text = "turn: Player 2";
                        turnText.color = Color.red;
                    } else {
                        turnText.text = "turn: Player 1";
                        turnText.color = Color.blue;
                    }
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

                    // アルファ値を0<->1で切り替える
                    if (objectColor. a == 0.0f)
                    objectColor.a = 1.0f;
                    else
                    objectColor.a = 0.0f;

                    // 変更した色をマテリアルに適用
                    objectRenderer.material.color = objectColor;
                }
                else
                {
                    Debug.LogWarning("Rendererが見つかりません！");
                }
            }
        }  
    }
}
