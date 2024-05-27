using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleDiceController : MonoBehaviour
{
    [SerializeField] private GameObject _diceParticle;

    private Animator _animator;
    private DiceGroupController _diceGroupController;
    private bool _isAnimationPlaying;

    public void Initialize(DiceGroupController diceGroupController)
    {
        _diceGroupController = diceGroupController;
        _animator = GetComponent<Animator>();
    }

    public void SetRotation(Vector3 newRotation)
    {
        transform.localEulerAngles = newRotation + Vector3.one;
    }
    public Animator GetAnimator()
    {
        return _animator;
    }

    public void StartAnimation()
    {
        StartCoroutine(WaitDiceChangeTransformForParticle());
        _isAnimationPlaying = true;
    }

    private IEnumerator WaitDiceChangeTransformForParticle()
    {
        yield return new WaitForSeconds(.1f);
        _diceParticle.SetActive(true);
    }
    public void EndAnimation()
    {
        _diceParticle.SetActive(false);
        _isAnimationPlaying = false;
        _diceGroupController.EndAnimations();
    }

    public bool GetIsAnimationPlaying()
    {
        return _isAnimationPlaying;
    }

}
