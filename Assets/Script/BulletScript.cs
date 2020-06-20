using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "enemy")
        {
            //Get enemy object and destory
            GameObject enemy = other.transform.gameObject;
            enemy.tag = "deadenemy";
            enemy.GetComponent<Animator>().SetTrigger("dead");

            //Remove Box Collider did this so player can pass through the enemy after dead
            Destroy(enemy.GetComponent<BoxCollider>() , 0.2f);

            //Destroy Enemy
            Destroy(enemy, 1.0f);

            //Player is not underattack anymore
            GameLogic.isPlayerUnderAttack = false;

            //Get and stop bullet purple particle
            gameObject.transform.Find("PurpleParticle").GetComponent<ParticleSystem>().Stop();

            //Get and play bullet particle burst
            gameObject.transform.Find("BurstParticle").GetComponent<ParticleSystem>().Play();

            //Stop Bullet Force moving
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;

            //When Touch Enemy Destroy bullet
            Destroy(gameObject , 0.5f);

            //Player Dead Sound
            AudioScript.enemyHitSoundPlay();

            //Enemies left Count
            GameLogic.enemiesLeft--;

        }
    }

}
