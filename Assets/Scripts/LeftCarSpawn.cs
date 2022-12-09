using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftCarSpawn : MonoBehaviour
{
   [SerializeField] public GameObject leftCar;
   [SerializeField] public GameObject rightCar;
   private float moveSpeed = 10f;
   private float spawnRate;
   private float _spawnRate;

   private void Start()
   {
      StartCoroutine(SpawnCarLeftCar());
      StartCoroutine(SpawnRightCar());
   }

   IEnumerator SpawnCarLeftCar()
   {
      spawnRate = UnityEngine.Random.Range(2,6);
      yield return new WaitForSeconds(spawnRate);
      GameObject newcar = Instantiate(leftCar, gameObject.transform.position, Quaternion.identity);
      Rigidbody2D rb = newcar.GetComponent<Rigidbody2D>();
      rb.velocity = gameObject.transform.GetChild(0).transform.position.normalized * moveSpeed;
      StartCoroutine(SpawnCarLeftCar());
      yield return  new WaitForSeconds(10f);
      Destroy(newcar);
   }

   IEnumerator SpawnRightCar()
   {
      _spawnRate = UnityEngine.Random.Range(2,6);
      yield return new WaitForSeconds(_spawnRate);
      GameObject newcar = Instantiate(rightCar, gameObject.transform.GetChild(1).transform.position, Quaternion.identity);
      Rigidbody2D rb = newcar.GetComponent<Rigidbody2D>();
      rb.velocity = gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).transform.position.normalized * moveSpeed;
      StartCoroutine(SpawnRightCar());
      yield return  new WaitForSeconds(10f);
      Destroy(newcar);
   }
}
