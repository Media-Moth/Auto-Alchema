using UnityEngine;

public class Dropper : MonoBehaviour
{
    [SerializeField] GameObject ingredient;
    [SerializeField] float frequency = 1;
    [SerializeField] Vector3 offset;
    float counter = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        counter += Time.deltaTime;
        if (counter >= frequency) {
            counter = 0;
            Instantiate(ingredient, transform.position + offset, Quaternion.identity);
        }
    }
}
