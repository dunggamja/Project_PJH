using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimatorTest : MonoBehaviour {

    private Animator _aniController = null;
    private float _directionX = 0f;
    private float _directionY = 0f;
    private bool _IsWalking = false;

    private Vector3 _oriScale = Vector3.one;
    private SpriteRenderer _renderSprite = null;
	// Use this for initialization
	void Awake () {
        _aniController = GetComponent<Animator>();
        _oriScale = transform.localScale;
        _renderSprite = GetComponentInChildren<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {

        if (_aniController)
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            _IsWalking = true;
            if (h > 0)
                _directionX = 1;
            else if (h < 0)
                _directionX = -1;
            else
                _directionX = 0;

            if (v > 0)
                _directionY = 1;
            else if (v < 0)
                _directionY = -1;
            else
                _directionY = 0f;

            if(h == 0f && v == 0f)
            {
                _directionX = 0;
                _directionY = 0;
                _IsWalking = false;
            }

            _aniController.SetFloat("DirX", _directionX);
            _aniController.SetFloat("DirY", _directionY);
            _aniController.SetBool("Walking", _IsWalking);

            if(_renderSprite)
            {
                var curAniState = _aniController.GetCurrentAnimatorStateInfo(0);

                if (curAniState.IsName("Walk") && _directionX > 0)
                {
                    _renderSprite.flipX = true;
                }
                else
                {
                    _renderSprite.flipX = false;
                }
            }
        }
	}
}
