using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BallMove : MonoBehaviour
{
    private Rigidbody2D ballsRigidbody;
    private Vector2 velocity;

    //for audio
    public AudioClip balldie;
    public AudioClip bump1;
    public AudioClip bump2;
    public AudioClip bump3;
    public AudioClip bump4;
    public AudioClip immune;
    public AudioClip bumpPlayer;
    public AudioClip damageBrick;
    private AudioSource source;
    private float playerPitch = 1f;
    private float defaultPitch = 1f;
    private float soundTimer = 0f;

    //used for finding out where it hit the player
    private Transform playerPosition;

    private Vector2 ballPostition = new Vector2(-1.5f, -2.5f);
    private int damage = 3;
    private float startingAngle = 0.5f;
    private float angleOfBall;
    private float ballSpeed = 6;

    //default at 0.7f
    private float paddleWidth = 0.7f;


    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        //sets the initial angle of the ball and movement speed
        velocity = FinlayUtilities2D.AngleToVector(startingAngle, ballSpeed);

        //gets the balls rigidbody to update the movement
        ballsRigidbody = GetComponent<Rigidbody2D>();
    }


    void FixedUpdate()
    {
        //moves the ball in relation to the new vector2
        ballsRigidbody.MovePosition(ballsRigidbody.position + velocity * Time.fixedDeltaTime);

        //used for finding out the correct ball sound
        if (soundTimer > 0)
        {
            soundTimer -= Time.deltaTime;
        }
        else
        {
            soundTimer = 0;
        }
    }

    //when the ball hits, first update the direction.
    private void OnCollisionEnter2D(Collision2D collision)
    {

        soundTimer++;
        //check to see if ball collides with player
        if (collision.gameObject.tag == "Player")
        {
          
            //used for getting the players box collider
            playerPosition = collision.transform;
            //finds angle in radians between both objects, paddle width is used to reduce the angle which the ball is returned
            angleOfBall = FinlayUtilities2D.AngleDifference(gameObject, collision.gameObject, false) * paddleWidth;
            //Calculates the new Vector 2 and sets the ball
            velocity = FinlayUtilities2D.AngleToVector(angleOfBall, ballSpeed);

            //pitches the sound depending on the angle
            playerPitch = (angleOfBall/3)+1;
            source.pitch = playerPitch;
            source.PlayOneShot(bumpPlayer);
        }
        else
        {
            //resets sound pitch if its not the player
            source.pitch = defaultPitch;

            //reflects the angle which the ball hits 
            velocity = Vector2.Reflect(velocity, collision.contacts[0].normal);

            //depending on what the ball hits, something happens
            if (collision.gameObject.tag == "Bricks")
            {
                //when the ball hits a brick, damage the brick
                BrickHealth brickScript = collision.gameObject.GetComponent<BrickHealth>();
                brickScript.DamageBrick(damage);


                //used for playing correct sound
                if (soundTimer >= 3)
                {
                    source.PlayOneShot(bump4);
                }
                else if (soundTimer >= 2)
                {
                    source.PlayOneShot(bump3);
                }
                else if (soundTimer >= 1)
                {
                    source.PlayOneShot(bump2);
                }
                else
                {
                    source.PlayOneShot(bump1);
                }
            }
            else if (collision.gameObject.tag == "Wall")
            {
                Debug.Log("Hit Wall");
                source.PlayOneShot(immune);

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Kill Ball");
        source.PlayOneShot(balldie);
        // gameObject.SetActive(false);
        BallSpawn();
    }

    private void BallSpawn()
    {
        transform.position = ballPostition;
        gameObject.SetActive(true);
    }

}
