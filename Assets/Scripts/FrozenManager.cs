using System.Collections;
using UnityEngine;

namespace Frozen
{
    public class FrozenManager : MonoBehaviour
    {
        [SerializeField] private Material material;
        [SerializeField] private float timer;
        [SerializeField] private float speedFreezing;
        [SerializeField] private float speedDefrosting;
        [SerializeField] private AnimationCurve curve;

        private float sides = 150;
        private float alfa = 0;
        private float startTimer;
        private Vector3 tmpMousePosition;
        private Coroutine Freez;
        private Coroutine Defreez;

        private void Start()
        {
            startTimer = timer;
            material.SetFloat("_Sides", 150);
            material.SetFloat("_Alfa", -2);
            tmpMousePosition = Input.mousePosition;
        }

        private void Update()
        {
            if (tmpMousePosition != Input.mousePosition || Input.GetMouseButton(0)|| Input.GetMouseButton(1)|| Input.GetMouseButton(2))
            {
                timer = startTimer;
                tmpMousePosition = Input.mousePosition;
                if (Freez != null)
                {
                    StopCoroutine(Freez);
                }
                if (Defreez == null)
                    Defreez = StartCoroutine(Defrosting());

                return;
            }
            if (timer >= 0)
            {
                timer -= Time.fixedDeltaTime;
                if (timer <= 0)
                {
                    if (Defreez != null) StopCoroutine(Defreez);
                    Freez = StartCoroutine(Freezing());
                    Defreez = null;
                }
            }
        }

        private IEnumerator Freezing()
        {
            while (alfa < 0.0f)
            {
                yield return new WaitForSeconds(0.1f);
                alfa += speedDefrosting / 100;
                material.SetFloat("_Alfa", alfa);
            }
            alfa = 0;
            material.SetFloat("_Alfa", alfa);
            while (sides >= 5.0f)
            {
                sides -= speedFreezing * curve.Evaluate(sides / 150);
                material.SetFloat("_Sides", sides);
                yield return new WaitForSeconds(0.1f);
            }
        }

        private IEnumerator Defrosting()
        {
            while (sides <= 150.0f)
            {
                sides += speedDefrosting * curve.Evaluate(sides / 150);
                material.SetFloat("_Sides", sides);
                yield return new WaitForSeconds(0.1f);
            }
            while (alfa > -2.0f)
            {
                alfa -= speedDefrosting / 100;
                material.SetFloat("_Alfa", alfa);
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}