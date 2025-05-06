using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalRifle : BaseGun
{
    private PlayerController _playerController;

    private void Start()
    {
        _playerController = GetComponentInParent<PlayerController>();
    }
    protected override void FireBullet()
    {
        base.FireBullet();
        _playerController.PlayerAnimator.SetTrigger("isShooting"); 
    }
}
