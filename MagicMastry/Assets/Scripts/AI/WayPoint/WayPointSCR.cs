using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointSCR : MonoBehaviour
{
    [Tooltip("目的地")][SerializeField]
    private List<PointSCR> m_pointSCRList;
    [Tooltip("目的地のID")][SerializeField]
    private List<int> m_pointSCRList_ID;


    // 目的地.
    public void SetPointSCRList(List<PointSCR> list)
    {
        m_pointSCRList = list;
    }
    public List<PointSCR> GetPointSCRList()
    {
        return m_pointSCRList;
    }
    // 目的地のID.
    public void SetPointSCRList_ID(List<int> list)
    {
        m_pointSCRList_ID = list;
    }
    public List<int> GetPointSCRList_ID()
    {
        return m_pointSCRList_ID;
    }

    //// 次の目的地を取得する.
    //public PointSCR GetNextPointSCR()
    //{
    //    // 仮
    //    int rand;
    //    rand = Random.Range(0, m_pointSCRList.Count);

    //    return m_pointSCRList[rand];
    //}
}
