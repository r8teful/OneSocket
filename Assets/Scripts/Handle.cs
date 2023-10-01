using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Handle : MonoBehaviour {

    [SerializeField] private float _rotationSpeed = 1;
    [SerializeField] private float _chargeSpeed = 1;
    private bool _isInsideTrigger = false;

    // Start is called before the first frame update
    void Start() {
        // Get parent
    }


    private void OnTriggerEnter(Collider other) {
        _isInsideTrigger = true;
        
        // Todo say that I'm hovering
    }

    private void OnTriggerExit(Collider other) {
        _isInsideTrigger = false;
        // Todo say that I'm not hovering anymore
    }

    // Update is called once per frame
    private void Update() {
        if (Input.GetMouseButton(0) && _isInsideTrigger) {
            // When holding down, charge the battery
            transform.Rotate(transform.forward, _rotationSpeed * Time.deltaTime);
            // Increment the battery charge
            Generator.Instance.GeneratorCharge += _chargeSpeed * Time.deltaTime;    
            // Play sound
            if(!Generator.Instance.Rotating.isPlaying) {
                Generator.Instance.Rotating.Play();
            } 
        } else {
            Generator.Instance.Rotating.Stop();
        }
    }
}
