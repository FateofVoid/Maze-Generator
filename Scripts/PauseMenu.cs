using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// this script handles everything related to generating the ingame menu that generates the mazes.
public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public static MazeType CurrentMazeType = MazeType.SIMPLE;

    public Dropdown MazeSelectDropdown;
    public Slider widthSlider;
    public Slider heightSlider;
    public GameObject pauseMenuUI;
    public Transform Player;

    // generates the dropdown based on the MazeType enum and generates an initial maze.
    private void Start()
    {

        //generate dropdown options for Maze selection.
        foreach (MazeType type in MazeType.GetAll())
        {
            MazeSelectDropdown.options.Add(new Dropdown.OptionData() { text = type.ToString() });
        }
        MazeSelectDropdown.RefreshShownValue();

        GenerateMaze();
        UpdateSliders();
    }

    //convert dropdown selection into Mazetype  
    private MazeType getMazeType(string option)
    {
        if (option == MazeType.SIMPLE.name) return MazeType.SIMPLE;
        if (option == MazeType.LARGE.name) return MazeType.LARGE;
        return MazeType.SIMPLE;
        
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SwitchMenuState();
        }
    }

    //turns menu on or off.
    public void SwitchMenuState()
    {
            GameIsPaused = !GameIsPaused;
            pauseMenuUI.SetActive(GameIsPaused);
    }

    //generates Maze based on dropdown selection. Renderer will reset player position
    public void GenerateMaze()
    {
        ResetMaze();

        Debug.Log("generate mazes");

        MazeType mazeType = getMazeType(MazeSelectDropdown.options[MazeSelectDropdown.value].text);

        if (mazeType.Equals(MazeType.SIMPLE))
        {
            SimpleMazeRenderer.BeginMazeRendering = true;
        }
        else if (mazeType.Equals(MazeType.LARGE))
        {
            LargeMazeRendere.BeginMazeRendering = true;
        }

        // turn off menu and center cam on player;
        SwitchMenuState();
        PerspectivePan.IsFreeCam = false;
    }

    private void ResetMaze()
    {
        Debug.Log("reset mazes");

        DestoyMazes();
        uint width = (uint)widthSlider.value;
        uint height = (uint)heightSlider.value;

        widthSlider.value = width;
        heightSlider.value = height;
    }

    public void UpdateSliders()
    {
        
        CurrentMazeType = getMazeType(MazeSelectDropdown.options[MazeSelectDropdown.value].text);
        widthSlider.minValue = CurrentMazeType.minSize;
        widthSlider.maxValue = CurrentMazeType.maxSize;
        heightSlider.minValue = CurrentMazeType.minSize;
        heightSlider.maxValue = CurrentMazeType.maxSize;

    }

    private void DestoyMazes()
    {
        SimpleMazeRenderer.DestroyMaze = true;
        LargeMazeRendere.DestroyMaze = true;
    }
}
