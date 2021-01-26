﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ChaseState : BaseState
{
    private Enemy _enemy;
    //This is the player
    private Transform target;
    public float speed = 3f;
    private bool hasCircled = false;
    private GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

    /*
    Purpose: constructor recieves all needed values from enemy class and recieves
    the transform component from the player.
    Recieves: the enemy class from the enemy.cs file
    Returns: nothing
    */
    public ChaseState(Enemy enemy) : base (enemy.gameObject)
    {
        _enemy = enemy;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    /*
    Purpose: Calls the MoveTowards function in order for the enemy to chase the
    player, If the enemy gets close enough to the player the attack state is started.
    Recieves: nothing
    Returns: the type of the current chase state consistently returned, until the 
    enemy gets close, then the type of the attack state is returned
    */
    public override Type Tick()
    { 
        var delta_x = transform.position.x - target.position.x;
        var delta_y = transform.position.y - target.position.y;
        float angle = Mathf.Atan2(delta_y, delta_x) * 180 / Mathf.PI;
        if (angle < 0.0f) {
            angle = angle + 360f;
        }
        ChasePlayer(angle);

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
    
        if (Vector2.Distance(transform.position, target.position) <= 1) {
            return typeof(AttackState);
        } 

        return typeof(ChaseState);
    }

    private void ChasePlayer(float angle) {

        // LEFT
        if ((angle > 337.5 && angle < 360) || (angle > 0 && angle < 22.5)) {
            transform.position += _enemy.moveDirections[1] * speed * Time.deltaTime;
            _enemy.currMoveDirection = 1;
        }
        // DOWN
        if (angle > 67.5 && angle < 112.5) {
            transform.position += _enemy.moveDirections[3] * speed * Time.deltaTime;
            _enemy.currMoveDirection = 3;
        } 
        // DOWN RIGHT
        if (angle > 112.5 && angle < 157.5) {
            transform.position += _enemy.moveDirections[7] * speed * Time.deltaTime;
            _enemy.currMoveDirection = 7;
        }
        //DOWN LEFT
        if (angle > 22.5 && angle < 67.5) {
            transform.position += _enemy.moveDirections[5] * speed * Time.deltaTime;
            _enemy.currMoveDirection = 5;
        }
        



        // RIGHT
        if (157.5 < angle && angle < 202.5) {
            transform.position += _enemy.moveDirections[0] * speed * Time.deltaTime;
            _enemy.currMoveDirection = 0;
        }
        // RIGHT & UP
        if (202.5 < angle && angle < 247.5) {
            transform.position += _enemy.moveDirections[6] * speed * Time.deltaTime;
            _enemy.currMoveDirection = 6;
        }
        // UP
        if (247.5 < angle && angle < 292.5) {
            transform.position += _enemy.moveDirections[2] * speed * Time.deltaTime;
            _enemy.currMoveDirection = 2;
        }
        // LEFT & UP
        if (292.5 < angle && angle < 337.5) {
            transform.position += _enemy.moveDirections[4] * speed * Time.deltaTime;
            _enemy.currMoveDirection = 4;
        }
    }
}