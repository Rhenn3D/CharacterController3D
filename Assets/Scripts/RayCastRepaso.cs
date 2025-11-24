using UnityEngine.InputSystem;
using UnityEngine;

public class RayCastRepaso : MonoBehaviour
{
    InputAction _clickAction;
    InputAction _positionAction;

    Vector2 _mousePosition;

    void Awake()
    {
        _clickAction = InputSystem.actions["Attack"];
        _positionAction = InputSystem.actions["Look"];
    }


    void Update()
    {
        _mousePosition = _positionAction.ReadValue<Vector2>();

        if(_clickAction.WasPerformedThisFrame())
        {
            ShootRayCast();
        }
    }


    void ShootRayCast()
    {
        //Busca la camara principal para crear un rayo en la _mousePosition
        Ray ray = Camera.main.ScreenPointToRay(_mousePosition);
        RaycastHit hit;
        //Lanzamos(El ray, almacenarlo en el hit, con distancia infinita)
        if(Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if(hit.transform.gameObject.layer == 3)
            {
                
            }

            if(hit.transform.tag == "LoQueSea")
            {
                
            }

            if(hit.transform.name == "LoQueSea2")
            {
                
            }
        }
    }
}

