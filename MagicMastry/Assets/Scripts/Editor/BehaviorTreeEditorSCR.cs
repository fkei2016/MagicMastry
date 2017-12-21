using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BehaviorTreeEditorSCR : EditorWindow
{
    // 画面の最小サイズ.
    static private Vector2 windowMinSize = new Vector2(325, 40);

    // 分岐の種類.
    enum Composite
    {
        Priority,
        Selector,
        Sequence,
    }

    // 選択された分岐の種類.
    Composite m_composite;

    // 親にするオブジェクト.
    GameObject m_parent;


    [MenuItem("Custom/BehaviorTreeEditor")]
    static void ShowWindow()
    {
        EditorWindow window;
        window = EditorWindow.GetWindow<BehaviorTreeEditorSCR>();
        window.minSize = windowMinSize;
    }

    private void OnGUI()
    {
        // 1.親のオブジェクトを設定する.
        SetParentObject();

        // 2.分岐の種類を設定する.
        SetComposite();
    }

    private void SetParentObject()
    {
        m_parent = (GameObject)EditorGUILayout.ObjectField("Parent", m_parent, typeof(GameObject), GUILayout.MinWidth(300.0f));
    }

    private void SetComposite()
    {
        EditorGUILayout.BeginHorizontal();

        // 1.分岐の種類を設定する.
        m_composite = (Composite)EditorGUILayout.EnumPopup("Composite", (Composite)m_composite, GUILayout.MinWidth(250.0f));

        // 2.ボタンが押されたとき→オブジェクトを生成する.
        if (GUILayout.Button("OK!", GUILayout.Width(50.0f)))
        {
            // 2.1.親が未設定の場合→処理を終える.
            if (m_parent == null) return;

            // 2.2.分岐の種類を生成する.
            else CreateComposite();
        }
        EditorGUILayout.EndHorizontal();
    }

    private void CreateComposite()
    {
        string pass = "";
        switch (m_composite)
        {
            case Composite.Priority: pass = "PriorityNode"; break;
            case Composite.Selector: pass = "SelectorNode"; break;
            case Composite.Sequence: pass = "SequenceNode"; break;
        }

        GameObject prefab = (GameObject)Resources.Load("AI/BehaviorTree/" + pass);
        GameObject obj = Instantiate(prefab);
        obj.transform.SetParent(m_parent.transform);

        CompositeSCR compositeSCR = m_parent.GetComponent<CompositeSCR>();
        if (compositeSCR)
            compositeSCR.AddNode(obj.GetComponent<BehaviorTreeNodeSCR>());
        else
        {
            BehaviorTreeSCR behaviorTreeSCR = m_parent.GetComponent<BehaviorTreeSCR>();
            if (behaviorTreeSCR)
                behaviorTreeSCR.SetNode(obj.GetComponent<BehaviorTreeNodeSCR>());
            else
            {
                behaviorTreeSCR = m_parent.AddComponent<BehaviorTreeSCR>();
                behaviorTreeSCR.SetNode(obj.GetComponent<BehaviorTreeNodeSCR>());
            }     
        }

        //BehaviorTreeSCR behaviorTreeSCR = m_parent.GetComponent<BehaviorTreeSCR>();
        //if (behaviorTreeSCR)
        //    behaviorTreeSCR.SetNode(obj.GetComponent<BehaviorTreeNodeSCR>());

    }
}