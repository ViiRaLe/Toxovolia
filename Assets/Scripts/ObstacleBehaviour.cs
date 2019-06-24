using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObstacleBehaviour : MonoBehaviour
{
    Animator ani;
    public enum Identity { Wood, Rock, Iron, Target, MinTarget };

    [Header("Obstacle Identity")]
    public Identity identity;


    [Header("Modality")]

    [Tooltip("If the obstacle is a moving one. Can't be intermittable too.")]
    public bool isMobile = false;
    [Tooltip("If the obstacle is a disappering one. Can't be movable too.")]
    public bool isIntermittent = false;

    [Header("Values")]

    [Header("Mobile")]
    [Tooltip("Horizontal Speed. Y speed has to be 0.")]
    public float MobileSpeedX = 0f;
    [Tooltip("Movement space. Starting from the actual position of the obstacle.")]
    public float movementOffsetRight = 0f;
    public float movementOffsetLeft = 0f;

    [Header("Intermittent")]
    [Tooltip("Time in seconds of deactivation/reactivation.")]
    public float IntermissionTime = 0;

    [Header("Target")]
    [Tooltip("Points that this target gives when hitted not on the right spot. ONLY MINTARGET OBJECTS!")]
    public int minPoints;

    private int dir = 1;
    [HideInInspector]
    public Rigidbody2D body;
    BoxCollider2D coll;
    Renderer rend;

    [HideInInspector]
    public Vector3 contactVector;

    Vector2 contactDone;
    public static Animator bossAnimator;

    private float startXPos;


    private void Start()
    {

        switch (identity)
        {
            case Identity.Target:
                {
                    gameObject.tag = "Target";
                    break;
                }
            case Identity.MinTarget:
                {
                    gameObject.tag = "MinTarget";
                    break;
                }
            case Identity.Wood:
            case Identity.Rock:
            case Identity.Iron:
                {
                    gameObject.tag = "Obstacle";
                    break;
                }
        }

        body = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        rend = GetComponent<Renderer>();
        ani = GetComponent<Animator>();
        startXPos = transform.position.x;

        if (gameObject.tag != "MinTarget")
        {
            minPoints = 0;
        }

        if (isMobile && isIntermittent)
        {
            Debug.LogWarning("OBSTACLE" + this + "CANNOT BE BOTH MOBILE AND INTERMITTENT");
            return;
        }

        if (isIntermittent || gameObject.tag == "Target")
        {
            StartCoroutine(SetIntermission(IntermissionTime));
        }
    }


    private void Update()
    {
        if (rend != null && rend.isVisible && gameObject.tag == "Target")
        {
            bossAnimator = gameObject.GetComponentInParent<Animator>();
            ResetCollider();
        }

    }

    private void FixedUpdate()
    {
        if (isMobile)
        {
            SetDir(SetMove(GetDir()));
        }
    }

    IEnumerator SetIntermission(float seconds)
    {
        if (ani != null)
        {
            if (ani.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                ani.SetTrigger("Activate");
                if (coll != null) coll.enabled = true;
            }
            else
            {
                ani.SetTrigger("Deactivate");
                if (coll != null) coll.enabled = false;
            }
        }
        yield return new WaitForSeconds(seconds);
        StartCoroutine(SetIntermission(IntermissionTime));
    }

    public int GetDir()
    {
        return dir;
    }
    public void SetDir(int newDir)
    {
        dir = newDir;
    }

    public int SetMove(int dir) //movimento iniziale freccia
    {

        if (MobileSpeedX != 0) //apply velocity X
        {
            float applyVelX = MobileSpeedX * dir;
            body.velocity = new Vector2(applyVelX, 0);
        }


        //handle direction
        if (MobileSpeedX != 0)
        {
            if ((dir == 1 && transform.position.x >= startXPos + movementOffsetRight) || (dir == -1 && transform.position.x <= startXPos - movementOffsetLeft))
            {
                dir = -dir;
                transform.Rotate(new Vector3(0, 180, 0));

            }
        }

        return dir;
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool isHerrow = collision.gameObject.tag == "Herrow";
        if (isHerrow)
        {

            contactDone = collision.transform.position;

            GetComponentInParent<AudioSource>().Play();
            if (gameObject.tag == "Target")
            {
                bossAnimator.SetTrigger("Hit");
                
                ScoreSystem.AddScore(contactDone, GameRules.Instance.currentPlayer, 0, minPoints);
            }
            else if (gameObject.tag == "MinTarget")
            {
                
                ScoreSystem.AddScore(contactDone, GameRules.Instance.currentPlayer, 1, minPoints);
            }
            else
            {
                if (isMobile)
                {
                    MobileSpeedX = 0;
                    movementOffsetRight = 0;
                    movementOffsetLeft = 0;
                    body.velocity = Vector3.zero;
                    GetComponent<Animator>().SetTrigger("Collision");
                }
                ScoreSystem.Missed(contactDone, GameRules.Instance.currentPlayer);
            }

            GetComponent<Collider2D>().enabled = false;

        }

    }

    void ResetCollider()
    {
        AnimationCurve basePoints = GameRules.Instance.basePoints;
        CapsuleCollider2D collider = gameObject.GetComponent<CapsuleCollider2D>();

        // float top = collider.offset.y + (collider.size.y / 2f);
        float btm = collider.offset.y - (collider.size.y / 2f);
        float left = collider.offset.x - (collider.size.x / 2f);
        float right = collider.offset.x + (collider.size.x / 2f);

        // Vector3 topLeft = transform.TransformPoint(new Vector3(left, top, 0f));
        // Vector3 topRight = transform.TransformPoint(new Vector3(right, top, 0f));
        Vector3 btmLeft = transform.TransformPoint(new Vector3(left, btm, 0f));
        Vector3 btmRight = transform.TransformPoint(new Vector3(right, btm, 0f));
        Vector3 btmCenter = transform.TransformPoint(new Vector3(collider.offset.x, 0f, 0f));



        Keyframe[] keys = basePoints.keys; // Get a copy of the array
        keys[0].time = btmLeft.x;
        keys[1].time = btmCenter.x;
        keys[2].time = btmRight.x;
        basePoints.keys = keys; // assign the array back to the property

        GameRules.Instance.basePoints = basePoints;
    }


}
