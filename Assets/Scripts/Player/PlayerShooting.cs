using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] Weapon WepL;
    [SerializeField] Weapon WepR;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("XRI_Left_TriggerButton")&&WepL.gameObject.activeInHierarchy)
        {
            WepL.Shoot();
        }
        if (Input.GetButtonDown("XRI_Right_TriggerButton")&&WepR.gameObject.activeInHierarchy)
            WepR.Shoot();
    }
}
