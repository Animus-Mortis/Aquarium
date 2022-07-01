using System.Collections;
using UnityEngine;

namespace Fish
{
    public class Fish : MonoBehaviour
    {
        [SerializeField] private float maxRightPosition;
        [SerializeField] private float maxLeftPosition;
        [SerializeField] private float speedMoving;

        private void Start()
        {
            StartCoroutine(Moving());
        }

        private IEnumerator Moving()
        {
            while (true)
            {
                while (maxRightPosition >= transform.position.x)
                {
                    transform.Translate(speedMoving, 0, 0);
                    yield return new WaitForFixedUpdate();
                }
                transform.rotation = Quaternion.Euler(Vector3.up * 180);
                while (maxLeftPosition <= transform.position.x)
                {
                    transform.Translate(speedMoving, 0, 0);
                    yield return new WaitForFixedUpdate();
                }
                transform.rotation = Quaternion.Euler(Vector3.zero);
                yield return null;
            }
        }
    }
}