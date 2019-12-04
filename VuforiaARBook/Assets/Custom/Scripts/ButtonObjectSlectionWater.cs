using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

// Created by kerihobo.com


public class ButtonObjectSlectionWater : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    public class MaterialSet
    {
        public MaterialSet(Material _material)
        {
            material = _material;
            color = _material.GetColor("_Color");
        }
        public Material material;
        public Color color;
    }

    private List<MaterialSet> previewMaterialSets = new List<MaterialSet>();
    public Interactable interactableToSpawn;
    private GameObject previewObject;
    private LayerMask mask;
    private Vector3 spawnPosition;
    private Quaternion spawnRotation;
    private RaycastHit hit;
    private Button button;
    private bool isDragging;
    private const string TAG = "Water";
    private List<Material> previewMaterials = new List<Material>();

    // Start is called before the first frame update
    void Awake()
    {
        button = GetComponent<Button>();
        mask = LayerMask.GetMask("Terrain");
        // PopulatePreviewMaterials();
    }

    private void Start()
    {
        InitializePreviewObject();
    }
    // Update is called once per frame
    void Update()
    {
        UpdatePreviewMaterial();
    }

    private void InitializePreviewObject()
    {
        previewObject = Instantiate(interactableToSpawn, transform.position, Quaternion.identity).gameObject;
        previewObject.transform.SetParent(CustomTrackableEventHandler.instance.transform);
        previewObject.gameObject.AddComponent<DetectOverlap>();
        previewObject.gameObject.SetActive(false);
        foreach (Rigidbody rb in previewObject.GetComponentsInChildren<Rigidbody>())
        {
            rb.isKinematic = true;
        }
        PopulatePreviewMaterials();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        FlagObjectForPlacement();
    }

    private void LateUpdate()
    {
        RaycastFindTerrain();
    }


    public void OnPointerUp(PointerEventData eventData)
    {
        if (hit.transform && DetectOverlap.overlaps.Count == 0)
        {
            SpawnCopy();
        }

        RestoreAvailability();
    }

    private void FlagObjectForPlacement()
    {
        isDragging = true;

        SetObjectLayer(previewObject.transform, "Overlay");

        button.interactable = false;
        previewObject.gameObject.SetActive(true);

        MoveAlongOverlay();
    }

    private void RestoreAvailability()
    {
        isDragging = false;

        RecolourMaterialsInHierachy(Color.white);
        hit = new RaycastHit();

        SetObjectLayer(previewObject.transform, "Overlay");

        button.interactable = true;
        previewObject.gameObject.SetActive(false);
    }

    private void SetObjectLayer(Transform _transform, string _layerName)
    {
        Transform[] transforms = _transform.GetComponentsInChildren<Transform>();
        foreach (Transform t in transforms)
        {
            t.gameObject.layer = LayerMask.NameToLayer(_layerName);
        }
    }

    private void RaycastFindTerrain()
    {
        if (!isDragging)
        {
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 50.0f, mask))
        {
            MoveAlongTerrain();
        }
        else
        {
            MoveAlongOverlay();
        }
    }

    private void MoveAlongTerrain()
    {
        SetObjectLayer(interactableToSpawn.transform, "Default");

        spawnPosition = hit.point;
        previewObject.transform.position = spawnPosition;

        spawnRotation = Quaternion.FromToRotation(previewObject.transform.up, hit.normal) * previewObject.transform.rotation;
        previewObject.transform.rotation = spawnRotation;
    }

    private void MoveAlongOverlay()
    {
        SetObjectLayer(previewObject.transform, "Overlay");

        Vector3 pos = Input.mousePosition;
        pos.z = transform.forward.z - Camera.main.transform.forward.z + 3;

        previewObject.transform.position = Camera.main.ScreenToWorldPoint(pos);
        previewObject.transform.rotation = Camera.main.transform.rotation;
    }

    private void SpawnCopy()
    {
        
        Transform t = Instantiate(interactableToSpawn, hit.point , spawnRotation, CustomTrackableEventHandler.instance.transform).transform;
        t.localScale = previewObject.transform.localScale;
        SetObjectLayer(t, "Default");

        t.gameObject.AddComponent<RigidBodyAssistant>();
    }

    private void PopulatePreviewMaterials()
    {
        Renderer[] renderers = previewObject.gameObject.GetComponentsInChildren<Renderer>();

        foreach (Renderer r in renderers)
        {
            foreach (Material m in r.materials)
            {
                previewMaterialSets.Add(new MaterialSet(m));
            }
        }
    }

    private void UpdatePreviewMaterial()
    {
        if (previewMaterialSets == null || previewMaterialSets.Count <= 0) { return; }

        if (hit.transform)
        {
            RecolourMaterialsInHierachy((DetectOverlap.overlaps.Count > 0) ? Color.red : Color.green);
        }
        else
        {
            RecolourMaterialsInHierachy(Color.white);
        }
    }

    private void RecolourMaterialsInHierachy(Color _colour)
    {
        Renderer[] renderers = previewObject.GetComponentsInChildren<Renderer>();
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material.SetColor("_Color", _colour == Color.white ? previewMaterialSets[i].color : _colour);
        }
    }

    private void CopyMaterialsToNewInstanceWithDefaultColour(Transform _transform)
    {
        Renderer[] renderers = _transform.gameObject.GetComponentsInChildren<Renderer>();

        foreach (Renderer r in renderers)
        {
            for (int i = 0; i < r.materials.Length; i++)
            {
                r.materials[i] = new Material(r.materials[i]);
                r.materials[i].color = Color.white;
            }
        }
    }
    public void ClearField() => SceneManager.LoadScene(0);
}
