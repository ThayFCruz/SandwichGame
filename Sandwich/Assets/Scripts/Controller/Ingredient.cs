using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

[Serializable]
public class Ingredient : MonoBehaviour
{
    public Ingredient ingredient;
    public bool isBread;

    private void Start()
    {
        this.transform.DOPunchScale(new Vector3(0.3f,0,0.3f), 0.2f);
    }

}

