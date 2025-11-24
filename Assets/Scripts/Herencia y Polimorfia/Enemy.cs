using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float movementSpeed = 5;
    public float attackDamge = 10;

    public void Movement()
    {
        Debug.Log("Movimiento Base");
    }

    public virtual void Attack()
    {
        Debug.Log("Ataque Base");
    }    
}
