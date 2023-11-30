using BaseTemplate.Behaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoSingleton<PlayerController>
{
    public GameObject hook;
    public GameObject hookHead;
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
    public float maxDistanceHook;

    float baseAngleHook;
    float raycastDistance = 0f; // Distance du rayo               // Masque de couche (LayerMask) pour la couche "FrontEnvironment"
    bool isObstacleDetect;
    public bool isRight;
    public bool isNoMoveCam;

    public float minFOV;
    public float maxFOV;
    public float lentOrthoSizeSpeed;
    public Cinemachine.CinemachineVirtualCamera virtualCamera;

    public Vector3 checkPointPosition;

    public float flappybirdForce;
    public Sprite flappybirdSprite;
    public float massInFlappyBird;
    public float speedInFlappyBird;
    public float scaleInFlappyBird;
    public float fovFlappyBird;
    public float maxVelocityY;
    public float speedVelocityY;
    public float speedVelocityNegativeY;
    public ParticleSystem flappyBirdParticles;
    public bool isConcerveStateAtDeath;

    Sprite basicSprite;

    bool isFlappyBird;

    RaycastHit2D hitHook;
    RaycastHit2D hiGround;
    Collider2D hiGroundBox;
    Collider2D[] colliderboxResult;
    ContactFilter2D contactFilter2;

    bool isGrounded;
    Vector3 rayOrigin;
    float baseForceRightOnHoldHook;
    float baseForceAddOnGround;
    float baseHookSpeed;
    float baseMinLentOrthoSize;

    float baseScalePlayer;

    

    public void Init()
    {
        checkPointPosition = transform.position;
        distanceJoint.enabled = false;
        baseAngleHook = angleHook;
        baseForceRightOnHoldHook = forceRightOnHoldHook;
        baseForceAddOnGround = forceAddOnGround;
        baseHookSpeed = hookSpeed;
        baseMinLentOrthoSize = minFOV;
        isFirstCheckpoint = true;
        basicSprite = spriteRenderer.sprite;
        colliderboxResult = new Collider2D[1];
        contactFilter2 = new ContactFilter2D();
        contactFilter2.SetLayerMask(isGroundedMask);
        contactFilter2.layerMask = isGroundedMask;
        baseScalePlayer = spriteRenderer.gameObject.transform.localScale.x;
        //TransformPlayerIntoFlappyBird();
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

                    hook.transform.localScale = Vector3.one + Vector3.up * (Mathf.Clamp(hook.transform.localScale.y + hookSpeed * Time.deltaTime, 0, maxDistanceHook));
                    //hook.transform.localScale.Set(hook.transform.localScale.x, hook.transform.localScale.y + hookSpeed * Time.deltaTime, 1);
                    raycastDistance = hook.transform.localScale.y * 0.1f;

                    // Position de départ du rayon, généralement la position de votre objet
                    Vector3 rayDirectionHook = Quaternion.Euler(0, 0, hook.transform.rotation.eulerAngles.z) * Vector2.up;
                    hook.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -angleHook));



                    hitHook = Physics2D.Raycast(rayOrigin, rayDirectionHook, raycastDistance, mask);

                    //hookHead.transform.position = transform.TransformDirection(hookHead.transform.position.x, hook.transform.localScale.y * 0.1f, 0);
                    
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

        if (isRight && rb.velocity.x < -0.1f)
        {
            FlipPlayer();
        }
        else if (!isRight && rb.velocity.x > 0.1f) {
            FlipPlayer();
        }
        //Mathf.Lerp(valeurCourante, valeurCible, vitesseAugmentation * Time.deltaTime);
        if (!isNoMoveCam)
        {
            virtualCamera.m_Lens.FieldOfView = Mathf.Lerp(virtualCamera.m_Lens.FieldOfView,
                                                                Mathf.Clamp((Mathf.Abs(rb.velocity.x) + Mathf.Abs(rb.velocity.y))*4, minFOV, maxFOV),
                                                                lentOrthoSizeSpeed * Time.deltaTime);
        }

        if (Physics2D.OverlapBox(new Vector2(transform.position.x, transform.position.y-1f), Vector2.one * .4f, 0, contactFilter2, colliderboxResult) > 0)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

    }

    bool isMouseTouch;
    float rbY = 0;
    private void Update()
    {
        if (isObstacleDetect)
        {
            Vector3 targetPosition = hitHook.point;

            Vector3 direction = (targetPosition - transform.position);
            direction.z = 0;
            direction.Normalize();
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            hook.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
            hookHead.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
            hookHead.transform.localPosition = Vector3.zero;
        } else
        {
            hookHead.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -angleHook));
            //hookHead.transform.up 
            hookHead.transform.localPosition = hookHead.transform.up * (hook.transform.localScale.y * 0.1f);
            //hookHead.transform.localPosition =  new Vector3(0, hook.transform.localScale.y * 0.1f, 0);
        }
            
        if (isFlappyBird)
        {
            if (Input.GetMouseButton(0))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    flappyBirdParticles.Play();
                }
                rbY = Mathf.Lerp(rbY, maxVelocityY, speedVelocityY * Time.deltaTime);
                rb.velocity = Vector2.right * speedInFlappyBird + Vector2.up * rbY;
            }
            else if (!isGrounded)
            {
                rbY = Mathf.Lerp(rbY, -maxVelocityY, speedVelocityNegativeY * Time.deltaTime);
                rb.velocity = Vector2.right * speedInFlappyBird + Vector2.up * rbY;
            } else
                rbY = 0;


            if (Input.GetMouseButtonUp(0))
            {
                flappyBirdParticles.Stop();
            }
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


        hook.transform.localScale = Vector3.up * distanceJoint.distance * 10 + Vector3.right + Vector3.forward;


        distanceJoint.enabled = true;

        hookHead.transform.position = Vector3.zero;
        hookHead.transform.parent = staticObject.transform;
    }

    public void ResetPlayer()
    {
        flappyBirdParticles.Stop();
        transform.position = checkPointPosition;
        spriteRenderer.flipX = false;
        angleHook = baseAngleHook;
        hookSpeed = baseHookSpeed;
        minFOV = baseMinLentOrthoSize;
        forceRightOnHoldHook = baseForceRightOnHoldHook;
        rb.velocity = Vector2.zero;
        isRight = true;
        forceAddOnGround = baseForceAddOnGround;
        ResetHook();
        PowerupManager.Instance.reAddPowerUp();
        PowerupManager.Instance.StopAll();
        UiManager.Instance.Reset();
        if (isFirstCheckpoint)
        {
            TimerManager.Instance.ResetTime();
        }
        StartCoroutine(WaitAndResetPlayer());

        if (!isConcerveStateAtDeath)
        {
            spriteRenderer.sprite = basicSprite;
            if (isFlappyBird)
            {
                isNoMoveCam = false;
                isFlappyBird = false;
            }
            spriteRenderer.transform.localScale = Vector3.one * baseScalePlayer;
            rb.gravityScale = 1;
        }
    }

    IEnumerator WaitAndResetPlayer()
    {
        yield return new WaitForEndOfFrame();
        spriteRenderer.flipX = false;
        angleHook = baseAngleHook;
        hookSpeed = baseHookSpeed;
        minFOV = baseMinLentOrthoSize;
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
        //hook.transform.localPosition = new Vector3(-hook.transform.localPosition.x, hook.transform.localPosition.y, 0);
    }

    public void ResetHook()
    {
        isObstacleDetect = false;
        raycastDistance = 0;
        hook.transform.localScale = Vector3.one + Vector3.up * 0;
        distanceJoint.enabled = false;
        hookHead.transform.parent = transform;
    }

    public void ResetHookStats()
    {
        forceRightOnHoldHook = baseForceRightOnHoldHook;
        minFOV = baseMinLentOrthoSize;
        hookSpeed = baseHookSpeed;
    }
    public void TransformPlayerIntoFlappyBird()
    {
        ResetHook();
        isFlappyBird = true;
        spriteRenderer.sprite = flappybirdSprite;
        rb.gravityScale = massInFlappyBird;
        //minFOV = minFOVFlappyBird;
        virtualCamera.m_Lens.FieldOfView = fovFlappyBird;
        spriteRenderer.transform.localScale = Vector3.one * scaleInFlappyBird;
        isNoMoveCam = true;
        rb.velocity = Vector2.right * speedInFlappyBird + Vector2.up * rb.velocity.y;
    }

    public void ResetTransformation()
    {
        spriteRenderer.sprite = basicSprite;
        isFlappyBird = false;
        spriteRenderer.transform.localScale = Vector3.one * baseScalePlayer;
        rb.gravityScale = 1;
        isNoMoveCam = false;
        flappyBirdParticles.Stop();
    }

}