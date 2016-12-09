using UnityEngine;

namespace Assets.Scripts.SharedLib.UI.Animations
{
    public class AnimationPlayer : MonoBehaviour
    {
        public Animation Animation;

        public bool ToStartOnAwake = true;

        public bool IsAtEnd { get { return _isAtEnd; } }

        public float Length
        {
            get
            {
                if (_animation) return _animation.length;
                return 0;
            }
        }

        public float AnimationSpeed = 1;
        
        [SerializeField]
        private string AnimationName;

        private bool _isAtEnd = false;

        private AnimationState _animation;

        void Awake()
        {
            Animation.wrapMode = WrapMode.Once;
            _animation = Animation[AnimationName];
            if (ToStartOnAwake)
                ResetToStart();
        }

        public void ResetToStart()
        {
            if (_animation == null) return;
            _animation.time = 0;
            Animation.Stop(AnimationName);
            _isAtEnd = false;
        }

        public void ResetToEnd()
        {
            if (_animation == null) return;
            _animation.time = _animation.length - 0.01f;
            Animation.Play(AnimationName);
            _isAtEnd = true;
        }

        public void PlayBackward()
        {
			//if (_isAtEnd)
			//{
            if (_animation == null)
                return;

                _animation.speed = -AnimationSpeed;
				_animation.time = _animation.length - 0.01f;
				Animation.Play(AnimationName);
				_isAtEnd = false;
			//}
        }

        public void PlayForward()
        {
            if (_animation == null)
                return;
			//if (!_isAtEnd)
			//{
                _animation.speed = AnimationSpeed;
				Animation.Play(AnimationName);
				_isAtEnd = true;
			//}
        }

        public void PlayPingStopPong()
        {
            if(_isAtEnd)
                PlayBackward();
            else
                PlayForward();
        }
    }
}
