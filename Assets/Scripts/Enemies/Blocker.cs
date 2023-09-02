using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocker : MonoBehaviour
{
    [SerializeField] GameObject Shield;
    void OnEnable()
    {
        Shield.SetActive(true);
    }
}
