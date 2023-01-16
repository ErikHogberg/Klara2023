using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Health : MonoBehaviour, IDamage
{
    public int HP = 3;
    public UnityEvent onFail = new UnityEvent();
    public Text healthNumberText = null;

    public void OnDamage(int dmg)
    {
        HP -= dmg;
        UpdateText();
        if (HP <= 0)
        {
            ObjectSpawner.active = false;
            ScoreManager.canIncreaseScore = false;
            onFail.Invoke();
        }
    }

    private void UpdateText()
    {
        if (healthNumberText != null)
        {
            healthNumberText.text = HP.ToString();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateText();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position.y < -2)
        {
            HP = 0;
            UpdateText();
            ObjectSpawner.active = false;
            ScoreManager.canIncreaseScore = false;
            onFail.Invoke();
        }
    }
}
