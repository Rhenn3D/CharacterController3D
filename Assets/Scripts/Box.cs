using UnityEngine;

public class Box : MonoBehaviour, IGrabeable, IDamageable
{

    [SerializeField] private float _health = 20;


    public void Grab()
    {
        Debug.Log("Recogiendo Cajas");
    }
    
    public void TakeDamage(float damage)
    {
        _health -= damage;

        if(_health <= 0)
        {
            Destroy(gameObject);
        } 
    }
}
