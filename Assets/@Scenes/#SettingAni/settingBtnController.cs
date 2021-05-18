using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class settingBtnController : MonoBehaviour
{
    public Animator settingAni;
    public void Toggle()
    {
        settingAni.SetTrigger("toggle");
    }
}
