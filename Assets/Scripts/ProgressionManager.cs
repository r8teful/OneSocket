using UnityEngine;

public class ProgressionManager : PersistentSingleton<ProgressionManager> {

    // 0 is not complete. 1 is complete
    private int _completedTutorial;

    public int CompletedTutorial {
        get { return _completedTutorial; }
        set { _completedTutorial = value;
              PlayerPrefs.SetInt("Tutorial", value);
        }
    }

    protected override void Awake() {
        base.Awake();
        if (!PlayerPrefs.HasKey("Tutorial")) return;
        _completedTutorial = PlayerPrefs.GetInt("Tutorial");    
    }

#if UNITY_EDITOR
    [Button("Clear PlayerPrefs")]
    public void ClearPlayerPrefs() {
        PlayerPrefs.DeleteAll();
    }
#endif
}