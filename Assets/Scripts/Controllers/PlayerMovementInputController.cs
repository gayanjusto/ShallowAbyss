using Assets.Scripts.Managers;
using UnityEngine;

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

    public SpriteRenderer spriteRenderer;
    public PlayerLifeManager playerLifeManager;

    float h_value;
    float v_value;

    public float mov_buff_value;

    ParticleSystem.VelocityOverLifetimeModule bubblesVelocity;

    private void Start()
    {
        bubblesVelocity = bubblesParticle.velocityOverLifetime;
    }

    private void FixedUpdate()
    {
        MovementService.BoostObject(h_value, v_value, rigidBody, maxSpeed);
        h_value = 0;
        v_value = 0;
    }

    public void MoveUp()
    {
        RemoveVelocity();

        v_value = 1 * mov_buff_value;

        bubblesParticleGameObject.transform.localPosition = new Vector3(0, -1, 1);
        bubblesVelocity.y = -3f;
        bubblesVelocity.x = 0;
        bubblesParticle.Play();
    }

    public void MoveDown()
    {
        RemoveVelocity();

        v_value = -1 * mov_buff_value;
      
        bubblesParticleGameObject.transform.localPosition = new Vector3(0, 1, 1);
        bubblesVelocity.y = 3f;
        bubblesVelocity.x = 0;
        bubblesParticle.Play();
    }

    public void MoveLeft()
    {
        short directionValue = -1;
        SetDashParticleDirection(directionValue * -1, directionValue);

        RemoveVelocity();

        h_value = directionValue * mov_buff_value;
  

        bubblesParticleGameObject.transform.localPosition = new Vector3(1, 0, 1);
        bubblesVelocity.y = 0;
        bubblesVelocity.x = 3f;
        bubblesParticle.Play();

        if (!spriteRenderer.flipX)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }
    }

    public void MoveRight()
    {
        short directionValue = 1;
        SetDashParticleDirection(directionValue * -1, directionValue);

        RemoveVelocity();
        h_value = directionValue * mov_buff_value;
   

        bubblesParticleGameObject.transform.localPosition = new Vector3(-1, 0, 1);
        bubblesVelocity.y = 0;
        bubblesVelocity.x = -3f;
        bubblesParticle.Play();

        if (spriteRenderer.flipX)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }
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
 
}
