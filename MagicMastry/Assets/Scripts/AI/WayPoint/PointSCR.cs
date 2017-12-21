using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PointSCR : MonoBehaviour
{
    [Tooltip("目的地")] [SerializeField]
    private List<PointSCR> m_destinationPointSCRList;


    /**
    * @brief 目的地のリストを渡す
    * @return List<PointSCR>(目的地のリスト)
    * @author shinji
    * @date 12/18
    */
    public List<PointSCR> GetDestinationPointSCRList()
    {
        return m_destinationPointSCRList;
    }

    /**
    * @brief 目的地を渡す
    * @return PointSCR(目的地)
    * @author shinji
    * @date 12/18
    */
    public PointSCR GetDestinationPointSCR()
    {
        // 1.目的地を決める.
        PointSCR nextPointSCR;
        {
            int count = m_destinationPointSCRList.Count;
            count = Random.Range(0, count);

            nextPointSCR = m_destinationPointSCRList[count];
        }

        // 2.次の目的地を設定する.
        SetDestinationPosition(nextPointSCR.gameObject);

        // 3..目的地を渡す.
        return nextPointSCR;
    }

    Vector3 m_destinationPosition;
    int m_destinationPositionNum;

    /**
    * @brief 目的地を設定する
    * @param GameObject(次の目的地)
    * @author shinji
    * @date 12/18
    */
    private void SetDestinationPosition(GameObject obj)
    {
        int count = obj.transform.childCount;
        int randA = Random.Range(0, 2);
        int randB = Random.Range(0, 4);

        if(randA == 0)
            randB += 4;//5?

        m_destinationPositionNum = randB;
        m_destinationPosition = obj.transform.GetChild(m_destinationPositionNum).position;      
    }


    public Vector3 GetDestinationPosition()
    {
        return m_destinationPosition;
    }

    public int GetDestinationPositionNum()
    {
        return m_destinationPositionNum;
    }

    // 初期化.
    public void SetUp()
    {
         // 子供たちを作る.
         CreateChildren();
    }

    // 子供たちを作る.
    private void CreateChildren()
    {
        for (int i = 0; i < 9; i++)
            if (i != 4) CreateChild(i);
    }
    // 子供を作る
    private void CreateChild(int i)
    {
        // 1.オブジェクトを作成する.
        GameObject prefab = (GameObject)Resources.Load("AI/PointChild");
        GameObject obj = Instantiate(prefab);

        // 2.親を決める.
        obj.transform.SetParent(this.gameObject.transform);

        // 3.座標を変更する.
        Vector3 position = Vector3.zero;
        position.x = i % 3 - 3 / 2;
        position.z = 3 / 2 - i / 3;
        obj.transform.position = position * 3 + this.transform.position;

        // 4.名前を付ける.
        position pos = (position)i;
        obj.name = this.gameObject.name + "_" + pos.ToString();
    }

    public enum position
    {
        TopLeft,
        TopCentor,
        TopRight,

        MiddleLeft,
        MiddleCentor,
        MiddleRight,

        BottomLeft,
        BottomCentor,
        BottomRight,
    }
}
