using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyData", menuName = "ScriptableObjects/EnemyData", order = 1)]
public class Enemys : ScriptableObject
{

        public float health;
        public float speed;
        public float damage;
        public float detectionRange;
    
}
