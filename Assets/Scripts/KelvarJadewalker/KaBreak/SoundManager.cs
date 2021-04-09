using UnityEngine;

namespace KelvarJadewalker.KaBreak
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private AudioClip brickHitClip = null;

        private AudioSource[] audioSources;

        private void Awake()
        {
            audioSources = GetComponents<AudioSource>();
            
           
        }

        // Start is called before the first frame update
        private void Start()
        {
            Debug.Log("Number of Audio Sources Found :" + audioSources.Length);
        }

        public void PlayBrickSound()
        {
            foreach (var source in audioSources)
            {
                if (source.isPlaying) continue;
                source.PlayOneShot(brickHitClip);
                return;
            }
        }

        
    }
}
