using System;
using UnityEngine;

public class SummonController: MonoBehaviour
{
    public UiButtonManager uiButtonManager;
    public GameObject[] summonUnits;
    public string summonableTagName;
    public int summonUnitIndex = 0;
    public int money = 100;

    [Header("Summon Units Cost")]
    public int Element_0_Cost;
    public int Element_1_Cost;
    public int Element_2_Cost;
    public int Element_3_Cost;
    public int Element_4_Cost;

    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            summonUnitIndex = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            summonUnitIndex = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            summonUnitIndex = 2;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            summonUnitIndex = 3;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            summonUnitIndex = 4;
        }*/

        if (Input.GetMouseButtonDown(0))
        {
            SummonUnit(summonUnitIndex);
        }
    }

    private void SummonUnit(int summonIndex)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.CompareTag(summonableTagName))
            {
                Vector3 clickPosition = hit.point;

                if (summonUnits != null)
                {
                    if(CanBuy(summonIndex)==true)
                        Instantiate(summonUnits[summonIndex], clickPosition, Quaternion.identity);
                }
            }
        }
    }

    public void addMoney(int moneyToAdd)
    {
        money += moneyToAdd;
    }

    public void setSummonUnitIndex(int newIndex)
    {
        summonUnitIndex = newIndex;
        uiButtonManager.showSelection(newIndex);
    }
    
    private Boolean CanBuy(int index)
    {
        switch (index)
        {
            case 0: if (money - Element_0_Cost >= 0) { money -= Element_0_Cost; return true; } break;
            case 1: if (money - Element_1_Cost >= 0) { money -= Element_1_Cost; return true; } break;
            case 2: if (money - Element_2_Cost >= 0) { money -= Element_2_Cost; return true; } break;
            case 3: if (money - Element_3_Cost >= 0) { money -= Element_3_Cost; return true; } break;
            case 4: if (money - Element_4_Cost >= 0) { money -= Element_4_Cost; return true; } break;
        }

        return false;
    }
}
