using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompositeSCR : BehaviorTreeNodeSCR
{
    // "NodeSCR"の動的配列.
    [SerializeField]
    protected BehaviorTreeNodeSCR[] m_nodeSCRList;
    // 選択中のノード.
    protected BehaviorTreeNodeSCR m_currentNodeSCR;
    // 選択中のノードの番号.
    protected int m_currentNodeSCRNumber;

    void Awake()
    {
        // オブザーバパターン用の初期設定を行う.
        for (int i = 0; i < m_nodeSCRList.Length; i++)
            m_nodeSCRList[i].Attach(this);
    }
    /**
     * @brief 初期化
     * @author shinji
     * @date 11/09
     */
    public override void Setup()
    {
        // 選択中のノードの番号を初期化する.
        m_currentNodeSCRNumber = GetNodeNumber();
        // 選択中のノードを設定する.
        m_currentNodeSCR = m_nodeSCRList[m_currentNodeSCRNumber];
        // 選択中のノードを初期化する.
        m_currentNodeSCR.Setup();
    }

    /**
     * @brief 実行
     * @author shinji
     * @date 11/09
     */
    public override void DoAction()
    {
        m_currentNodeSCR.DoAction();
    }

    protected virtual int GetNodeNumber() { return 0; }
}