using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraScript : MonoBehaviour
{
    private Camera _camera;
    private Rect _rect;

    [SerializeField]
    private Transform Player;

    [SerializeField]
    private BoxCollider2D _boxCollider;

    [SerializeField]
    private EdgeCollider2D _edgeCollider2;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    private void Start()
    {
        RefreshBounds();
        UpdateEdgeColliderBounds();
    }

    private void LateUpdate()
    {
        if (Player == null || _boxCollider == null)
        {
            return;
        }

        RefreshBounds();
        transform.position = GetClampedCameraPosition(Player.position);
    }

    private void OnDrawGizmos()
    {
        if (_boxCollider == null)
        {
            return;
        }

        Gizmos.color = new Color(1f, 0.5f, 0f);
        DrawRect(GetWorldBounds(_boxCollider));
    }

    public Rect GetWorldBounds(BoxCollider2D boxCollider2D)
    {
        if (boxCollider2D == null)
        {
            return new Rect();
        }

        Bounds bounds = boxCollider2D.bounds;
        return new Rect(bounds.min.x, bounds.min.y, bounds.size.x, bounds.size.y);
    }

    private void RefreshBounds()
    {
        _rect = GetWorldBounds(_boxCollider);
    }

    private Vector3 GetClampedCameraPosition(Vector3 targetPosition)
    {
        if (!_camera.orthographic)
        {
            return new Vector3(targetPosition.x, targetPosition.y, transform.position.z);
        }

        float verticalExtent = _camera.orthographicSize;
        float horizontalExtent = verticalExtent * _camera.aspect;

        float minX = _rect.xMin + horizontalExtent;
        float maxX = _rect.xMax - horizontalExtent;
        float minY = _rect.yMin + verticalExtent;
        float maxY = _rect.yMax - verticalExtent;

        float x = ClampOrCenter(targetPosition.x, minX, maxX);
        float y = ClampOrCenter(targetPosition.y, minY, maxY);

        return new Vector3(x, y, transform.position.z);
    }

    private float ClampOrCenter(float value, float min, float max)
    {
        if (min > max)
        {
            return (min + max) * 0.5f;
        }

        return Mathf.Clamp(value, min, max);
    }

    private void UpdateEdgeColliderBounds()
    {
        if (_edgeCollider2 == null || _boxCollider == null)
        {
            return;
        }

        Vector2 bottomLeft = ToEdgeColliderLocal(new Vector2(_rect.xMin, _rect.yMin));
        Vector2 topLeft = ToEdgeColliderLocal(new Vector2(_rect.xMin, _rect.yMax));
        Vector2 topRight = ToEdgeColliderLocal(new Vector2(_rect.xMax, _rect.yMax));
        Vector2 bottomRight = ToEdgeColliderLocal(new Vector2(_rect.xMax, _rect.yMin));

        _edgeCollider2.points = new[]
        {
            bottomLeft,
            topLeft,
            topRight,
            bottomRight,
            bottomLeft
        };
    }

    private Vector2 ToEdgeColliderLocal(Vector2 worldPosition)
    {
        Vector3 localPosition = _edgeCollider2.transform.InverseTransformPoint(worldPosition);
        return new Vector2(localPosition.x, localPosition.y);
    }

    private void DrawRect(Rect rect)
    {
        Gizmos.DrawWireCube(new Vector3(rect.center.x, rect.center.y, 0.01f), new Vector3(rect.size.x, rect.size.y, 0.01f));
    }
}
