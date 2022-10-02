using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{

    private SpriteRenderer playerGraphic;
    private PlayerStatsController playerStats;

    public ClassData userClass;
    public Rigidbody2D userProjectile;
    public float fireRate;

    public int expGoal;
    public int currentExp;

    public int userLevel;
    public Slider levelDisplay;

    public float inputX, inputY; // The x and y direction movement of the character

    private Rigidbody2D rb2d;

    public Image[] slots;
    public Sprite[] results;

    public Image[] hearts;

    public TextMeshProUGUI congrats;
    public TextMeshProUGUI result;

    public GameObject levelUpPanel;
    public GameObject spinButton;
    public GameObject resumeButton;

    public AudioSource playerEffects;
    public AudioClip[] generalSounds;

	private void Awake()
	{
        playerGraphic = GetComponent<SpriteRenderer>();
        playerStats = GetComponent<PlayerStatsController>();
        rb2d = GetComponent<Rigidbody2D>();
        playerEffects = GetComponent<AudioSource>();
        Time.timeScale = 1;
	}

	// Start is called before the first frame update
	void Start()
    {
        userClass = playerStats.currentClass;
        fireRate = userClass.attackRate;
        expGoal = userClass.expToLevel;
        playerGraphic.sprite = userClass.characterIcon;
        currentExp = 0;
        levelUpPanel.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if(playerStats.classChanged == true)
		{
            userClass = playerStats.currentClass;
            fireRate = userClass.attackRate;
            expGoal = userClass.expToLevel;
            playerGraphic.sprite = userClass.characterIcon;
            playerStats.classChanged = false;
        }

        levelDisplay.maxValue = expGoal;
        levelDisplay.value = currentExp;

        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");
        Vector2 moveDirection = new Vector2(inputX, inputY);

        //userProjectile.gameObject.GetComponent<SpriteRenderer>().sprite = userClass.weaponIcon;
        
        //Fire a shot only when moving
        if ((int)inputX != 0 || (int)inputY != 0)
        {
            if(fireRate <= 0)
			{
                Rigidbody2D bullet;
                bullet = Instantiate(userProjectile, gameObject.transform.position, gameObject.transform.rotation);
                bullet.GetComponent<SpriteRenderer>().sprite = userClass.weaponIcon;
                bullet.velocity = transform.TransformDirection(moveDirection * userClass.bulletSpeed);

                fireRate = userClass.attackRate;
            }
        }
        
        fireRate -= Time.deltaTime;

        if(currentExp >= expGoal)
		{
            congrats.text = "SPIN TO WIN YOUR PRIZE";
            resumeButton.gameObject.SetActive(false);
            Time.timeScale = 0;
            levelUpPanel.gameObject.SetActive(true);
            expGoal *= Mathf.RoundToInt(Mathf.Sqrt(expGoal)/2);
            currentExp = 0;
		}

        DisplayHearts(userClass.hitPoints);

        if(userClass.hitPoints <= 0)
		{
            // Play some sort of death noise
            userClass.isDead = true;
            playerStats.SwapClasses();
		}
        
    }

	private void FixedUpdate()
	{
        rb2d.MovePosition(rb2d.position + new Vector2(inputX, inputY) * userClass.moveSpeed * Time.fixedDeltaTime);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Exp"))
		{
            currentExp += collision.gameObject.GetComponent<ExpValue>().value;
            Destroy(collision.gameObject, 0.01f);
            playerEffects.clip = generalSounds[0];
            playerEffects.Play();
        }
        else if (collision.gameObject.CompareTag("End"))
		{
            SceneManager.LoadScene(2);
		}
	}

    public void onTakeDamage(int damage)
	{
        playerEffects.clip = userClass.hurtSound;
        playerEffects.Play();

        userClass.hitPoints -= damage;


	}

    public void LevelUp()
	{
        playerEffects.clip = generalSounds[1];
        playerEffects.Play();
        // This is where I would put my slot machine IF I HAD ONE
        userClass.hitPoints = playerStats.currentClass.hitPoints;

        //Extra health
        //More Damage
        //Faster Movespeed
        //AllUp

        int x = Random.Range(1, 5);
        int y = Random.Range(1, 5);
        int z = Random.Range(1, 5);

        int prize = x;

        if(x == y && x == z)
		{
            prize += 8;
		}
        else if (x == y && x != z)
		{
            prize += 4;
		}

        // Create four spin animations one for each sprite
        //after you roll the stats play the animations symultaniously for each one
        // have each animation trigger its own stop sound
        // then finally have the game run a single peice of code after all the animations have stopped (like a tripple stop check) and play the correct sound based
        // on how many slots are in the right spot.

        switch (prize)
		{
            case 0: Debug.Log("How in the fuck");
                break;
            case 1: playerStats.currentClass.hitPoints += 1; userClass.hitPoints = playerStats.currentClass.hitPoints;
                congrats.text = "CONGRAGULATIONS YOU'VE WON";
                result.text = "A +1 to Health";
                playerEffects.clip = generalSounds[3];
                playerEffects.Play();
                break;
            case 2: playerStats.currentClass.damage += 1; userClass.damage = playerStats.currentClass.damage;
                userClass.hitPoints = playerStats.currentClass.hitPoints;
                congrats.text = "CONGRAGULATIONS YOU'VE WON";
                result.text = "A +1 to Damage";
                playerEffects.clip = generalSounds[3];
                playerEffects.Play();
                break;
            case 3: playerStats.currentClass.moveSpeed += 1; userClass.moveSpeed = playerStats.currentClass.moveSpeed;
                userClass.hitPoints = playerStats.currentClass.hitPoints;
                congrats.text = "CONGRAGULATIONS YOU'VE WON";
                result.text = "A +1 to Move Speed";
                playerEffects.clip = generalSounds[3];
                playerEffects.Play();
                break;
            case 4: playerStats.currentClass.attackRate -= 0.05f; userClass.hitPoints = playerStats.currentClass.hitPoints;
                congrats.text = "CONGRAGULATIONS YOU'VE WON";
                result.text = "A +1 to Reload";
                playerEffects.clip = generalSounds[3];
                playerEffects.Play();
                break;
            case 5: playerStats.currentClass.hitPoints += 2; userClass.hitPoints = playerStats.currentClass.hitPoints;
                userClass.hitPoints = playerStats.currentClass.hitPoints;
                congrats.text = "CONGRAGULATIONS YOU'VE WON";
                result.text = "A +2 to Health";
                playerEffects.clip = generalSounds[3];
                playerEffects.Play();
                break;
            case 6: playerStats.currentClass.damage += 2; userClass.damage = playerStats.currentClass.damage;
                userClass.hitPoints = playerStats.currentClass.hitPoints;
                congrats.text = "CONGRAGULATIONS YOU'VE WON";
                result.text = "A +2 to Damage";
                playerEffects.clip = generalSounds[3];
                playerEffects.Play();
                break;
            case 7: playerStats.currentClass.moveSpeed += 2; userClass.moveSpeed = playerStats.currentClass.moveSpeed;
                userClass.hitPoints = playerStats.currentClass.hitPoints;
                congrats.text = "CONGRAGULATIONS YOU'VE WON";
                result.text = "A +2 to Move Speed";
                playerEffects.clip = generalSounds[3];
                playerEffects.Play();
                break;
            case 8: playerStats.currentClass.attackRate -= 0.05f; userClass.hitPoints = playerStats.currentClass.hitPoints;
                congrats.text = "CONGRAGULATIONS YOU'VE WON";
                result.text = "A +1 to Reload"; 
                playerEffects.clip = generalSounds[3];
                playerEffects.Play();
                break;
            case 9: playerStats.currentClass.hitPoints += 3; userClass.hitPoints = playerStats.currentClass.hitPoints;
                congrats.text = "CONGRAGULATIONS YOU'VE WON";
                result.text = "A +3 to health";
                playerEffects.clip = generalSounds[2];
                playerEffects.Play();
                break;
            case 10: playerStats.currentClass.damage += 3; userClass.damage = playerStats.currentClass.damage;
                userClass.hitPoints = playerStats.currentClass.hitPoints;
                congrats.text = "CONGRAGULATIONS YOU'VE WON";
                result.text = "A +3 to Damage";
                playerEffects.clip = generalSounds[2];
                playerEffects.Play();
                break;
            case 11: playerStats.currentClass.moveSpeed += 3; userClass.moveSpeed = playerStats.currentClass.moveSpeed;
                userClass.hitPoints = playerStats.currentClass.hitPoints;
                congrats.text = "CONGRAGULATIONS YOU'VE WON";
                result.text = "A +3 to Move Speed";
                playerEffects.clip = generalSounds[2];
                playerEffects.Play();
                break;
            case 12:
                playerStats.currentClass.hitPoints += 3; userClass.hitPoints = playerStats.currentClass.hitPoints;
                playerStats.currentClass.damage += 3; userClass.damage = playerStats.currentClass.damage;
                playerStats.currentClass.moveSpeed += 3; userClass.moveSpeed = playerStats.currentClass.moveSpeed;
                playerStats.currentClass.attackRate -= 0.05f; userClass.hitPoints = playerStats.currentClass.hitPoints;
                congrats.text = "CONGRAGULATIONS YOU'VE WON";
                result.text = "A +3 to All Stats";
                playerEffects.clip = generalSounds[2];
                playerEffects.Play();
                break;
		}
        slots[0].sprite = results[x - 1];
        slots[1].sprite = results[y - 1];
        slots[2].sprite = results[z - 1];



        spinButton.gameObject.SetActive(false);
        resumeButton.gameObject.SetActive(true);
        

    }

    public void EndLevelUp()
	{
        levelUpPanel.gameObject.SetActive(false);
        Time.timeScale = 1;
        result.text = "";
        spinButton.gameObject.SetActive(true);
        resumeButton.gameObject.SetActive(false);
	}

    public void DisplayHearts(int health)
	{
        health = Mathf.Clamp(health, 0, 8);

        switch(health)
		{
            case 0:
                hearts[0].gameObject.SetActive(false);
                hearts[1].gameObject.SetActive(false);
                hearts[2].gameObject.SetActive(false);
                hearts[3].gameObject.SetActive(false);
                hearts[4].gameObject.SetActive(false);
                hearts[5].gameObject.SetActive(false);
                hearts[6].gameObject.SetActive(false);
                hearts[7].gameObject.SetActive(false);
                break;
            case 1:
                hearts[0].gameObject.SetActive(true);
                hearts[1].gameObject.SetActive(false);
                hearts[2].gameObject.SetActive(false);
                hearts[3].gameObject.SetActive(false);
                hearts[4].gameObject.SetActive(false);
                hearts[5].gameObject.SetActive(false);
                hearts[6].gameObject.SetActive(false);
                hearts[7].gameObject.SetActive(false);
                break;
            case 2:
                hearts[0].gameObject.SetActive(true);
                hearts[1].gameObject.SetActive(true);
                hearts[2].gameObject.SetActive(false);
                hearts[3].gameObject.SetActive(false);
                hearts[4].gameObject.SetActive(false);
                hearts[5].gameObject.SetActive(false);
                hearts[6].gameObject.SetActive(false);
                hearts[7].gameObject.SetActive(false);
                break;
            case 3:
                hearts[0].gameObject.SetActive(true);
                hearts[1].gameObject.SetActive(true);
                hearts[2].gameObject.SetActive(true);
                hearts[3].gameObject.SetActive(false);
                hearts[4].gameObject.SetActive(false);
                hearts[5].gameObject.SetActive(false);
                hearts[6].gameObject.SetActive(false);
                hearts[7].gameObject.SetActive(false);
                break;
            case 4:
                hearts[0].gameObject.SetActive(true);
                hearts[1].gameObject.SetActive(true);
                hearts[2].gameObject.SetActive(true);
                hearts[3].gameObject.SetActive(true);
                hearts[4].gameObject.SetActive(false);
                hearts[5].gameObject.SetActive(false);
                hearts[6].gameObject.SetActive(false);
                hearts[7].gameObject.SetActive(false);
                break;
            case 5:
                hearts[0].gameObject.SetActive(true);
                hearts[1].gameObject.SetActive(true);
                hearts[2].gameObject.SetActive(true);
                hearts[3].gameObject.SetActive(true);
                hearts[4].gameObject.SetActive(true);
                hearts[5].gameObject.SetActive(false);
                hearts[6].gameObject.SetActive(false);
                hearts[7].gameObject.SetActive(false);
                break;
            case 6:
                hearts[0].gameObject.SetActive(true);
                hearts[1].gameObject.SetActive(true);
                hearts[2].gameObject.SetActive(true);
                hearts[3].gameObject.SetActive(true);
                hearts[4].gameObject.SetActive(true);
                hearts[5].gameObject.SetActive(true);
                hearts[6].gameObject.SetActive(false);
                hearts[7].gameObject.SetActive(false);
                break;
            case 7:
                hearts[0].gameObject.SetActive(true);
                hearts[1].gameObject.SetActive(true);
                hearts[2].gameObject.SetActive(true);
                hearts[3].gameObject.SetActive(true);
                hearts[4].gameObject.SetActive(true);
                hearts[5].gameObject.SetActive(true);
                hearts[6].gameObject.SetActive(true);
                hearts[7].gameObject.SetActive(false);
                break; 
            case 8:
                hearts[0].gameObject.SetActive(true);
                hearts[1].gameObject.SetActive(true);
                hearts[2].gameObject.SetActive(true);
                hearts[3].gameObject.SetActive(true);
                hearts[4].gameObject.SetActive(true);
                hearts[5].gameObject.SetActive(true);
                hearts[6].gameObject.SetActive(true);
                hearts[7].gameObject.SetActive(true);
                break;

		}
	}
}
