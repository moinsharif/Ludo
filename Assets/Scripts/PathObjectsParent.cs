using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathObjectsParent : MonoBehaviour
{
    public PathPoint[] commonPathPoints;
    public PathPoint[] redPathPoints;
    public PathPoint[] greenPathPoints;
    public PathPoint[] bluePathPoints;
    public PathPoint[] yellowPathPoints;

    [Header("Scale And Positions Difference")]
    public float[] scales;
    public float[] positionDifference;
}
