using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// オブザーバーパターン用の初期設定.
[RequireComponent(typeof(BehaviorTree_ObseverSCR))]
[RequireComponent(typeof(BehaviorTree_SubjectSCR))]

public class BehaviorTreeSCR : BehaviorTreeNodeSCR
{
    [SerializeField]
    protected BehaviorTreeNodeSCR m_nodeSCR;

    private void Start()
    {
        // サブジェクトのオブザーバーを設定する.
        m_nodeSCR.GetSubjectSCR().Attach(m_obseverSCR);

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
        m_nodeSCR.GetComponent<CompositeSCR>().Setup();
    }

    public override void Accept(int error)
    {
        base.Accept(error);

        m_nodeSCR.GetComponent<CompositeSCR>().Setup();
    }

    private void OnTriggerEnter(Collider other)
    {
        this.gameObject.transform.eulerAngles += new Vector3(0, 170, 0);
        // 行動を考え直す.
        Accept();
    }
}