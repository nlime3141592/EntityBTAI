using UnityEngine;

public class VelocityController2D : MonoBehaviour
{
    private Vector2 m_tvel;
    private Vector2 m_cvel;
    private Rigidbody2D m_rigid;

    public void Subscribe(Rigidbody2D rigidbody)
    {
        m_rigid = rigidbody;
    }

    public void Unsubscribe()
    {
        m_rigid = null;
    }

    public float GetVelocityX()
    {
        return m_rigid.velocity.x;
    }

    public float GetVelocityY()
    {
        return m_rigid.velocity.y;
    }

    public void SetVelocityXY(float x, float y)
    {
        m_tvel.Set(x, y);
        ApplyVelocity();
    }

    public void SetVelocityX(float x)
    {
        float y = m_rigid.velocity.y;
        SetVelocityXY(x, y);
    }

    public void SetVelocityY(float y)
    {
        float x = m_rigid.velocity.x;
        SetVelocityXY(x, y);
    }

    private void ApplyVelocity()
    {
        m_cvel = m_tvel;
        m_rigid.velocity = m_tvel;
    }

    private void FixedUpdate()
    {
        m_cvel = m_rigid.velocity;
    }

    private float GetParabolaValue(int maxFps, int currentFps, bool isIncrease)
    {
        if(maxFps < 1)
            return 1.0f;
        else if(currentFps < 0)
            currentFps = 0;
        else if(currentFps >= maxFps)
            currentFps = maxFps - 1;

        if(isIncrease)
            currentFps = (maxFps - 1) - currentFps;

        float t = maxFps - 1;
        float x = currentFps;
        
        return 1.0f - (x * x) / (t * t);
    }
}