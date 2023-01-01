using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal;
using UnityEngine;
using Sirenix.OdinInspector;

public enum IndicatorShape {
    Square,
    Cone,
    Circle
}
//MAYBE SHOULDN'T be monobehavior?
public class IndicatorController : SerializedMonoBehaviour
{
    private Vector3 mousePos;
    [SerializeField]
    private  GameObject m_DecalProjectorPrefab;

    private GameObject m_DecalProjectorObject;

    [DictionaryDrawerSettings(DisplayMode = DictionaryDisplayOptions.Foldout)]
    public Dictionary<string, Material> materials;

    private DecalProjector decalProjector;
    private RaycastHit hit;
    // Start is called before the first frame update
    void Start()    
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()   
    {
        mousePos = GenericHelper.GetMousePostion();
        var mousePosYAdjusted = new Vector3(mousePos.x, mousePos.y + 5.0f, mousePos.z); 
        if (m_DecalProjectorObject) {
            m_DecalProjectorObject.transform.position = mousePosYAdjusted;
        }
    }

    public void SpawnDecal(CommonParams commonParams, bool? stickToMouse) {
        if (m_DecalProjectorPrefab) {
            m_DecalProjectorObject = Instantiate(m_DecalProjectorPrefab);
            m_DecalProjectorObject.transform.position = commonParams.location;
            decalProjector = m_DecalProjectorObject.GetComponent<DecalProjector>();
            if (decalProjector) {
                decalProjector.material = materials["TealCircle"];
                decalProjector.size = commonParams.size;
            }
        } 

    }

    public void DespawnDecal() {
        Destroy(m_DecalProjectorObject);
    }

}


