using UnityEngine;

public class AutonomousAgent : AIAgent
{
    [SerializeField] Movement movement;
    [SerializeField] Perception seekPerception;
    [SerializeField] Perception fleePerception;

    void Start()
    {



    }
    void Update()
    {
        if (seekPerception != null)
        {
            var gameObjects = seekPerception.GetGameObjects();
            if (gameObjects.Length > 0)
            {
                Vector3 force = Seek(gameObjects[0]);
                movement.ApplyForce(force);
            }
        }

        if (fleePerception != null)
        {
            var gameObjects = fleePerception.GetGameObjects();
            if (gameObjects.Length > 0)
            {
                Vector3 force = Flee(gameObjects[0]);
                movement.ApplyForce(force);
            }
        }
        //foreach (var go in gameObjects)
        //{
        //    Debug.DrawLine(transform.position, go.transform.position, Color.red);
        //}

        
        transform.position = Utilities.Wrap(transform.position,new Vector3(-150,-150,-150),new Vector3(150,150,150));

        if (movement.Velocity.sqrMagnitude > 0.0f)
        {
            transform.rotation = Quaternion.LookRotation(movement.Velocity);
        }
    }

    // Seek behavior implementation
    Vector3 Seek(GameObject go)
    {
        Vector3 direction = go.transform.position - transform.position;
        Vector3 force = GetStereingForce(direction);
        
        return force;
    }
    // Flee behavior implementation
    Vector3 Flee(GameObject go)
    {
        Vector3 direction = transform.position - go.transform.position;
        Vector3 force = GetStereingForce(direction);
        
        return force;
    }

    // Calculate the steering force towards a desired direction
    public Vector3 GetStereingForce(Vector3 direction)
    {
        Vector3 desire = direction.normalized * movement.maxSpeed;
        Vector3 steer = desire - movement.Velocity;
        Vector3 force = Vector3.ClampMagnitude(steer, movement.maxForce);

        return force;
    }

}


