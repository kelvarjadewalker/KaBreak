using System.Collections;
using UnityEngine;

namespace KelvarJadewalker.KaBreak.bricks
{
    public class Explosion : MonoBehaviour
    {
        private readonly WaitForSeconds timeOnScreen = new WaitForSeconds(5f);
        private void Start()
        {
            StartCoroutine(ExplosionDelay());
        }

        // I like this as a 'clean' way to auto destroy the game object
        private IEnumerator ExplosionDelay()
        {
            yield return timeOnScreen;
            Destroy(gameObject);
        }
    }
}
