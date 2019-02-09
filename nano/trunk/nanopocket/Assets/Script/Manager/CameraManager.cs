using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour
{

    public GameManager m_GameManager;
    public Transform[] m_posCamera;

    private GameObject m_CameraTarget;

    private EnumDefine.CameraState m_CrrentCameraState;
    private float m_fFieldOfView;

    private Vector3 m_LastPosition;
    private Quaternion m_LastRotation;
    private float m_fTimePos;
    private float m_fTimeRot;

    /// 
	public Transform m_camTransform;
	
	// How long the object should shake for.
	public float m_fshake = 0f;
	
	// Amplitude of the shake. A larger value shakes the camera harder.
	public float m_fshakeAmount = 0.1f;
	public float m_fdecreaseFactor = 1.0f;
	
	Vector3 originalPos;
	
	void Awake()
	{
		if (m_camTransform == null)
		{
			m_camTransform = GetComponent(typeof(Transform)) as Transform;
		}
	}

    public void SetCameraTarget(GameObject objTarget)
    {
        m_CameraTarget = objTarget;
    }

    void Start()
    {
        m_CrrentCameraState = EnumDefine.CameraState.PLAY;
        m_fFieldOfView = GetComponent<Camera>().fieldOfView;
        m_LastPosition = gameObject.transform.position;
        m_LastRotation = gameObject.transform.rotation;
    }

    void Update()
    {
        float fBallPow = m_GameManager.GetBallPower() / 150f;

        if (m_CameraTarget == null)
        {
            return;
        }

        if (m_fshake > 0)
        {
            m_camTransform.localPosition = originalPos + Random.insideUnitSphere * m_fshakeAmount;

            m_fshake -= Time.deltaTime * m_fdecreaseFactor;
        }

        switch (m_CrrentCameraState)
        {
            case EnumDefine.CameraState.READY:

                if (fBallPow == 0)
                {
                    gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, m_CameraTarget.transform.position + new Vector3(0, 15f, 0), 3f * TimeHelper.updateDeltaTime);
                    gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, new Quaternion(0.4f, 0, 0, 1), 2.0f * TimeHelper.updateDeltaTime);
                }
                else
                {
                    gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, m_CameraTarget.transform.position + new Vector3(0, 30f, 0), 3f * TimeHelper.updateDeltaTime);
                    gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, new Quaternion(0.4f, 0, 0, 1), 2.0f * TimeHelper.updateDeltaTime);
                }
                break;

            case EnumDefine.CameraState.PLAY:

                if (fBallPow == 0)
                {
                    gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, m_CameraTarget.transform.position + new Vector3(0, 15f, 0), 3f * TimeHelper.updateDeltaTime);
                    gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, new Quaternion(1f, 0, 0, 1), 2.0f * TimeHelper.updateDeltaTime);
                }
                else
                {
                    gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, m_CameraTarget.transform.position + new Vector3(0, 30f, 0), 3f * TimeHelper.updateDeltaTime);
                    gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, new Quaternion(1f, 0, 0, 1), 2.0f * TimeHelper.updateDeltaTime);
                }
                break;
            case EnumDefine.CameraState.CONTROL:

                gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, m_CameraTarget.transform.position + new Vector3(0, 30f, 0), 3f * TimeHelper.updateDeltaTime);
                gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, new Quaternion(1f, 0, 0, 1), 2.0f * TimeHelper.updateDeltaTime);

                break;

            case EnumDefine.CameraState.DIE:

                break;
        }
    }
    public void ChangeCameraState(EnumDefine.CameraState cState)
    {
        m_CrrentCameraState = cState;
        m_fTimePos = m_fTimeRot = 0f;
        m_LastPosition = gameObject.transform.position;
        m_LastRotation = gameObject.transform.rotation;
    }

    public void CameraShake(float _fvalue)
    {
        originalPos = m_camTransform.localPosition;
        m_fshake = _fvalue;
    }

}