using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// オブザーバーパターン用の初期設定.
[RequireComponent(typeof(BehaviorTree_ObseverSCR))]
[RequireComponent(typeof(BehaviorTree_SubjectSCR))]

public class ActionNodeSCR : BehaviorTreeNodeSCR
{
    /**
     * @brief 行動
     * @author shinji
     * @date 11/02
     */
    public override void DoAction()
    {
        print("Attack:" + gameObject.name);
        m_subjectSCR.Notify();
    }
}