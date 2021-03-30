using UnityEngine;

public class PickUpController : MonoBehaviour
{
    private bool equipped;
    private static bool handIsFull;

    public Transform Player, Hand, FpsCam;
    public float PickUpRange;


    public void Update()
    {
        if (IsEnableToPickUp() && IsInRangeToPickUp() && Input.GetMouseButtonDown(0))
        {
            PickUp();
        }
        else if (equipped && Input.GetKeyDown(KeyCode.G))
        {
            Drop();
        }
        
    }

    private bool IsEnableToPickUp()
    {
        return !equipped && !handIsFull;
    }

    private bool IsInRangeToPickUp()
    {
        var distanceToPlayer = Player.position - transform.position;
        return distanceToPlayer.magnitude <= PickUpRange;
    }

    private void PickUp()
    {
        SwtichToPickUp(true);
        SetToolToCorrectPositionAndRotation();        
    }

    private void SetToolToCorrectPositionAndRotation()
    {
        transform.SetParent(Hand);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
    }

    private void Drop()
    {
        transform.SetParent(null);
        SwtichToPickUp(false);
    }

    private void SwtichToPickUp(bool toggle)
    {
        equipped = toggle;
        handIsFull = toggle;
        GetComponent<Rigidbody>().isKinematic = toggle;
        GetComponent<BoxCollider>().isTrigger = toggle;
    }
}
