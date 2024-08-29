using UnityEngine;
using UnityEngine.UI;

public class Arma : MonoBehaviour
{
    public RawImage alvo;
    public GameObject decalPrefab;

    void Start()
    {

    }

    void Update()
    {
        VerificarAlvo();
    }

    private void VerificarAlvo()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.collider.GetComponent<Vilao>())
            {
                alvo.color = Color.red;
            }
            else
            {
                alvo.color = Color.white;
            }

            if (Input.GetButton("Fire1"))
            {
                if (hit.collider.GetComponent<Vilao>())
                {
                    Destroy(hit.collider.gameObject);
                }
                else
                {
                    DecalInst(hit);
                }
            }
        }
        else
        {
            alvo.color = Color.white;
        }
    }

    private void DecalInst(RaycastHit hitInf)
    {
        var decalVar = Instantiate(decalPrefab);
        decalVar.transform.position = hitInf.point;
        decalVar.transform.forward = hitInf.normal * -1;
    }
}
