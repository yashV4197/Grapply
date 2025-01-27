
using System.Collections;
using UnityEngine;

public class PlayerView : MonoBehaviour
{

    private PlayerController playerController;
    private Transform grappledObjectTransform;
    [SerializeField] Animator playerAnimator;
    [SerializeField] Transform muzzleTransfrom;
    [SerializeField] Transform grappleGunTransform;
    [SerializeField] Transform playerRadiusTransform;
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] LayerMask ignoreLayer;
    [SerializeField] float playerRadiusRotationSpeed;
    public void SetController(PlayerController playerController)
    {
        this.playerController = playerController;
    }

    private void Update()
    {
        if(playerController?.IsDead==false)
        {
            Vector3 mousePos= Input.mousePosition;
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 0));
            playerController?.SetPlayerDirection(mouseWorldPos);
            WeaponMove(mouseWorldPos);
            Grapple(mouseWorldPos);
            GatherEnemies();
            RotatePlayerRadius();
            if(playerController.IsGrappling==true&&grappledObjectTransform!=null)
            {
                UpdateLineRenderer();
            }
        }
    }

    private void RotatePlayerRadius()
    {
        playerRadiusTransform.Rotate(Vector3.forward * Time.deltaTime * playerRadiusRotationSpeed);
    }

    private void GatherEnemies()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            playerController.OnSpaceClicked();
        }
    }

    private void WeaponMove(Vector3 mouseWorldPos)
    {
        Vector2 aimDirection=(mouseWorldPos-grappleGunTransform.position).normalized;
        float angle= Mathf.Atan2(aimDirection.y, aimDirection.x)*Mathf.Rad2Deg;
        grappleGunTransform.eulerAngles=new Vector3(0,0,angle);
    }

    private void UpdateLineRenderer()
    {
        lineRenderer.SetPosition(0,muzzleTransfrom.position);
        lineRenderer.SetPosition(1,grappledObjectTransform.position);
        StartCoroutine(GrappleChecker());
    }

    private IEnumerator GrappleChecker()
    {
        yield return new WaitForSeconds(.2f);
        playerController.EndGrapple(grappledObjectTransform);
    }

    private void Grapple(Vector3 mouseWorldPos)
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray2D ray = new Ray2D();
            ray.origin = muzzleTransfrom.position;
            ray.direction=((Vector2)mouseWorldPos-ray.origin).normalized;
            RaycastHit2D hit= Physics2D.Raycast(ray.origin,ray.direction,playerController.GrappleDistance,~ignoreLayer);
            if(hit.collider!=null)
            {
                StopAllCoroutines();
                grappledObjectTransform = hit.transform;
                playerController.SetIsGrapple(true);
                playerController.PullObject(grappledObjectTransform);

                lineRenderer.enabled = true;
                lineRenderer.SetPosition(0,ray.origin);
                
            }
        }



    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer==6)
        {
            playerController?.AddEnemyInRadius(collision.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.layer==6)
        {
            playerController?.RemoveEnemyFromRadius(collision.transform);
        }
    }


    public Animator GetPlayerAnimator() => playerAnimator;
    public LineRenderer GetLineRenderer() => lineRenderer;  

}
