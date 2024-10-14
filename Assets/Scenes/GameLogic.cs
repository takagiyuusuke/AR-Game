using UnityEngine;
using Vuforia;

public class MemoryGameManager : MonoBehaviour
{
    // 図形のPrefab（〇と×）
    public GameObject circlePrefab;
    public GameObject crossPrefab;

    // マーカーを管理する配列
    public GameObject[] markers;

    // 図形のランダム配置
    void Start()
    {
        // 配置する図形をリストで保持
        GameObject[] shapes = { circlePrefab, circlePrefab, crossPrefab, crossPrefab };

        // 図形をシャッフル
        Shuffle(shapes);

        // マーカーに図形を配置
        for (int i = 0; i < markers.Length; i++)
        {
            // マーカーがVuforiaで認識されたら、図形を配置
            if (markers[i].GetComponent<ObserverBehaviour>().TargetStatus.Status == Status.TRACKED)
            {
                // マーカーの位置に図形を生成
                Instantiate(shapes[i], markers[i].transform.position, Quaternion.identity);
            }
        }
    }

    // 図形をランダムにシャッフルする関数
    void Shuffle(GameObject[] array)
    {
        for (int i = array.Length - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            GameObject temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }
    }
}
