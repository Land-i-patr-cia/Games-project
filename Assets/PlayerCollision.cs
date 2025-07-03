using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    void OnCollisionEnter(Collision collisionInfo)
    {
        //Could use .tag but cant add tags to unity project
        //Debug.Log(collisionInfo.collider.name == "Wall");
}

} 
