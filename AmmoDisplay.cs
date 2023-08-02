using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AmmoDisplay : MonoBehaviour
{
    public GameObject ammoTextUI;
    public static int pistolCount = 10;

    void Update()
    {
        ammoTextUI.GetComponent<TMP_Text>().text = "AMMO: " + pistolCount;    
    }
}
