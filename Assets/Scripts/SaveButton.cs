using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveButton : MonoBehaviour
{
    public int index;
    private MenuController menuController;
    private void Start()
    {
        menuController = GameObject.FindGameObjectWithTag("MenuController").GetComponent<MenuController>();
        var q = GameObject.FindGameObjectWithTag("UILoadContent").transform;
        for (int i = 0; i < q.childCount; i++)
        {
            if (this.gameObject.transform.Equals(q.GetChild(i)))
            {
                index = i;
                break;
            }
        }
    }
    public void showStats()
    {
        menuController.ShowStats(index);
        menuController.ShowStatsSave(index);

    }

}
