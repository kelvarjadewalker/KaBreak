using KelvarJadewalker.KaBreak.bricks;
using UnityEngine;

namespace KelvarJadewalker.KaBreak
{
    public class LevelGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject brickPrefab = null;
        [Range(1,5)][SerializeField] private int maximumIndestructible = 4;
        [Range(1,5)][SerializeField] private int maximumIndestructibleInRow = 2;
        [Range(1,5)][SerializeField] private int destructiveChance = 5;

        private int destructibleInLevel = 0;

        public void Generate(int level)
        {
            // by default a blue brick will be generated unless we step in
            
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
                var destructibleInRow = 0;
                
                while (colX <= maxX)
                {
                    var brickPos = new Vector3(colX, rowY, 0);
                    var brick = Instantiate(brickPrefab, brickPos, brickPrefab.transform.rotation);
                    var brickScript = brick.GetComponent<Brick>();

                    // programmatically set the brick type 
                    // very small chance the brick will be destructible
                    if (destructibleInLevel < maximumIndestructible && destructibleInRow < maximumIndestructibleInRow)
                    {
                        var choice = Random.Range(0, 100);
                        if (choice <= destructiveChance)
                        {
                            brickScript.MakeIndestructible();
                            destructibleInRow++;
                            destructibleInLevel++;
                            continue;
                        }
                        
                       
                    }
                
                    // We did not set the brick to indestructible
                    switch (level)
                    {
                        case 1:
                            break;
                        case 2:
                            break;
                        case 3:
                            break;
                        case 4:
                            break;
                        case 5:
                            break;
                        
                    }
                    
                
                    colX += spacingX;
                }

                rowY -= spacingY;
            }

            
        }

        
    }
}
