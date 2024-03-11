using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DepthManager : MonoBehaviour
{
    [SerializeField] GameObject submersible;
    [SerializeField] GameObject depth_reference;
    [SerializeField] TextMeshProUGUI depth_text;
    private float submmersibleYaxis;
    private float referenceYaxis;
    private float distance;
    private int depth;
    private Transform submersible_transform;
    private Transform reference_transform;
    // Start is called before the first frame update
    void Start()
    {
       
        

    }

    private void FixedUpdate()
    {
        submersible_transform = submersible.transform;
        reference_transform = depth_reference.transform;
        submmersibleYaxis = submersible_transform.position.y;
        referenceYaxis = reference_transform.position.y;
        distance = Mathf.Abs(submmersibleYaxis - referenceYaxis);
        depth = Mathf.RoundToInt(distance);
        depth_text.text = depth.ToString()+"m";
    }
}
