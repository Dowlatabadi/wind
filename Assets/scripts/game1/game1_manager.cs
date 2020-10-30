using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Classes;
using LinePoint;
using UnityEngine;

public class game1_manager : MonoBehaviour
{
    //-3,3 x
    //-2,2 y
    public GameObject checkpivot_pefab;

    public List<Vector2> pivots_pos;

    Level lvl;

    int current_order;

    int failure_counter;

    int renew_limit = 3;

    List<GameObject> gos = new List<GameObject>();

    public int OneHitOccured()
    {
        return ++current_order;
    }

    public void OneMistakeOccured()
    {
        failure_counter++;
        if (failure_counter > renew_limit)
        {
            renew();
        }
        else
        {
            reset();
        }
    }

    void renew()
    {
    }

    void reset()
    {
        var cyl_parent = GameObject.FindGameObjectsWithTag("cylinderparent")[0];
        cyl_parent.GetComponent<rotate>().stop();
        current_order = 0;
        foreach (var pvt_go in gos)
        {
            GameObject.Destroy (pvt_go);
        }
        gos = new List<GameObject>();
        UnityEngine.Debug.Log("reset");
        Draw_level (lvl);
    }

    void Draw_level(Level lvl)
    {
        int i = 0;
        foreach (var pvt in lvl.Pivots)
        {
            i++;

            pivots_pos.Add(pvt.pivot_pos);
            GameObject go;

            if (pvt.labeled)
            {
                go =
                    GameObject
                        .Instantiate(checkpivot_pefab,
                        new Vector3(pvt.pivot_pos.x,
                            pvt.pivot_pos.y,
                            checkpivot_pefab.transform.position.z),
                        Quaternion.identity);
                go.GetComponent<pivotActions>().set_number(i);
            }
            else
            {
                go =
                    GameObject
                        .Instantiate(checkpivot_pefab,
                        new Vector3(pvt.pivot_pos.x,
                            pvt.pivot_pos.y,
                            checkpivot_pefab.transform.position.z),
                        Quaternion.identity);
            }
            go.GetComponent<pivotActions>().labled = pvt.labeled;

            if (pvt.pivot_type == Pivot_type.ClockWise)
            {
                go.gameObject.tag = "clockwise";

                go.GetComponent<SpriteRenderer>().color = Color.red;
            }
            else
            {
                go.GetComponent<SpriteRenderer>().color = Color.blue;
                go.gameObject.tag = "counterclockwise";
            }
            gos.Add (go);
        }
        var cyl_parent = GameObject.FindGameObjectsWithTag("cylinderparent")[0];
        cyl_parent.GetComponent<rotate>().SPAWN_pivot(gos.ElementAt(0));
		 cyl_parent.transform.eulerAngles =
                        new Vector3(cyl_parent.transform.eulerAngles.x,
                            cyl_parent.transform.eulerAngles.y,
                            0);
        cyl_parent.GetComponent<rotate>().stop();
    }

    // Start is called before the first frame update
    void Start()
    {
        failure_counter = 0;
        current_order = 0;
        UnityEngine
            .Debug
            .Log(Helper
                .LinePointGetDist(new Vector2(1, 1.5f),
                new Line { m_slope = 1, b = 0 }));

        // lvl=new Level(5);
        lvl =
            new Level(game_mode.pivotCreation,
                new List<(
                        Vector2 pivot_pos,
                        Pivot_type pivot_type,
                        bool labeled
                    )
                > {
                    (new Vector2(1, 1), Pivot_type.ClockWise, true),
                    (new Vector2(0, 0), Pivot_type.CounterClockWise, false),
                    (new Vector2(-1, 2), Pivot_type.ClockWise, true),
                    (new Vector2(-2, 3), Pivot_type.ClockWise, true),
                    (new Vector2(2f, -2.5f), Pivot_type.ClockWise, false),
                    (new Vector2(1.5f, -3.5f), Pivot_type.ClockWise, false)
                },
                "string Info",
                2);

        Draw_level (lvl);
    }

    // Update is called once per frame
    bool dragging = false;

    Vector3 drag_pos = Vector3.zero;

    void Update()
    {

		
        if (
            Input.GetMouseButton(0) && !dragging &&
            GameObject
                .FindGameObjectsWithTag("cylinderparent")[0]
               
                .GetComponent<rotate>()
                .stopped
        )
        {
        UnityEngine.Debug.Log("started");

            dragging = true;
            drag_pos = Input.mousePosition;
        }
        if (
            Input.GetMouseButton(0) &&
            dragging &&
            GameObject
                .FindGameObjectsWithTag("cylinderparent")[0]
                
                .GetComponent<rotate>()
                .stopped
        )
		//rotation phase
        {
        UnityEngine.Debug.Log("updown");

            var mill =
                GameObject
                    .FindGameObjectsWithTag("cylinderparent")[0]
                    ;
            
            var currpos = Input.mousePosition;
			var amount=currpos.y-drag_pos.y;
            mill.transform.eulerAngles =
                new Vector3(mill.transform.eulerAngles.x,
                    mill.transform.eulerAngles.y,
                    mill.transform.eulerAngles.z + .003f*amount);
        }
        if (
            Input.GetMouseButtonUp(0) &&
            dragging &&
            GameObject
                .FindGameObjectsWithTag("cylinderparent")[0]
                
                .GetComponent<rotate>()
                .stopped
        )
        {
        UnityEngine.Debug.Log("release");

            dragging = false;
            drag_pos = Vector3.zero;
        }
    }
}
