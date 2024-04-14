using UnityEngine;

public class SummonController: MonoBehaviour
{
    public GameObject[] summonUnits;
    public string summonableTagName;
    public int summonUnitIndex = 0;
    public int money = 100;

    void Update()
    {
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
        }

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
                    Instantiate(summonUnits[summonIndex], clickPosition, Quaternion.identity);
                }
            }
        }
    }
}
