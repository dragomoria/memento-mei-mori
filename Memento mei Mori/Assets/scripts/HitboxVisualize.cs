using UnityEngine;

[ExecuteAlways]
public class HitboxGizmo : MonoBehaviour
{
    private Collider2D col;

    public Color gizmoColor = Color.green;

    private void OnDrawGizmos()
    {
        if (col == null) col = GetComponent<Collider2D>();
        if (col == null) return;

        Gizmos.color = gizmoColor;

        if (col is BoxCollider2D box)
        {
            // Use local offset and size
            Vector2 offset = box.offset;
            Vector2 size = box.size;

            // Position in world space = object position + rotated offset
            Vector3 worldOffset = transform.rotation * (Vector3)offset;
            Vector3 center = transform.position + worldOffset;

            // Build a matrix that includes rotation and position
            Matrix4x4 rotationMatrix = Matrix4x4.TRS(center, transform.rotation, Vector3.one);
            Gizmos.matrix = rotationMatrix;

            Gizmos.DrawWireCube(Vector3.zero, size);
            Gizmos.matrix = Matrix4x4.identity;
        }
        else if (col is CircleCollider2D circle)
        {
            // Circle offset is simpler
            Vector3 center = transform.position + (Vector3)circle.offset;
            Gizmos.DrawWireSphere(center, circle.radius);
        }
    }
}
