using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FrameAnimationController : MonoBehaviour
{
    [Header("Frame Pieces")]
    public List<Transform> framePieces; // Assign Frame (1), Frame (3), Frame, Frame (2)
    
    [Header("Settings")]
    public Camera targetCamera;
    public float animationDuration = 1f;
    public AnimationCurve movementCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    
    [Header("Frame Configuration")]
    public float frameThickness = 0.5f; // Thickness of the frame border
    public Vector2 frameOffset = Vector2.zero; // Optional offset from screen edges
    
    private List<Vector3> originalPositions = new List<Vector3>();
    private List<Vector3> targetPositions = new List<Vector3>();
    
    void Start()
    {
        if (targetCamera == null)
            targetCamera = Camera.main;
        
        // Store original positions
        foreach (Transform frame in framePieces)
        {
            originalPositions.Add(frame.position);
        }
    }
    
    public IEnumerator AnimateFramesToCamera()
    {
        CalculateFrameTargetPositions();
        
        float elapsed = 0f;
        
        while (elapsed < animationDuration)
        {
            elapsed += Time.deltaTime;
            float t = movementCurve.Evaluate(elapsed / animationDuration);
            
            for (int i = 0; i < framePieces.Count; i++)
            {
                framePieces[i].position = Vector3.Lerp(originalPositions[i], targetPositions[i], t);
            }
            
            yield return null;
        }
        
        // Ensure final positions are exact
        for (int i = 0; i < framePieces.Count; i++)
        {
            framePieces[i].position = targetPositions[i];
        }
    }
    
    void CalculateFrameTargetPositions()
    {
        targetPositions.Clear();
        
        // Get camera bounds in world space
        float cameraHeight = targetCamera.orthographicSize * 2f;
        float cameraWidth = cameraHeight * targetCamera.aspect;
        Vector3 cameraCenter = targetCamera.transform.position;
        
        // Calculate positions for each frame piece (top, bottom, left, right)
        // Assuming 4 frame pieces in order: top, bottom, left, right
        // Adjust indices based on your actual frame setup
        
        if (framePieces.Count >= 4)
        {
            // Top frame
            targetPositions.Add(new Vector3(
                cameraCenter.x + frameOffset.x,
                cameraCenter.y + (cameraHeight / 2f) - (frameThickness / 2f) + frameOffset.y,
                cameraCenter.z
            ));
            
            // Bottom frame
            targetPositions.Add(new Vector3(
                cameraCenter.x + frameOffset.x,
                cameraCenter.y - (cameraHeight / 2f) + (frameThickness / 2f) + frameOffset.y,
                cameraCenter.z
            ));
            
            // Left frame
            targetPositions.Add(new Vector3(
                cameraCenter.x - (cameraWidth / 2f) + (frameThickness / 2f) + frameOffset.x,
                cameraCenter.y + frameOffset.y,
                cameraCenter.z
            ));
            
            // Right frame
            targetPositions.Add(new Vector3(
                cameraCenter.x + (cameraWidth / 2f) - (frameThickness / 2f) + frameOffset.x,
                cameraCenter.y + frameOffset.y,
                cameraCenter.z
            ));
        }
        else
        {
            // Fallback: just move all to camera center
            foreach (Transform frame in framePieces)
            {
                targetPositions.Add(cameraCenter);
            }
        }
    }
    
    public IEnumerator ResetFrames()
    {
        float elapsed = 0f;
        List<Vector3> currentPositions = new List<Vector3>();
        
        foreach (Transform frame in framePieces)
        {
            currentPositions.Add(frame.position);
        }
        
        while (elapsed < animationDuration)
        {
            elapsed += Time.deltaTime;
            float t = movementCurve.Evaluate(elapsed / animationDuration);
            
            for (int i = 0; i < framePieces.Count; i++)
            {
                framePieces[i].position = Vector3.Lerp(currentPositions[i], originalPositions[i], t);
            }
            
            yield return null;
        }
        
        // Ensure final positions are exact
        for (int i = 0; i < framePieces.Count; i++)
        {
            framePieces[i].position = originalPositions[i];
        }
    }
}