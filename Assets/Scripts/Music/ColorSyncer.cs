using System.Collections;
using UnityEngine;

namespace Music
{
    public class ColorSyncer : BaseSyncer
    {
        public Color restColor = Color.white;

        private void Start()
        {
            GetComponent<Renderer>().material.color = restColor;
        }

        public override void OnBeat()
        {
            base.OnBeat();
            var color = new Color(Random.value, Random.value, Random.value, 1.0f);
            GetComponent<Renderer>().material.color = color;
            // StartCoroutine(MoveToColor());
        }

        public override void Update()
        {
            base.Update();
            transform.localScale = Vector3.one + Vector3.up * AudioEngine.Volume;
        }

        private IEnumerator MoveToColor(Color target)
        {
            var curr = GetComponent<Renderer>().material.color;
            var initial = curr;
            float timer = 0;

            while (curr != target)
            {
                curr = Color.Lerp(initial, target, timer / timeToBeat);
                timer += Time.deltaTime;

                GetComponent<Renderer>().material.color = curr;

                yield return null;
            }

            IsBeat = false;
        }
    }
}