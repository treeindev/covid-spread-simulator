using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Subject : MonoBehaviour
{
    // Private properties
    private bool isInfected;
    private bool isWearingMask = true;
    private bool isVaccined;
    private bool isAsymptomatic = false;
    private bool isDead;
    private int rulesComplianceLevel;
    private int health = 100;
    private int hospitalThreshold = 33;
    private bool inQuarantine = false;
    private bool isHealing = false;
    private Vector3 destination;

    // Getters
    public bool isSubjectInfected { get { return isInfected;} }
    public bool isSubjectWearingMask { get { return isWearingMask;} }
    public bool isSubjectVaccined { get { return isVaccined;} }
    public bool isSubjectDead { get { return isDead;} }

    // Start is called before the first frame update
    void Start()
    {
        this.setNewDestination();
    }

    // Update is called once per frame
    void Update()
    {
        if (!this.hasArrivedToDestination()) {
            // Subject is still travelling to destination.
            return;
        }

        if (!this.inQuarantine && !this.isHealing) {
            // Arrived to destination and requires a new one.
            this.setNewDestination();
        } else if (this.inQuarantine) {
            // Subject is currently in quarantine.
            return;
        } else if (this.isHealing) {
            // Subject is healing at Hospital.
            return;
        }
    }

    // When subject collides with object
    void OnCollisionEnter(Collision col)
    {
        var collider = col.gameObject.GetComponent<Subject>();
        if (collider && !this.isInfected && collider.isSubjectInfected) {
            var infectionProbability = 0;

            if (this.areBothProtected(this, collider)) {
                // If both fully protected, low chance.
                infectionProbability = Constants.SUBJECT_CONTACT_BOTH_PROTECTED;
            } else if (this.isOneProtected(this, collider)) {
                // If either of them semi protected, middle chance.
                infectionProbability = Constants.SUBJECT_CONTACT_ONE_PROTECTED;
            } else {
                // If neither of them are protected, high chance infection.
                infectionProbability = Constants.SUBJECT_CONTACT_NONE_PROTECTED;
            }

            var infectionLevel = Random.Range(0,100);
            if (infectionProbability > infectionLevel) {
                // User has been infected.
                this.isInfected = true;
                var materials = GameObject.Find("materials").GetComponent<Materials>();
                this.GetComponent<Renderer>().material = materials.material_infected;
            }
        }
    }

    // Sets the subject infected state.
    public void setInfection(bool infected)
    {
        this.isInfected = infected;
    }

    // Sets the subject rules complience
    public void setRulesCompliance(int level)
    {
        this.rulesComplianceLevel = level;
    }

    // Sets the subject asymptomatic state
    public void setAsymptomatic(bool asymptomatic) {
        this.isAsymptomatic = asymptomatic;
    }

    // Sets the subject vaccined state
    public void setVaccined(bool vaccined) {
        this.isVaccined = vaccined;
    }

    // Get coordinates from park
    private Vector3 getParkCoordinates()
    {
        float x_position = Random.Range(Constants.PARK_TOP_LEFT[0], Constants.PARK_TOP_RIGHT[0]);
        float y_position = Constants.MAP_Z;
        float z_position = Random.Range(Constants.PARK_TOP_RIGHT[1], Constants.PARK_BOTTOM_RIGHT[1]);

        return new Vector3(x_position, y_position, z_position);
    }

    // Get coordinates from restaurant
    private Vector3 getRestaurantCoordinates()
    {
        float x_position = Random.Range(Constants.RESTAURANT_TOP_LEFT[0], Constants.RESTAURANT_TOP_RIGHT[0]);
        float y_position = Constants.MAP_Z;
        float z_position = Random.Range(Constants.RESTAURANT_TOP_RIGHT[1], Constants.RESTAURANT_BOTTOM_RIGHT[1]);

        return new Vector3(x_position, y_position, z_position);
    }
    
    // Get coordinates from home
    private Vector3 getHomeCoordinates()
    {
        float x_position = Random.Range(Constants.HOME_TOP_LEFT[0], Constants.HOME_TOP_RIGHT[0]);
        float y_position = Constants.MAP_Z;
        float z_position = Random.Range(Constants.HOME_TOP_RIGHT[1], Constants.HOME_BOTTOM_RIGHT[1]);

        return new Vector3(x_position, y_position, z_position);
    }

    // Get coordinates from Hospital
    private Vector3 getHospitalCoordinates()
    {
        float x_position = Random.Range(Constants.HOSPITAL_TOP_LEFT[0], Constants.HOSPITAL_TOP_RIGHT[0]);
        float y_position = Constants.MAP_Z;
        float z_position = Random.Range(Constants.HOSPITAL_TOP_RIGHT[1], Constants.HOSPITAL_BOTTOM_RIGHT[1]);

        return new Vector3(x_position, y_position, z_position);
    }

    // Set a new destination for the subject
    private void setNewDestination()
    {
        var complianceLevel = Random.Range(0,100);
        if (this.isAsymptomatic || !this.isInfected) {
            if (this.rulesComplianceLevel > complianceLevel) {
                // A subject is not infected or asymptomatic go to park if high compliance.
                this.destination = this.getParkCoordinates();
            } else {
                // A subject is not infected or asymptomatic go to restaurant if low compliance.
                this.destination = this.getRestaurantCoordinates();
            }
        } else {
            if (this.health < this.hospitalThreshold) {
                // A subject that is infected and has low health should go to hospital.
                this.destination = this.getHospitalCoordinates();
            } else {
                // A subject that is infected and has high health should go to home.
                this.destination = this.getHomeCoordinates();
            }   
        }

        // A subject can chose to either wear a mask or not before going to their destination
        // A new compliance level is calculated to avoid collisions with previous checking.
        var materials = GameObject.Find("materials").GetComponent<Materials>();
        complianceLevel = Random.Range(0,100);

        if (this.rulesComplianceLevel > complianceLevel) {
            // The subject is high compliance with rules and wears a mask.
            isWearingMask = true;

            // Check for full protection and update its material
            if (!this.isInfected && this.isVaccined) {
                this.GetComponent<Renderer>().material = materials.material_protected_full;
            } else if (!this.isInfected) {
                this.GetComponent<Renderer>().material = materials.material_protected_semi;
            }
        } else {
            // The subject decides not to wear a mask.
            isWearingMask = false;

            // Check for subject status to update its material.
            if (!this.isInfected && this.isVaccined) {
                this.GetComponent<Renderer>().material = materials.material_protected_semi;
            } else if (!this.isInfected) {
                this.GetComponent<Renderer>().material = materials.material_normal;
            }
        }

        var agent = this.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.SetDestination(this.destination);
    }

    // Check subject proximity to current destination.
    private bool hasArrivedToDestination()
    {
        return Vector3.Distance(transform.position, this.destination) < 1.0f;
    }

    // Check if two subjects are semi protected.
    private bool areBothProtected(Subject subject1, Subject subject2)
    {
        return subject1.isSubjectWearingMask && subject2.isSubjectWearingMask;
    }

    // Check if at least of subject is using protection.
    private bool isOneProtected(Subject subject1, Subject subject2)
    {
        return subject1.isSubjectWearingMask || subject2.isSubjectWearingMask;
    }
}
