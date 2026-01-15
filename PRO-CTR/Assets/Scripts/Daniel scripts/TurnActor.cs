using System;
using UnityEngine;

namespace Daniel_scripts
{
    public abstract class TurnActor : MonoBehaviour
    {
        public Action OnTurnFinished;

        public abstract void StartTurn();
    }
}