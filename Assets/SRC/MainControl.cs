using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ObjectState
{
    public GameObject Object;
    public bool DefaultState;
}

[Serializable]
public class Condition
{
    public Camera Camera;
    public List<ObjectState> Objects;
}

public class MainControl : MonoBehaviour
{
    public List<Condition> Conditions;
    public int currentConditionIndex = 0;

    private void Start()
    {
        InitializeConditions();
    }

    private void InitializeConditions()
    {
        foreach (var condition in Conditions)
        {
            if (condition.Camera != null)
                condition.Camera.gameObject.SetActive(false);

            foreach (var objState in condition.Objects)
            {
                if (objState.Object != null)
                    objState.Object.SetActive(objState.DefaultState);
            }
        }

        // Activate the first condition by default
        if (Conditions.Count > 0)
            ActivateCondition(0);
    }

    public void NextCondition()
    {
        int nextIndex = (currentConditionIndex + 1) % Conditions.Count;
        ActivateCondition(nextIndex);
    }

    public void PreviousCondition()
    {
        int prevIndex = (currentConditionIndex - 1 + Conditions.Count) % Conditions.Count;
        ActivateCondition(prevIndex);
    }

    public void AppearAllObjectsInCurrentCondition()
    {
        if (currentConditionIndex < 0 || currentConditionIndex >= Conditions.Count) return;

        var condition = Conditions[currentConditionIndex];
        foreach (var objState in condition.Objects)
        {
            if (objState.Object != null)
                objState.Object.SetActive(true); // Make all objects visible
        }
    }

    public void HideAllObjectsInCurrentCondition()
    {
        if (currentConditionIndex < 0 || currentConditionIndex >= Conditions.Count) return;

        var condition = Conditions[currentConditionIndex];
        foreach (var objState in condition.Objects)
        {
            if (objState.Object != null)
                objState.Object.SetActive(false); // Make all objects invisible
        }
    }

    public void ResetObjectVisibilityToDefault(ObjectState objState)
    {
        if (objState.Object != null)
        {
            objState.Object.SetActive(objState.DefaultState);
        }
    }

    public void ResetAllObjectsToDefault()
    {
        if (currentConditionIndex < 0 || currentConditionIndex >= Conditions.Count) return;

        var condition = Conditions[currentConditionIndex];
        foreach (var objState in condition.Objects)
        {
            if (objState.Object != null)
                objState.Object.SetActive(objState.DefaultState); // Reset to default state
        }
    }

    private void ActivateCondition(int index)
    {
        if (index < 0 || index >= Conditions.Count) return;

        // Deactivate all conditions
        foreach (var condition in Conditions)
        {
            if (condition.Camera != null)
                condition.Camera.gameObject.SetActive(false);
        }

        // Activate the selected condition
        var selectedCondition = Conditions[index];
        if (selectedCondition.Camera != null)
            selectedCondition.Camera.gameObject.SetActive(true);

        foreach (var objState in selectedCondition.Objects)
        {
            if (objState.Object != null)
                objState.Object.SetActive(objState.DefaultState);
        }

        currentConditionIndex = index;
    }

    // Method to toggle object visibility
    public void ToggleObjectVisibility(GameObject obj, bool state)
    {
        if (obj != null)
        {
            obj.SetActive(state);
        }
    }
}
