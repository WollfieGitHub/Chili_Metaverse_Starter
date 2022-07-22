using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class RefillArrows : MonoBehaviour
{
    [SerializeField] private Arrow arrowPrefab;
    
    private void Start()
    {
        Button button = GetComponentInChildren<Button>();
        button.onClick.AddListener(NewVolley);
    }

    private void NewVolley()
    {
        ScoreManager.Instance.NewVolley();
        new List<Arrow>(GameObject.FindObjectsOfType<Arrow>())
            .ConvertAll(arrow => arrow.gameObject)
            .ForEach(DestroyImmediate);
        
        new List<GameObject>(GameObject.FindGameObjectsWithTag("GroundedArrowSocket"))
            .ConvertAll(obj => obj.GetComponent<XRSocketInteractor>())
            .ConvertAll(socket => (socket, Instantiate(arrowPrefab.gameObject)))
            .ForEach(((XRSocketInteractor socket, GameObject arrow) t) => 
                t.socket.interactionManager.SelectEnter(
                    t.socket, 
                    t.arrow.GetComponent<XRGrabInteractable>() as IXRSelectInteractable
                    ));
    }
}
