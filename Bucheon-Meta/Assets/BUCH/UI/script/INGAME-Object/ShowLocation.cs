using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShowLocation : MonoBehaviour
{
    [SerializeField] Text locationText;
    [SerializeField] Text _locationinfo;

    private Coroutine co;

    public void Init(string locationName, string locationinfo)
    {
        if (co != null)
        {
            StopCoroutine(co);
            gameObject.SetActive(false);
        }
        locationText.text = locationName;
        _locationinfo.text = locationinfo;
        gameObject.SetActive(true);
        co = StartCoroutine(SetActiveFalseDelay(2.0f));
    }
    private IEnumerator SetActiveFalseDelay(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        gameObject.SetActive(false);
    }
	public void SetActiveFalse(){
		gameObject.SetActive(false);
	}
}
