using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CodeHandler : StaticInstance<CodeHandler> {
  
    [SerializeField] private TextMeshProUGUI _codeText;
    //[SerializeField] private TMP_Text _displayText;
    [SerializeField] private Button[] _numberButtons;
    [SerializeField] private Button _backButton;
    [SerializeField] private Button _enterButton;
    [SerializeField] private AudioSource _pressAudio;
    [SerializeField] private AudioClip _numberClip;
    [SerializeField] private AudioClip _WrongClip;
    [SerializeField] private AudioClip _RightClip;


    private string _currentCode = "";

    private void Start() {
        // Attach button click event handlers
        for (int i = 0; i < _numberButtons.Length; i++) {
            int buttonIndex = i; // Capture the current index
            _numberButtons[i].onClick.AddListener(() => OnNumberButtonClick(buttonIndex));
        }

        _backButton.onClick.AddListener(OnBackButtonClick);
        _enterButton.onClick.AddListener(OnEnterButtonClick);
        
    }

    private void UpdateCodeDisplay() {
        _codeText.text = _currentCode;
       
    }

    public void OnNumberButtonClick(int number) {
        if(_currentCode.Length < 3) {
            _currentCode += number.ToString();
            _pressAudio.clip = _numberClip;
            _pressAudio.Play();
            UpdateCodeDisplay();

        }
    }

    public void OnBackButtonClick() {
        if (_currentCode.Length > 0) {
            _currentCode = _currentCode.Substring(0, _currentCode.Length - 1);
            _pressAudio.clip = _numberClip;
            _pressAudio.Play();
            UpdateCodeDisplay();
        }
    }

    public void OnEnterButtonClick() {
        Debug.Log("e");
        var code = "";
        // Replace this with your code validation logic
        foreach (int num in GameManager.Instance.Code) {
            code += num.ToString();
        }
        if (_currentCode == code) {
            // You did it!
            _pressAudio.clip = _RightClip;
            _pressAudio.Play();
            GameEnd();
        } else {
            // Error sound!
            // Reset the code input
            _pressAudio.clip = _WrongClip;
            _pressAudio.Play();
            _currentCode = "";
            UpdateCodeDisplay();
        }
    }

    private void GameEnd() {
        Debug.Log("YOU DID IT");
        GameManager.Instance.GameWin();
        //GameManager.Instance.GameWin = true;
        //SceneHandler.Instance.GameWin();
    }
}
