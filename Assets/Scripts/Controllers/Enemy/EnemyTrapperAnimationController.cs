using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    public class EnemyTrapperAnimationController : MonoBehaviour
    {
        Animator animator;
        int openMouthHash = Animator.StringToHash("OpenMouth");
        int closingMouth = Animator.StringToHash("CloseMouth");

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        public void PlayOpenMouth()
        {
            animator.SetTrigger(openMouthHash);
        }

        public void PlayClosingMouth()
        {
            animator.SetTrigger(closingMouth);
        }
    }
}
