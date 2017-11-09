using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// オブザーバーパターン用の初期設定.
[RequireComponent(typeof(BehaviorTree_ObseverSCR))]
[RequireComponent(typeof(BehaviorTree_SubjectSCR))]

public class BehaviorTreeNodeSCR : MonoBehaviour
{
    /*--------------------------------------------------------------------------------------------*/
    // オブザーバーパターンの組み込み.
    /*--------------------------------------------------------------------------------------------*/
    // オブザーバー(監視者).
    protected BehaviorTree_ObseverSCR m_obseverSCR;
    public BehaviorTree_ObseverSCR GetObseverSCR() { return m_obseverSCR; }
    public void SetObseverSCR(BehaviorTree_ObseverSCR obseverSCR) { m_obseverSCR = obseverSCR; }
    // サブジェクト(報告者).
    protected BehaviorTree_SubjectSCR m_subjectSCR;
    public BehaviorTree_SubjectSCR GetSubjectSCR() { return m_subjectSCR; }
    public void SetSubjectSCR(BehaviorTree_SubjectSCR subjectSCR) { m_subjectSCR = subjectSCR; }
    /*--------------------------------------------------------------------------------------------*/
    protected virtual void Awake()
    {
        // 1.BehaviorTree用Obseverを設定する.
        m_obseverSCR = this.gameObject.GetComponent<BehaviorTree_ObseverSCR>();
        m_subjectSCR = this.gameObject.GetComponent<BehaviorTree_SubjectSCR>();

        // 2.Obseverに自分自身を追加する.
        m_obseverSCR.SetNodeSCR(this);
    }
    /*--------------------------------------------------------------------------------------------*/
    // 初期化.
    public virtual void Setup() { }
    // アップデート.
    public virtual void DoAction() { }
    /*--------------------------------------------------------------------------------------------*/
    // 通知を受け取る.
    public virtual void Accept() { }
    public virtual void Accept(int error) { }
    /*--------------------------------------------------------------------------------------------*/
}