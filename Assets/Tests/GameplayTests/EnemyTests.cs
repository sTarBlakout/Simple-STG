// using System.Collections;
// using Enemy;
// using NUnit.Framework;
// using Player;
// using Ship;
// using UnityEditor;
// using UnityEngine;
// using UnityEngine.TestTools;

namespace Tests
{
    public class EnemyTests
    {
        // [UnityTest]
        // public IEnumerator EnemyDamageTest()
        // {
        //     var prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/EnemyShip.prefab");
        //     var enemy = GameObject.Instantiate(prefab, Vector3.zero, Quaternion.identity).GetComponent<EnemyShip>();
        //     enemy.health = 100;
        //     
        //     enemy.Damage(10);
        //     
        //     Assert.AreEqual(enemy.health, 90);
        //     
        //     yield return null;
        // }
        //
        // [UnityTest]
        // public IEnumerator EnemyDeathTest()
        // {
        //     var prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/EnemyShip.prefab");
        //     var enemy = GameObject.Instantiate(prefab, Vector3.zero, Quaternion.identity).GetComponent<EnemyShip>();
        //     enemy.health = 10;
        //     
        //     enemy.Damage(10);
        //     
        //     Assert.IsTrue(enemy.IsDestroyed);
        //     
        //     yield return null;
        // }
        //
        // [UnityTest]
        // public IEnumerator GatlingGunTest()
        // {
        //     var prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/GatlingGun.prefab");
        //     var gun = GameObject.Instantiate(prefab, Vector3.zero, Quaternion.identity).GetComponent<GatlingGun>();
        //
        //     gun.Shoot();
        //     
        //     Assert.IsTrue(GameObject.FindObjectOfType<Bullet>() != null);
        //     
        //     yield return null;
        // }
    }
}
