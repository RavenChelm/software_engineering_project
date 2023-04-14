using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewSaveButton : MonoBehaviour
{
    private MenuController menuController;
    private void Start()
    {
        menuController = GameObject.FindGameObjectWithTag("MenuController").GetComponent<MenuController>();
    }
    public void showStats()
    {
        menuController.ShowDefaultStats();
        menuController.ShowDefaultStatsSave();

    }

}
