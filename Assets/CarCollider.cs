
using UnityEngine;

public class CarCollider : MonoBehaviour
{

    public CarController controller;

    void OnCollisionEnter(Collision collisionInfo)
    {
        if(collisionInfo.collider.tag == "Obstacle")
        {
            controller.enabled = false;
            FindObjectOfType<GameManager>().EndGame();

        }
    }
}
