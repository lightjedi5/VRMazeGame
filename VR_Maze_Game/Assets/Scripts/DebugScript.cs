using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugScript : MonoBehaviour
{
    public OVRCameraRig test;
    public float move_speed;
    private Rigidbody rig;
    private bool flip;
    public float constant_slow_mult;
    public float turn_speed;
    // Use this for initialization
    void Start()
    {
        rig = GetComponent<Rigidbody>();

    }

    public int mySign(float inpt)
    {
        if (Mathf.Abs(inpt) < 0.2f)
            return 0;
        else if (inpt > 0)
            return 1;
        else
            return -1;
    }

    public bool notZero(float inpt)
    {
        if (Mathf.Abs(inpt) < 0.2f)
            return false;
        return true;
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();



        Movement();
        SkyChange();
    }

    private void Movement()
    {
        if (notZero(Input.GetAxisRaw("Horizontal")) || notZero(Input.GetAxisRaw("Vertical")))
        {


            float bonus_mult = 1;
            float dir_angle = 0;

            if (notZero(Input.GetAxisRaw("Horizontal")) && notZero(Input.GetAxisRaw("Vertical")))
            {
                if (flip)
                    dir_angle = Mathf.PI / 2 + Mathf.PI / 2 * mySign(Input.GetAxisRaw("Horizontal"));
                else
                    dir_angle = Mathf.PI - Mathf.PI / 2 * mySign(Input.GetAxisRaw("Vertical"));
                flip = !flip;
                bonus_mult = Mathf.Sqrt(2);

            }
            else if (notZero(mySign(Input.GetAxisRaw("Horizontal"))))
                dir_angle = Mathf.PI / 2 + Mathf.PI / 2 * mySign(Input.GetAxisRaw("Horizontal"));
            else
                dir_angle = Mathf.PI - Mathf.PI / 2 * mySign(Input.GetAxisRaw("Vertical"));

            dir_angle -= Mathf.PI / 2;

            float radians = Mathf.Deg2Rad * rig.rotation.eulerAngles.y;

            rig.velocity += new Vector3(Mathf.Sin(dir_angle + radians), 0, Mathf.Cos(dir_angle + radians)) * move_speed * bonus_mult;


        }
        rig.velocity *= constant_slow_mult;
    }

    private void SkyChange()
    {
        foreach (Transform camera_trans in test.transform.Find("TrackingSpace"))
        {
            try
            {
                Camera camera = camera_trans.gameObject.GetComponent<Camera>();
                Quaternion a = test.transform.Find("TrackingSpace").Find("CenterEyeAnchor").rotation;
                camera.backgroundColor = new Color(a.x + a.w ,a.y+ a.w ,a.z+ a.w,1);
            }
            catch
            {

            }
        }



        /*
        bool above = test.transform.Find("TrackingSpace").Find("CenterEyeAnchor").rotation.eulerAngles.x < 180;


        if (above)
        {
            foreach (Transform camera_trans in test.transform.Find("TrackingSpace"))
            {
                try
                {
                    Camera camera = camera_trans.gameObject.GetComponent<Camera>();
                    camera.backgroundColor = Color.red;
                }
                catch
                {

                }
            }
        }
        else
        {
            foreach (Transform camera_trans in test.transform.Find("TrackingSpace"))
            {
                try
                {
                    Camera camera = camera_trans.gameObject.GetComponent<Camera>();
                    camera.backgroundColor = Color.blue;
                }
                catch
                {

                }
            }
        }
        */
    }

    private void Rotate()
    {
        float turn_dir = 0;
        if (Input.GetKey(KeyCode.LeftArrow) ^ Input.GetKey(KeyCode.RightArrow))
        {
            turn_dir = turn_speed;
            if (Input.GetKey(KeyCode.LeftArrow))
                turn_dir *= -1;
            rig.rotation = Quaternion.Euler(0, turn_dir + rig.rotation.eulerAngles.y, 0);
        }
    }
}
