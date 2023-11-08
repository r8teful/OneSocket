using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequencePlay : Sequencer {

    protected override IEnumerator Sequence() {
        Generator.Instance.EnableDischarge();
        return base.Sequence();
    }

}
