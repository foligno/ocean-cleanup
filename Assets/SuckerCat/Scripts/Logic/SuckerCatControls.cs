using UnityEngine;

namespace OceanCleanup.SuckerCat.Logic
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class SuckerCatControls : MonoBehaviour
    {
        public delegate void FuelChangeEvent(float newFuelAmount);
        public static event FuelChangeEvent OnFuelChange;
    
        public float maxSpeed = 1;

        [SerializeField] private float _boostFuel = 100;
        private bool _boosting = true;
        [SerializeField] private float _boostScalar = 2.0f;
    
        private float _velocity;
        private Rigidbody2D _rigidbody;
        [SerializeField] private BoxCollider2D _moveRegion;
        [SerializeField] private float _boostDecrement = 10f;

        private Camera _camera;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _camera = Camera.main;
        }

        private void FixedUpdate()
        {
            Vector3 currentPosition = transform.position;

            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (_boostFuel > 0)
                {
                    _boosting = true;

                    _boostFuel -= _boostDecrement * Time.fixedDeltaTime;
                }
                else
                {
                    _boosting = false;
                }
            }
            else
            {
                _boosting = false;

                if (_boostFuel < 100)
                {
                    _boostFuel += _boostDecrement * Time.fixedDeltaTime;
                }
            }

            if (Input.GetMouseButton(0)) // Left
            {
                if (_boosting)
                {
                    _velocity = Mathf.Lerp(_velocity, maxSpeed * _boostScalar, Time.fixedDeltaTime);
                }
                else
                {
                    _velocity = Mathf.Lerp(_velocity, maxSpeed, Time.fixedDeltaTime);
                }
            
                Vector3 translatedMousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
                Vector3 mousePosition = new Vector3(
                    translatedMousePosition.x, 
                    translatedMousePosition.y, 
                    0.0f);
            
                float angleDifference = AngleBetweenPoints(currentPosition, mousePosition);
            
                Quaternion newOrientation = Quaternion.Euler(0.0f, 0.0f,angleDifference);
            
                transform.rotation = Quaternion.Lerp(
                    transform.rotation, 
                    newOrientation, 
                    2 * Time.fixedDeltaTime
                );
            }
            else
            {
                _velocity = Mathf.Lerp(_velocity, 0, Time.fixedDeltaTime);
            }

            Vector3 targetPosition = Vector3.Lerp(
                currentPosition,
                currentPosition + (transform.right * _velocity),
                2 * Time.fixedDeltaTime
            );

            if (_moveRegion.OverlapPoint(targetPosition))
            {
                _rigidbody.MovePosition(targetPosition);
            }
        }

        private void Update()
        {
            OnFuelChange?.Invoke(_boostFuel);
        }

        private static float AngleBetweenPoints(Vector2 firstPosition, Vector2 secondPosition)
        {
            return Mathf.Atan2(
                secondPosition.y - firstPosition.y, 
                secondPosition.x - firstPosition.x
            ) * 180 / Mathf.PI;
        }
    }
}
