package be.hers.client;

import java.time.LocalDateTime;

public class LigneTache {

    private int IDTache;
    private String description;
    private String echeance;
    private String statusGlobal;
    private String statusTiming;
    private int IDCollaborateur;
    private String createur;
    private String travailleur;

    /**
     * Créer une ligne d'une tache avec son ID
     * @param IDTache
     * @param description
     * @param echeance
     * @param statusGlobal
     * @param statusTiming
     * @param createur
     * @param travailleur
     */
    public LigneTache(int IDTache, String description, String echeance, String statusGlobal, String statusTiming, String createur,String travailleur) {
        this.IDTache = IDTache;
        this.description = description;
        this.echeance = echeance;
        this.statusGlobal = statusGlobal;
        this.statusTiming = statusTiming;
        this.createur = createur;
        this.travailleur = travailleur;
    }

    /**
     *
     * @return la description de la tache
     */
    public String getDescription() {
        return description;
    }

    /**
     * Initialise la description de la tache
     * @param description la description de la tache
     */
    public void setDescription(String description) {
        this.description = description;
    }

    /**
     *
     * @return l'échéance de la tache
     */
    public String getEcheance() {
        return echeance;
    }

    /**
     * Initialise l' echeance de la tache
     * @param echeance l'échéance de la tache
     */
    public void setEcheance(String echeance) {
        this.echeance = echeance;
    }

    /**
     *
     * @return le status global de la tache
     */
    public String getStatusGlobal() {
        return statusGlobal;
    }

    /**
     * Initialise le status global de la tache
     * @param statusGlobal le status global de la tache
     */
    public void setStatusGlobal(String statusGlobal) {
        this.statusGlobal = statusGlobal;
    }

    /**
     *
     * @return le status timing de la tache
     */
    public String getStatusTiming() {
        return statusTiming;
    }

    /**
     * Initialise le status timing de la tache
     * @param statusTiming le status timing de la tache
     */
    public void setStatusTiming(String statusTiming) {
        this.statusTiming = statusTiming;
    }

    /**
     *
     * @return l'id du collaborateur
     */
    public int getIDCollaborateur() {
        return IDCollaborateur;
    }

    /**
     * Initialise l'id du collaborateur
     * @param IDCollaborateur l'id du collaborateur
     */
    public void setIDCollaborateur(int IDCollaborateur) {
        this.IDCollaborateur = IDCollaborateur;
    }

    /**
     *
     * @return L'id de la tache
     */
    public int getIDTache() {
        return IDTache;
    }

    /**
     * Initialise l'id de la tache
     * @param IDTache L'id de la tache
     */
    public void setIDTache(int IDTache) {
        this.IDTache = IDTache;
    }

    /**
     *
     * @return le nom et prénom du créateur
     */
    public String getCreateur() {
        return createur;
    }

    /**
     * Initialise le créateur
     * @param createur le nom et prénom du créateur
     */
    public void setCreateur(String createur) {
        this.createur = createur;
    }

    /**
     * 
     * @return le travailleur de la tache
     */
    public String getTravailleur() {
        return travailleur;
    }

    /**
     * Initialise le travailleur de la tache
     * @param travailleur le travailleur de la tache
     */
    public void setTravailleur(String travailleur) {
        this.travailleur = travailleur;
    }

}
