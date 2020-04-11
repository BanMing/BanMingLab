using UnityEngine;
using UnityEngine.UI;

public enum BtnDir
{
    Left = -1,
    Right = 1,
    Up=10,
    Down=-10
}

public class ScrollViewMoveBtn : MonoBehaviour
{
    public BtnDir btnDir = BtnDir.Left;
    private Button button;
    public RectTransform content;
    public float speed = 50;
    private float verticalSpend;
    private float horizontalSpeed;

    private void Start()
    {
        if (btnDir == BtnDir.Right || btnDir == BtnDir.Left)
        {
            verticalSpend = 0;
            horizontalSpeed = speed;
        }
        else
        {
            verticalSpend = speed/10;
            horizontalSpeed = 0;
        }
        button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            if (content == null)
            {
                return;
            }

            content.position = new Vector3(content.transform.position.x + (int) btnDir*horizontalSpeed,
                content.transform.position.y + (int) btnDir*verticalSpend,
                content.transform.position.z);
        });
    }
}