using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Need to rewrite this to be generic and find the weapon in your hand and see if the trigger hand matches the actual trigger

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] GameObject weplGO;
    [SerializeField] GameObject weprGO;

    void Update()
    {
        if (Input.GetButtonDown("XRI_Left_TriggerButton"))
            weplGO.GetComponent<Weapon>().TryFire(true);
        else if (Input.GetButtonUp("XRI_Left_TriggerButton"))
            weplGO.GetComponent<Weapon>().TryFire(false);
        else if (Input.GetButtonDown("XRI_Right_TriggerButton"))
            weprGO.GetComponent<Weapon>().TryFire(true);
        else if (Input.GetButtonUp("XRI_Right_TriggerButton"))
            weprGO.GetComponent<Weapon>().TryFire(false);
    }
}
