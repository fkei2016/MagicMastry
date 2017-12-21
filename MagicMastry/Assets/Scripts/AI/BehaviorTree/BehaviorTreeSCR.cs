using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorTreeSCR : BehaviorTreeNodeSCR
{
    [SerializeField]
    protected BehaviorTreeNodeSCR m_nodeSCR;

    public void SetNode(BehaviorTreeNodeSCR node)
    {
        m_nodeSCR = node;
    }

    private void Start()
    {
        // サブジェクトのオブザーバーを設定する.
        m_nodeSCR.Attach(this);

        // 初期化.
        m_nodeSCR.Setup();
    }

    private void Update()
    {
        // Updateに酷似する関数を実行する.
        m_nodeSCR.DoAction();
    }

    public override void Accept()
    {
        m_nodeSCR.Setup();
    }

    public override void Accept(int error)
    {
        base.Accept(error);

        m_nodeSCR.Setup();
    }
}