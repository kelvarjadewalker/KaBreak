using UnityEngine;

namespace KelvarJadewalker.KaBreak
{
    public class Paddle : MonoBehaviour
    {
        [SerializeField] private Vector3 startingPosition = new Vector3(0, 0, 0);
        [SerializeField] private float speed = 1.0f;
         
        
        // Start is called before the first frame update
        private void Start()
        {
            transform.position = startingPosition;
        }

        // Update is called once per frame
        private void Update()
        {
            var xMovement = Input.GetAxis("Horizontal");
            const float yMovement = 0.0f;
            const float zMovement = 0.0f;

            var newPosition = new Vector3(xMovement, yMovement, zMovement);
            
            
            transform.position +=  newPosition * (speed * Time.deltaTime);
        }
    }
}
