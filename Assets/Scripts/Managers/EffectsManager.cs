using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsManager : MonoBehaviour
{
    Dictionary<GameObject,ObjectPool> pools= new Dictionary<GameObject,ObjectPool>();
    public void RegisterNew(GameObject prefab)
    {
        ObjectPool pool=gameObject.AddComponent<ObjectPool>();
        pool.prefab = prefab;
        pools.Add(prefab, pool);
    }

    void Check(GameObject prefab)
    {
        if (!pools.ContainsKey(prefab))
            RegisterNew(prefab);
    }

    public void Spawn(GameObject prefab,Vector3 location,Quaternion rotation)
    {
        Check(prefab);
        GameObject spawn=pools[prefab].GetObject();
        spawn.transform.position = location;
        spawn.transform.rotation = rotation;
    }
}
