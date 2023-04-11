using UnityEngine.UI;
using UnityEngine;

public class EarthMover : MonoBehaviour
{

    [SerializeField]
    Slider earthSlider;

    float rotationFactor = 0;

    bool movingDirectionHoriz = false;


    [SerializeField] Button directionBtn;
    Transform earthTransform;
    Vector3 rotationValue;

    void Start()
    {
        movingDirectionHoriz = true;
        rotationFactor = 360;
        earthTransform = GetComponent<Transform>();
        earthSlider.onValueChanged.AddListener(MoveEarth);
    }

    private void onBtnClick()
    {
        movingDirectionHoriz = !movingDirectionHoriz;
    } 
    public void MoveEarth(float value)
    {
        rotationValue = earthTransform.eulerAngles;
        if(movingDirectionHoriz)
            rotationValue.y = value * rotationFactor;
        else
            rotationValue.x = value * rotationFactor;
        earthTransform.eulerAngles = rotationValue;
    }
}
