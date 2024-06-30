using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerUp : PowerUp
{
    public float speedMultiplier = 1.5f; 
    public float boostDuration = 5.0f;

    protected override void ApplyPowerUpEffect(PlayerController player)
    {
        player.StartCoroutine(player.ApplySpeedBoostCoroutine(speedMultiplier, boostDuration));
    }
}
