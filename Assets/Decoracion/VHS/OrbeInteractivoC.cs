using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ObjetoInteractivoInfo : MonoBehaviour
{
    public GameObject panelInformacion;
    public TextMeshProUGUI textoInteraccion;

    private bool jugadorCerca = false;

    void Start()
    {
        panelInformacion.SetActive(false);
        textoInteraccion.gameObject.SetActive(false);
    }

    void Update()
    {
        if (jugadorCerca && Input.GetKeyDown(KeyCode.E))
        {
            panelInformacion.SetActive(true);
            textoInteraccion.gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = true;
            textoInteraccion.text = "Presiona E para ver información";
            textoInteraccion.gameObject.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = false;
            panelInformacion.SetActive(false);
            textoInteraccion.gameObject.SetActive(false);
        }
    }
}
