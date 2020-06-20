using UnityEngine;
using System.Collections;

public class Shooting : MonoBehaviour {

    float bulletSpeed = 1100;//1000
    public GameObject bullet;
    GameObject player;
    Animator playerAnim;

    // Use this for initialization
    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("player");
        playerAnim = player.GetComponent<Animator>();
	}

    void Fire()
    {
        //Shoot
        GameObject tempBullet = Instantiate(bullet, transform.position, transform.rotation) as GameObject;
        Rigidbody tempRigidBodyBullet = tempBullet.GetComponent<Rigidbody>();
        tempRigidBodyBullet.AddForce(tempRigidBodyBullet.transform.forward * bulletSpeed);
        Destroy(tempBullet, 0.5f);

        //Player Shot animation
        playerAnim.SetTrigger("shoot");

        //Shoot Sound
        AudioScript.shootSoundPlay();

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt) || Input.GetKeyDown(KeyCode.RightAlt) || TouchScript.playerShootAxisTouch)
        {
            //Check if game is not over
            if (!GameLogic.isGameOver)
            {
                Fire();
            }
        }
    }
}
