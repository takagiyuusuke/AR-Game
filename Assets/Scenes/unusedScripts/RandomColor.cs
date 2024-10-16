using UnityEngine;

public class RandomColorSetter : MonoBehaviour
{
    // ゲーム開始時に呼ばれる
    void Start()
    {
        // オブジェクトのRendererを取得
        Renderer objectRenderer = GetComponent<Renderer>();

        if (objectRenderer != null)
        {
            // ランダムな色を生成 (RGB値は0〜1の範囲)
            Color randomColor = new Color(Random.value, Random.value, Random.value);

            // 取得したRendererのマテリアルの色をランダムに設定
            objectRenderer.material.color = randomColor;
        }
        else
        {
            Debug.LogWarning("Rendererが見つかりません！");
        }
    }
}
