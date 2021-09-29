using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    Camera _camera;
    Rect _rect;

    [SerializeField]
    Transform Player;
    [SerializeField]
    BoxCollider2D _boxCollider;
    [SerializeField]
    EdgeCollider2D _edgeCollider2;

    // Start is called before the first frame update
    void Start()
    {
        _camera = gameObject.GetComponent<Camera>();
        _rect = GetWorldBounds(_boxCollider);

        var points = _edgeCollider2.points;

        points[0] = new Vector2(_rect.xMin + (_rect.xMin / 2f), _rect.yMin + (_rect.yMin / 2f));
        points[1] = new Vector2(_rect.xMin + (_rect.xMin / 2f), _rect.yMax - (_rect.yMin / 2f));
        points[2] = new Vector2(_rect.xMax - (_rect.xMin / 2f), _rect.yMax - (_rect.yMin / 2f));
        points[3] = new Vector2(_rect.xMax - (_rect.xMin / 2f), _rect.yMin + (_rect.yMin / 2f));
        points[4] = new Vector2(_rect.xMin + (_rect.xMin / 2f), _rect.yMin + (_rect.yMin / 2f));

        _edgeCollider2.points = points;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1.0f, 0.5f, 0.0f);        
        DrawRect(_rect);
    }

    public Rect GetWorldBounds(BoxCollider2D boxCollider2D)
    {
        float worldRight = boxCollider2D.transform.TransformPoint(boxCollider2D.offset + new Vector2(boxCollider2D.size.x * 0.5f, 0)).x;
        float worldLeft = boxCollider2D.transform.TransformPoint(boxCollider2D.offset - new Vector2(boxCollider2D.size.x * 0.5f, 0)).x;

        float worldTop = boxCollider2D.transform.TransformPoint(boxCollider2D.offset + new Vector2(0, boxCollider2D.size.y * 0.5f)).y;
        float worldBottom = boxCollider2D.transform.TransformPoint(boxCollider2D.offset - new Vector2(0, boxCollider2D.size.y * 0.5f)).y;

        return new Rect(
            worldLeft,
            worldBottom,
            worldRight - worldLeft,
            worldTop - worldBottom
        );
    }

    void DrawRect(Rect rect)
    {
        Gizmos.DrawWireCube(new Vector3(rect.center.x, rect.center.y, 0.01f), new Vector3(rect.size.x, rect.size.y, 0.01f));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var rect = GetWorldBounds(_boxCollider);

        var x = Mathf.Clamp(Player.transform.position.x, rect.xMin, rect.xMax);
        var y = Mathf.Clamp(Player.transform.position.y, rect.yMin, rect.yMax);

        _camera.transform.position = new Vector3(x, y, _camera.transform.position.z);
    }
}
