using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "player")
        {
            //Play Coin Sound
            AudioScript.coinSoundPlay();

            //Coin counter less
            GameLogic.coinsLeft--;

            //Play Particle
            gameObject.transform.Find("particle").GetComponent<ParticleSystem>().Play();

            //Destroy Coins
            GetComponent<Collider>().enabled = false;
            GetComponent<MeshRenderer>().enabled = false;
            Destroy(gameObject, 2.0f);
        }
    }

}
