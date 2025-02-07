using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;

    [Header("Dodge Settings")]
    public float dodgeSpeedMultiplier = 2f;  
    public float dodgeDuration = 0.3f;       
    public float dodgeCooldown = 1f;        
    public float invincibilityDuration = 0.3f; 

    private bool isDodging = false;
    private bool canDodge = true;
    private Rigidbody2D rb;
    private Vector2 movement;

    // For camera clamping
    private Camera mainCamera;
    private Vector2 minBounds;
    private Vector2 maxBounds;
    private float objectWidth;
    private float objectHeight;

    private PlayerInvincibility invincibility;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            objectWidth = sr.bounds.extents.x;
            objectHeight = sr.bounds.extents.y;
        }
        else
        {
            objectWidth = 0.5f;
            objectHeight = 0.5f;
        }
        UpdateCameraBounds();


        invincibility = GetComponent<PlayerInvincibility>();
    }

    void Update()
    {

        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.Space) && canDodge)
        {
            StartCoroutine(PerformDodge());
        }

        UpdateCameraBounds();
    }

    void FixedUpdate()
    {

        Vector2 newPosition = rb.position + movement * moveSpeed * Time.fixedDeltaTime;


        newPosition.x = Mathf.Clamp(newPosition.x, minBounds.x + objectWidth, maxBounds.x - objectWidth);
        newPosition.y = Mathf.Clamp(newPosition.y, minBounds.y + objectHeight, maxBounds.y - objectHeight);

        rb.MovePosition(newPosition);
    }

    IEnumerator PerformDodge()
    {
        isDodging = true;
        canDodge = false;

        float originalSpeed = moveSpeed;
        moveSpeed *= dodgeSpeedMultiplier;

        if (invincibility != null)
        {
            invincibility.StartInvincibility(invincibilityDuration);
        }


        yield return new WaitForSeconds(dodgeDuration);
        moveSpeed = originalSpeed;
        isDodging = false;


        yield return new WaitForSeconds(dodgeCooldown);
        canDodge = true;
    }

    void UpdateCameraBounds()
    {

        Vector3 bottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
        Vector3 topRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane));
        minBounds = new Vector2(bottomLeft.x, bottomLeft.y);
        maxBounds = new Vector2(topRight.x, topRight.y);
    }


    public void ApplyUpgrade(Upgrade upgrade)
    {
        switch (upgrade.type)
        {
            case Upgrade.UpgradeType.DodgeSpeed:
                dodgeSpeedMultiplier *= upgrade.value;
                Debug.Log("New Dodge Speed Multiplier: " + dodgeSpeedMultiplier);
                break;
            case Upgrade.UpgradeType.InvincibilityDuration:
                invincibilityDuration += upgrade.value;
                Debug.Log("New Invincibility Duration: " + invincibilityDuration);
                break;
            case Upgrade.UpgradeType.ExtraHealth:
                PlayerHealth health = GetComponent<PlayerHealth>();
                if (health != null)
                {
                    health.IncreaseMaxHealth((int)upgrade.value);
                    Debug.Log("New Max Health: " + health.maxHealth);
                }
                break;
        }
    }
}




