using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequencePlay : Sequencer {

    protected override IEnumerator Sequence() {

        // TODO doesn't actually enable it 
        Generator.Instance.EnableDischarge();
        return base.Sequence();
    }

}
