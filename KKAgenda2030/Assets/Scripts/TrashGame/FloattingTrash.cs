using UnityEngine;

public class FloattingTrash : MonoBehaviour {

    public float degreesPerSecond = 15.0f;
    public float amplitude = 0.5f;
    public float frequency = 1f;

    Vector3 posOffset = new Vector3();
    Vector3 TempPos = new Vector3();


    // Use this for initialization
    void Start()
    {
        posOffset = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, Time.deltaTime * degreesPerSecond, 0f), Space.World);

        TempPos = posOffset;
        TempPos.y = Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;

        transform.position = TempPos;
    }
}
