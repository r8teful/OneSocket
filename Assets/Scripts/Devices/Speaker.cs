using TMPro;
using UnityEngine;

public class Speaker : PlugDevice {
    [SerializeField] private TextMeshProUGUI _chargesText;
    [SerializeField] private AudioSource _audioSource;
    private int _charges = 3;

    protected override void OnActivate() {
        if (_charges <= 0) return;
        _audioSource.Play();
        
        if(Monster.Instance!=null) Monster.Instance.Scare();
        _charges--;
        _chargesText.text=_charges.ToString();
        base.OnActivate();
    }

    protected override void OnDeactivate() {
        _audioSource.Stop(); 
        base.OnDeactivate();
    }
}