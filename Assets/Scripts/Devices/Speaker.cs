using TMPro;
using UnityEngine;

public class Speaker : PlugDevice {
    [SerializeField] private TextMeshProUGUI _chargesText;
    [SerializeField] private AudioSource _audioSource;

    private int _charges;
    public int Charges {
        get { return _charges; }
        set { _charges = value;
            _chargesText.text = Charges.ToString();
        }
    }
    private void Start() {
        _charges = 3;
    }

    protected override void OnActivate() {
        if (Charges <= 0) return;
        _audioSource.Play();
        if(Monster.Instance!=null) Monster.Instance.Scare();
        Charges--;
        base.OnActivate();
    }

    protected override void OnDeactivate() {
        _audioSource.Stop(); 
        base.OnDeactivate();
    }
}