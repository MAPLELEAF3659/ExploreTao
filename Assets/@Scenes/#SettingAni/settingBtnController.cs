using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class settingBtnController : MonoBehaviour
{
    Animator settingAni;
    private void Start()
    {
        settingAni = gameObject.GetComponent<Animator>();
    }
    public void Toggle()
    {
        settingAni.SetTrigger("toggle");
    }
}
