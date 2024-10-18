using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tourch : MonoBehaviour
{
    // Start is called before the first frame update
    private Renderer objectRenderer;
    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    // void Update()
    // {
    //   // タッチまたはマウスクリックの判定
    //         if (Input.GetMouseButtonDown(0) )
    //         {
    //             // タッチ位置からレイを生成
    //             Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //             RaycastHit hit;

    //             // レイキャストでオブジェクトにヒットしたか確認
    //             if (Physics.Raycast(ray, out hit))
    //             {
    //                 GameObject touchedObject = hit.collider.gameObject;
    //                 Debug.Log("Touched object: " + touchedObject.name);

    //                 // オブジェクトのRendererを取得
    //                 Renderer objectRenderer = touchedObject.GetComponent<Renderer>();

    //                 if (objectRenderer != null)
    //                 {
    //                     // マテリアルのレンダリングモードをTransparentに変更
    //                     objectRenderer.material.SetOverrideTag("RenderType", "Transparent");
    //                     objectRenderer.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
    //                     objectRenderer.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
    //                     objectRenderer.material.SetInt("_ZWrite", 0);
    //                     objectRenderer.material.DisableKeyword("_ALPHATEST_ON");
    //                     objectRenderer.material.EnableKeyword("_ALPHABLEND_ON");
    //                     objectRenderer.material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
    //                     objectRenderer.material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;

    //                     // 現在のマテリアルの色を取得
    //                     Color objectColor = objectRenderer.material.color;

    //                     // アルファ値を0<->1で切り替える
    //                     if (objectColor. a == 0.0f)
    //                     objectColor.a = 1.0f;
    //                     else
    //                     objectColor.a = 0.0f;

    //                     // 変更した色をマテリアルに適用
    //                     objectRenderer.material.color = objectColor;
    //                 }
    //                 else
    //                 {
    //                     Debug.LogWarning("Rendererが見つかりません！");
    //                 }
    //             }
    //         }
    //     // }   
    // }
}