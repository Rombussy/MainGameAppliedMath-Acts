using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 5;
  
    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        Vector3 moveInput = new Vector3(moveX, moveY, 0f);

        Vector3 moveNormalize = moveInput.normalized;

        transform.position += moveNormalize * moveSpeed * Time.deltaTime;

    }
}
