using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class rotate : MonoBehaviour
{
    public int speed = 20;

    int rotationSign;

    public GameObject pivot;

    Transform child_transform;

    // Start is called before the first frame update
    void Start()
    {
        child_transform = this.gameObject.transform.GetChild(0);

        rotationSign = 1;
    }

    public bool stopped;

    public void stop()
    {
        stopped = true;
    }

    public void start()
    {
        stopped = false;
        pivot.GetComponent<pivotActions>().OnTriggerEnter2D(this.gameObject.GetComponent<BoxCollider2D>());
    }

    public void SPAWN_pivot(GameObject pivot1)
    {
        pivot = pivot1;
        var pivot_pos = pivot.transform.position;
        transform.position = pivot_pos;
    }

    public void set_pivot(GameObject pivot1, bool clockwise)
    {
        pivot = pivot1;
        var pivot_pos = pivot.transform.position;
        var saved_child_pos = child_transform.position;

        var dis = child_transform.position - transform.position;
        transform.position = pivot_pos;
        child_transform.position = pivot_pos + dis;
        if (clockwise)
        {
            rotationSign = 1;
            // pivot.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1); // Set to opaque bla
        }
        else
        {
            rotationSign = -1;
            // pivot.GetComponent<SpriteRenderer>().color = new Color(0, 82f/255f, 1); // Set to opaque bla
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (pivot == null || stopped) return;
        transform.Rotate(Vector3.back * speed * Time.deltaTime * rotationSign);
    }
}
