public class ClickArea : Interactable {

    private PlugDevice _parentScript;
    private bool _isInsideTrigger = false;

    private void Awake() {
        _parentScript = GetComponentInParent<PlugDevice>();
    }

    protected override void OnMouseDown() {
        _parentScript.OnPlugClicked();
    }
}
