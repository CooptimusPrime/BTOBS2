using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Probably need to redo this

public class Projectile : MonoBehaviour
{
    /////References/////
    GameObject source;
    Lifespan lifespan;
    Explosive explosivecomponent;

    /////Component Variables/////
    int damage, type = 1, piercing = 0, element = 0;
    float homing;
    bool explosive = false;
    string alignment;
    //bullet drop for launchers?

    /////Initialization/////
    void Start()
    {
        lifespan = gameObject.AddComponent<Lifespan>();
        explosivecomponent = gameObject.AddComponent<Explosive>();
        if (!explosive)
            explosivecomponent.enabled = false;
    }

    //////Component Functions/////

    //might want an update function to rotate the projectile

    public void SetStats(GameObject source, int damage, float lifespan, int type = 1, int element = 0, int piercing = 0, float homing = 0, bool explosive = false)
    {
        if (this.source != source)
        {
            this.source = source;
            SetAlignment();
        }
        this.damage = damage;
        this.lifespan.SetLifespan(lifespan);
        this.type = type;
        this.element = element;
        this.piercing = piercing;
        this.homing = homing;
        if (explosive) //need to add support for changing explosive values too
            explosivecomponent.enabled = true;
    }

    void OnTriggerEnter(Collider other)
    {
        //hitting other projectiles, piercing, friendly fire

        if (alignment != CheckAlignment(other.gameObject))
        {
            if (explosive)
                explosivecomponent.Explode(source,damage);
            else if (other.TryGetComponent(out Health health))
                health.GetHit(source, damage, type, element);
            gameObject.SetActive(false);
        }
    }

    string CheckAlignment(GameObject source)
    {
        if (source.CompareTag("PlayerWeapon") || source.CompareTag("AllyWeapon") || source.CompareTag("Player") || source.CompareTag("Ally"))
            return ("player");
        else if (source.CompareTag("EnemyWeapon") || source.CompareTag("Enemy"))
            return ("enemy");
        else
            return ("neutral");
    }

    void SetAlignment()
    {
        alignment = CheckAlignment(this.source);
    }
}
