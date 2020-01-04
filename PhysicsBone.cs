/*
 * PhysicsBone 1.0.0
 * Script written by Abcight
 * https://github.com/Abcight/PhysicsBone
 * 
 *  MIT License
    Copyright (c) 2020 Abcight

    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:

    The above copyright notice and this permission notice shall be included in all
    copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
    SOFTWARE.
*/

using UnityEngine;

namespace Abcight
{
    public class PhysicsBone : MonoBehaviour
    {
        [SerializeField] [Range(0.01f, 5)] private float collisionRadius = 0.1f;
        [SerializeField] [Range(1f, 1000)] private float collisionAccuracy = 1000f;
        [SerializeField] [Range(0.1f, 5)] private float stiffness = 1;
        [SerializeField] private float gravity = 9.81f;
        [SerializeField] private AnimationCurve forceFaloff;

        private Transform[] bones;
        private Vector3[] boneLocalPositions;
        private Quaternion[] boneLocalRotations;
        private float[] boneLocalDistances;

        private Vector3[] lastFrameBonePositions;
        private Quaternion gravityRootRotation;

        // Setup
        public void Start()
        {
            bones = GetComponentsInChildren<Transform>();

            boneLocalPositions = new Vector3[bones.Length];
            lastFrameBonePositions = new Vector3[bones.Length];
            boneLocalRotations = new Quaternion[bones.Length];
            boneLocalDistances = new float[bones.Length];

            for (int i = 0; i < bones.Length; i++)
            {
                lastFrameBonePositions[i] = bones[i].position;
                boneLocalPositions[i] = bones[i].localPosition;
                boneLocalRotations[i] = bones[i].localRotation;
                if (i > 0) boneLocalDistances[i] = Vector3.Distance(bones[i].position, bones[i].parent.position);
            }

            gravityRootRotation = bones[0].rotation;
        }


        public void LateUpdate()
        {
            for (int i = 0; i < bones.Length; i++)
            {
                bones[i].position = lastFrameBonePositions[i];
                bones[0].localPosition = boneLocalPositions[0];

                bones[0].rotation = Quaternion.Lerp(bones[0].rotation, gravityRootRotation, gravity * Time.deltaTime);

                if (i > 0)
                {
                    handleMovement(i);
                    handleStretching(i);
                    handleCollision(i);
                }
            }

            //set last frame positions
            for (int i = 0; i < bones.Length; i++)
            {
                lastFrameBonePositions[i] = bones[i].position;
            }
        }

        private void handleMovement(int i)
        {
            float percentage = ((float)i / bones.Length);
            float curveValue = (1 - forceFaloff.Evaluate(percentage));
            bones[i].localPosition = Vector3.Lerp(bones[i].localPosition, boneLocalPositions[i], curveValue * Time.deltaTime * 10 * stiffness);
            bones[i].localRotation = Quaternion.Lerp(bones[i].localRotation, boneLocalRotations[i], curveValue * Time.deltaTime * 10 * stiffness);
        }

        // TODO: Fix choppiness
        private void handleStretching(int i)
        {
            float distanceToParent = Vector3.Distance(bones[i].parent.position, bones[i].position);
            if (distanceToParent > boneLocalDistances[i])
            {
                float difference = distanceToParent - boneLocalDistances[i];
                Vector3 direction = (bones[i].parent.position - bones[i].position).normalized;
                bones[i].position = bones[i].position + (direction * difference);
            }
        }

        private void handleCollision(int i)
        {
            // Prevent from crashing
            collisionAccuracy = Mathf.Clamp(collisionAccuracy, 1, float.MaxValue);

            Collider[] collisions = Physics.OverlapSphere(bones[i].position, collisionRadius);
            foreach (Collider collider in collisions)
            {
                if (collider is MeshCollider) continue;
                Vector3 point = collider.ClosestPoint(bones[i].position);
                Vector3 outDirection = (bones[i].position - collider.bounds.center).normalized;
                while (Vector3.Distance(point, bones[i].position) < collisionRadius)
                {
                    bones[i].position = bones[i].position + (outDirection / collisionAccuracy);
                }
            }
        }

        #if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            collisionAccuracy = Mathf.Clamp(collisionAccuracy, 1, float.MaxValue);
            foreach (Transform bone in GetComponentsInChildren<Transform>())
            {
                Gizmos.color = Color.green;
                Gizmos.DrawWireSphere(bone.position, collisionRadius);
            }
        }
        #endif
    }

}