using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMagus : MonoBehaviour
{

    private const int maximumSlotNum = 4;

    [SerializeField]
    public MagicScript[] magic;

    public CooldownGaugeManager ui;
    public BarGaugeControler ui2;
    public ClockControler ui3;

    private float maximumCoolTime;
    private float coolTime;
    // Use this for initialization
    void Start()
    {

        Debug.Log("s");
        maximumCoolTime = 15.0f;
        coolTime = 0;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown("a") && coolTime <= 0)
        {
            coolTime = maximumCoolTime;
            magic[0].CastSpell();
        }
        ui.GaugeUp.setValue(maximumCoolTime, maximumCoolTime - coolTime);
        ui2.setValue(maximumCoolTime, coolTime);
        ui3.setValue(maximumCoolTime, coolTime);

        coolTime -= 0.1f;

        coolTime = Mathf.Clamp(coolTime, 0, maximumCoolTime);

    }
}
