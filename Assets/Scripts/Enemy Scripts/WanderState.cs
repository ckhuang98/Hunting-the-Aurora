﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class WanderState : BaseState
{
    private Enemy _enemy;
    private float speed = 1f;
    private Vector2 decisionTime = new Vector2(1, 4);
    internal float decisionTimeCount = 0f;
    private bool choice;
    private float thirty = 30f;

    private GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

    bool hasMoved = false;
    /*
    Purpose: constructor recieves all needed values from enemy class, sets the
    first time when to change direction, and sets the first direction to move in
    Recieves: the enemy class from the enemy.cs file
    Returns: nothing
    */
    public WanderState(Enemy enemy) : base(enemy.gameObject)
    {
        _enemy = enemy;
        decisionTimeCount = UnityEngine.Random.Range(decisionTime.x, decisionTime.y);
        ChooseMoveDirection();
    }


    /*
    Purpose: moves the enemy in the current direction chosen and calls the change
    direction function when the move time is up. If the enemy wanders inside the 
    bounds of the player, chase state is started.
    Recieves: nothing
    Returns: the type of the wander state constatntly, until in the players bounds.
    The type of the chase state is returned.
    */
    public override Type Tick()
    {
        transform.position += _enemy.moveDirections[_enemy.currMoveDirection] * Time.deltaTime * speed;
        
        if (decisionTimeCount >= 0)
        {
            decisionTimeCount -= Time.deltaTime;
            
        } else
        {
            if (hasMoved == false) {
                hasMoved = true;
            }
            decisionTimeCount = UnityEngine.Random.Range(decisionTime.x, decisionTime.y);
            ChooseMoveDirection();
        }

        foreach (GameObject __enemy in enemies) {
            if (__enemy != null) {
                float currentDistance = Vector3.Distance(transform.position, __enemy.transform.position);
                if (currentDistance < 2.0f)
                {
                Vector3 dist = transform.position - __enemy.transform.position;
                transform.position += dist * Time.deltaTime;
                } 
            }
        }

        WallDetection();

        if (_enemy.inBounds == true) {
            //return typeof(ChaseState);
        }
        return typeof(WanderState);
    }

    /*
    Purpose: Changes the current move direction of the enemy to a new one
    Recieves: nothing
    Returns: nothing
    */
    private void ChooseMoveDirection()
    { 
        //Random Movement at first call
        if (hasMoved == false) {
            int index = Mathf.FloorToInt(UnityEngine.Random.Range(0, _enemy.moveDirections.Length));
            //int index = 0;
            _enemy.weightList[index] = 1;
            //return index;
        } else {

            /*
            var printArr = "AFTER: [ ";
            for (int i = 0; i < _enemy.moveDirections.Count(); i ++) {
                printArr += _enemy.weightList[i].ToString() + ", ";
            }
            printArr += " ]";
            Debug.Log(printArr);
            */

            // UP
            if (_enemy.weightList[0] == 1) {
                choice = (UnityEngine.Random.value > 0.5f);
                _enemy.weightList[0] = 0;
                if (choice == true) {
                    // Turn right up
                    _enemy.weightList[1] = 1;
                }      
                else {
                    // Turn left up
                    _enemy.weightList[7] = 1;
                }
            }

            // RIGHT UP
            if (_enemy.weightList[1] == 1) {
                choice = (UnityEngine.Random.value > 0.5f);
                _enemy.weightList[1] = 0;
                if (choice == true) {
                    // Turn up
                    _enemy.weightList[0] = 1;
                }      
                else {
                    // Turn right
                    _enemy.weightList[2] = 1;
                }
            }

            // RIGHT
            if (_enemy.weightList[2] == 1) {
                choice = (UnityEngine.Random.value > 0.5f);
                _enemy.weightList[2] = 0;
                if (choice == true) {
                    // Turn right up
                    _enemy.weightList[1] = 1;
                }      
                else {
                    // Turn right down
                    _enemy.weightList[3] = 1;
                }
            }

            // RIGHT DOWN
            if (_enemy.weightList[3] == 1) {
                choice = (UnityEngine.Random.value > 0.5f);
                _enemy.weightList[3] = 0;
                if (choice == true) {
                    // Turn right
                    _enemy.weightList[2] = 1;
                }      
                else {
                    // Turn down
                    _enemy.weightList[4] = 1;
                }
            }

            // DOWN
            if (_enemy.weightList[4] == 1) {
                choice = (UnityEngine.Random.value > 0.5f);
                _enemy.weightList[4] = 0;
                if (choice == true) {
                    // Turn down right
                    _enemy.weightList[3] = 1;
                }      
                else {
                    // Turn down left
                    _enemy.weightList[5] = 1;
                }
            }

            // LEFT DOWN
            if (_enemy.weightList[5] == 1) {
                choice = (UnityEngine.Random.value > 0.5f);
                _enemy.weightList[5] = 0;
                if (choice == true) {
                    // Turn left
                    _enemy.weightList[6] = 1;
                }      
                else {
                    // Turn down
                    _enemy.weightList[4] = 1;
                }
            }

            // LEFT
            if (_enemy.weightList[6] == 1) {
                choice = (UnityEngine.Random.value > 0.5f);
                _enemy.weightList[6] = 0;
                if (choice == true) {
                    // Turn left up
                    _enemy.weightList[7] = 1;
                }      
                else {
                    // Turn left down
                    _enemy.weightList[5] = 1;
                }
            }

            // LEFT UP
            if (_enemy.weightList[7] == 1) {
                choice = (UnityEngine.Random.value > 0.5f);
                _enemy.weightList[7] = 0;
                if (choice == true) {
                    // Turn up
                    _enemy.weightList[0] = 1;
                }      
                else {
                    // Turn left
                    _enemy.weightList[6] = 1;
                }
            }
            
        }

        for (int i = 0; i < _enemy.moveDirections.Count(); i++) {
            //Debug.Log("in da mf loop");
            if (_enemy.weightList[i] == 1) {
                _enemy.currMoveDirection = i;
                //Debug.Log("weight of " + i + " is == 1!");
            }
        }
        return;
    }

    private void WallDetection() {
        for (int i = 0; i < _enemy.moveDirections.Count(); i ++) {
            if (_enemy.castList[i].collider != null) {
                
                if (_enemy.castList[i].distance <= 1) {                    
                    var about_face = i;
                    /*
                    about_face += 4;
                    if (about_face >= 8) {
                        about_face = about_face % 8;
                    }
                    */
                    if (about_face >= 4) {
                        about_face -= 4;
                    } else if (about_face < 4) {
                        about_face += 4;
                    }
                    decisionTimeCount = UnityEngine.Random.Range(decisionTime.x, decisionTime.y);
                    _enemy.weightList[about_face] = 1;
                    _enemy.currMoveDirection = about_face;
                }
            }
        }
    }

    private void NPCDetection() {
        
    }
    
}