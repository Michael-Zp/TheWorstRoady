using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollider : MonoBehaviour
{

    List<ParticleCollisionEvent> ParticleCollisionEvents = new List<ParticleCollisionEvent>();

    private void OnParticleCollision(GameObject other)
    {
        ParticlePhysicsExtensions.GetCollisionEvents(GetComponent<ParticleSystem>(), other, ParticleCollisionEvents);
        
        if(other.tag == "Floor")
        {
            foreach (var collEve in ParticleCollisionEvents)
            {
                EventSystem.Instance.AddBloodSplatter(collEve.intersection);
            }
        }
    }
}
