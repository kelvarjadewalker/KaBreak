using KelvarJadewalker.KaBreak.bricks;
using UnityEngine;

namespace KelvarJadewalker.KaBreak
{
    public class LevelGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject brickPrefab = null;
        
        

        public void Generate(int level)
        {
            var startX = -7.0f;
            var startY = 3.0f;
            var spacingX = 1.0f;
            var spacingY = 0.5f;
            var maxX = 7.0f;
            var minY = -1.0f;


           var rowY = startY;
           while (rowY >= minY)
            {
                var colX = startX;
                
                while (colX <= maxX)
                {
                    var brickPos = new Vector3(colX, rowY, 0);
                    var brick = Instantiate(brickPrefab, brickPos, brickPrefab.transform.rotation);
                    var brickScript = brick.GetComponent<Brick>();
                
                    // programmatically set the brick type 
                    brickScript.MakeIndestructible();
                
                    colX += spacingX;
                }

                rowY -= spacingY;
            }

            
        }

        
    }
}
