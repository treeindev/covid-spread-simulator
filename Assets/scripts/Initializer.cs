using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Initializer : MonoBehaviour
{
    public Text GUIText;
    
    private List<GameObject> subjects = new List<GameObject>();
    private int subjectsNumber = 200;
    private int initialInfectionProbability = 10;
    private bool initializedInfection = false;
    private int complianceProbability = 70;

    // Start is called before the first frame update
    void Start()
    {
        for(var i=0; i<this.subjectsNumber; i++)
        {
            // Create cordinates
            float x_position = Random.Range(Constants.HOME_TOP_LEFT[0], Constants.HOME_TOP_RIGHT[0]);
            float y_position = Constants.MAP_Z;
            float z_position = Random.Range(Constants.HOME_TOP_LEFT[1], Constants.HOME_BOTTOM_RIGHT[1]);

            // Create object on game
            GameObject subject = GameObject.CreatePrimitive(PrimitiveType.Sphere);

            // Set object properties
            subject.name = "subject_" + i;
            subject.transform.position = new Vector3(x_position, y_position, z_position);
            subject.transform.localScale = new Vector3(Constants.SUBJECT_SCALE_X, Constants.SUBJECT_SCALE_Y, Constants.SUBJECT_SCALE_Z);
            subject.AddComponent<Rigidbody>();
            subject.AddComponent<Subject>();
            subject.AddComponent<UnityEngine.AI.NavMeshAgent>();

            // Increase subject's collider radius to prevent AI movement from NavMeshAgent
            var collider = subject.GetComponent<SphereCollider>();
            collider.radius += Constants.SUBJECT_COLLISION_RADIUS;

            // Set subject state
            subject = this.setSubjectInitialState(subject);

            // Add new object to collection
            this.subjects.Add(subject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        int nInfected = 0;
        int nWearingMask = 0;
        int nVaccined = 0;
        int nInQuarantine = 0;
        int nInHospital = 0;

        for(var i=0; i<this.subjectsNumber; i++)
        {
            var subject = this.subjects[i];
            var instance = subject.GetComponent<Subject>();
            if (instance.isSubjectInfected) {
                nInfected++;
            }

            if (instance.isSubjectWearingMask) {
                nWearingMask++;
            }

            if (instance.isSubjectVaccined) {
                nVaccined++;
            }

            if (instance.isSubjectInQuarantine) {
                nInQuarantine++;
            }

            if (instance.isSubjectInHospital) {
                nInHospital++;
            }
        }

        var text = "Number of subjects: "+this.subjectsNumber;
        text = text+"\nNumber wearing mask: "+nWearingMask;
        text = text+"\nNumber vaccined: "+nVaccined;
        text = text+"\nNumber of infected: "+nInfected;
        text = text+"\nNumber in quarantine: "+nInQuarantine;
        text = text+"\nNumber in hospital: "+nInHospital;

        GUIText.text = text;
    }

    // Set the initial state of a subject
    private GameObject setSubjectInitialState(GameObject subject)
    {
        var materials = GameObject.Find("materials").GetComponent<Materials>();

        // Instance of the Subject class
        var instance = subject.GetComponent<Subject>();

        // Set compliance of subject based on probability
        var complianceRange = Random.Range(0,100);
        var complianceLevel = 0;
        var isVaccined = false;
        
        if (this.complianceProbability > complianceRange) {
            // Subject compliance is high
            // This subjects are going to be vaccined.
            complianceLevel = Random.Range(this.complianceProbability, 100);
            isVaccined = true;

            // Vaccined subjects get an update on their material.
            subject.GetComponent<Renderer>().material = materials.material_protected_semi;
        } else {
            // Subject compliance is low
            complianceLevel = Random.Range(0,50);

            // Unvaccined subjects with low compliance get the "normal" material.
            subject.GetComponent<Renderer>().material = materials.material_normal;
        }

        instance.setRulesCompliance(complianceLevel);
        instance.setVaccined(isVaccined);

        // Set inital infection state based on probability
        var probabilityNumber = Random.Range(0,100);

        if (!this.initializedInfection || this.initialInfectionProbability > probabilityNumber) {
            instance.setInfection(true);
            subject.GetComponent<Renderer>().material = materials.material_infected;
            this.initializedInfection = true;

            // There is a percentage of positive subjects that are asymptomatic.
            var probabilityAsymptomatic = Random.Range(0, 100);
            if (Constants.SUBJECT_ASYMPTOMATIC_LEVEL > probabilityAsymptomatic) {
                instance.setAsymptomatic(true);
            }
        }

        return subject;
    }
}
