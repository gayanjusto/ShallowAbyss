using UnityEngine;

public static class MovementService
{
    static float boostValue = 200f;

    public static void AddRelativeForce(float hValue, float vValue, Rigidbody2D objRigidBody, float objMaxSpeed)
    {
        objRigidBody.AddRelativeForce(new Vector2(hValue * objMaxSpeed, vValue * objMaxSpeed), ForceMode2D.Impulse);
    }

    public static void BoostObjectVertically(float hValue, float vValue, Rigidbody2D objRigidBody, float objMaxSpeed)
    {
        objRigidBody.AddForce(new Vector2(0, 40f * vValue));
    }

    public static void BoostObject(float hValue, float vValue, Rigidbody2D objRigidBody, float objMaxSpeed)
    {
        objRigidBody.AddForce(new Vector2(boostValue * hValue, boostValue * vValue));
    }

    public static void TranslateObjectHorizontally(GameObject movingObj, float h_value, float speed)
    {
        movingObj.transform.Translate(new Vector3(1 * h_value, 0, 0) * Time.deltaTime * speed);
    }

    public static void TranslateObjectVertically(GameObject movingObj, float v_value, float speed)
    {
        movingObj.transform.Translate(new Vector3(0, 1 * v_value, 0) * Time.deltaTime * speed);
    }
}
