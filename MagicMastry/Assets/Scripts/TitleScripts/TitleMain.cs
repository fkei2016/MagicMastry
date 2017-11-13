using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleMain : MonoBehaviour
{

    //エンターをチェック
    class EnterChecker{
        const float DOUBLE_CHECK_TIME = 0.4f;
        bool oldCheck = false; //前回チェックされているか
        float doubleCheckTime = 0; //連続でエンターが押されたと感知するまでの時間
        //エンターが押されたか
        public bool CheckEnter() {
            doubleCheckTime -= Time.deltaTime;
            if(Input.GetAxisRaw("Enter") != 0) {
                //前回も押されていたら感知させない
                if (oldCheck) return false;
                //時間内に押されているか
                if(doubleCheckTime > 0f) return true;
                oldCheck = true;
                doubleCheckTime = DOUBLE_CHECK_TIME;
            }
            else {
                oldCheck = false;
            }
            return false;
        }
    }

    EnterChecker ec;




    [SerializeField]
    private GameObject center;

    [SerializeField]
    private Camera camera;

    [SerializeField]
    private GameObject titleImage;

    [SerializeField]
    private GameObject selectImage;

    [SerializeField]
    private GameObject magicImage;

    [SerializeField]
    GameObject nameSetScene; //プレイヤーの名前を決めるシーン

    [SerializeField]
    InputField inputField; //名前入力フィールド

    private PlyayType playtype;

    private MagicSelect magicSelect;

    private bool moveFlag = false;

    enum State
    {
        stop,
        Title,
        Select,
        SelectOne,
        MagicSelect,
        MagicSelectOne,
        NameSet,
    }

    State state = State.stop;

    // Use this for initialization
    void Start()
    {
        playtype =GetComponent<PlyayType>();
        magicSelect = GetComponent<MagicSelect>();
    }

    // Update is called once per frame
    void Update()
    {

        //デバッグワープ
        if (Input.GetKeyDown(KeyCode.F1)) SceneManager.LoadScene("Game");


        //スペースキーでセレクトモードへ
        if (state == State.stop && Input.GetAxisRaw("Space") != 0)
        {
            //非表示にする
            titleImage.SetActive(false);
            state = State.Title;
        }

        switch (state)
        {

            case State.stop:
                break;

            case State.Title:

                //移動してなかったら移動開始
                if (moveFlag == false)
                {
                    moveFlag = true;
                    //カメラの自動回転停止
                    center.GetComponent<CameraRote>().enabled = false;
                    //iTweenを使ったカメラの移動と回転
                    iTween.RotateTo(center, iTween.Hash("y", 0, "time", 0.4f));
                    iTween.MoveTo(center, iTween.Hash("y", -14, "time", 0.4f, "oncomplete", "NextState", "oncompletetarget", gameObject));
                    iTween.RotateTo(camera.gameObject, iTween.Hash("x", 0, "islocal", true));
                }
                break;

            case State.Select:

                //移動してなかったら移動開始
                if (moveFlag == false)
                {
                    moveFlag = true;
                    //iTweenを使ったカメラの移動
                    iTween.RotateTo(center, iTween.Hash("y", 0, "time", 1));
                    iTween.MoveTo(center, iTween.Hash("x", -2, "y", -14, "z", 100, "time", 2, "oncomplete", "NextState", "oncompletetarget", gameObject));
                    iTween.RotateTo(camera.gameObject, iTween.Hash("y", 0, "islocal", true));
                }
                break;

            case State.SelectOne:

                //PlayTypeを実行可能に変更
                if (playtype.enabled == false)
                {
                    selectImage.SetActive(true);
                    playtype.enabled = true;
                }
                //スペースキーで選択しているシーンに移動
                if (Input.GetAxisRaw("Space") != 0)
                {
                    //フラグがプレイなら魔法を選択しに行く
                    if (playtype.getPlay)
                    {
                        //選択肢を非表示に変更とPlayTypeスクリプトの実行終了
                        selectImage.SetActive(false);
                        playtype.enabled = false;
                        NextState();
                    }
                    //エンドなら終了
                    else
                        print("End");
                }

                break;

            case State.MagicSelect:
                
                //移動してなかったら移動開始
                if (moveFlag == false)
                {
                    moveFlag = true;
                    //iTweenを使ったカメラの移動と回転
                    iTween.MoveTo(center, iTween.Hash("x", 6.5, "z", 115, "time", 3, "oncomplete", "NextState", "oncompletetarget", gameObject));
                    iTween.RotateTo(camera.gameObject, iTween.Hash("y", 180, "islocal", true));
                }
                break;

            case State.MagicSelectOne:

                //マジックセレクトを表示
                if(magicImage.GetActive() == false)
                {
                    magicImage.SetActive(true);
                    magicSelect.enabled = true;
                }

                //ゲームを開始させる
                if(Input.GetAxisRaw("Space") != 0)
                {
                    ChangeNameSetScene();
                }

                //一つ前に戻る
                if (Input.GetAxisRaw("Cancel") != 0)
                {
                    state = State.Select;
                    magicImage.SetActive(false);
                    magicSelect.enabled = false;
                }

                break;

            //名前設定時
            case State.NameSet:
                //名前を決定したらマップへ
                CheckDecideName();
                break;
            
        }
    }


    void NextState()
    {
        state = state + 1;
        moveFlag = false;
    }


    //名前設定シーンに移動
    void ChangeNameSetScene() {
        //選択肢を非表示に変更とPlayTypeスクリプトの実行終了
        magicImage.SetActive(false);
        nameSetScene.SetActive(true);
        magicSelect.enabled = false;
        state = State.NameSet;

        ec = new EnterChecker();
        inputField.ActivateInputField();
    }


    //名前を決定させたか
    void CheckDecideName() {
        //エンターが押されたかチェック
        if (ec.CheckEnter()) {
            //名前をセーブ
            if (inputField.text == "") inputField.text = "名無しさん" + Random.Range(0,100).ToString();
            PlayerPrefs.SetString(SaveDataKey.PLAYER_NAME_KEY, inputField.text);
            SceneManager.LoadScene("Game");
        }
    }


#if UNITY_EDITOR
    // GUI表示
    void OnGUI() {
        // Photon接続状態
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
    }
#endif



}
