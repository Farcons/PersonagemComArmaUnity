using Cinemachine;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float inputX, inputZ;
    public Vector3 dirMoveDesejada;
    public float velRotDesejada = 0.1f;
    public Animator anim;
    public float speed;
    public float permiteRotPlayer = 0.3f;
    public Camera cam;
    public float verticalVel;
    public Vector3 movVector;

    public CinemachineVirtualCamera vcam;
    public float[] posCam;
    public int id;
    public CinemachineFramingTransposer composer;

    public InputManager inp;
    private CharacterController heroiCh;


    void Start()
    {
        anim = GetComponent<Animator>();
        cam = Camera.main;

        //composer = vcam.GetCinemachineComponent<CinemachineFramingTransposer>();
        //composer.m_CameraDistance = posCam[id];

        inp = GameManager.Instance.inputM;
        heroiCh = GetComponent<CharacterController>();
    }

    void Update()
    {
        //InputMagnitude();

        //if (Input.GetButton("CameraAjust"))
        //{
        //    if (Input.GetButtonDown("CameraAjust"))
        //        if (id == posCam.Count() - 1)
        //            id = 0;
        //        else
        //            id++;

        //    composer.m_CameraDistance = posCam[id];
        //}

        Vector3 movimento = new(inp.horizontal, 0, inp.vertical);
        //heroiCh.SimpleMove(150 * Time.deltaTime * movimento);

        anim.SetFloat("X", inp.horizontal, 0.1f, Time.deltaTime);
        anim.SetFloat("Z", inp.vertical, 0.1f, Time.deltaTime);

        float rotacaoH = Input.GetAxis("Mouse X");

        transform.Rotate(Vector3.up, rotacaoH * Time.deltaTime * 150);

        if (inp.vertical != 0)
        {
            heroiCh.SimpleMove(150 * inp.vertical * Time.deltaTime * transform.forward);
        }
        if (inp.horizontal != 0)
        {
            heroiCh.SimpleMove(150 * inp.horizontal * Time.deltaTime * transform.right);
        }
    }

    private void PlayerMoveRot()
    {
        inputX = Input.GetAxis("Horizontal");
        inputZ = Input.GetAxis("Vertical");

        Vector3 frente = cam.transform.forward;
        Vector3 direita = cam.transform.right;

        frente.Normalize();
        direita.Normalize();

        dirMoveDesejada = frente * inputZ + direita * inputX;
        Quaternion rot = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dirMoveDesejada), velRotDesejada);

        transform.rotation = new Quaternion(0, rot.y, 0, rot.w);
    }

    private void InputMagnitude()
    {
        inputX = Input.GetAxis("Horizontal");
        inputZ = Input.GetAxis("Vertical");

        anim.SetFloat("Z", inputZ, 0, Time.deltaTime * 2);
        anim.SetFloat("X", inputX, 0, Time.deltaTime * 2);

        speed = new Vector2(inputX, inputZ).sqrMagnitude;
    }
}
