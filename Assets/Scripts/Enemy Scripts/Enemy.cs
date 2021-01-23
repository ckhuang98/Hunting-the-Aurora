﻿using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using System;
using System.Linq;

public class Enemy : MonoBehaviour
{
    //Variables regarding enemy stats
    Rigidbody2D rb;
    public float healthAmount;
    public float speed = 1;

    public float timer = 0;

    public GameObject shard;
    //Area of Effect
    public GameObject AOE;
    
    //Target is the players' current location
    private Transform target;
    public bool inBounds = false;
    public bool hasCircled = false;

    private GameObject[] enemyList;
    public static int enemyAmount;
    ////////////////////////////////

    StateMachine stateMachine;

    private float _rayDistance = 3.0f;
    private int layerMask = 1 << 9;
    public RaycastHit2D[] castList = new RaycastHit2D[8];
    public int[] weightList = new int[8];
    //An array carrying all 8 movement options for the enemy
    internal Vector3[] moveDirections = new Vector3[] { Vector3.right, Vector3.left, Vector3.up, Vector3.down, 
        Vector3.Normalize(Vector3.left + Vector3.up), Vector3.Normalize(Vector3.left + Vector3.down),
        Vector3.Normalize(Vector3.right + Vector3.up), Vector3.Normalize(Vector3.right + Vector3.down) };

    // Start is called before the first frame update
    void Start()
    {
        healthAmount = 3f;
        rb = GetComponent<Rigidbody2D>();

        //getting transform component from the Player
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        enemyList = GameObject.FindGameObjectsWithTag("Enemy");
        enemyAmount = enemyList.Length;
        stateMachine = new StateMachine();
        InitializeStateMachine();
        
    }

    // Update is called once per frame
    void Update() {
        isDead(PlayerController.gameOver);
        stateMachine.Update();
        DisplayRays();
    }


    // Deal damage to player on contact
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.name.Equals("SlashSpriteSheet_0") && timer >= .5)
        {
            healthAmount -= collider.transform.parent.parent.GetComponent<PlayerController>().whatIsStrength();
            var thisColor = this.GetComponent<Renderer>().material.color;
            thisColor.a -= .1f;
            this.GetComponent<Renderer>().material.color = thisColor;

            timer = 0;
        }
        
        //check for when players view is overlapping with the enemy
        if (collider.gameObject.name.Equals("View"))
        {
            inBounds = true;
        }
        
        
    }

    void spawnShard() {
        if(UnityEngine.Random.value > .33) {
            GameObject go = (GameObject)Instantiate(shard);
            go.transform.position = this.transform.position;
        }
    }

    void isDead(bool gameOver){
        if (!gameOver) { 
            if (healthAmount <= 0) {
                Destroy(this.gameObject);
                enemyAmount -= 1;
                spawnShard();
            }
            timer += Time.deltaTime; // Temporary
        }
    }

    /*
    Purpose: Initializes the state machine with all the states attached in
    the enemy scripts folder.
    Recieves: Nothing
    Returns: nothing
    */
    void InitializeStateMachine()
    {
        var states = new Dictionary<Type, BaseState>()
        {
            { typeof(WanderState), new WanderState(this) },
            { typeof(ChaseState), new ChaseState(this) },
            { typeof(CircleState), new CircleState(this) },
            { typeof(AttackState), new AttackState(this) }
        };

        stateMachine.SetStates(states);
    }

    private void DisplayRays()
    {
        for (int i = 0; i < moveDirections.Count(); i ++) {
            var rayColor = Color.green;
            Debug.DrawRay(transform.position, moveDirections[i] * _rayDistance, rayColor);
            castList[i] = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), 
                new Vector2(moveDirections[i].x, moveDirections[i].y), _rayDistance, layerMask);
            weightList[i] = 0;
        }
        for (int i = 0; i < moveDirections.Count(); i ++) {
            if (castList[i].collider != null) {
                var rayColor = Color.red;
                Debug.DrawRay(transform.position, moveDirections[i] * _rayDistance, rayColor);
            }
        }
    }
}
