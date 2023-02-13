using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Health : MonoBehaviour, IDamage
{
    public int HP = 3;
    public UnityEvent onFail = new UnityEvent();
    public UnityEvent onHit; // <-- lägg till
    public Text healthNumberText = null;

    public void OnDamage(int dmg)
    {
        // detta sker varje gång skada tas

        HP -= dmg; // hp subtraheras
        UpdateText(); // text uppdateras
        if (HP <= 0)
        {
            // här inne sker saker när hp tar slut
            ObjectSpawner.active = false;
            ScoreManager.canIncreaseScore = false;

            onFail.Invoke();
        }

        onHit.Invoke(); // <-- lägg till
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
            // spelaren dör om de faller under -2 i position
            HP = 0;
            UpdateText();
            ObjectSpawner.active = false;
            ScoreManager.canIncreaseScore = false;
            onFail.Invoke();
        }
    }
}
