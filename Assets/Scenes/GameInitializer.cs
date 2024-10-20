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
    public TextMeshProUGUI description; //操作を指示するテキスト
    public GameObject button; //次のターンにするボタン
    public TextMeshProUGUI scoreText1; //プレイヤー１のスコア表示
    public TextMeshProUGUI scoreText2; //プレイヤー２のスコア表示
    public TextMeshProUGUI resultText; //結果の表示
    public GameObject memoryGameButton;
    public GameObject blackJackButton;

    public GameObject _FireworkParticle;

    
    public GameObject _HitParticle; //パーティクルエフェクト

    private GameObject[] selected = new GameObject[2];
    private int score1 = 0;
    private int score2 = 0;
    private int gameMode = 0;


    void Start()
    {
        turnText.text = "Select Game";
        description.text = "";
        scoreText1.text = "";
        scoreText2.text = "";
        button.SetActive(false);
        resultText.text = "";
    }

    void Update()
    {   
        if (Input.GetMouseButtonDown(0) )
        {
            if (gameMode == 0){
                GameObject selectedUI = EventSystem.current.currentSelectedGameObject;
                // Debug.Log("UIボタンが押されました" + selectedUI.name);
                if (selectedUI.name[0] == 'L'){
                    randomObjectManager.AssignRandomObjectsToTargets(targetNames);
                    turnText.text = "turn: Player 1";
                    turnText.color = Color.blue;
                    scoreText1.color = Color.blue;
                    scoreText2.color = Color.red;
                    description.text = "Please click card! (2 cards left)";
                    scoreText1.text = "Player1: 0pair";
                    scoreText2.text = "Player2: 0pair";
                    memoryGameButton.SetActive(false);
                    blackJackButton.SetActive(false);
                    gameMode = 1;
                } else if (selectedUI.name[0] == 'R') {
                    randomObjectManager.AssignRandomObjectsToTargets_BJ(targetNames);
                    turnText.text = "turn: Player 1";
                    turnText.color = Color.blue;
                    scoreText1.color = Color.blue;
                    scoreText2.color = Color.red;
                    description.text = "Please click card!";
                    scoreText1.text = "Player1: 0pt";
                    scoreText2.text = "Player2: 0pt";
                    memoryGameButton.SetActive(false);
                    blackJackButton.SetActive(false);
                    gameMode = 2;
                    button.SetActive(true);
                }
            } else if (gameMode == 1) {
                MemoryGame();
            } else if (gameMode == 2) {
                BlackJack();
            }
        }
    }

    void BlackJack()
    {
        Debug.Log("helloあああ");
        // タッチまたはマウスクリックの判定
        if (EventSystem.current.IsPointerOverGameObject())
        {   
            //画面のターンを変更
                if (turnText.text == "turn: Player 1")  {
                    turnText.text = "turn: Player 2";
                    turnText.color = Color.red;
                } else {
                    if (score1 > score2) {
                        resultText.text = "Winner: Player 1";
                        resultText.color = Color.blue;
                    } else {
                        resultText.text = "Winner: Player 2";
                        resultText.color = Color.red;
                    }
                    turnText.text = "Congraturation!!";
                    for (int i = -40; i <= 40; i+=10) {
                        for (int j = -40; j <= 40; j+=10) {
                            for (int k = -10; k <= 10; k+=5) {
                                GameObject particle1 = Instantiate(_FireworkParticle,Camera.main.transform.position + new Vector3(i, j, k), Quaternion.identity);
                                Destroy(particle1, 5.0f);
                            }
                        }
                    }
                
            }
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
                // AppearCard(touchedObject.name[..8]);
                if (turnText.text == "turn: Player 1") {
                    int s = touchedObject.name[8] - '0';
                    score1 += s == 1 ? 10 : s;
                    scoreText1.text = "Player1: " + score1.ToString() + "pt";
                } else {
                    int s = touchedObject.name[8] - '0';
                    score2 += s == 1 ? 10 : s;
                    scoreText2.text = "Player2: " + score2.ToString() + "pt";
                }
                if (score1 > 21 || score2 > 21){
                    if (score2 > 21) {
                        resultText.text = "Winner: Player 1";
                        turnText.color = Color.blue;
                        resultText.color = Color.blue;
                    } else {
                        resultText.text = "Winner: Player 2";
                        turnText.color = Color.red;
                        resultText.color = Color.red;
                    }
                    turnText.text = "Congraturation!!";
                    for (int i = -40; i <= 40; i+=10) {
                        for (int j = -40; j <= 40; j+=10) {
                            for (int k = -10; k <= 10; k+=5) {
                                GameObject particle1 = Instantiate(_FireworkParticle,Camera.main.transform.position + new Vector3(i, j, k), Quaternion.identity);
                                Destroy(particle1, 5.0f);
                            }
                        }
                    }
                }
                // GameObject particle2 = Instantiate(_HitParticle, touchedObject.transform.position, Quaternion.identity);
                // Destroy(particle2, 1.0f);
            }
            else
            {
                Debug.LogWarning("Rendererが見つかりません！");
            }
        }
    }

    void MemoryGame()
    {
        // タッチまたはマウスクリックの判定
        if (EventSystem.current.IsPointerOverGameObject())
        {   
            //画面のターンを変更
            
            if (description.text == "Wrong!!") {
                SetTransparency(selected[0], 1);
                SetTransparency(selected[1], 1);
                // DisappearCard(selected[0].name[..8]);
                // DisappearCard(selected[1].name[..8]);
                if (turnText.text == "turn: Player 1")  {
                    turnText.text = "turn: Player 2";
                    turnText.color = Color.red;
                } else {
                    turnText.text = "turn: Player 1";
                    turnText.color = Color.blue;
                }
            }
            description.text = "Please click card! (2 cards left)";
            description.color = Color.white;
            Array.Clear(selected, 0, selected.Length);
            button.SetActive(false);
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
                // AppearCard(touchedObject.name[..8]);
                if (selected[0] == null) {
                    selected[0] = touchedObject;
                    description.text = "Please click card! (1 card left)";
                    description.color = Color.gray;
                } else {
                    selected[1] = touchedObject;
                    if (touchedObject.name[5] == selected[0].name[5]){
                        description.text = "Correct!!";
                        description.color = Color.green;
                        GameObject selectedBefore = selected[0];
                        GameObject particle = Instantiate(_HitParticle, selectedBefore.transform.position, Quaternion.identity);
                        GameObject particle2 = Instantiate(_HitParticle, touchedObject.transform.position, Quaternion.identity);
                        Destroy(particle, 1.0f);
                        Destroy(particle2, 1.0f);
                        
                        if (turnText.text == "turn: Player 1") {
                            score1 ++;
                            scoreText1.text = "Player1: " + score1.ToString() + "pair";
                        } else {
                            score2 ++;
                            scoreText2.text = "Player2: " + score2.ToString() + "pair";
                        }
                        if (score1 + score2 == 3){
                            if (score1 > score2) {
                                resultText.text = "Winner: Player 1";
                                resultText.color = Color.blue;
                            } else {
                                resultText.text = "Winner: Player 2";
                                resultText.color = Color.red;
                            }
                            turnText.text = "Congraturation!!";
                            for (int i = -40; i <= 40; i+=10) {
                                for (int j = -40; j <= 40; j+=10) {
                                    for (int k = -10; k <= 10; k+=5) {
                                        GameObject particle1 = Instantiate(_FireworkParticle,Camera.main.transform.position + new Vector3(i, j, k), Quaternion.identity);
                                        Destroy(particle1, 5.0f);
                                    }
                                }
                            }

                        } else button.SetActive(true);

                    } else {
                        description.text = "Wrong!!";
                        description.color = Color.red;
                        button.SetActive(true);
                    }
                }
            }
            else
            {
                Debug.LogWarning("Rendererが見つかりません！");
            }
        }
    }  


    void AppearCard(string objectName){
        GameObject card = GameObject.Find("objectName");
        GameObject back = GameObject.Find("objectName" + "-Back");
        SetTransparency(card, 1.0f);
        SetTransparency(back, 0.0f);
    }

    void DisappearCard(string objectName){
        GameObject card = GameObject.Find("objectName");
        GameObject back = GameObject.Find("objectName" + "-Back");
        SetTransparency(card, 0.0f);
        SetTransparency(back, 1.0f);
    }

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
}
