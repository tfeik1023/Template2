using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class PlayerScript : MonoBehaviour

{

    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    private Rigidbody2D rd2d;
    private int count;
    public float speed;
    public GameObject heart1, heart2, heart3, youLose;
    public static int health;
    private bool facingRight = true;
    Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        count = 0;

        SetCountText();
        winTextObject.SetActive(false);

        health = 3;
        heart1.gameObject.SetActive (true);
        heart2.gameObject.SetActive (true);
        heart3.gameObject.SetActive (true);
        youLose.gameObject.SetActive (false);

    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if(count == 4)
        {
            transform.position = new Vector3(44.0f, 0.2f, 0.0f);
            health = 3;
        }
        if(count >= 8)
        {
            winTextObject.SetActive(true);
        }
            
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float verMovement = Input.GetAxis("Vertical");

        rd2d.AddForce(new Vector2(hozMovement * speed, verMovement * speed));

        if (Input.GetKey("escape"))
        {
        Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            anim.SetInteger("State", 1);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            anim.SetInteger("State", 1);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            anim.SetInteger("State", 0);
        }

        if (health > 3)
        health = 3;
        

        switch (health)
        {
        case 3:
            heart1.gameObject.SetActive (true);
            heart2.gameObject.SetActive (true);
            heart3.gameObject.SetActive (true);
            break;
        case 2:
            heart1.gameObject.SetActive (true);
            heart2.gameObject.SetActive (true);
            heart3.gameObject.SetActive (false);
            break;
        case 1:
            heart1.gameObject.SetActive (true);
            heart2.gameObject.SetActive (false);
            heart3.gameObject.SetActive (false);
            break;
        case 0:
            heart1.gameObject.SetActive (false);
            heart2.gameObject.SetActive (false);
            heart3.gameObject.SetActive (false);
            youLose.gameObject.SetActive (true);
            gameObject.SetActive (false);
            Time.timeScale = 0;
            break;
        }
        
        if (facingRight == false && hozMovement > 0)
        {
        Flip();
        }
        else if (facingRight == true && hozMovement < 0)
        {
        Flip();
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;

            SetCountText();
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.SetActive(false);
            SetCountText();
            health -= 1;
        }

    }

    

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.collider.tag == "Ground")
        {
            if(Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }
        }
    }

    void Flip()
   {
     facingRight = !facingRight;
     Vector2 Scaler = transform.localScale;
     Scaler.x = Scaler.x * -1;
     transform.localScale = Scaler;
   }

}
