using UnityEngine;

public class Handle : Interactable {

    [SerializeField] private float _rotationSpeed = 1;
    [SerializeField] private float _chargeSpeed = 1;
    private bool _isInsideTrigger = false;

    protected override void OnMouseDown() {
        _isInsideTrigger = true;
    }

    private void OnMouseDrag() {
    }
    private void OnMouseUp() {
        _isInsideTrigger = false;
    }


    // Update is called once per frame
    private void Update() {
        if (Input.GetMouseButton(0) && _isInsideTrigger) {
            // When holding down, charge the battery
            transform.Rotate(transform.forward, _rotationSpeed * Time.deltaTime);
            // Increment the battery charge, but only if not already max charge
            if(!Generator.Instance.IsMaxCharge) Generator.Instance.GeneratorCharge += _chargeSpeed * Time.deltaTime;    
            // Play sound
            if(!Generator.Instance.Rotating.isPlaying) {
                Generator.Instance.Rotating.Play();
            } 
        } else {
            Generator.Instance.Rotating.Stop();
        }
    }
}
