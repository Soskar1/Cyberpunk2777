using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Hack : MonoBehaviour
{
    [Header("Hack Settings")]
    public bool hackable;
    public int hackDifficulty;

    [Header("Hack UI")]
    public GameObject hackPanel;
    [SerializeField] private GameObject hackButton;

    [Header("Hack Results For Turret")]
    [SerializeField] private LayerMask whoIsEnemy;
    [SerializeField] private SpriteRenderer turretsSide;

    private int clickedButtons;
    private int hackButtonAmount;
    private bool hackBegan;

    private void Update()
    {
        if (hackBegan)
        { 
            if (clickedButtons == hackButtonAmount)
            {
                SuccessfulHack();
            }
        }
    }

    public void Hacking()
    {
        if (hackable)
        {
            hackBegan = true;
            FindObjectOfType<Movement>().isHacking = true;

            hackPanel.SetActive(true);

            for (int index = 0; index < hackDifficulty; index++)
            {
                //Все значения взяты благодаря размеру канваса
                Vector2 spawnPos = new Vector2(Random.Range(-865f, 865f), Random.Range(-397f, 155f));
                GameObject buttonInstance = Instantiate(hackButton, spawnPos, Quaternion.identity);
                buttonInstance.transform.SetParent(hackPanel.transform);
                buttonInstance.GetComponent<HackButton>().buttonRectTrans.localPosition = new Vector3(spawnPos.x, spawnPos.y, 0);
                buttonInstance.GetComponent<HackButton>().buttonRectTrans.localScale = new Vector3(1, 1, 1);
                buttonInstance.GetComponent<HackButton>().hack = GetComponent<Hack>();
                buttonInstance.GetComponent<Button>().onClick.AddListener(() => Clicked());

                hackButtonAmount++;
            }
        }
    }

    public void Clicked()
    {
        clickedButtons++;
        EventSystem.current.currentSelectedGameObject.GetComponent<HackButton>().animator.SetBool("Clicked", true);
        Destroy(EventSystem.current.currentSelectedGameObject.gameObject, 1f);
    }

    public void SuccessfulHack()
    {
        //Возвращаем все значения к норме
        clickedButtons = 0;
        hackButtonAmount = 0;
        FindObjectOfType<Movement>().isHacking = false;
        hackPanel.SetActive(false);
        hackBegan = false;

        //Что происходит с хакнутым устройством???
        if (GetComponentInChildren<Turret>() != null)
        {
            GetComponentInChildren<Turret>().whoIsEnemy = whoIsEnemy;
            turretsSide.color = Color.green;
        }

        hackable = false;
    }
}
