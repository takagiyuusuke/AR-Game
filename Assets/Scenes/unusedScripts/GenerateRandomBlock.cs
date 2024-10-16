using UnityEngine;

public class RandomShapeSpawner : MonoBehaviour
{
    // ランダムに表示する2つのプレハブを指定する
    public GameObject shape1;  // 1つ目のプレハブ
    public GameObject shape2;  // 2つ目のプレハブ

    private GameObject currentShape;

    // 再生ボタンが押されたときに呼ばれる
    void Start()
    {
        SpawnRandomShape();
    }

    // ランダムな図形を生成する関数
    void SpawnRandomShape()
    {
        // もし既に図形が存在していたら削除する
        if (currentShape != null)
        {
            Destroy(currentShape);
        }

        // shape1かshape2をランダムに選択
        int randomIndex = Random.Range(0, 2);
        GameObject selectedShape = randomIndex == 0 ? shape1 : shape2;

        // 選ばれたプレハブを生成
        currentShape = Instantiate(selectedShape, transform.position, Quaternion.identity);
    }
}
