using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewClassData", menuName = "ScriptableObjects/ClassData")]
public class ClassData : ScriptableObject

{
	public string className;
	public bool isDead;
	public Sprite characterIcon;
	public Sprite weaponIcon;
	public AudioClip hurtSound;
	public AudioClip weaponSound;

	public int expToLevel;

	[Header("Stats")]
	public int hitPoints;
	public float moveSpeed;
	public float bulletSpeed;
	public float attackRate;
	public int damage;
	public int defense;


}
