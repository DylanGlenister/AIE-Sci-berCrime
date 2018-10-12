using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : MonoBehaviour
{
    // Background update gun refresh virtual function
    public virtual void GunUpdate()
    { }

    // Current gun update virtual function
    public virtual void ActiveGunUpdate(GameObject parent)
    { }
}
