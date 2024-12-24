using System.Collections.Generic;
using Microsoft.Xna.Framework;


namespace GameDevProject.Entities
{
    public class GolemEnemy : PatrollingEnemy
    {
        private List<Vector2> patrolPoints;
        private int currentPatrolIndex;
        public GolemEnemy(Dictionary<string, Animation> animations, Vector2 startPosition, float speed, List<Vector2> patrolPoints) : base(animations, startPosition, speed, patrolPoints)
        {
            this.currentPatrolIndex = 0;
            this.patrolPoints = patrolPoints;
        }
    }
}
