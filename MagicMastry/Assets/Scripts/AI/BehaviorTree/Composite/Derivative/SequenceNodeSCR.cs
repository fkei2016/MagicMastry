using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// オブザーバーパターン用の初期設定.
[RequireComponent(typeof(BehaviorTree_ObseverSCR))]
[RequireComponent(typeof(BehaviorTree_SubjectSCR))]

public class SequenceNodeSCR : CompositeSCR
{
    /*--------------------------------------------------------------------------------------------*/
    // オブザーバーパターン.
    /*--------------------------------------------------------------------------------------------*/
    /**
     * @brief 通知を受けた場合に呼ばれる
     * @author shinji
     * @date 11/02
     */
    public override void Accept()
    {
        // 1.ノードの番号を次に進める(11/02).
        m_currentNodeSCRNumber++;

        // 2.1.ノードが存在しない場合->オブザーバーの実行(11/02).
        if (m_currentNodeSCRNumber >= m_nodeSCRList.Length)
        {
            m_subjectSCR.Notify();
        }
        // 2.2.ノードが存在しない場合->初期化を行う(11/02).
        else
        {
            m_currentNodeSCR = m_nodeSCRList[m_currentNodeSCRNumber];
            m_currentNodeSCR.Setup();
        }
    }
    /**
     * @brief 通知を受けた場合に呼ばれる
     * @param エラーコード
     * @author shinji
     * @date 11/02
     */
    public override void Accept(int error)
    {
        //print("Error:" + gameObject.name);
        m_subjectSCR.Notify(error);
    }
    /*--------------------------------------------------------------------------------------------*/
}