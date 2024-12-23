using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _maxForwardSpeed = 5f;
    [SerializeField] private float _maxBackwardSpeed = 2f;
    [SerializeField] private float _maxStrafeSpeed = 3f;
    [SerializeField] private float _runSpeedBoost = 2f;
    [SerializeField] private AudioClip _walkingSound;
    [SerializeField] private AudioClip _runningSound;
    private float _forwardRunSpeed;
    private float _backwardRunSpeed;
    private float _strafeRunSpeed;
    [SerializeField] private float _maxLookAngle;
    [SerializeField] private float _minLookAngle;
    [Range (0,2)] [SerializeField] private float _sensitivity = 1;
    public float Sensitivity
    {
        get => _sensitivity;

        set => _sensitivity = value;
    }
    private CharacterController _controller;
    private AudioSource _audioSource;
    private Vector3 _velocity;
    private Vector3 _motion;
    private Transform _head;
    private Vector3 _headRotation;
    private bool _isRunning;

    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _audioSource = GetComponent<AudioSource>();
        _head = GetComponentInChildren<Camera>().transform;
        _forwardRunSpeed = _maxForwardSpeed + _runSpeedBoost;
        _backwardRunSpeed = _maxBackwardSpeed + _runSpeedBoost;
        _strafeRunSpeed = _maxStrafeSpeed + _runSpeedBoost;
        HideCursor();
    }
    void Update()
    {
        UpdateRotation();
        UpdateHead();
    }
    void FixedUpdate()
    {
        UpdateVelocity();
        UpdatePosition();
        PlaySound();
    }
    private void HideCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void UpdateRotation()
    {
        float rotation = Input.GetAxis("Mouse X") * _sensitivity;

        transform.Rotate(0f,rotation,0f);
    }
    private void UpdateHead()
    {
        _headRotation = _head.localEulerAngles;

        _headRotation.x -= Input.GetAxis("Mouse Y") * _sensitivity;

        if (_headRotation.x >180f)
            _headRotation.x =Mathf.Max(_maxLookAngle,_headRotation.x);
        else
            _headRotation.x = Mathf.Min(_minLookAngle,_headRotation.x);

        _head.localEulerAngles = _headRotation;
    }
    private void UpdateVelocity()
    {
        float forwardAxis = Input.GetAxis("Vertical");
        float strafeAxis = Input.GetAxis("Horizontal");

        if (Input.GetKey(KeyCode.LeftShift))
        {
            _isRunning = true;

            if (forwardAxis > 0)
                _velocity.z = forwardAxis * _forwardRunSpeed;
            else
                _velocity.z = forwardAxis * _backwardRunSpeed;
            _velocity.x  = strafeAxis * _strafeRunSpeed;

            if (_velocity.magnitude > _forwardRunSpeed)
                _velocity = _velocity.normalized * (forwardAxis > 0 ? _forwardRunSpeed : _backwardRunSpeed);
        }
        else
        {
            _isRunning = false;

            if (forwardAxis > 0)
                _velocity.z = forwardAxis * _maxForwardSpeed;
            else
                _velocity.z = forwardAxis * _maxBackwardSpeed;
            _velocity.x  = strafeAxis * _maxStrafeSpeed;

            if (_velocity.magnitude > _maxForwardSpeed)
                _velocity = _velocity.normalized * (forwardAxis > 0 ? _maxForwardSpeed : _maxBackwardSpeed);
        }
    }
    private void UpdatePosition()
    {
        _motion = transform.TransformVector(_velocity * Time.fixedDeltaTime);


        _controller.Move(_motion);
    }
    private void PlaySound()
    {
        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        {
            _audioSource.clip  = _isRunning ? _runningSound : _walkingSound;


            if (!_audioSource.isPlaying) _audioSource.Play(); Debug.Log("Playing Footsteps");
        }
        else if (_audioSource.isPlaying)
            _audioSource.Pause(); Debug.Log("Stop Footsteps");
    }
}

