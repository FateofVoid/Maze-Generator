using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//base class for all Maze Renderers
public abstract class BaseMazeRenderer : MonoBehaviour
{

    // the field the maze will be generated on
    protected GameObject MazeField;

    //maze dimensions.
    [SerializeField]
    protected Slider widthSlider;
    [SerializeField]
    protected Slider heightSlider;

    // Update is called once per frame
    

    protected void DestroyMazeField()
    {
        Destroy(MazeField);
    }
}
