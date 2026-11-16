using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{

    //ok so why not i didnt think making the the player jump as cooldown instead it only jumps when detecting a ground.. But actually tbf para nga ma detect na rin nag collide ung player sa ground
    [SerializeField] private float _moveSpd = 5f;
    [SerializeField] private float _gravity = -10f;
    [SerializeField] private float _jumpF = 5f;

    private Vector2 _velocity;
    private bool isGrounded;
    private AABBCollider _collider;

    private void Start()
    {
        _collider = GetComponent<AABBCollider>();
    }
    private void Update()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        _velocity.x = moveInput * _moveSpd;

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            _velocity.y = _jumpF;
            isGrounded = false;
        }
        _velocity.y += _gravity * Time.deltaTime;
        transform.position += (Vector3)_velocity * Time.deltaTime;
    }


    public void OnCol(AABBCollider col)
    {
        // for future reference aldrich me thinks its better to use seperate func than using tags on one function???? Idk... Oh well as long it works for now we gonna make new system in da future
        //also wtf why am i doing the same statement on a different func. I should find a way where i just compile all col detection in 1 script. 
        Debug.Log($"Player collided: {col.name}");
        float _overLapX = Mathf.Min(_collider.max.x, col.max.x) - Mathf.Max(_collider.min.x, col.min.x);
        float _overLapY = Mathf.Min(_collider.max.y, col.max.y) - Mathf.Max(_collider.min.y, col.min.y);

        //ground platform uhh imagine the wooden platforms in growtopia where you cant collide under the ground but can collide on top of Y
        if (col.CompareTag("Ground"))
        {
            if (_overLapY < _overLapX && transform.position.y > col.transform.position.y && _velocity.y <= 0f)
            {
                transform.position += new Vector3(0f, _overLapY, 0f);
                _velocity.y = 0f;
                isGrounded = true;
            }
        }
        //Obstacle is more like solid block ground where you collide underside and on the sides but still can stand on top of it.. Basically wall!

        else if (col.CompareTag("Obstacle"))
        {
            if (_overLapY < _overLapX)
            {
                transform.position += new Vector3(0f, _overLapY, 0f);
                _velocity.y = 0f;
                isGrounded = true;
            }
            else
            //this is when player hit the right and left side of the obstacle 
            // wait am i supposed to just compile this nlng on 1 function why am i doing it seperately but its ok if i review this ill get the silliest idea 
            {
                if (transform.position.x < col.transform.position.x)
                    transform.position -= new Vector3(_overLapX, 0f, 0f);
                else
                    transform.position += new Vector3(_overLapX, 0f, 0f);
            }
        }
        else if (col.CompareTag("Fall"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
