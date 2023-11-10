using System.Collections;

public class SequencePlay : Sequencer {
    protected override IEnumerator Sequence() {
        Generator.Instance.EnableDischarge();
        GameManager.Instance.EnableMonster();
        return base.Sequence();
    }
}