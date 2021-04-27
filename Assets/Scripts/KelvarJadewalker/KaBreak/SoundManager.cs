using UnityEngine;

namespace KelvarJadewalker.KaBreak
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private AudioClip brickHitClip = null;

        private AudioSource[] _audioSources;

        private void Awake()
        {
            _audioSources = GetComponents<AudioSource>();
        }

        // Start is called before the first frame update
        private void Start()
        {
            Debug.Log("Number of Audio Sources Found :" + _audioSources.Length);
        }

        public void PlayBrickSound()
        {
            foreach (var source in _audioSources)
            {
                if (source.isPlaying) continue;
                source.PlayOneShot(brickHitClip);
                return;
            }
        }

        
    }
}
