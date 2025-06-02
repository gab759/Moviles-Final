using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    private void Update()
    {
        float inputX = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * inputX * speed * Time.deltaTime);
    }
}