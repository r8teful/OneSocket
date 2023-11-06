public class ClickArea : Interactable {

    private PlugDevice _parentScript;
    private bool _canInteract = true;

    private void Awake() {
        _parentScript = GetComponentInParent<PlugDevice>();
    }

    protected override void OnMouseDown() {
        if (_canInteract) {
            _parentScript.OnPlugClicked();
        }
    }

    protected override void OnMouseEnter() {
        if (_canInteract) {
            base.OnMouseExit();
        }
    }

    protected override void OnMouseExit() {
        if (_canInteract) {
            base.OnMouseExit();
        }
    }


    public void SetInteraction(bool b) {
        _canInteract = b;
    }
}
