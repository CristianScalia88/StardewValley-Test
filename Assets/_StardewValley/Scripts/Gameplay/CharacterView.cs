using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterView : MonoBehaviour
{
    private const string UP_STATE = "Up";
    private const string RIGHT_STATE = "Right";
    private const string DOWN_STATE = "Down";
    private const string LEFT_STATE = "Left";
    
    [SerializeField] Animator bodyAnimator;
    [SerializeField] Animator clothsAnimator;
    [SerializeField] Animator hairAnimator;
    [SerializeField] List<EquipementView> StartingEquipment;

    private Dictionary<EquipmentType, Animator> animatorsMap;

    private string lastDirectionState;
    private bool moving = true;
    
    private void Awake()
    {
        animatorsMap = new Dictionary<EquipmentType, Animator>()
        {
            {EquipmentType.Body, bodyAnimator},
            {EquipmentType.Cloths, clothsAnimator},
            {EquipmentType.Hair, hairAnimator},
        };
    }

    private void Start()
    {
        StartingEquipment.ForEach(ev => SetEquipment(ev));
        SetAnimationsEnabled(false);
    }

    public void SetEquipment(EquipementView equipmentView)
    {
        if (animatorsMap.TryGetValue(equipmentView.type, out Animator animator))
        {
            animator.runtimeAnimatorController = equipmentView.runtimeController;
        }
    }

    public void SetMovementDirection(DirectionType directionType)
    {
        switch (directionType)
        {
            case DirectionType.Up:
                SetAnimatorsState(UP_STATE);
                break;
            case DirectionType.Right:
                SetAnimatorsState(RIGHT_STATE);
                break;
            case DirectionType.Down:
                SetAnimatorsState(DOWN_STATE);
                break;
            case DirectionType.Left:
                SetAnimatorsState(LEFT_STATE);
                break;
        }
    }

    public void SetAnimationsEnabled(bool enabled)
    {
        if (enabled == moving)
            return;
        
        moving = enabled;
        bodyAnimator.speed = enabled? 1 : 0;
        clothsAnimator.speed = enabled? 1 : 0;
        hairAnimator.speed = enabled? 1 : 0;
        if (!enabled)
        {
            SetAnimatorsState(lastDirectionState);
        }
    }
    
    private void SetAnimatorsState(string state)
    {
        bodyAnimator.Play(state,0,0);
        clothsAnimator.Play(state,0,0);
        hairAnimator.Play(state,0,0);
        lastDirectionState = state;
    }
}