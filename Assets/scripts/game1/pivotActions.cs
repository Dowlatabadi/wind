using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pivotActions : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    public bool labled ;

    public void check_up()
    {
        gameObject
            .transform
            .Find("check")
            .GetComponent<Animator>()
            .SetBool("grow", true);
    }

    public int get_my_num()
    {
        var str =
            gameObject
                .transform
                .Find("number")
                .GetComponent<TMPro.TextMeshPro>()
                .text;
        return int.Parse(str);
    }

    public void set_number(int num)
    {
        //set color first
        //gameObject.transform.Find("number").GetComponent<TMPro.TextMeshPro>().text=num.ToString();;
        gameObject
            .transform
            .Find("number")
            .GetComponent<TMPro.TextMeshPro>()
            .enabled = true;
        gameObject
            .transform
            .Find("number")
            .GetComponent<TMPro.TextMeshPro>()
            .text = num.ToString();
        gameObject
            .transform
            .Find("number")
            .GetComponent<Animator>()
            .SetBool("grow", true);
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
		var mill= GameObject.FindGameObjectsWithTag("cylinderparent")[0];
		if (mill.GetComponent<rotate>().stopped)
		return;
        var current_num =
            Camera.main.GetComponent<game1_manager>().OneHitOccured();
        if (!labled)
            set_number(current_num);
        else
        {
            if (get_my_num() == current_num)
                check_up();
            else
                Camera.main.GetComponent<game1_manager>().OneMistakeOccured();
        }






		if (this.gameObject.tag=="clockwise")

		mill.GetComponent<rotate>().set_pivot(this.gameObject,true);
		else if (this.gameObject.tag=="counterclockwise")
		mill.GetComponent<rotate>().set_pivot(this.gameObject,false);

    }

    // Update is called once per frame
    void Update()
    {
      
    }
}
