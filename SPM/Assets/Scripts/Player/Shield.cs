using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public Player Player;
    public LayerMask ProjectileMask;
    public Camera playerCamera;
    public float FacingOffset;
    public int ShieldHealth = 500;
    public int CurrentHealth;
    public GameObject ShieldObject;
    public bool IsBlocking;
    public Animator Anim;
    private BoxCollider boxCollider;
    private Vector3 shieldPos;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
        EventHandler.Instance.Register(EventHandler.EventType.ShieldBlock, PlayShieldBlockClip);
        //shieldPos = new Vector3(0.31f, 0.45f, -0.43f);
        shieldPos = new Vector3(0.2f, 0.3f, -0.5f);
        boxCollider = GetComponentInChildren<BoxCollider>();
        CurrentHealth = ShieldHealth;
        IsBlocking = false;
        //Player.Transition<ShieldState>();
    }
    void OnDisable()
    {
        Player.SpeedModifier = 1f;
    }
    // Update is called once per frame
    void Update()
    {
        UpdateTransformation();
        if (InputManager.Instance.GetkeyDown(KeybindManager.Instance.BlockAndAim, InputManager.ControllMode.Play))
        {
            if (IsBlocking  == false && Player.GetComponent<Weapon>().Sword.GetComponent<Sword>().IsBladeStorming == false)
            {
                Block();
                Player.SpeedModifier = 0.85f;
            }
            else
            {
                GoToIdle();
            }
        }
        if (InputManager.Instance.GetkeyUp(KeybindManager.Instance.BlockAndAim, InputManager.ControllMode.Play))
        {
            GoToIdle();
            Player.SpeedModifier = 1f;
        }
        //  ExtDebug.DrawBoxCastBox(transform.position, Quaternion.Euler(0, 90, 0) * boxCollider.size / 2, transform.rotation, transform.forward, 0.5f, Color.white);
    }

    public void UpdateTransformation()
    {

        Vector3 direction = Vector3.ProjectOnPlane(playerCamera.transform.forward, Vector3.up);
       // Vector3 direction = playerCamera.transform.forward;
        transform.rotation = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 90, 0);
        Vector3 update = transform.rotation * shieldPos.normalized;
        transform.position = update * shieldPos.magnitude + Player.transform.position;
    }
    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        if (CurrentHealth <= 0 && ShieldObject != null)
        {
            Destroy(ShieldObject);
        }
    }

    public void GoToIdle()
    {
        Anim.SetBool("IsBlocking", false);
        IsBlocking = false;
    }

    private void PlayShieldBlockClip(BaseEventInfo e)
    {
        var audio = e as AudioEventInfo;
        if (audio != null)
        {
            audioSource.clip = audio.audioClip;
            if (!audioSource.isPlaying)
                audioSource.Play();
        }
    }
    void Block()
    {
        Anim.SetBool("IsBlocking", true);
        IsBlocking = true;
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
