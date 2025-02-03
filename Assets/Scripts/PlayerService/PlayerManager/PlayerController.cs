
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController
{
    private PlayerView playerView;
    private bool isDead;
    private float grappleDistance;
    private float grappleSpeed;
    private bool isGrappling;
    private Animator playerAnimator;
    private List<Transform> enemiesInRadius;



    public bool IsDead {  get { return isDead; } }
    public float GrappleDistance {  get { return grappleDistance; } }
    public bool IsGrappling { get { return isGrappling; } }
    public PlayerController(PlayerView playerView,float grappleSpeed)
    {
        this.playerView = playerView;
        this.playerView.SetController(this);
        playerAnimator = playerView.GetPlayerAnimator();
        grappleDistance = 20f;
        this.grappleSpeed = grappleSpeed;
        enemiesInRadius = new List<Transform>();
        GameService.Instance.startGame += OnGameStart;
    }

    public void OnGameStart()
    {
        SetPlayerAliveStatus(true);
        enemiesInRadius.Clear();
    }


    public void SetPlayerDirection(Vector3 mouseWorldPos)
    {
        Vector3 direction=(mouseWorldPos-playerView.transform.position).normalized;
        playerAnimator.SetFloat("x", direction.x);
        playerAnimator.SetFloat ("y", direction.y);
    }

    public void SetPlayerAliveStatus(bool isAlive)
    {
        if(isAlive)
        {
            isDead=false;
            
        }
        else
        {
            isDead = true;
        }
        playerAnimator.SetBool("isDead", isDead);
    }



    public void SetIsGrapple(bool isGrapple)
    {
        isGrappling = isGrapple;
    }


    public void PullObject(Transform target)
    {
        if(PlayerPrefs.GetInt("FirstTime",1)==1)
        {
            GameService.Instance.FadeManager.StartTextFading(FadeTextType.GRAPPLE);
            PlayerPrefs.SetInt("FirstTime", 0);
        }
        Rigidbody2D rb2D = target.GetComponent<Rigidbody2D>();
        if(rb2D!=null)
        {
            rb2D.linearVelocity = (playerView.transform.position - target.position).normalized * grappleSpeed;
        }
    }
    public void EndGrapple(Transform grappledObjectTransform)
    {
        SetIsGrapple(false);
        playerView.GetLineRenderer().enabled = false;
        grappledObjectTransform = null;
    }

    public void AddEnemyInRadius(Transform transform)
    {
        enemiesInRadius.Add(transform);
    }

    public void RemoveEnemyFromRadius(Transform transform)
    {
        if (enemiesInRadius.Contains(transform))
        {
            enemiesInRadius.Remove(transform);
        }
    }


    public void OnSpaceClicked()
    {
        if(PlayerPrefs.GetInt("FirstSpace", 1) == 1)
        {
            if(enemiesInRadius.Count>0)
            {
                PlayerPrefs.SetInt("FirstSpace", 0);
                GameService.Instance.FadeManager.StartTextFading(FadeTextType.SPACE);
                GameService.Instance.UIService.GetInGameUIController().CheckFirstTimeStatus();
                Time.timeScale = 1f;
            }
        }
        List<Transform>toDestroy = new List<Transform>();
        foreach(Transform t in enemiesInRadius)
        {
            toDestroy.Add(t);
        }

        foreach(Transform t in toDestroy)
        {
            enemiesInRadius.Remove(t);
            t.gameObject.GetComponent<EnemyView>().ReturnToPool();
        }
        GameService.Instance.UIService.GetInGameUIController().UpdateBalloonsCollected(GameService.Instance.UIService.GetInGameUIController().CurrentBaloonsCollected+toDestroy.Count);
    }

    public void CheckFirstSpaceClicked()
    {
        if (PlayerPrefs.GetInt("FirstSpace", 1) == 1)
        {
            //Time.timeScale = 0f;
            GameService.Instance.FadeManager.ShowText(FadeTextType.SPACE);
        }
    }

}
