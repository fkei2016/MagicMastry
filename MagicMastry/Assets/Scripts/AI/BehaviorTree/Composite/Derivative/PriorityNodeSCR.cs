using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// オブザーバーパターン用の初期設定.
[RequireComponent(typeof(BehaviorTree_ObseverSCR))]
[RequireComponent(typeof(BehaviorTree_SubjectSCR))]

public class PriorityNodeSCR : CompositeSCR
{
    /*--------------------------------------------------------------------------------------------*/
    [SerializeField, Tooltip("優先度")]
    int[] m_prioritise;
    /*--------------------------------------------------------------------------------------------*/
    protected override int GetNodeNumber()
    {
        // 1. 優先度の合計を取得する. 
        int sum = 0;
        for (int i = 0; i < m_prioritise.Length; i++)
            sum += m_prioritise[i];

        // 2. 優先度の合計から数字を１個だけ取得する. 
        int number;
        number = Random.Range(0, sum);

        // 3. 取得した数字からノードの番号を決定する.
        for (int i = 0; i < m_prioritise.Length; i++)
        {
            number -= m_prioritise[i];

            if (number <= 0)
                return i;
        }
        return -1;
    }
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
        m_subjectSCR.Notify();
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