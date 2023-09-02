using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUps : MonoBehaviour
{
    float InfTime=0;
    float InvTime=0;

    bool inf;
    bool inv;

    [SerializeField] Weapon WepL;
    [SerializeField] Weapon WepR;

    [SerializeField] Image Inf;
    [SerializeField] Image Inv;
    [SerializeField] Image Key1;
    [SerializeField] Image Key2;
    [SerializeField] Image Key3;
    [SerializeField] Image Key4;
    [SerializeField] Image Key5;

    [SerializeField] AudioSource One;
    [SerializeField] AudioSource Two;
    [SerializeField] AudioSource Three;
    [SerializeField] AudioSource Key;

    // Update is called once per frame
    void Update()
    {
        if (inv)
        {
            if (InvTime > 0)
            {
                InvTime -= Time.deltaTime;
                Inv.color = new Vector4(1, 1, 1, 1 * InvTime / 10);
            }
            else
            {
                GetComponent<Health>().invincible = false;
                Inv.color = new Vector4(1, 1, 1, 0);
                inv = false;
            }
        }

        if (inf)
        {
            if (InfTime > 0)
            {
                InfTime -= Time.deltaTime;
                Inf.color = new Vector4(1, 1, 1, 1 * InfTime / 10);
            }
            else
            {
                WepL.Boost(false);
                WepR.Boost(false);
                Inf.color = new Vector4(1, 1, 1, 0);
                inf = false;
            }
        }
    }

    public void Apply(int type)
    {
        if (type == 0) //health
            GetComponent<Health>().Heal(15);
        else if (type == 1) //infinite ammo
        {
            WepL.Boost(true);
            WepR.Boost(true);
            InfTime = 10;
            Inf.color = Color.white;
            inf = true;
            Two.Play();
        }
        else if (type == 2)
        {
            GetComponent<Health>().invincible = true;
            InvTime = 10;
            Inv.color = Color.white;
            inv = true;
            Three.Play();
		}
        else if (type == 3)
        {
            Apply(0);
            Apply(1);
            Apply(2);
        }
        else
        {
            //apply key type;
        }
    }
}
