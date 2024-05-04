using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMarkerFollow : MonoBehaviour
{
    public Transform target; // Takip edilecek hedef

    void Update()
    {
        if (target != null)
        {
            // Hedefin konumunu takip et, y ekseni için biraz yukarýda ayarla isteðe baðlý
            transform.position = new Vector3(target.position.x, 0.1f, target.position.z);
        }
        else
        {
            UnitSelectionManager.Instance.SetAttackMarker();
        }
    }
}
