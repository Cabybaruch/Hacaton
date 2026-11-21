using System;
using Game.scripts;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Game.scripts.UI
{
    public class PopUp : MonoBehaviour
    {
        [Header("Objects")]
        [SerializeField] private Canvas _canvas;
        [SerializeField] private Image _image;
        
        [Header("options")]
        [SerializeField] private Vector3 _scale = new(0f, 0f, 0f);
        [SerializeField] private float _duration = 0.5f;
        
        private Sequence _sequence;
        private Vector3 _startScale;


        private void Awake()
        {
            _canvas.gameObject.SetActive(false);
            _startScale = _image.transform.localScale;
            
            _sequence = DOTween.Sequence()
                .Append(_image.transform.DOScale(_scale, _duration))
                .Append(_image.transform.DOScale(_startScale, _duration))
                .SetLoops(-1, LoopType.Yoyo);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            PlayerAnimation player = other.gameObject.GetComponent<PlayerAnimation>();

            if (player != null)
            {
                _canvas.gameObject.SetActive(true);
                if(!_sequence.IsPlaying())
                {
                    _sequence.Play();
                }
            }
            
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            PlayerAnimation player = other.gameObject.GetComponent<PlayerAnimation>();

            if (player != null)
            {
                _sequence.Pause();
                _canvas.gameObject.SetActive(false);
                _image.transform.localScale = _startScale;
            }
        }
        
    }
}
