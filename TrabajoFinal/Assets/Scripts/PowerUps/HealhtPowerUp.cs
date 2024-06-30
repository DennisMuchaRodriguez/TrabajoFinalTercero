using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealhtPowerUp : PowerUp
{
    public float healthAmount = 1; 

    protected override void ApplyPowerUpEffect(PlayerController player)
    {
        player.ChangeLife(healthAmount);
    }
}
