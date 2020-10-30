using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
public class pivot_interact : MonoBehaviour
{
	GameObject child_transform;
	private int score=0;
    // Start is called before the first frame update
    void Start()
    {
        child_transform = this.gameObject.transform.GetChild(0).gameObject;
    }
void add_score(){
	score++;
	child_transform = this.gameObject.transform.GetChild(0).gameObject;
	child_transform.GetComponent<TextMesh>().text=score.ToString();;
	var audioData = Camera.main.GetComponent<AudioSource>();
        audioData.Play(0);
}
    // Update is called once per frame
    void Update()
    {
        
    }

	void OnTriggerEnter(Collider col)
	{
		add_score();
		Debug.Log("sdasd");
	}
}
