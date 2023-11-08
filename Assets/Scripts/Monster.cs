using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

// Should probably not be a static instance incase we want several monsters, usefull for now
public class Monster : StaticInstance<Monster> {

    // Positions are z axis closes to furthest
    public float BaseMovementSpeed = 0.01f;
    private float _jumpProbability = 0.02f;
    private float _jumpDistance = 0.5f;
    private bool _dead;
    private void Start() {
        // start coroutine
        StartCoroutine(AttackCheck());
        // todo Start random sound coroutine
        //StartCoroutine(RandomSound());
    }

    private void FixedUpdate() {
        // Only move when not lookig. Can sometimes have random jumps, both forwads and backwards
        if (CameraMovement.Instance.LookState.Equals(CameraMovement.CameraState.Room)) return;
        if(Random.value < _jumpProbability) {
            var jumpDir = Random.Range(-1f, 1f);
            transform.Translate(0,0, jumpDir*_jumpDistance);
        } else {
            transform.Translate(0f, 0f, -BaseMovementSpeed);
        }
    }

    private IEnumerator AttackCheck() {
        while (true) {
            // Roll a dice
            var r = Random.value;
            var c = AttackChance(transform.position.z);
            //Debug.Log("Attack chance is " + c + " and rolled " + r);
            if (r < c) AttackPlayer();
            yield return new WaitForSeconds(1f);
        }
    }

    private float AttackChance(float z) {
        if (CameraMovement.Instance.LookState.Equals(CameraMovement.CameraState.Room)) {
            // Chances when player is looking directly at the monster these are the lowest values
            // Never scare when looking in the room
            return 0f;
            //if (z > -12) return 0f;
            //if (z <= -12 && z > -14) return 0f;
            //if (z <= -14 && z > -18) return 0.1f;
            //if (z <= -18 && z > -20) return 0.4f;
            //if (z <= -20) return 1;
        } else if (CameraMovement.Instance.LookState.Equals(CameraMovement.CameraState.Code)) {
            // Chances when player is looking away from the monster these are the highest values
            if (z > -12) return 0f;
            if (z <= -12 && z > -14) return 0.01f;
            if (z <= -14 && z > -18) return 0.1f;
            if (z <= -18 && z > -20) return 0.6f;
            if (z <= -20) return 1f;
        } else {
            if (z > -12) return 0f;
            if (z <= -12 && z > -14) return 0.005f;
            if (z <= -14 && z > -18) return 0.15f;
            if (z <= -18 && z > -20) return 0.5f;
            if (z <= -20) return 1f;
            return 0f;
        }
        return 0f;
    }
   

    private void AttackPlayer() {
        // Jumpscare!
        Debug.Log("BOO. YOU DIED");
        if (!_dead) {
            JumpScare.Instance.Scare();
            _dead = true;
            StartCoroutine(Lose());
            
        }
    }
    private IEnumerator Lose() {
        yield return new WaitForSeconds(4f);
        GameManager.Instance.GameLose();
        //GameManager.Instance.GameLose = true;
    }

    public void Scare() {
        if(transform.position.z < -20) return;
        transform.position = new Vector3(transform.position.x, transform.position.y, -5);
    }
}
