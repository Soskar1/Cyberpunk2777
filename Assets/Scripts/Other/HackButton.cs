using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HackButton : MonoBehaviour
{
    [Header("Hack Button Settings")]
    public RectTransform buttonRectTrans;
    [SerializeField] private RectTransform timeImage;
    [Range(0.1f, 1f)][SerializeField] private float coefficientDifficulty;

    [Header("Animations")]
    public Animator animator;

    [HideInInspector] public Hack hack;
    private float maxLifeTime;
    private float lifeTime;

    private void Start()
    {
        maxLifeTime = hack.hackDifficulty / coefficientDifficulty;
        lifeTime = maxLifeTime;
    }

    private void Update()
    {
        if (lifeTime > 0)
        {
            lifeTime -= Time.deltaTime;

            Vector2 imageScale = new Vector2(lifeTime / maxLifeTime, 1);
            timeImage.localScale = imageScale;
        }
        else
        {
            //Провал хака
            hack.hackable = false;
            FindObjectOfType<Movement>().isHacking = false;

            DestroyAllButtons();

            hack.hackPanel.SetActive(false);
            Destroy(gameObject);
        }
    }

    private void DestroyAllButtons()
    {
        HackButton[] hackButtons = FindObjectsOfType<HackButton>();

        foreach (HackButton button in hackButtons)
        {
            Destroy(button.gameObject);
        }
    }
}
