using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using Cinemachine;

/// <summary>
/// Управление камерой
/// </summary>
public class CameraController : BaseGameObject
{
	public static CameraController Instance { get; private set; }

	public Camera main;
	public Material skybox;

    public Vector2 DefaultResolution = new Vector2(1080, 1920);
    [Range(0f, 1f)] public float WidthOrHeight = 0;

	public bool isFog				= false;
	public Color fogColor			= new Color32(207, 221, 231, 255);
	public FogMode fogMode			= FogMode.Linear;
	public float fogStartDistance	= 2000;
	public float fogEndDistance		= 5000;

	public bool offPostProcessDevice = false;

	private CinemachineVirtualCamera virtualCamera;
	private CinemachineBasicMultiChannelPerlin noise;

	//Для рассчета дальности камеры
	private float initialSize;
	private float targetAspect;
    private float initialFov;
    private float horizontalFov = 120f;

	private void Awake()
    {
		Instance = this;
    }

	private void Start()
	{
		virtualCamera = this.GetComponent<CinemachineVirtualCamera>();
		noise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

		if(skybox != null)
		{ 
			RenderSettings.skybox = skybox;	
		}

		RenderSettings.fog = isFog;
		RenderSettings.fogColor = fogColor;
		RenderSettings.fogMode = fogMode;
		RenderSettings.fogStartDistance = fogStartDistance;
		RenderSettings.fogEndDistance = fogEndDistance;

		// Отключение пост обработки на устройстве
		#if UNITY_ANDROID || UNITY_IOS
		if(offPostProcessDevice)
		{
			if (main.TryGetComponent<PostProcessLayer>(out PostProcessLayer ppl))
			{
				ppl.enabled = false;
			}

			if (main.TryGetComponent<PostProcessVolume>(out PostProcessVolume ppv))
			{
				ppv.enabled = false;
			}
		}
		#endif

		// Рассчет дальности камеры
		CameraAutoSize();
	}

	private void CameraAutoSize()
	{
		float CalcVerticalFov(float hFovInDeg, float aspectRatio)
		{
			float hFovInRads = hFovInDeg * Mathf.Deg2Rad;

			float vFovInRads = 2 * Mathf.Atan(Mathf.Tan(hFovInRads / 2) / aspectRatio);

			return vFovInRads * Mathf.Rad2Deg;
		}

        initialSize		= main.orthographicSize;
        targetAspect	= DefaultResolution.x / DefaultResolution.y;
        initialFov		= main.fieldOfView;
        horizontalFov	= CalcVerticalFov(initialFov, 1 / targetAspect);

        if (main.orthographic)
        {
            float constantWidthSize = initialSize * (targetAspect / main.aspect);
            virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(constantWidthSize, initialSize, WidthOrHeight);
        }
        else
        {
            float constantWidthFov = CalcVerticalFov(horizontalFov, main.aspect);
            virtualCamera.m_Lens.FieldOfView = Mathf.Lerp(constantWidthFov, initialFov, WidthOrHeight);
        }
	}

	/// <summary>
	/// Телепорт камеры вместе с объектом наблюдения в случайную позицию.
	/// </summary>
	public IEnumerator TeleportInNewPos(Transform target, Vector3 newPos, float time)
	{
		Vector3 offcet = this.transform.position - target.position;
		target.position = newPos;

		//Отключение виртуальной камеры
		virtualCamera.Follow = null;
		virtualCamera.LookAt = null;
		virtualCamera.enabled = false;

		//Переход на картинку простой камерой
		main.transform.position = this.transform.position + offcet;
		main.transform.LookAt(target);

		//Перенос виртуальной камеры
		virtualCamera.transform.position = this.transform.position + offcet;
		virtualCamera.transform.LookAt(target);
		virtualCamera.Follow = target;
		virtualCamera.LookAt = target;

		//Ожидание самонастройки камеры перед включением
		yield return new WaitForSeconds(time);
		virtualCamera.enabled = true;
		yield return null;
	}

	public IEnumerator Shock()
	{
		noise.m_AmplitudeGain = 1;
		noise.m_FrequencyGain = 1;
		yield return new WaitForSeconds(1f);
		noise.m_AmplitudeGain = 0;
		noise.m_FrequencyGain = 0;
		yield return null;
	}
}
