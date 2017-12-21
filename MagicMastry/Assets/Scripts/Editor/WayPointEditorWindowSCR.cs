using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class WayPointEditorWindowSCR : EditorWindow
{
    // 画面の最小サイズ.
    static private Vector2 windowMinSize = new Vector2(325, 40);

    [MenuItem("Custom/WayPoint")]



    static void ShowWindow()
    {
        EditorWindow window;
        window = EditorWindow.GetWindow<WayPointEditorWindowSCR>();
        window.minSize = windowMinSize;

        GameObject prefab = (GameObject)Resources.Load("AI/WayPoint");
        GameObject obj = Instantiate(prefab);       
    }

    List<PointSCR> m_pointSCRList = null;





    private void OnGUI()
    {
        // 保存用のスクリプトを設定する.
        SetWayPointStorageSCR();


        // 初期化.
        Reset();


        EditorGUILayout.Space();
        GUILayout.Box("", GUILayout.Width(this.position.width), GUILayout.Height(1));
        EditorGUILayout.Space();

        CreatePoint();




        // 
        ShowPoint();


        EditorGUILayout.BeginHorizontal();
        ChangeColor();





        ResetColor();

        EditorGUILayout.EndHorizontal();





        LoadObjects();


       
       
    }
    

    private void OnEnable()
    {
        
    }



    //WayPointSCR m_wayPointSCR;


    // 保存用のスクリプト.
    WayPointStorageSCR m_wayPointStorageSCR;

    // 保存用のスクリプトを設定する.
    private void SetWayPointStorageSCR()
    {
        EditorGUILayout.BeginHorizontal();

        WayPointStorageSCR tmp;
        tmp = m_wayPointStorageSCR;

        // 名前の入力フィールド.
        EditorGUILayout.LabelField("保存場所:", GUILayout.Width(55));

        // 入力フィールドを作成する.
        m_wayPointStorageSCR = (WayPointStorageSCR)EditorGUILayout.ObjectField(m_wayPointStorageSCR, typeof(WayPointStorageSCR), true);

        // 更新があった場合.
        if (m_wayPointStorageSCR != tmp)
        {
            m_pointSCRList = m_wayPointStorageSCR.GetPointSCRList();
            m_pointSCRList_ID = m_wayPointStorageSCR.GetPointSCRList_ID();
        }

        // セーブボタンを押したとき→セーブする.
        if (GUILayout.Button("Save"))
            Save();

        EditorGUILayout.EndHorizontal();
    }




    private void Save()
    {
        m_wayPointStorageSCR.SetPointSCRList(m_pointSCRList);
        m_wayPointStorageSCR.SetPointSCRList_ID(m_pointSCRList_ID);
    }


    //private void OnDisable()
    //{
    //    Save();
    //}





    // 初期化.
    private void Reset()
    {
        // 1.ボタンが押されたとき.
        if (GUILayout.Button("AllClear!"))
        {
            // 2.インデックスを削除する.
            if (m_pointSCRList != null)
                m_pointSCRList.Clear();

            if (m_pointSCRList_ID != null)
                m_pointSCRList_ID.Clear();
        }
    }

    string m_name;

    // 
    private void CreatePoint()
    {
        EditorGUILayout.BeginHorizontal();

        PointSCR pointSCR = null;
        GameObject obj = null;

        // 名前の入力フィールド.
        EditorGUILayout.LabelField("名前:", GUILayout.Width(30));
        m_name = EditorGUILayout.TextField(m_name);

        // ボタンが押されたとき.
        if (GUILayout.Button("Create!"))
        {
            // オブジェクトを生成する.
            GameObject prefab = (GameObject)Resources.Load("AI/Point");
            obj = Instantiate(prefab);
            obj.name = m_name;

            // オブジェクトごとに8個のポイントを作成する.

            pointSCR = obj.GetComponent<PointSCR>();
            pointSCR.SetUp();

            //obj.transform.SetParent(m_wayPointSCR.transform);
            obj.transform.SetParent(m_wayPointStorageSCR.transform);

            //Save();
        }

        EditorGUILayout.EndHorizontal();

        // 目的地を追加する. 
        if (pointSCR)
            AddPoint(pointSCR);
    }


    // インデックスを追加する.
    private void AddPoint(PointSCR pointSCR)
    {
        EditorGUILayout.BeginHorizontal();

        // 1.重複を防ぐ.
        bool have = false;
        if (m_pointSCRList != null)
            for (int i = 0; i < m_pointSCRList.Count; i++)
                if (m_pointSCRList[i] == pointSCR)
                    have = true;
        if (have == false)
        {
            // 2.インデックスを追加する.
            m_pointSCRList.Add(pointSCR);

            // 3.オブジェクトをセーブする.
            SaveObject(pointSCR.gameObject);
        }

        EditorGUILayout.EndHorizontal();
    }


    private void ShowPoint()
    {
        EditorGUILayout.LabelField("追加されているインデックス");

        // 1.インデックスない→終了する.
        if (m_pointSCRList == null)
            return;

        // 2.全体を取得する.
        for (int i = 0; i < m_pointSCRList.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();

            // 3.インデックスを表示する.
            EditorGUILayout.ObjectField(m_pointSCRList[i], typeof(PointSCR), true);

            // 4.ボタンが押されたとき.
            if (GUILayout.Button("Del"))
            {
                // 5.オブジェクトを削除する.
                PointSCR pointSCR;
                pointSCR = m_pointSCRList[i];
                if (pointSCR)
                    Object.DestroyImmediate(pointSCR.gameObject);

                // 6.インデックスを削除する.
                m_pointSCRList.RemoveAt(i);
                m_pointSCRList_ID.RemoveAt(i);
            }
            EditorGUILayout.EndHorizontal();
        }
    }

    // 色を変更する.
    private void ChangeColor()
    {
        // 1..ボタンが押さていない→処理を終了する.
        if (!GUILayout.Button("ChangeColor!"))
            return;

        // 2.全体を取得する.
        foreach (GameObject obj in Selection.gameObjects)
        {
            // 3.スクリプトを取得する.
            PointSCR pointSCR;
            pointSCR = obj.GetComponent<PointSCR>();
            if (pointSCR)
            {
                // 4.中に格納されている配列を取得する.
                List<PointSCR> list;
                list = pointSCR.GetDestinationPointSCRList();

                // 5.色を変更する.
                for (int i = 0; i < list.Count; i++)
                    list[i].gameObject.GetComponent<Renderer>().material.color = Color.black;
            }
        }
    }
    // 色を初期化する.
    private void ResetColor()
    {
        // 1..ボタンが押さていない→処理を終了する.
        if (!GUILayout.Button("ResetColor!"))
            return;

        // 2.色を変更する.
        for (int i = 0; i < m_pointSCRList.Count; i++)
            m_pointSCRList[i].GetComponent<Renderer>().material.color = Color.white;
    }

    // オブジェクトとIDを紐付けする
    List<int> m_pointSCRList_ID;

    // オブジェクト達をロードする.
    private void LoadObjects()
    {
        // 1.オブジェクトのリストがnullの場合→終了.
        if (m_pointSCRList.Count == 0)
            return;


        // 1.オブジェクトのリストがnullの場合→終了.
        if (m_pointSCRList == null)
            return;

        if (m_pointSCRList[0] == null)
        {
            m_pointSCRList = m_wayPointStorageSCR.GetPointSCRList();
            m_pointSCRList_ID = m_wayPointStorageSCR.GetPointSCRList_ID();


            //// 2.配列全体を取得する.
            //for (int i = 0; i < m_pointSCRList.Count; i++)
            //{
            //    // 4.実際にオブジェクトをロードする.
            //    GameObject obj;
            //    obj = LoadObject(i);

            //    // 5.配列を更新する.
            //    m_pointSCRList[i] = obj.GetComponent<PointSCR>();
            //}
        }

        //// 2.配列全体を取得する.
        //for (int i = 0; i < m_pointSCRList.Count; i++)
        //{
        //    // 3.中身がnullの場合.
        //    if (m_pointSCRList[i] == null)
        //    {
        //        // 4.実際にオブジェクトをロードする.
        //        GameObject obj;
        //        obj = LoadObject(i);

        //        // 5.配列を更新する.
        //        m_pointSCRList[i] = obj.GetComponent<PointSCR>();
        //    }
        //}
    }
    // オブジェクトをロードする.
    GameObject LoadObject(int i)
    {
        //m_pointSCRList = m_wayPointStorageSCR.GetPointSCRList();
        //m_pointSCRList_ID = m_wayPointStorageSCR.GetPointSCRList_ID();

        // 1.考えられる数字の場合のみ.
        if (m_pointSCRList_ID.Count > i)
        {
            // 2.データを取得する.
            int id = m_pointSCRList_ID[i];
            return EditorUtility.InstanceIDToObject(id) as GameObject;
        }
        return null;
    }

    // オブジェクトをセーブする.
    int SaveObject(GameObject obj)
    {
        // 1.がない→終了.
        if (obj == null)
            return -1;

        // 2.idを取得する.
        int id = obj.GetInstanceID();
        
        // 3.保存済み→終了.
        for(int i = 0; i < m_pointSCRList_ID.Count; i++)
            if(m_pointSCRList_ID[i] == id)
                return -1;

        // 4.idを保存する.
        m_pointSCRList_ID.Add(id);

        return id;
    }
}