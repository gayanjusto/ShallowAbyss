using Assets.Scripts.Managers;
using Assets.Scripts.Managers.Settings;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovementInputController : MonoBehaviour
{
    public Rigidbody2D rigidBody;

    public float maxSpeed;

    public float h_offset;
    public float v_offset;
    public float delayTimeDash;
    public float dashCoolDownTime;
    public short amountInputToDash;
    public bool isDashing;
    public float dashDuration;
    public float dashMovementValue;

    public GameObject bubblesParticleGameObject;
    public GameObject dashParticleGameObject;

    public ParticleSystem bubblesParticle;

    //public SpriteRenderer spriteRenderer;
    //public SpriteRenderer propellerRenderer;
    public PlayerSpritesManager playerSpritesManager;

    public PlayerLifeManager playerLifeManager;
    public UserInputManager userInputManager;
    public AudioSource swimAudio;

    float h_value;
    float v_value;
    float h_debuff;
    float v_debuff;


    public float mov_boost_value;

    ParticleSystem.VelocityOverLifetimeModule bubblesVelocity;

    private void Start()
    {
        playerSpritesManager = GetComponent<PlayerSpritesManager>();
        bubblesVelocity = bubblesParticle.velocityOverLifetime;
    }

    private void FixedUpdate()
    {
        MovementService.BoostObject(h_value + h_debuff, v_value + v_debuff, rigidBody, maxSpeed);

        h_value = 0;
        v_value = 0;
    }

    void ChangeDashParticlePositionRotation(Vector2 currentMovement)
    {
        if (dashParticleGameObject.active)
        {
            Vector3 posRight = new Vector3(-1, 0, 1);
            Vector3 posLeft = new Vector3(1, 0, 1);
            Quaternion angle_90 = Quaternion.Euler(0, 0, 90);
            Quaternion angle_180 = Quaternion.Euler(0, 0, 180);
            Quaternion angle_0 = Quaternion.Euler(0, 0, 0);



            Vector3 leftScale = new Vector3(1, -1, 1);
            Vector3 rightScale = new Vector3(1, 1, 1);

            float dirOffset = .5f;

            //is moving only right
            if (currentMovement.x > dirOffset && (currentMovement.y > -dirOffset && currentMovement.y < dirOffset))
            {
                dashParticleGameObject.transform.localPosition = posRight;
                dashParticleGameObject.transform.localRotation = angle_90;
                return;
            }

            //is moving only left
            if (currentMovement.x < -dirOffset && (currentMovement.y > -dirOffset && currentMovement.y < dirOffset))
            {
                dashParticleGameObject.transform.localPosition = posLeft;
                dashParticleGameObject.transform.localRotation = angle_90;
                return;
            }

            //is moving only up
            if ((currentMovement.x > -dirOffset && currentMovement.x < dirOffset) && currentMovement.y > dirOffset)
            {
                dashParticleGameObject.transform.localRotation = angle_180;
                return;
            }

            //is moving only right
            if ((currentMovement.x > -dirOffset && currentMovement.x < dirOffset) && currentMovement.y < dirOffset)
            {
                dashParticleGameObject.transform.localRotation = angle_0;
                return;
            }
        }
    }

    void ChangeBubblesParticlesPosition(Vector2 currentMovement)
    {
        //if isn't moving, disable particles
        if (currentMovement.x == 0 && currentMovement.y == 0)
        {
            bubblesParticleGameObject.SetActive(false);
            return;
        }
        else
        {
            bubblesParticleGameObject.SetActive(true);
        }

        Vector3 bubblesPos = bubblesParticleGameObject.transform.localPosition;
        float dashParticleRotation = 0;
        //is moving to right?
        if (currentMovement.x > .5f)
        {
            bubblesParticleGameObject.transform.localPosition = new Vector3(-1, bubblesParticleGameObject.transform.localPosition.y, bubblesParticleGameObject.transform.localPosition.z);
        }
        else if (currentMovement.x < -.5f)
        {
            bubblesParticleGameObject.transform.localPosition = new Vector3(1, bubblesParticleGameObject.transform.localPosition.y, bubblesParticleGameObject.transform.localPosition.z);
        }
        else
        {
            bubblesParticleGameObject.transform.localPosition = new Vector3(0, bubblesParticleGameObject.transform.localPosition.y, bubblesParticleGameObject.transform.localPosition.z);
        }

        //is moving up?
        if (currentMovement.y > .5f)
        {
            bubblesParticleGameObject.transform.localPosition = new Vector3(bubblesParticleGameObject.transform.localPosition.x, -1, bubblesParticleGameObject.transform.localPosition.z);
        }
        else if (currentMovement.y < -.5f)
        {
            bubblesParticleGameObject.transform.localPosition = new Vector3(bubblesParticleGameObject.transform.localPosition.x, 1, bubblesParticleGameObject.transform.localPosition.z);

        }
        else
        {
            bubblesParticleGameObject.transform.localPosition = new Vector3(bubblesParticleGameObject.transform.localPosition.x, 0, bubblesParticleGameObject.transform.localPosition.z);
        }
    }

    public void MoveUp()
    {
        swimAudio.Play();

        RemoveVelocity();

        v_value = 1 * mov_boost_value;

        SetBubblesPosition(new Vector3(0, -1, 1), -3, 0);

        ChangeDashParticlePositionRotation(new Vector2(h_value, v_value));

        bubblesParticle.Play();
    }

    public void MoveDown()
    {
        swimAudio.Play();

        RemoveVelocity();

        v_value = -1 * mov_boost_value;

        SetBubblesPosition(new Vector3(0, 1, 1), 3, 0);

        ChangeDashParticlePositionRotation(new Vector2(h_value, v_value));

        bubblesParticle.Play();
    }

    public void MoveLeft()
    {
        swimAudio.Play();

        short directionValue = -1;
        SetDashParticleDirection(directionValue * -1, directionValue);

        RemoveVelocity();

        h_value = directionValue * mov_boost_value;

        SetBubblesPosition(new Vector3(1, 0, 1), 0, 3);

        ChangeDashParticlePositionRotation(new Vector2(h_value, v_value));

        bubblesParticle.Play();

        //Is moving right
        if (!playerSpritesManager.IsMovingLeft())
        {
            playerSpritesManager.FlipSprites_X();
        }
    }

    public void MoveRight()
    {
        swimAudio.Play();

        short directionValue = 1;
        SetDashParticleDirection(directionValue * -1, directionValue);

        RemoveVelocity();
        h_value = directionValue * mov_boost_value;

        SetBubblesPosition(new Vector3(-1, 0, 1), 0, -3);

        ChangeDashParticlePositionRotation(new Vector2(h_value, v_value));

        bubblesParticle.Play();

        if (playerSpritesManager.IsMovingLeft())
        {
            playerSpritesManager.FlipSprites_X();
        }
    }

    void SetBubblesPosition(Vector3 position, float vel_y, float vel_x)
    {
        bubblesParticleGameObject.transform.localPosition = position;
        bubblesVelocity.y = vel_y;
        bubblesVelocity.x = vel_x;
    }

    void RemoveVelocity()
    {
        this.rigidBody.velocity = Vector2.zero;
        this.rigidBody.angularVelocity = 0;
    }

    void SetDashParticleDirection(float direction_X, float scale_Y)
    {
        dashParticleGameObject.transform.localPosition = new Vector3(direction_X, dashParticleGameObject.transform.localPosition.y);
        dashParticleGameObject.transform.localScale = new Vector3(dashParticleGameObject.transform.localScale.x, scale_Y, 1);
    }


    public void SetH_Debuff(float value)
    {
        this.h_debuff = value;
    }

    public void SetV_Debuff(float value)
    {
        this.v_debuff = value;
    }
}
