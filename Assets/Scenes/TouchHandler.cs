using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tourch : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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

                    // オブジェクトにタッチされた際の処理をここに記述
                    Destroy(hit.collider.gameObject);
                }
            }
        // }   
    }
}