using UnityEngine;

namespace Game.Animations
{
    public class DangerExclamation : MonoBehaviour
    {
        [SerializeField] private float animationTime;
        [SerializeField] private bool animateHorizontal;

        GameObject thisGameObject;
        Transform thisTransform;

        private void Awake()
        {
            thisGameObject = gameObject;
            thisTransform = transform;
        }

        private void OnEnable()
        {
            thisTransform.localScale = Vector3.one;

            if (animateHorizontal)
                AnimateExclamationLeft();
            else
                AnimateExclamationUp();
        }

        public void AnimateExclamationLeft()
        {
            LeanTween.scaleX(thisGameObject, -1f, animationTime).setOnComplete(AnimateExclamationRight);
        }

        public void AnimateExclamationRight()
        {
            LeanTween.scaleX(thisGameObject, 1f, animationTime).setOnComplete(AnimateExclamationLeft);
        }

        public void AnimateExclamationUp()
        {
            LeanTween.scaleY(thisGameObject, -1f, animationTime).setOnComplete(AnimateExclamationDown);
        }

        public void AnimateExclamationDown()
        {
            LeanTween.scaleY(thisGameObject, 1f, animationTime).setOnComplete(AnimateExclamationUp);
        }
    }
}
