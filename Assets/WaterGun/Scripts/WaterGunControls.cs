using UnityEngine;

namespace OceanCleanup.WaterGun
{
    public class WaterGunControls : MonoBehaviour
    {
        [SerializeField] private AreaEffector2D _waterStreamEffector;

        private Camera _camera;
        private void Awake()
        {
            _camera = Camera.main;
        }

        private void FixedUpdate()
        {
            if (Input.GetMouseButton(0)) // Left
            {
                Vector3 translatedMousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
                Vector3 mousePosition = new Vector3(
                    translatedMousePosition.x, 
                    translatedMousePosition.y, 
                    0.0f);
            
                float angleDifference = AngleBetweenPoints(transform.position, mousePosition);
            
                Quaternion newOrientation = Quaternion.Euler(0.0f, 0.0f,angleDifference);

                transform.rotation = newOrientation;
            }

            if (Input.GetKey(KeyCode.A))
            {
                // Suck
                SuckTrash();
            } else if (Input.GetKey(KeyCode.D))
            {
                // Blow
                BlowTrash();
            }
            else
            {
                _waterStreamEffector.enabled = false;
            }
        }

        private void SuckTrash()
        {
            _waterStreamEffector.forceMagnitude = -2;
            _waterStreamEffector.enabled = true;
        }

        private void BlowTrash()
        {        
            _waterStreamEffector.forceMagnitude = 2;
            _waterStreamEffector.enabled = true;
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
