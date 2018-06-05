using System;
using UnityEngine;

// Implementation of WaitWhile yield instruction. This can be later used as:
// yield return new WaitWhile(() => Princess.isInCastle);
namespace Utils
{
    class WaitWhile : CustomYieldInstruction
    {
        readonly Func<bool> _waitChecker;

        public override bool keepWaiting => _waitChecker();

        public WaitWhile(Func<bool> waitChecker) 
        { _waitChecker = waitChecker; }
    }
}