using System.Collections;

public class SequencePlay : Sequencer {
    protected override IEnumerator Sequence() {
        Generator.Instance.EnableDischarge();
        GameManager.Instance.SetMonster(true);
        GameManager.Instance.ResetClockTime();
        GameManager.Instance.SetSpeakerCharge(3);
        return base.Sequence();
    }
}