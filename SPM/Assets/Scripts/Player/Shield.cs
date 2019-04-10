using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{

    private BoxCollider boxCollider;
    public Player Player;
    public LayerMask ProjectileMask;
    public Camera playerCamera;
    private Vector3 shieldPos;
    // Start is called before the first frame update
    void Start()
    {
        shieldPos = new Vector3(0f, 0.2f, 1.0f);
        boxCollider = GetComponentInChildren<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
      //  Reflect();
        var direction = playerCamera.transform.forward; 
        //direction = Vector3.ProjectOnPlane(direction * 3.0f, new Vector3(0,-0.5f,0));
        transform.rotation = Quaternion.LookRotation(direction);
        Vector3 update = transform.rotation * shieldPos.normalized;
        transform.position = update * shieldPos.magnitude + Player.transform.position;
        Debug.DrawRay(transform.position, playerCamera.transform.forward);
        //var topRight = transform.position + new Vector3(boxCollider.size.x / 2, boxCollider.size.z / 2);
        //var topLeft = transform.position +  new Vector3(-boxCollider.size.x / 2, boxCollider.size.y / 2);
        //var botRight = transform.position + new Vector3(boxCollider.size.x / 2, -boxCollider.size.z / 2);
        //var botLeft = transform.position - new Vector3(boxCollider.size.x / 2, boxCollider.size.z / 2);



        //Debug.DrawRay(topRight, playerCamera.transform.forward);
        //Debug.DrawRay(topLeft, playerCamera.transform.forward);

        //Debug.DrawRay(botRight, playerCamera.transform.forward);
        //Debug.DrawRay(botLeft, playerCamera.transform.forward);

        ExtDebug.DrawBoxCastBox(transform.position, Quaternion.Euler(0, 90, 0) * boxCollider.size / 2, transform.rotation, transform.forward, 0.5f, Color.white);

        //Debug.Log(transform.position);
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Projectile"))
    //    {
    //        Debug.Log("Träff");
    //        other.GetComponent<Projectile>().Velocity = -other.GetComponent<Projectile>().Velocity.normalized * other.GetComponent<Projectile>().Velocity.magnitude;
    //    }
    //}

    public void Reflect()
    {
        Debug.Log("test");
        var size = Quaternion.Euler(0, 90, 0) * boxCollider.size /2 ;
        RaycastHit hit;
        //Debug.DrawRay(transform.position, transform.position.normalized * transform.position.magnitude);
        if (Physics.BoxCast(transform.position, size, transform.forward, out hit, transform.rotation, 0.5f, ProjectileMask))
        {
            if (hit.collider != null)
            {
                Debug.Log("test2");
                hit.collider.GetComponent<Projectile>().Velocity = -hit.collider.GetComponent<Projectile>().Velocity;
            }
        }
    }
  
 public static class ExtDebug
{
    //Draws just the box at where it is currently hitting.
    public static void DrawBoxCastOnHit(Vector3 origin, Vector3 halfExtents, Quaternion orientation, Vector3 direction, float hitInfoDistance, Color color)
    {
        origin = CastCenterOnCollision(origin, direction, hitInfoDistance);
        DrawBox(origin, halfExtents, orientation, color);
    }

    //Draws the full box from start of cast to its end distance. Can also pass in hitInfoDistance instead of full distance
    public static void DrawBoxCastBox(Vector3 origin, Vector3 halfExtents, Quaternion orientation, Vector3 direction, float distance, Color color)
    {
        direction.Normalize();
        Box bottomBox = new Box(origin, halfExtents, orientation);
        Box topBox = new Box(origin + (direction * distance), halfExtents, orientation);

        Debug.DrawLine(bottomBox.backBottomLeft, topBox.backBottomLeft, color);
        Debug.DrawLine(bottomBox.backBottomRight, topBox.backBottomRight, color);
        Debug.DrawLine(bottomBox.backTopLeft, topBox.backTopLeft, color);
        Debug.DrawLine(bottomBox.backTopRight, topBox.backTopRight, color);
        Debug.DrawLine(bottomBox.frontTopLeft, topBox.frontTopLeft, color);
        Debug.DrawLine(bottomBox.frontTopRight, topBox.frontTopRight, color);
        Debug.DrawLine(bottomBox.frontBottomLeft, topBox.frontBottomLeft, color);
        Debug.DrawLine(bottomBox.frontBottomRight, topBox.frontBottomRight, color);

        DrawBox(bottomBox, color);
        DrawBox(topBox, color);
    }

    public static void DrawBox(Vector3 origin, Vector3 halfExtents, Quaternion orientation, Color color)
    {
        DrawBox(new Box(origin, halfExtents, orientation), color);
    }
    public static void DrawBox(Box box, Color color)
    {
        Debug.DrawLine(box.frontTopLeft, box.frontTopRight, color);
        Debug.DrawLine(box.frontTopRight, box.frontBottomRight, color);
        Debug.DrawLine(box.frontBottomRight, box.frontBottomLeft, color);
        Debug.DrawLine(box.frontBottomLeft, box.frontTopLeft, color);

        Debug.DrawLine(box.backTopLeft, box.backTopRight, color);
        Debug.DrawLine(box.backTopRight, box.backBottomRight, color);
        Debug.DrawLine(box.backBottomRight, box.backBottomLeft, color);
        Debug.DrawLine(box.backBottomLeft, box.backTopLeft, color);

        Debug.DrawLine(box.frontTopLeft, box.backTopLeft, color);
        Debug.DrawLine(box.frontTopRight, box.backTopRight, color);
        Debug.DrawLine(box.frontBottomRight, box.backBottomRight, color);
        Debug.DrawLine(box.frontBottomLeft, box.backBottomLeft, color);
    }

    public struct Box
    {
        public Vector3 localFrontTopLeft { get; private set; }
        public Vector3 localFrontTopRight { get; private set; }
        public Vector3 localFrontBottomLeft { get; private set; }
        public Vector3 localFrontBottomRight { get; private set; }
        public Vector3 localBackTopLeft { get { return -localFrontBottomRight; } }
        public Vector3 localBackTopRight { get { return -localFrontBottomLeft; } }
        public Vector3 localBackBottomLeft { get { return -localFrontTopRight; } }
        public Vector3 localBackBottomRight { get { return -localFrontTopLeft; } }

        public Vector3 frontTopLeft { get { return localFrontTopLeft + origin; } }
        public Vector3 frontTopRight { get { return localFrontTopRight + origin; } }
        public Vector3 frontBottomLeft { get { return localFrontBottomLeft + origin; } }
        public Vector3 frontBottomRight { get { return localFrontBottomRight + origin; } }
        public Vector3 backTopLeft { get { return localBackTopLeft + origin; } }
        public Vector3 backTopRight { get { return localBackTopRight + origin; } }
        public Vector3 backBottomLeft { get { return localBackBottomLeft + origin; } }
        public Vector3 backBottomRight { get { return localBackBottomRight + origin; } }

        public Vector3 origin { get; private set; }

        public Box(Vector3 origin, Vector3 halfExtents, Quaternion orientation) : this(origin, halfExtents)
        {
            Rotate(orientation);
        }
        public Box(Vector3 origin, Vector3 halfExtents)
        {
            this.localFrontTopLeft = new Vector3(-halfExtents.x, halfExtents.y, -halfExtents.z);
            this.localFrontTopRight = new Vector3(halfExtents.x, halfExtents.y, -halfExtents.z);
            this.localFrontBottomLeft = new Vector3(-halfExtents.x, -halfExtents.y, -halfExtents.z);
            this.localFrontBottomRight = new Vector3(halfExtents.x, -halfExtents.y, -halfExtents.z);

            this.origin = origin;
        }


        public void Rotate(Quaternion orientation)
        {
            localFrontTopLeft = RotatePointAroundPivot(localFrontTopLeft, Vector3.zero, orientation);
            localFrontTopRight = RotatePointAroundPivot(localFrontTopRight, Vector3.zero, orientation);
            localFrontBottomLeft = RotatePointAroundPivot(localFrontBottomLeft, Vector3.zero, orientation);
            localFrontBottomRight = RotatePointAroundPivot(localFrontBottomRight, Vector3.zero, orientation);
        }
    }

    //This should work for all cast types
    static Vector3 CastCenterOnCollision(Vector3 origin, Vector3 direction, float hitInfoDistance)
    {
        return origin + (direction.normalized * hitInfoDistance);
    }

    static Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Quaternion rotation)
    {
        Vector3 direction = point - pivot;
        return pivot + rotation * direction;
    }
}
}
