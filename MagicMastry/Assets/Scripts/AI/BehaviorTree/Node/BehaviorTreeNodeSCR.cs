using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorTreeNodeSCR : MonoBehaviour
{
    /*-----------------------------------------------------------------*/
    // オブザーバーパターン
    /*-----------------------------------------------------------------*/
    private List<BehaviorTreeNodeSCR> m_obseverList = new List<BehaviorTreeNodeSCR>();
    /**
     * @brief 通知を受け取る
     * @author shinji
     * @date 11/09
     */
    public virtual void Accept() { }
    /**
     * @brief 通知を受け取る
     * @author shinji
     * @date 11/09
     */
    public virtual void Accept(int error) { }
    /**
     * @brief ObserverをListに追加する
     * @param 追加するオブザーバー
     * @author shinji
     * @date 11/09
     */
    public void Attach(BehaviorTreeNodeSCR obsever)
    {
        m_obseverList.Add(obsever);
    }
    /**
     * @brief Observerをlistから削除する
     * @param 削除するオブザーバー
     * @author shinji
     * @date 11/09
     */
    public void Deteach(BehaviorTreeNodeSCR obsever)
    {
        m_obseverList.Remove(obsever);
    }
    /**
     * @brief Observerに通知を送る
     * @author shinji
     * @date 11/09
     */
    public virtual void Notify()
    {
        foreach (BehaviorTreeNodeSCR obsever in m_obseverList)
            obsever.Accept();
    }
    /**
     * @brief Observerに通知を送る
     * @author shinji
     * @date 11/09
     */
    public virtual void Notify(int error)
    {
        foreach (BehaviorTreeNodeSCR obsever in m_obseverList)
            obsever.Accept(error);
    }
    /*-----------------------------------------------------------------*/
    // 初期化.
    public virtual void Setup() { }
    // アップデート.
    public virtual void DoAction() { }
}