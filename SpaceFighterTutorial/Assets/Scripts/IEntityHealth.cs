using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEntityHealth
{
    // a method for taking damage
    void ITakeDamage(float damage);

    // a method for gaining health
    void IGainHealth(float healthAmount);
}
