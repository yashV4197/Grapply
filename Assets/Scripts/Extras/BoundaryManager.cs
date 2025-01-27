
using System.Collections.Generic;
using UnityEngine;

public class BoundaryManager : MonoBehaviour
{
    [SerializeField] EdgeCollider2D edgeCollider;
    [SerializeField] float reflectSpeed;
    [SerializeField] LayerMask EnemyLayer;
    private void Awake()
    {
        GameService.Instance.startGame += OnGameStart;
    }

    public void OnGameStart()
    {
        CreateEdges();
    }

    private void CreateEdges()
    {
        List<Vector2> edges = new List<Vector2>();
        edgeCollider.enabled = true;
        edges.Add(Camera.main.ScreenToWorldPoint(Vector2.zero));
        edges.Add(Camera.main.ScreenToWorldPoint(new Vector2(Screen.width,0)));
        edges.Add(Camera.main.ScreenToWorldPoint(new Vector2(Screen.width,Screen.height)));
        edges.Add(Camera.main.ScreenToWorldPoint(new Vector2(0,Screen.height)));
        edges.Add(Camera.main.ScreenToWorldPoint(Vector2.zero));
        edgeCollider.SetPoints(edges);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //change later
        if(collision.gameObject.layer==6)
        {
            ReflectObject(collision.transform);
        }
    }


    private void ReflectObject(Transform target)
    {
        Ray ray = new Ray();
        Rigidbody2D rb2D= target.GetComponent<Rigidbody2D>();
        if(rb2D!=null)
        {
            ray.origin = target.position;
            ray.direction=rb2D.linearVelocity.normalized;
            RaycastHit2D[] hit2D = Physics2D.RaycastAll(ray.origin, ray.direction, 100f);
            if (hit2D.Length > 1)
            {
                Vector2 contactPoint = hit2D[1].point;
                Vector2 normal = Vector2.Perpendicular(contactPoint - GetNearestEdgePoint(contactPoint)).normalized;
                Vector2 reflectedDirection = ReflectRay(ray.direction, normal, contactPoint).normalized;
                rb2D.linearVelocity = -reflectedDirection * reflectSpeed;
            }
        }

    }

    private Vector2 ReflectRay(Vector3 direction, Vector2 normal,Vector2 contactPoint)
    {
        float scalerProj=Vector2.Dot(direction, normal);
        return contactPoint - scalerProj * normal * 2;
    }

    private Vector2 GetNearestEdgePoint(Vector2 contactPoint)
    {
        Vector2[] points = edgeCollider.points;
        float shortestDist = Vector2.Distance(contactPoint, points[0]);
        Vector2 shortestPoint = points[0];
        for(int i = 1; i < points.Length; i++)
        {
            float newDist = Vector2.Distance(points[i], contactPoint);
            if (shortestDist > newDist)
            {
                shortestDist=newDist;
                shortestPoint = points[i];
            }
        }
        return shortestPoint;
    }
}
