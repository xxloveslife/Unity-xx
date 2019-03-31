using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace test.xx{

    //协程开启寻路，开始将路标都放入数组内，for循环调用方法移动到下一个点的位置
    public class WayPoint : MonoBehaviour {

        public GameObject[] wp;  //Unity拖入的Gameobject数组
        public float speed = 5f;  //spedd

        public void Start()
        {
            StartCoroutine(PathFinding());

        }
        //寻找路径位置，调用移动方法
        IEnumerator PathFinding() {
            for (int i = 0; i < wp.Length; i++) {
                
                yield return StartCoroutine(MoveTarget(wp[i].transform.position));//当前（物体移动）协程调用完后，在使用该协程

            }
        }

        //物体移动
        IEnumerator MoveTarget(Vector3 position) {
            while (Vector3.Distance(transform.position, position) > 0.1f) {
                transform.position = Vector3.MoveTowards(transform.position, position, speed * Time.deltaTime);
                yield return new WaitForFixedUpdate();//固定帧数，协程调用，每帧结束后，利用while循环，可以看出移动轨迹

            }
        }



    }
}
 