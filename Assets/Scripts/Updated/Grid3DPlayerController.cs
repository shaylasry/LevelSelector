using System;
using UnityEngine;
using UnityEngine.Serialization;


public class Grid3DPlayerController : MonoBehaviour
{
    public static event Action PlayerDidDie;

    public bool IsWalking { get; private set; }
    public bool IsAlive { get; private set; } = true;
    public Vector3[,] GridData;

    [FormerlySerializedAs("smokeParticlesystem")] [SerializeField]
    private ParticleSystem smokeParticleSystem;

    [SerializeField] private AudioClip grassPopAudioClip;
    [SerializeField] private AudioClip explosionAudioClip;
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private GameObject explosionPrefab;

    public Vector3 Direction { get; private set; } = Vector3.right;
    private Vector2 gridPosition = new Vector2(0, 0);

    private float rotationDuration = .12f;
    private float rotationTime;

    private AudioSource audioSource;
    private float audioPitch = 1f;
    private float grassPopCooldown;
    private bool shouldSmoke;
    private bool didWin;
    private Rigidbody rigidbody1;
    private Vector2 nextGridPosition;
    private float walkDuration = .15f;
    private float walkTimer;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(GridData[(int)nextGridPosition.x, (int)nextGridPosition.y], Vector3.one * .5f);
    }

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    private void Start()
    {
        rigidbody1 = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        HandleSmoke();
        HandlePitch();
    }

    #region Event Handling

    private void SubscribeEvents()
    {
        WinConditionController.PlayerDidWin += OnPlayerDidWin;
    }

    private void UnsubscribeEvents()
    {
        WinConditionController.PlayerDidWin -= OnPlayerDidWin;
    }

    private void OnPlayerDidWin()
    {
        didWin = true;
    }

    #endregion

    #region Configuration

    public void HandleDrive()
    {
        if (!IsAlive) return;

        if (IsWalking)
        {
            var nextWorldPos = GridData[(int)nextGridPosition.x, (int)nextGridPosition.y];
            var currentWorldPos = GridData[(int)gridPosition.x, (int)gridPosition.y];
            if (walkTimer <= walkDuration)
            {
                rigidbody1.MovePosition(Vector3.LerpUnclamped(currentWorldPos, nextWorldPos, walkTimer / walkDuration));
                walkTimer += Time.deltaTime;
            }
            else
            {
                gridPosition = nextGridPosition;
                IsWalking = false;
            }
        }
    }

    public void HandleMovement()
    {
        if (!IsAlive || GridData == null || GridData.Length == 0 || IsWalking) return;

        Vector2 targetGridPosition = new Vector2(
            gridPosition.x + Direction.x,
            gridPosition.y + Direction.z);

        if (IsTargetGridPositionOutOfBounds(targetGridPosition)) return;

        nextGridPosition = targetGridPosition;

        walkTimer = 0f;
        IsWalking = true;
    }

    public void HandleRotation()
    {
        if (!IsAlive) return;

        var targetRotation = Quaternion.Slerp(
            rigidbody1.rotation,
            Quaternion.LookRotation(Direction, Vector3.up),
            Time.deltaTime * 20f
        );
        
        rigidbody1.MoveRotation(targetRotation);
    }

    public void ChangeDirection(TapSideDetector.ScreenSide tappedSide)
    {
        Direction = GetNewDirection(tappedSide, Direction);
    }

    public void ChangeDirection(KeyCode keyCode)
    {
        Direction = GetNewDirection(keyCode, Direction);
    }

    public void ChangeDirection(Vector3 newDirection)
    {
        Direction = newDirection;
    }

    #endregion

    #region Collision Handling

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponentInParent<MoleController>() != null || other.gameObject.CompareTag("Obstacle"))
        {
            HandleMoleCollision();
        }
        else if (other.gameObject.CompareTag("GrassBlade"))
        {
            HandleGrassBladeCollision();
        }
    }

    private void HandleMoleCollision()
    {
        if (didWin) return;

        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        CinemachineShake.Instance.ShakeCamera(5f, 1f);

        GameObject explosionSource = new GameObject("Explosion Source");
        AudioSource explosionAudioSource = explosionSource.AddComponent<AudioSource>();
        explosionAudioSource.PlayOneShot(explosionAudioClip);
        Destroy(explosionSource, 10f);

        IsAlive = false;
        Destroy(gameObject);

        PlayerDidDie?.Invoke();
    }

    private void HandleGrassBladeCollision()
    {
        audioPitch += .01f;
        if (grassPopCooldown <= 0)
        {
            grassPopCooldown = .05f;
            audioSource.PlayOneShot(grassPopAudioClip);
        }
    }

    #endregion

    #region Private Implementation Details

    private bool IsTargetGridPositionOutOfBounds(Vector2 targetGridPosition)
    {
        return targetGridPosition.x >= GridData.GetLength(0) || targetGridPosition.x < 0 ||
               targetGridPosition.y >= GridData.GetLength(1) || targetGridPosition.y < 0;
    }

    private void HandleSmoke()
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            smokeParticleSystem.Play();
        }
    }

    private void HandlePitch()
    {
        if (grassPopCooldown > 0)
        {
            grassPopCooldown -= Time.deltaTime;
            grassPopCooldown = Mathf.Max(0, grassPopCooldown);
        }

        if (audioPitch >= 1f)
        {
            audioPitch -= 1f * Time.deltaTime;
            audioPitch = Mathf.Max(1f, audioPitch);
        }

        audioSource.pitch = audioPitch;
    }

    private Vector3 GetNewDirection(KeyCode keyCode, Vector3 currentDirection)
    {
        Vector3 newDirection = Direction;

        switch (keyCode)
        {
            case KeyCode.LeftArrow:
                newDirection = new Vector3(-1, 0, 0);
                break;

            case KeyCode.RightArrow:
                newDirection = new Vector3(1, 0, 0);
                break;

            case KeyCode.UpArrow:
                newDirection = new Vector3(0, 0, 1);
                break;

            case KeyCode.DownArrow:
                newDirection = new Vector3(0, 0, -1);
                break;
        }

        return newDirection;
    }

    private Vector3 GetNewDirection(TapSideDetector.ScreenSide tappedSide, Vector3 currentDirection)
    {
        Vector3 newDirection;

        if (tappedSide == TapSideDetector.ScreenSide.Left)
        {
            // Rotate current direction 90 degrees counter-clockwise, or clockwise if flipped (on the XZ plane)
            newDirection = new Vector3(-currentDirection.z, 0, currentDirection.x);
        }
        else
        {
            // Rotate current direction 90 degrees clockwise, or counter-clockwise if flipped (on the XZ plane)
            newDirection = new Vector3(currentDirection.z, 0, -currentDirection.x);
        }

        return newDirection;
    }

    #endregion
}