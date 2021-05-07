using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using QRCode.Extensions;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace QRCode.Feedbacks.Demos
{
    public class CubesFeedbackDemo : MonoBehaviour
    {
        [Title("References")]
        public GameObject[] cubes = default;

        [SerializeField] private Color[] colors;
        
        
        private static readonly int Color = Shader.PropertyToID("Color");

        private List<Tween> _tweens;

        private void OnEnable()
        {
            colors = new []
            {
                new Color().ToColor("#34ace0"),
                new Color().ToColor("#227093"),
                new Color().ToColor("#33d9b2"),
                new Color().ToColor("#218c74")
            };

            _tweens = new List<Tween>();
            
            foreach (var cube in cubes)
            {
                cube.transform.localScale *= Random.Range(0.8f, 1.2f);
                
                _tweens.Add(cube.transform
                    .DOMoveY(
                        cube.transform.position.y + Random.Range(0.2f, 0.8f),
                        Random.Range(2f, 3f))
                    .SetEase(Ease.InOutSine)
                    .SetLoops(15000, LoopType.Yoyo));

                var mat = new Material(cube.GetComponent<MeshRenderer>().materials[0]);
                cube.GetComponent<MeshRenderer>().materials[0] = mat;
                cube.GetComponent<MeshRenderer>().materials[0].DOColor(colors[Random.Range(0, colors.Length)], .2f);
            }
        }

        private void OnDisable()
        {
            _tweens.ForEach(w => w.Kill());
        }
    }
}

