using System;
using Global;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Stats")]
        [SerializeField] private float speed;
        [SerializeField] private float tiltSpeed;
        [SerializeField] private float tiltMaxAngle;
        
        [Header("Limitations")]
        [SerializeField] private float verticalLimit;
        [SerializeField] private float horizontalLimit;
        
        private void FixedUpdate()
        {
            ProcessMoveInput();
        }

        private void ProcessMoveInput()
        {
            var hasInput = false;

            if (Input.GetKey(KeyCode.A))
            {
                Move(Direction.Left);
                hasInput = true;
            }
            if (Input.GetKey(KeyCode.D))
            {
                Move(Direction.Right);
                hasInput = true;
            }
            if (Input.GetKey(KeyCode.W))
            {
                Move(Direction.Forward);
                hasInput = true;
            }
            if (Input.GetKey(KeyCode.S))
            {
                Move(Direction.Backward);
                hasInput = true;
            }

            if (!hasInput) Move(Direction.None);
        }

        private void Move(Direction direction)
        {
            var currPos = transform.position;
            
            switch (direction)
            {
                case Direction.Forward: currPos.z += speed; break;
                case Direction.Backward: currPos.z -= speed; break;
                case Direction.Right: currPos.x += speed; break;
                case Direction.Left: currPos.x -= speed; break;
                case Direction.None: break;
            }

            if (currPos.z < verticalLimit && currPos.z > -verticalLimit && currPos.x < horizontalLimit && currPos.x > -horizontalLimit )
            {
                transform.position = currPos;
                Tilt(direction);
            }
            else Tilt(Direction.None);
        }

        private void Tilt(Direction direction)
        {
            float rotateAngle;
            
            switch (direction)
            {
                case Direction.Right: rotateAngle = -tiltMaxAngle; break;
                case Direction.Left: rotateAngle = tiltMaxAngle; break;
                default: rotateAngle = 0f; break;
            }
            
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, rotateAngle), tiltSpeed);
        }
    }
}
