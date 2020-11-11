using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadAbility : MonoBehaviour , IAbility
{
    public float reloadSpeed = 2.0f;
    Player player;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    public void ExecuteAbility()
    {
        player.ReloadGun(reloadSpeed);
    }

}
