using UnityEngine;
using UnityEngine.UI;


// check and update the helth of the player
public class Health : MonoBehaviour
{
    public Text healthText; // assumed that text is always the second child
    public Image healthBar; // assumed that image is always first child
    
    float health, maxHealth = 30f;
    float lerpSpeed;

    private void Start()
    {
        health = maxHealth;
        healthText = transform.GetComponentInChildren<Text>();
        updateHalth();
    }

    private void Update()
    {
        
    }

    void HealthBarFiller()
    {
        // healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, (health / maxHealth), lerpSpeed);
        healthBar.fillAmount = health / maxHealth;
        
    }
    void ColorChanger()
    {
        Color healthColor = Color.Lerp(Color.red, Color.green, 1-3*health);
        healthBar.color = healthColor;
        
    }

    bool DisplayHealthPoint(float _health, int pointNumber)
    {
        return ((pointNumber * 10) >= _health);
    }

    public void Damage(float damagePoints)
    {
        if (health >= 0){
            health -= damagePoints;
            updateHalth();
        }
    }
    public void Heal(float healingPoints)
    {
        if (health < maxHealth){
            health += healingPoints;
            health = Mathf.Clamp(health, 0, maxHealth);
            updateHalth();
        }
    }

    void updateHalth(){
        if(healthBar == null || healthText == null) return;
        healthText.text = "Health: " + health + "%";
        if (health > maxHealth) health = maxHealth;

        lerpSpeed = 3f * Time.deltaTime;

        HealthBarFiller();
        // ColorChanger();
    }
}

