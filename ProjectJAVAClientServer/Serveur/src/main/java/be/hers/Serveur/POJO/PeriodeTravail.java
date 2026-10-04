package be.hers.Serveur.POJO;

import java.time.LocalDateTime;

public class PeriodeTravail {
    private int ID;
    private long nbrMinutes;
    private LocalDateTime debut;
    private Tache tache;
    private Collaborateur collaborateur;

    /**
     * Créer une période de travail avec tous ses attributs
     * @param ID L'id de la période de travail
     * @param nbrMinutes Le nombre de minutes de la période de travail
     * @param tache La tache liée à la période de travail
     * @param collaborateur Le collaborateur lié à la période de travail
     * @param debut le début de la période de travail
     */
    public PeriodeTravail(int ID, int nbrMinutes, Tache tache, Collaborateur collaborateur, LocalDateTime debut) {
        this.ID = ID;
        this.nbrMinutes = nbrMinutes;
        this.collaborateur = collaborateur;
        this.tache = tache;
        this.debut = debut;
    }
    /**
     * Créer une période de travail
     * @param nbrMinutes Le nombre de minutes de la période de travail
     * @param tache La tache liée à la période de travail
     * @param collaborateur Le collaborateur lié à la période de travail
     * @param debut le début de la période de travail
     */
    public PeriodeTravail(int nbrMinutes, Tache tache, Collaborateur collaborateur, LocalDateTime debut) {
        this.nbrMinutes = nbrMinutes;
        this.collaborateur = collaborateur;
        this.tache = tache;
        this.debut = debut;
    }

    /**
     *
     * @return Le nombre de minutes de la période de travail
     */
    public long getNbrMinutes() {
        return nbrMinutes;
    }

    /**
     * Initialise le nombre de minutes de la période de travail
     * @param nbrMinutes Le nombre de minutes de la période de travail
     */
    public void setNbrMinutes(long nbrMinutes) {
        this.nbrMinutes = nbrMinutes;
    }

    /**
     *
     * @return Le collaborateur lié à la période de travail
     */
    public Collaborateur getCollaborateur() {
        return collaborateur;
    }

    /**
     * Initialise le collaborateur lié à la période de travail
     * @param collaborateur Le collaborateur lié à la période de travail
     */
    public void setCollaborateur(Collaborateur collaborateur) {
        this.collaborateur = collaborateur;
    }

    /**
     *
     * @return La tache liée à la période de travail
     */
    public Tache getTache() {
        return tache;
    }

    /**
     * Initialise la tache liée à la période de travail
     * @param tache La tache liée à la période de travail
     */
    public void setTache(Tache tache) {
        this.tache = tache;
    }

    /**
     *
     * @return la date de début de la période de travail
     */
    public LocalDateTime getDebut() {
        return debut;
    }

    /**
     * Initialise la date de début de la période de travail
     * @param debut la date de début de la période de travail
     */
    public void setDebut(LocalDateTime debut) {
        this.debut = debut;
    }

    /**
     *
     * @return L'id de la période de travail
     */
    public int getID() {
        return ID;
    }

    /**
     * Initialise l'id de la période de travail
     * @param ID L'id de la période de travail
     */
    public void setID(int ID) {
        this.ID = ID;
    }

}