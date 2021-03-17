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
           var rowcount = 1;
           var rowFactor = 10;
           var brickLevel = level;
           var rowsPerLevel = rowFactor / level;
           var rowsThisLevel = 0;
           
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
                    // The last level reserves the top row for the super speed bricks
                    
                    if (level == 5 && rowcount == 1)
                    {
                        brickScript.SetBrickLevel(level);
                    }
                    else if (level < 5 && rowcount == 1)
                    {
                        // The top row is always set to the max level the player is on
                        brickScript.SetBrickLevel(level);
                    }
                    else
                    {
                        brickScript.SetBrickLevel(brickLevel); 
                    }
                    
                    // very small chance the brick will be indestructible
                    if (destructibleInLevel < maximumIndestructible && destructibleInRow < maximumIndestructibleInRow)
                    {
                        var choice = Random.Range(0, 100);
                        if (choice <= destructiveChance)
                        {
                            brickScript.MakeIndestructible();
                            destructibleInRow++;
                            destructibleInLevel++;
                        }
                    }
                
                    colX += spacingX;
                }

                rowY -= spacingY;
                rowFactor--;
                rowcount++;
                rowsThisLevel++;

                if (rowsThisLevel < rowsPerLevel) continue;
                brickLevel--;
                rowsThisLevel = 0;
                if (brickLevel < 1) brickLevel = 1;
            }

            
        }

        
    }
}
