using System.Collections;
using UnityEngine;
using Game.Entities;
using Game.Managers;

public class PlayerSword : PlayerRotation
{
    [Header("Player Sword Particles")]
    [SerializeField] private ParticleSystem particles1;
    [SerializeField] private ParticleSystem particles2;
    [SerializeField] private ParticleSystem particles3;
    [SerializeField] private ParticleSystem particles4;
    
    protected override IEnumerator PlayerDies()
    {
        GameManager.Instance.PlayerDies();
        invincible = true;
        particles.Stop();
        particles1.Stop();
        particles2.Stop();
        particles3.Stop();
        particles4.Stop();
        spRenderer.enabled = false;
        baseRenderer.enabled = false;
        middleRenderer.enabled = false;
        rb2D.velocity = Vector2.zero;

        yield return new WaitForSecondsRealtime(timePassedWhenHit);

        particles.Play();
        particles1.Play();
        particles2.Play();
        particles3.Play();
        particles4.Play();
        spRenderer.enabled = true;
        baseRenderer.enabled = true;
        middleRenderer.enabled = true;
        invincible = false;
        GameManager.Instance.PlayerRevive();
    }
}
