using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using SnakeBotAdvanced.Objects;
using SnakeGame.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace SnakeBotAdvanced.Controllers
{
    [ApiController]
    public class GameController : ControllerBase
    {
        private AStarPathFinder tmp;

        [HttpPost]
        [Route("start")]
        public StartSettingInfo Start(StartGettingInfo startInfo)
        {
            return new StartSettingInfo();
        }

        [HttpPost]
        [Route("move")]
        public MoveSettingInfo Move(MoveGettingInfo moveInfo)
        {
            tmp = new AStarPathFinder();
            return new MoveSettingInfo() { Move = tmp.GetTheWay(moveInfo) };
        }
    }
}
