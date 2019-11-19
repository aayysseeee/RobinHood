using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterScript : MonoBehaviour {

    [SerializeField] private FPSViewScript mouseLook = null;
    private CharacterController player;
    
    private Vector3 move = Vector3.zero, vineInput, myScale, startPoint;
    private Vector2 moveInput, look;
    private Texture2D crosshair;
    private bool jumpTick, reloading;
    private float playerTimeScale;
    private GameObject emptyObject;

    public static bool paused, freeMouse, climbing, ziplining,onRamp, onVines, restart, dead;
    public static GameObject ziplineStart,ziplineEnd, vines;
    public static Camera fpsCamera;

    public float fallSpeedMax, moveSpeed, jumpForce, gravity, playerHealth, sprintSpeed, fov;
    public GameObject arrow, bow;

	void Start ()
    {
        player = gameObject.GetComponent<CharacterController>();
        startPoint = player.transform.position;
        playerTimeScale = Time.deltaTime;
        fpsCamera = Camera.main;
        emptyObject = new GameObject();
        fov = 60;
        mouseLook.Initialisation(transform, fpsCamera.transform);
        myScale = transform.localScale;
	}
	
	
	void Update ()
    {
        if (!ziplining)
        MovementManager();
        Aim();
        Rotate();
        ClimbingMovement();
        Zipline();
        FireShot();
        Restarting();
        Death();
	}

    void Death()
    {
        if (dead)
        {
            restart = true;
            print("You Died");
            dead = false;
        }
    }

    void Restarting()
    {
        if (restart)
        {
            player.transform.position = startPoint;
            restart = false;
        }
    }

    void Zipline()
    {    
            if (ziplining)
            {
                jumpTick = true;
                player.transform.position = Vector3.Lerp(transform.position, ziplineEnd.transform.position, 2f * Time.deltaTime);

                if (Input.GetKeyDown(KeyCode.Space)&&jumpTick)
                    ziplining = false;
            }    
    }

    void MovementManager()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        moveInput = new Vector2(horizontal, vertical);

        if(moveInput.sqrMagnitude > 1)
        {
            moveInput.Normalize();
        }

        Vector3 moveDirection = transform.forward * moveInput.y + transform.right * moveInput.x;

        if (!Input.GetKey(KeyCode.LeftShift))
        {
            move.x = moveDirection.x * moveSpeed;
            move.z = moveDirection.z * moveSpeed;
        }
        else
        {
            move.x = moveDirection.x * sprintSpeed;
            move.z = moveDirection.z * sprintSpeed;
        }


        if (player.isGrounded)
        {
            move.y = 0;
            jumpTick = false;
        }
        else if(!climbing)
        {
            Fall();
        }

        if (Input.GetButton("Jump") && jumpTick == false)
        {
            float i = 0;
            Jump(i);
        }

        
            
        if (!climbing)
            player.Move(move*Time.deltaTime);

        mouseLook.UpdateCursorLock();
    }

    void ClimbingMovement()
    {
        if (climbing)
        {
            jumpTick = false;
            move.y = 0;
            bow.SetActive(false);

            
            if (Input.GetKey(KeyCode.W))
                transform.Translate(Vector2.up * (0.25f));
            else if (Input.GetKey(KeyCode.S))
                transform.Translate(Vector2.down * (0.25f));

            if (onVines)
            {
                vineInput = player.transform.position;
                vineInput.x = vines.transform.localPosition.x - 1;
                player.transform.position = vineInput;

                transform.parent = vines.transform;

                if (Input.GetKey(KeyCode.D))
                    transform.Translate(vines.transform.right * (0.25f));
                else if (Input.GetKey(KeyCode.A))
                    transform.Translate(-vines.transform.right * (0.25f));

            }

        }
        else
        {
            transform.parent = null;
            transform.localScale = myScale;
            bow.SetActive(true);
        }


    }

    void Jump(float i)
    {
        float x = 0, f = 0;
        DynamicJump(out f, out x);

        if (climbing)
        {
            i = -jumpForce * 3;
            move.y = jumpForce / 2;
        }
        else if (onRamp)
        {
            move.y = jumpForce + jumpForce/3.3f;
        }
        else
            move.y = f;

        move.x = transform.forward.x * (i + x);
            jumpTick = true;
    }

    void DynamicJump(out float y, out float x)
    {
        if (move.x != 0)
        {
            y = jumpForce * 0.8f;
            x = sprintSpeed * 0.1f;
        }
        else
        {
            y = jumpForce;
            x = 0;
        }
       

    }

    void Fall()
    {
        move.y -= gravity;
    }


    void Rotate()
    {       
            mouseLook.LookRotation(transform, fpsCamera.transform);
    }

    void Aim()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
     
            if (Time.timeScale > 0.1f)
            {
                Time.timeScale -= 0.1f;
                gravity -= 0.1f;
   
                if(fpsCamera.fieldOfView > 30)
                   fpsCamera.fieldOfView -= 1;
            }
        }
        else
        {
            Time.timeScale = 1;
            gravity = 1;

            if (fpsCamera.fieldOfView < 60)
                fpsCamera.fieldOfView += 1;
        }
    }

    void Raycast()
    {
        GameObject i;
        RaycastHit hit;

        if(Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, 100f))
        {
            i = hit.collider.gameObject;
                ShootArrow(i);
        }

    }

    void FireShot()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !reloading)
        {
            arrow.GetComponent<Renderer>().enabled = false;
            Raycast();
            reloading = true;         
            StartCoroutine(Reload());
        }
    }

    IEnumerator Reload()
    {
            yield return new WaitForSecondsRealtime(2);
            arrow.GetComponent<Renderer>().enabled = true;
            reloading = false;    
    }

    void ShootArrow(GameObject target)
    {
        arrow.GetComponent<Renderer>().enabled = false;

        if (target.tag == "Shootable")
        {
            Destroy(target);
           
        }

        if (target.tag == "Enemy")
        {
            Destroy(target);
            Destroy(target.transform.parent.gameObject);
        }
    }

    void OnGUI()
    {
         GUI.Box(new Rect(Screen.width / 2, Screen.height / 2, 5, 5), "");
    }   
}
