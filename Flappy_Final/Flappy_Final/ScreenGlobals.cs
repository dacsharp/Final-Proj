using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flappy_Final
{
    class ScreenGlobals
    {

        public const int GAME_SPEED = 150;
        public const int SCREEN_SCALE = 3;
        public const int SCREEN_WIDTH = 256 * SCREEN_SCALE;
        public const int SCREEN_HEIGHT = 180 * SCREEN_SCALE;
        public const float BULLET_SPEED_X = GAME_SPEED * 2;
        public const float BULLET_SPEED_Y = GAME_SPEED * 2;

        public const string PLAYER_ASSETNAME = "!$ReBat";
       public  const string PLAYER_NEXT_ASSETNAME = "!$ReBat_se";
        public const string PLAYER_FINAL_ASSETNAME = "!$ReBat_xe";
    }
}
