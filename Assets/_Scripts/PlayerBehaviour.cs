using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public Transform bulletSpawn;
    public GameObject bullet;
    public int fireRate;


    public BulletManager bulletManager;

    [Header("Movement")]
    public float speed;
    public bool isGrounded;


    public RigidBody3D body;
    public CubeBehaviour cube;
    public Camera playerCam;

    public SceneManager m_sceneManager;

    void start()
    {
        //Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        _Fire();
        _Move();
    }

    private void _Move()
    {

        if (isGrounded)
        {
            body.velocity = new Vector3(body.velocity.x, 0.0f, body.velocity.z); // remove y
            

            if (Input.GetAxisRaw("Jump") > 0.0f)
            {
                body.velocity = transform.up * speed * 0.05f * Time.deltaTime;
            }

        }
        if (Input.GetAxisRaw("Horizontal") > 0.0f)
        {
            // move right
            body.velocity += 0.1f * new Vector3(playerCam.transform.right.x, 0.0f, playerCam.transform.right.x) * 0.1f * speed * Time.deltaTime;// - new Vector3(0.0f, playerCam.transform.right.y * 0.1f * speed * Time.deltaTime, 0.1f * -body.velocity.z);
        }
        else if (Input.GetAxisRaw("Horizontal") < 0.0f)
        {
            // move left
            body.velocity += -0.1f * new Vector3(playerCam.transform.right.x, 0.0f, playerCam.transform.right.z) * 0.1f * speed * Time.deltaTime;// - new Vector3(0.0f, -playerCam.transform.right.y * 0.1f * speed * Time.deltaTime, 0.1f * body.velocity.z);
        }

        if (Input.GetAxisRaw("Vertical") > 0.0f)
        {
            // move forward
            body.velocity += 0.1f* new Vector3(playerCam.transform.forward.x, 0.0f, playerCam.transform.forward.z) * 0.1f * speed * Time.deltaTime;// - new Vector3(0.1f * -body.velocity.x, playerCam.transform.forward.y * 0.1f * speed * Time.deltaTime, 0.0f);
        }
        else if (Input.GetAxisRaw("Vertical") < 0.0f)
        {
            // move Back
            body.velocity += -0.1f * new Vector3(playerCam.transform.forward.x, 0.0f, playerCam.transform.forward.z) * 0.1f * speed * Time.deltaTime;// - new Vector3(0.1f * body.velocity.x, -playerCam.transform.forward.y * 0.1f * speed * Time.deltaTime, 0.0f);
        }

        //body.velocity = Vector3.Lerp(body.velocity, Vector3.zero, 0.9f);
        body.velocity = new Vector3 (body.velocity.x * 0.9f, body.velocity.y, body.velocity.z * 0.9f);
        transform.position += body.velocity;
    }


    private void _Fire()
    {
        if (Input.GetAxisRaw("Fire1") > 0.0f)
        {
            // delays firing
            if (Time.frameCount % fireRate == 0)
            {

                var tempBullet = bulletManager.GetBullet(bulletSpawn.position, bulletSpawn.forward);
                tempBullet.transform.SetParent(bulletManager.gameObject.transform);
            }
        }
        if (Input.GetKeyDown(KeyCode.P)) //key p to go back to main scene
        {
            m_sceneManager.openScene("StartScene");
            //UnityEngine.SceneManagement.SceneManager.LoadScene("StartScene", UnityEngine.SceneManagement.LoadSceneMode.Single);
        }
    }

    void FixedUpdate()
    {
        GroundCheck();
    }

    private void GroundCheck()
    {
        isGrounded = cube.isGrounded;
    }

}
