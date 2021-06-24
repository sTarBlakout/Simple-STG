using System.Collections;
using NUnit.Framework;
using Player;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class PlayerTests
    {
        [UnityTest]
        public IEnumerator PlayerDamageBoostTest()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/PlayerShip.prefab");
            var player = GameObject.Instantiate(prefab, Vector3.zero, Quaternion.identity).GetComponent<PlayerController>();
            player.Init();
            var damageValue = player.CurrDamage;
        
            player.Boost(10, 10);
            
            Assert.AreEqual(player.CurrDamage, damageValue + 10);
            
            yield return null;
        }
        
        [UnityTest]
        public IEnumerator PlayerHealthBoostTest()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/PlayerShip.prefab");
            var player = GameObject.Instantiate(prefab, Vector3.zero, Quaternion.identity).GetComponent<PlayerController>(); 
            player.Init();
            player.health = 0f;

            player.Boost(10, 10);
            
            Assert.AreEqual(player.health, 10);
            
            yield return null;
        }
        
        [UnityTest]
        public IEnumerator PlayerDamageTest()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/PlayerShip.prefab");
            var player = GameObject.Instantiate(prefab, Vector3.zero, Quaternion.identity).GetComponent<PlayerController>();
            player.health = 100;
            
            player.Damage(10);
            
            Assert.AreEqual(player.health, 90);
            
            yield return null;
        }
        
        [UnityTest]
        public IEnumerator PlayerDeathTest()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/PlayerShip.prefab");
            var player = GameObject.Instantiate(prefab, Vector3.zero, Quaternion.identity).GetComponent<PlayerController>();
            player.health = 10;
            
            player.Damage(10);
            
            Assert.IsTrue(player.isDead);
            
            yield return null;
        }
    }
}
