using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Lection 3. Osnovu C#, part 1
public class Lection3Controller : MonoBehaviour
{
    // List of applied damages
    [SerializeField] private List<int> damages = new List<int>();

    // Current damage value entered in the Inspector
    [SerializeField] private int damage;

    // Current health value
    [SerializeField] private int health = 100;

    // The character is alive or not
    [SerializeField] private bool isAlive = true;

    [ContextMenu("Print Damages")] private void PrintDamages()
    {
        if (damages.Count == 0)
        {
            Debug.Log("No damages recorded");
            return;
        }

        string result = string.Join(", ", damages);
        Debug.Log("Damage list: " + result);
    }

    [ContextMenu("Add Damage")] private void AddDamage()
    {
        damages.Add(damage);
        Debug.Log("Added damage: " + damage);
    }

    [ContextMenu("Remove Damage")] private void RemoveDamage()
{
    bool removed = damages.Remove(damage);

    if (removed)
    {
        Debug.Log("Removed damage: " + damage);
    }
    else
    {
        Debug.Log("Damage " + damage + " not found");
    }
}


    [ContextMenu("Clear Damages")] private void ClearDamages()
    {
        damages.Clear();
        Debug.Log("Damage list cleared");
    }

    [ContextMenu("Sort Damages")]
    private void SortDamages()
    {
        damages.Sort();
        Debug.Log("Damage list sorted");
    }

    // Applies current damage to health
    [ContextMenu("Apply Damage")] private void ApplyDamage()
    {
        if (!isAlive)
        {
            Debug.Log("Cannot apply damage: character is already dead");
            return; // early return, more in the lecture 3 of the course Osnovu C#, part 1
        }

        health -= damage;    // subtract damage from health
        damages.Add(damage); // store applied damage in the list

        Debug.Log($"Damage {damage} applied. Health now: {health}");

        if (health <= 0)
        {
            isAlive = false;
            health = 0;
            Debug.Log("Character died!");
        }
    }
}




/*public class Lection3Controller : MonoBehaviour
{
    
    [SerializeField] private List<int> numbers = new List<int>();
    [SerializeField] private int value;

    [ContextMenu("Print List")]
    void PrintList()
    {
    if (numbers.Count == 0)
    {
        Debug.Log("List is empty");
        return;
    }

    string result = "";

    foreach (int n in numbers)
    {
        result += n + ", ";
    }

    Debug.Log(result);
}

    [ContextMenu("Add Value")] void AddValue()
    {
    numbers.Add(value);
    Debug.Log("Added: " + value);
    }

    [ContextMenu("Remove Value")] void RemoveValue()
    {
    bool removed = numbers.Remove(value);

    if (removed)
        Debug.Log("Removed: " + value);
    else
        Debug.Log("Value not found: " + value);
    }

    [ContextMenu("Clear List")] void ClearList()
    {
    numbers.Clear();
    Debug.Log("List cleared");
    }

    [ContextMenu("Sort List")] void SortList()
    {
    numbers.Sort();
    Debug.Log("List sorted");
    }
}*/


