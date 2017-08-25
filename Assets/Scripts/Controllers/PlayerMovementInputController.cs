using UnityEngine;
using UnityEngine.UI;

public class PlayerMovementInputController : MonoBehaviour
{
    public Rigidbody2D rigidBody;

    public float maxSpeed;
    public float h_value;
    public float v_value;

    public float h_offset;
    public float v_offset;

    public GameObject bubblesParticleGameObject;
    public ParticleSystem bubblesParticle;
    public SpriteRenderer spriteRenderer;

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
        v_value = 1;

        bubblesParticleGameObject.transform.localPosition = new Vector3(0, -1, 1);
        bubblesVelocity.y = -3f;
        bubblesVelocity.x = 0;
        bubblesParticle.Play();
    }

    public void MoveDown()
    {
        RemoveVelocity();
        v_value = -1;

        bubblesParticleGameObject.transform.localPosition = new Vector3(0, 1, 1);
        bubblesVelocity.y = 3f;
        bubblesVelocity.x = 0;
        bubblesParticle.Play();
    }

    public void MoveLeft()
    {
        RemoveVelocity();
        h_value = -1;


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
        RemoveVelocity();
        h_value = 1;

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
}
