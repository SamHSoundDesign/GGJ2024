using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private void OnMouseDown()
    {
        GridPoint gp = GetComponentInParent<GridPoint>();
        gp.Hit();
    }


}
