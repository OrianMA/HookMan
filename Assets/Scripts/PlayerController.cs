using BaseTemplate.Behaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoSingleton<PlayerController>
{
    public GameObject hook;
    public GameObject staticObject;
    public SpriteRenderer spriteRenderer;
    public DistanceJoint2D distanceJoint;
    public float hookSpeed;
    public float angleHook;
    public LayerMask mask;
    public LayerMask stopHookMask;
    public LayerMask isGroundedMask;
    public Rigidbody2D rb;
    public float forceRightOnHoldHook;
    public float forceAddOnGround;

    public float velocityNeedOnGround;
    public bool isFirstCheckpoint;


    float baseAngleHook;
    float raycastDistance = 0f; // Distance du rayo               // Masque de couche (LayerMask) pour la couche "FrontEnvironment"
    bool isObstacleDetect;
    public bool isRight;

    public float minLentOrthoSize;
    public float maxLentOrthoSize;
    public float lentOrthoSizeSpeed;
    public Cinemachine.CinemachineVirtualCamera virtualCamera;

    public Vector3 checkPointPosition;

    public float flappybirdForce;
    public Sprite flappybirdSprite;
    public float massInFlappyBird;
    public float speedInFlappyBird;

    Sprite basicSprite;

    bool isFlappyBird;

    RaycastHit2D hitHook;
    RaycastHit2D hiGround;
    bool isGrounded;
    Vector3 rayOrigin;
    float baseForceRightOnHoldHook;
    float baseForceAddOnGround;
    float baseHookSpeed;
    float baseMinLentOrthoSize;
    

    public void Init()
    {
        checkPointPosition = transform.position;
        distanceJoint.enabled = false;
        baseAngleHook = angleHook;
        baseForceRightOnHoldHook = forceRightOnHoldHook;
        baseForceAddOnGround = forceAddOnGround;
        baseHookSpeed = hookSpeed;
        baseMinLentOrthoSize = minLentOrthoSize;
        isFirstCheckpoint = true;
        basicSprite = spriteRenderer.sprite;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rayOrigin = transform.position;
        if (Input.GetMouseButton(0))
        {
            if (!isFlappyBird)
            {
                if (!isObstacleDetect)
                {

                    hook.transform.localScale = Vector3.one + Vector3.up * (hook.transform.localScale.y + hookSpeed * Time.deltaTime);
                    //hook.transform.localScale.Set(hook.transform.localScale.x, hook.transform.localScale.y + hookSpeed * Time.deltaTime, 1);
                    raycastDistance = hook.transform.localScale.y * 0.1f;

                    // Position de départ du rayon, généralement la position de votre objet
                    Vector3 rayDirectionHook = Quaternion.Euler(0, 0, hook.transform.rotation.eulerAngles.z) * Vector2.up;
                    hook.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -angleHook));


                    hitHook = Physics2D.Raycast(rayOrigin, rayDirectionHook, raycastDistance, mask);

                    if (hitHook.collider != null && hitHook.collider.gameObject.GetComponent<Rigidbody2D>() != null)
                    {
                        HookTouch(hitHook.point);
                    } else if (Physics2D.Raycast(rayOrigin, rayDirectionHook, raycastDistance, stopHookMask).collider != null)
                    {
                        ResetHook();
                    }
                    else
                    {
                        Debug.DrawRay(rayOrigin, rayDirectionHook * raycastDistance, Color.white);
                    }

                } else
                {
                    if (rb.velocity.y < 0)
                    {
                        rb.velocity = rb.velocity.normalized * (rb.velocity.magnitude * (forceRightOnHoldHook * Time.fixedDeltaTime));
                    }
                }
            } 
        } else if (hook.transform.localScale.y > 0)
        {
            //hook.transform.localScale -= Vector3.up * (hook.transform.localScale.y + (hookSpeed * Time.deltaTime));
            ResetHook();
        }


        if (isFlappyBird)
        {
            //rb.AddForce(Vector2.up * flappybirdForce);
            
            //rb.velocity = Vector2.right * speedInFlappyBird + Vector2.up * rb.velocity.y;
        }




        if (isRight && rb.velocity.x < 0)
        {
            FlipPlayer();
        }
        else if (!isRight && rb.velocity.x > 0) {
            FlipPlayer();
        }
        //Mathf.Lerp(valeurCourante, valeurCible, vitesseAugmentation * Time.deltaTime);
        virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(virtualCamera.m_Lens.OrthographicSize,
                                                            Mathf.Clamp((Mathf.Abs(rb.velocity.x) + Mathf.Abs(rb.velocity.y)) / 2, minLentOrthoSize, maxLentOrthoSize),
                                                            lentOrthoSizeSpeed * Time.deltaTime);


        Vector3 rayDirectionGrounded = Vector2.down;


        hiGround = Physics2D.Raycast(rayOrigin, rayDirectionGrounded, 1f, isGroundedMask);

        if (hiGround.collider != null)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

    }

    private void Update()
    {
        if(isObstacleDetect)
        {
            Vector3 targetPosition = hitHook.point;

            Vector3 direction = (targetPosition - transform.position);
            direction.z = 0;
            direction.Normalize();
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            hook.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
        }

        if (isFlappyBird && Input.GetMouseButton(0))
        {
            rb.velocity = (Vector2.right * rb.velocity.x) + Vector2.up * flappybirdForce;
        }
    }

    public void HookTouch(Vector2 pos)
    {
        isObstacleDetect = true;        

        Vector2 connectPoint = pos - new Vector2(pos.x, pos.y);
        connectPoint.x = connectPoint.x / 1;
        connectPoint.y = connectPoint.y / 1;

        distanceJoint.connectedAnchor = connectPoint;

        staticObject.transform.position = pos;

        if (isGrounded)
        {
            if (rb.velocity.x > velocityNeedOnGround)
            {
                transform.position = transform.position + Vector3.up * .5f;
                distanceJoint.distance = Vector2.Distance(transform.position, pos);
            } else
            {
                transform.position = transform.position + Vector3.up * .75f;
                distanceJoint.distance = Vector2.Distance(transform.position, pos);
                rb.AddForce(Vector3.right * forceAddOnGround);
            }
        }
        else
            distanceJoint.distance = Vector2.Distance(transform.position, pos);

        distanceJoint.enabled = true;
    }

    public void ResetPlayer()
    {
        
        transform.position = checkPointPosition;
        spriteRenderer.flipX = false;
        angleHook = baseAngleHook;
        hookSpeed = baseHookSpeed;
        minLentOrthoSize = baseMinLentOrthoSize;
        forceRightOnHoldHook = baseForceRightOnHoldHook;
        rb.velocity = Vector2.zero;
        isRight = true;
        forceAddOnGround = baseForceAddOnGround;
        ResetHook();
        PowerupManager.Instance.reAddPowerUp();
        PowerupManager.Instance.StopAll();

        if (isFirstCheckpoint)
        {
            TimerManager.Instance.ResetTime();
        }
        StartCoroutine(WaitAndResetPlayer());

        spriteRenderer.sprite = basicSprite;
        isFlappyBird = false;

        rb.gravityScale = 1;
    }

    IEnumerator WaitAndResetPlayer()
    {
        yield return new WaitForEndOfFrame();
        spriteRenderer.flipX = false;
        angleHook = baseAngleHook;
        hookSpeed = baseHookSpeed;
        minLentOrthoSize = baseMinLentOrthoSize;
        forceRightOnHoldHook = baseForceRightOnHoldHook;
        rb.velocity = Vector2.zero;
        isRight = true;
        forceAddOnGround = baseForceAddOnGround;
        ResetHook();
    }

    public void FlipPlayer()
    {
        isRight = !isRight;
        angleHook *= -1;
        spriteRenderer.flipX = !spriteRenderer.flipX;
        forceAddOnGround *= -1;
    }

    public void ResetHook()
    {
        isObstacleDetect = false;
        raycastDistance = 0;
        hook.transform.localScale = Vector3.one + Vector3.up * 0;
        distanceJoint.enabled = false;
    }

    public void ResetHookStats()
    {
        forceRightOnHoldHook = baseForceRightOnHoldHook;
        minLentOrthoSize = baseMinLentOrthoSize;
    }


    public void TransformPlayerIntoFlappyBird()
    {
        isFlappyBird = true;
        spriteRenderer.sprite = flappybirdSprite;
        rb.gravityScale = massInFlappyBird;
        minLentOrthoSize = 16;
        rb.velocity = Vector2.right * speedInFlappyBird + Vector2.up * rb.velocity.y;
    }

}

