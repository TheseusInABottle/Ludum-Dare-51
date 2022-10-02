using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsController : MonoBehaviour
{

    public ClassData[] defaultRoleData; // The default role data that we are copying from
    public ClassData warrior; // the warrior class
    public ClassData wizard; // the wizard class
    public ClassData rogue; // the rogue class

    public ClassData currentClass; // the current class of the player
    public bool classChanged; // A bool to tell if the class changed

    public GameObject gameplayController;

    [Header("Ten Second Class Change")]
    private float TENSECONDS = 10f; // a constant that is the amount of time between the player swapping classes
    public float timeRemainingTillSwap; // this is used to make the player actually swap classes

    public int classChangeNumber;

	private void Awake()
	{
        classChangeNumber = 0;
        warrior = (ClassData)ClassData.CreateInstance("ClassData"); // creating the class data for the warrior class and giving it the name warrior
        warrior.name = "WarriorData";
        wizard = (ClassData)ClassData.CreateInstance("ClassData");
        wizard.name = "WizardData";
        rogue = (ClassData)ClassData.CreateInstance("ClassData");
        rogue.name = "RogueData";

        AssignDefaultValues(); // asiging our data values from the default values into the chracter data
        timeRemainingTillSwap = TENSECONDS;
        currentClass = warrior;
    }

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timeRemainingTillSwap > 0)
		{
            timeRemainingTillSwap -= Time.deltaTime;
		}
        else if (timeRemainingTillSwap <= 0)
		{
            SwapClasses();
		}
    }

    private void AssignDefaultValues()
	{
        // Set Warrior to default values;
        warrior.className = defaultRoleData[0].className;
        warrior.characterIcon = defaultRoleData[0].characterIcon;
        warrior.weaponIcon = defaultRoleData[0].weaponIcon;
        warrior.hurtSound = defaultRoleData[0].hurtSound;
        warrior.weaponSound = defaultRoleData[0].weaponSound;

        warrior.hitPoints = defaultRoleData[0].hitPoints;
        warrior.moveSpeed = defaultRoleData[0].moveSpeed;
        warrior.bulletSpeed = defaultRoleData[0].bulletSpeed;
        warrior.attackRate = defaultRoleData[0].attackRate;
        warrior.damage = defaultRoleData[0].damage;
        warrior.defense = defaultRoleData[0].defense;

        warrior.expToLevel = 10;


        // Set Wizard to default values;
        wizard.className = defaultRoleData[1].className;
        wizard.characterIcon = defaultRoleData[1].characterIcon;
        wizard.weaponIcon = defaultRoleData[1].weaponIcon;
        wizard.hurtSound = defaultRoleData[1].hurtSound;
        wizard.weaponSound = defaultRoleData[1].weaponSound;

        wizard.hitPoints = defaultRoleData[1].hitPoints;
        wizard.moveSpeed = defaultRoleData[1].moveSpeed;
        wizard.bulletSpeed = defaultRoleData[1].bulletSpeed;
        wizard.attackRate = defaultRoleData[1].attackRate;
        wizard.damage = defaultRoleData[1].damage;
        wizard.defense = defaultRoleData[1].defense;

        wizard.expToLevel = 10;

        // Set Rogue to default values;
        rogue.className = defaultRoleData[2].className;
        rogue.characterIcon = defaultRoleData[2].characterIcon;
        rogue.weaponIcon = defaultRoleData[2].weaponIcon;
        rogue.hurtSound = defaultRoleData[2].hurtSound;
        rogue.weaponSound = defaultRoleData[2].weaponSound;

        rogue.hitPoints = defaultRoleData[2].hitPoints;
        rogue.moveSpeed = defaultRoleData[2].moveSpeed;
        rogue.bulletSpeed = defaultRoleData[2].bulletSpeed;
        rogue.attackRate = defaultRoleData[2].attackRate;
        rogue.damage = defaultRoleData[2].damage;
        rogue.defense = defaultRoleData[2].defense;

        rogue.expToLevel = 10;
    }

    public void SwapClasses()
	{
        classChangeNumber++;

        switch (classChangeNumber)
        {
            case 0:
                if (warrior.isDead == false)
                {
                    currentClass = warrior;
                    classChanged = true;
                }
				else
				{
                    SwapClasses();
				}

                break;

            case 1:
                if (wizard.isDead == false)
                {
                    currentClass = wizard;
                    classChanged = true;
                }
                else
                {
                    SwapClasses();
                }
                break;

            case 2:
                if (rogue.isDead == false)
                {
                    currentClass = rogue;
                    classChanged = true;
                }
                else
                {
                    SwapClasses();
                }
                break;

            case 3:
                if (warrior.isDead == false)
                {
                    currentClass = warrior;
                    classChanged = true;
                    classChangeNumber = 0;
                }
                else
                {
                    SwapClasses();
                }
                break;
            case 4:
                Time.timeScale = 0;
                classChangeNumber = 0;
                gameplayController.GetComponent<MenusController>().isGameOver = true;
                break;
        }
        timeRemainingTillSwap = TENSECONDS;
    }
}
