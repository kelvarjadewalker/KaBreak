using KelvarJadewalker.KaBreak.structs;
using UnityEngine;

namespace KelvarJadewalker.KaBreak
{
    public class Paddle : MonoBehaviour
    {
        [SerializeField] private Vector3 startingPosition = new Vector3(0, 0, 0);
        [SerializeField] private float speed = 1.0f;
        [SerializeField] private Boundary boundary = new Boundary();
         
        
        // Start is called before the first frame update
        private void Start()
        {
            transform.position = startingPosition;
        }

        // Update is called once per frame
        private void Update()
        {
            MoveByKeyboard();
        }

        private void MoveByKeyboard()
        {
            var xMovement = Input.GetAxis("Horizontal");
            const float yMovement = 0.0f;
            const float zMovement = 0.0f;

            var currentPosition = transform.position;

            var newPosition = new Vector3(xMovement, yMovement, zMovement);
            currentPosition +=  newPosition * (speed * Time.deltaTime);
            currentPosition.x = Mathf.Clamp(currentPosition.x, boundary.left, boundary.right);
    
            transform.position = currentPosition;
        }
    }
}
