package be.hers.Serveur.POJO;

import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;

public class Tache {
    private int ID;
    private String description;
    private LocalDateTime echeance;
    private StatusTache statusGlobal;
    private StatusTimingTache statusTiming;
    private Collaborateur createur;
    private Collaborateur travailleur;
    final static DateTimeFormatter CUSTOM_FORMATTER = DateTimeFormatter.ofPattern("yyyy-MM-dd HH:mm");

    /**
     * Créer une tache avec tous ses attributs
     * @param ID l'id de la tache
     * @param description la description de la tache
     * @param echeance l'échéance de la tache
     * @param statusGlobal le status global de la tache
     * @param statusTiming le status Timing de la tache
     * @param createur le créateur de la tache
     * @param travailleur le travailleur actuel de la tache
     */
    public Tache(int ID, String description, LocalDateTime echeance, StatusTache statusGlobal, StatusTimingTache statusTiming, Collaborateur createur, Collaborateur travailleur) {
        this.ID = ID;
        this.description = description;
        this.echeance = echeance;
        this.statusGlobal = statusGlobal;
        this.statusTiming = statusTiming;
        this.createur = createur;
        this.travailleur = travailleur;
    }
    /**
     * Créer une tache sans travailleur
     * @param ID l'id de la tache
     * @param description la description de la tache
     * @param echeance l'échéance de la tache
     * @param statusGlobal le status global de la tache
     * @param statusTiming le status Timing de la tache
     * @param createur le créateur de la tache
     */
    public Tache(int ID, String description, LocalDateTime echeance, StatusTache statusGlobal, StatusTimingTache statusTiming, Collaborateur createur) {
        this.ID = ID;
        this.description = description;
        this.echeance = echeance;
        this.statusGlobal = statusGlobal;
        this.statusTiming = statusTiming;
        this.createur = createur;
        this.travailleur = null;
    }
    /**
     * Créer une tache sans travailleur et sans ID
     * @param description la description de la tache
     * @param echeance l'échéance de la tache
     * @param statusGlobal le status global de la tache
     * @param statusTiming le status Timing de la tache
     * @param createur le créateur de la tache
     */
    public Tache(String description, LocalDateTime echeance, StatusTache statusGlobal, StatusTimingTache statusTiming, Collaborateur createur) {
        this.description = description;
        this.echeance = echeance;
        this.statusGlobal = statusGlobal;
        this.statusTiming = statusTiming;
        this.createur = createur;
        this.travailleur = null;
    }

    /**
     *
     * @return l'id de la tache
     */
    public int getID() {
        return ID;
    }

    /**
     * Initialise l'id de la tache
     * @param ID l'id de la tache
     */
    public void setID(int ID) {
        this.ID = ID;
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
    public LocalDateTime getEcheance() {
        return echeance;
    }

    /**
     * Initialise l'échéance de la tache
     * @param echeance l'échéance de la tache
     */
    public void setEcheance(LocalDateTime echeance) {
        this.echeance = echeance;
    }

    /**
     *
     * @return le status global de la tache
     */
    public StatusTache getStatusGlobal() {
        return statusGlobal;
    }

    /**
     * Initialise le status global de la tache
     * @param statusGlobal le status global de la tache
     */
    public void setStatusGlobal(StatusTache statusGlobal) {
        this.statusGlobal = statusGlobal;
    }

    /**
     *
     * @return le status Timing de la tache
     */
    public StatusTimingTache getStatusTiming() {
        return statusTiming;
    }

    /**
     * Initialise le status Timing de la tache
     * @param statusTiming le status Timing de la tache
     */
    public void setStatusTiming(StatusTimingTache statusTiming) {
        this.statusTiming = statusTiming;
    }

    /**
     *
     * @return le créateur de la tache
     */
    public Collaborateur getCreateur() {
        return createur;
    }

    /**
     * Initialise le créateur de la tache
     * @param createur le créateur de la tache
     */
    public void setCreateur(Collaborateur createur) {
        this.createur = createur;
    }

    public Collaborateur getTravailleur() {
        return travailleur;
    }
    /**
     * Initialise le travailleur de la tache
     * @param travailleur le travailleur de la tache
     */
    public void setTravailleur(Collaborateur travailleur) {
        this.travailleur = travailleur;
    }

    /**
     *
     * @return un String contenant tous les attributs d'une tache séparé par un ;. Le nom et prénom du créateur et du travailleur s'y trouve également
     */
    @Override
    public String toString() {
        String formattedString = echeance.format(CUSTOM_FORMATTER);
        return ID + ";" + description + ";" + formattedString + ";" + statusGlobal.getLabel() + ";" + statusTiming.getLabel() + ";" + createur.getPrenom() + " " + createur.getNom()+ ";" + ((travailleur == null)? " " : travailleur.getPrenom() + " " + travailleur.getNom());
    }
}
