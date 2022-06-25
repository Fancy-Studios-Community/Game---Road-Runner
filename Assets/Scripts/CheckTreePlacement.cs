using UnityEngine;

public class CheckTreePlacement : MonoBehaviour
{
    [SerializeField] GameObject blackcurtain;
    [SerializeField] GameObject[] trees;
    
    private void OnCollisionEnter(Collision collision)
    {
        blackcurtain.SetActive(false);
        setKinetic();
    }

    
    private void setKinetic()
    {
        foreach (var item in trees)
        {
            
            item.GetComponent<Rigidbody>().isKinematic = true;
        }
    }
}
