using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Arrow;
    public GameObject Parent;
    public GameObject Player;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            ShootArrow();
        }
        //transform.position = new Vector3(transform.position.x, Player.transform.position.y, transform.position.z);
    }
    private void ShootArrow()
    {
        var arrow = Instantiate(Arrow, transform.position, Quaternion.LookRotation(Player.transform.position), Parent.transform);
        arrow.GetComponent<Arrow>().ApplyInitialVelocity(Manager.Instance.GetInitialVelocity2(transform.position,Player.transform.position,-15));
    }
}
