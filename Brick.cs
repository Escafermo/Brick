using UnityEngine;
using System.Collections;

public class Brick : MonoBehaviour {

	public Sprite[] hitSprites;
	public static int brickCount = 0;
	public AudioClip crack;
    public GameObject smoke;
	   
	private int timesHit;
	private LevelManager levelManager;
	private bool isBreakable;
	
	void Start () {
		isBreakable = (this.tag == "Breakable");
		if (isBreakable){
			brickCount++;
		}
		timesHit = 0;
		levelManager = LevelManager.FindObjectOfType<LevelManager>();
	}
	
	void Update () {
	}
	
	void OnCollisionEnter2D (Collision2D collision){
		AudioSource.PlayClipAtPoint(crack,transform.position);
		if (isBreakable){
			handleHits();
		}
    }
			
	void handleHits (){
		timesHit++;
		int maxHits = hitSprites.Length +1;
		if (timesHit >= maxHits){
			brickCount--;
			Debug.Log(brickCount);
			levelManager.BrickDestroyed();
            Debug.Log(smoke);
            smokeHere();
            Destroy (gameObject);
		} else {
			
			LoadSprites();
		}
    }

    void smokeHere ()
    {
        GameObject smokeHere = Instantiate(smoke, transform.position, Quaternion.identity) as GameObject;
        smokeHere.particleSystem.startColor = gameObject.GetComponent<SpriteRenderer>().color;
    }
	
	void LoadSprites() {
		int spritesIndex = timesHit - 1;
		if (hitSprites[spritesIndex] != null) {
		    this.GetComponent<SpriteRenderer>().sprite = hitSprites[spritesIndex];
		}
        else
        {
            Debug.LogError("Error - brick sprite missing");
         }
    }		
	
	public void SimulateWin(){
		levelManager.LoadNextLevel();
    }   
}
