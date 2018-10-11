using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : MonoBehaviour
{
    public virtual void GunUpdate()
    { }

    public virtual void ActiveGunUpdate(GameObject parent)
    { }
}
