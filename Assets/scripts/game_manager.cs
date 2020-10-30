using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class game_manager : MonoBehaviour
{
	//-3,3 x
	//-2,2 y
	public GameObject pivot_pefab;
	public List<Vector2> pivots_pos;
	public int number = 0;
	// Start is called before the first frame update
	void Start()
	{
		pivots_pos = new List<Vector2>(){
			// new Vector2(-2f,1f),
			// new Vector2(3.5f,2f),
			// new Vector2(2f,1f),
			// new Vector2(1f,-1f),
			// new Vector2(-2f,0),
			// new Vector2(-3f,0),
			// new Vector2(-2f,2f),
			// new Vector2(-.3f,1.5f),
			// new Vector2(-.63f,2f),
		};
		List<GameObject> gos = new List<GameObject>();
		foreach (Vector2 v in pivots_pos)
		{
			// var x = Random.Range(-3f, 3f);
			// var y = Random.Range(-2f, 2f);
			var go = GameObject.Instantiate(pivot_pefab, new Vector3(v.x, v.y, pivot_pefab.transform.position.z), Quaternion.identity);
			var reverser = Random.Range(0, 2);
			if (reverser==0)
			{
				go.tag="counterclockwise";
		go.GetComponent<SpriteRenderer>().color = new Color(0, 82f/255f, 1); // Set to opaque bla
			
			}
			gos.Add(go);
		}
		var cyl_parent = GameObject.FindGameObjectsWithTag("cylinderparent")[0];
		cyl_parent.GetComponent<rotate>().SPAWN_pivot (gos.ElementAt(2));
		//spawn prefabs in positions
	}

	// Update is called once per frame
	void Update()
	{

	}
}
